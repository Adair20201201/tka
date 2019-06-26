using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using TKA.Model;
using TKA.ViewModel;


namespace TKA.Business
{
    public class PLCControler
    {
        private static readonly PLCControler m_Instence = new PLCControler();
        public static PLCControler Instence
        {
            get { return m_Instence; }
        }
        private PLCControler()
        {
            channels = new List<Channel>();

            //add by wyf
            //DispatcherTimer Test;
            //Test = new DispatcherTimer();
            //Test.Interval = TimeSpan.FromSeconds(3);
            //Test.Tick += Test_Tick;
            //Test.Start();
        }

        public MainWindowViewModel MWVM = (MainWindowViewModel)Application.Current.MainWindow.DataContext;

        public Thread PLCThread;
        public SerialPort Port;
        private List<Channel> channels;
        public string StationNumber = "00";
        private bool Connected = false;

        private bool ComTest()
        {
            string SendStr = "@" + StationNumber + "TS" + "123123123";
            string inStr = SendStr + FCS(SendStr) + "*" + "\r";
            char[] SendBuffer;
            char[] ReadBuffer;
            SendBuffer = inStr.ToCharArray();
            Port.Write(SendBuffer, 0, SendBuffer.Length);
            Thread.Sleep(100);
            if (Port.BytesToRead >= 5)
            {
                ReadBuffer = new char[Port.BytesToRead];
                Port.Read(ReadBuffer, 0, Port.BytesToRead);
                string instring = new string(ReadBuffer);
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 初始化PLC
        /// </summary>
        /// <returns>端口是否打开</returns>
        public void InitPLC()
        {
            Port = new SerialPort();
            Port.PortName = "COM1";
            Port.DataBits = 7;
            Port.Parity = Parity.Even;
            Port.StopBits = StopBits.Two;
            Port.WriteTimeout = 100;
            Port.Open();

            PLCThread = new Thread(mPLCThread_MultiCheck);//修改
            PLCThread.IsBackground = true;
            PLCThread.Start();

        }

        public void InitState()
        {
            int channelID;
            int positon;
            bool value;

            // get property channel id , and position
            GetChannelIDandPosition(out channelID, out positon, MWVM.CM.Scene);
            // get setting value for this property
            value = GetPropertyState(channelID, positon) == "1";
            // set value
            MWVM.Scene = value;

            // get property channel id , and position
            //GetChannelIDandPosition(out channelID, out positon, MWVM.CM.Auto);
            //// get setting value for this property
            //value = GetPropertyState(channelID, positon) == "1";
            //// set value
            //MWVM.Auto = value;
        }

        /// <summary>
        /// PLC查询端口内容 (一次读取多个端口的设置信息)
        /// </summary>
        private void mPLCThread_MultiCheck()
        {
            while (true)
            {
                while (!Connected)//修改 断线重连
                {
                    Connected = ComTest();
                    Thread.Sleep(5000);
                }

                //设置模式
                string Runmodel = SetRunModel("02");
                bool isFirst = true;

                while (true)
                {
                    channels.Clear();
                    string SendStr = "";
                    string ReadStr = "";
                    string ReadData = "";


                    if (Port.IsOpen == false)
                    {
                        MessageBox.Show("请打开串口！");
                        return;
                    }

                    bool isBreak = false;//增加的代码

                    //读取全部设备状态并缓存
                    foreach (MultiChannelInfo item in MWVM.CM.MultiChannelCollections)
                    {
                        string channelStartIndex = item.ChannelStartPoint;
                        int channelCount = item.ChannelCount;

                        SendStr = "@" + StationNumber + "RR" + channelStartIndex + channelCount.ToString("d4");
                        lock (this)
                        {
                            SendStr = SendStr + FCS(SendStr) + "*" + "\r";
                            ReadStr = CPM2A_MultiCheck(SendStr, channelCount * 4 + 7);
                            if (ReadStr == null)//修改
                            {
                                isBreak = true;
                                break;
                            }
                        }
                        ReadData = ReadStr.Substring(7, channelCount * 4);

                        DecodeChannelSettings(channelStartIndex, ReadData);
                    }

                    if (isBreak)//修改
                    {
                        Connected = false;
                        break;
                    }

                    //如果为第一次启动需要读取一遍所有设备的当前状态
                    if (isFirst)
                    {
                        //InitState();
                        isFirst = false;
                    }

                    //将读取出来的数据对所有VM赋值
                    ManageAllChannels();

                    Thread.Sleep(200);
                }
            }
        }

        


        private void DecodeChannelSettings(string channelId, string channelsetting)
        {
            int channelID = Convert.ToInt32(channelId);
            for (int i = 0; i < channelsetting.Length; i = i + 4)
            {

                if (i + 4 <= channelsetting.Length)
                {
                    channels.Add(new Channel() { ChannelID = channelID, Setting = GetChannelSetting(channelsetting.Substring(i, 4)) });
                    channelID++;
                }
            }
        }

        /// <summary>
        /// convert the number of 16-scale to 2-scale
        /// </summary>
        /// <param name="value">such as "F"</param>
        /// <returns>"1111"</returns>
        private string Convert16To2Scale(string value)
        {
            // convert 16-scale to 10-scale
            int scale_16 = Convert.ToInt32(value, 16);
            // convert 10-scale to 2-scale
            string scale_2 = Convert.ToString(scale_16, 2);
            // high byte is complemented by "0"
            int zero = 4 - scale_2.Length;
            for (int i = 0; i < zero; i++)
            {
                scale_2 = "0" + scale_2;
            }
            return scale_2;
        }

        /// <summary>
        /// convert the ChannelSetting of 16-scale to 2-scale
        /// </summary>
        /// <param name="settings_16_scale">such as "FFFF"</param>
        /// <returns>"1111111111111111"</returns>
        private string GetChannelSetting(string settings_16_scale)
        {

            string settings_2_scale = String.Empty;
            foreach (var item in settings_16_scale)
            {
                settings_2_scale += Convert16To2Scale(item.ToString());
            }
            return settings_2_scale;
        }

        /// <summary>
        /// 读取PLC模式
        /// </summary>
        /// <returns>读取PLC模式</returns>
        private string GetRunModel()
        {
            string SendStr = "";
            string ReadStr = "";
            string RunModel = "";
            if (Port.IsOpen == false)
            {
                return "-1";
            }
            SendStr = "@" + StationNumber + "MS";
            SendStr = SendStr + FCS(SendStr) + "*" + "\r";
            ReadStr = CPM2A(SendStr);
            RunModel = ReadStr.Substring(7, 2);
            return RunModel;
        }

        /// <summary>
        /// 置PLC模式
        /// </summary>
        /// <param name="_RunModel">模式代码</param>
        /// <returns>当前模式代码</returns>
        private string SetRunModel(String _RunModel)
        {
            string SendStr = "";
            SendStr = "@" + StationNumber + "SC" + _RunModel; ;
            SendStr = SendStr + FCS(SendStr) + "*" + "\r";
            COM2CPM2A(SendStr);
            return GetRunModel();
        }

        /// <summary>
        /// 向PLC发送信息
        /// </summary>
        /// <param name="Value2">信息内容</param>
        /// <returns>是否成功发送</returns>
        private bool COM2CPM2A(string Value2)
        {

            string weibu = "";
            char[] SendBuffer = new char[50];
            char[] ReadBuffer = new char[50];

            for (int i = 0; i < 50; i++)
            {
                ReadBuffer[i] = ' ';
            }

            SendBuffer = Value2.ToCharArray();

            lock (this)
            {
                Port.Write(SendBuffer, 0, SendBuffer.Length);
                Thread.Sleep(10);
                do
                {
                    Thread.Sleep(5);
                } while (Port.BytesToRead < 11);                                //等待接收所有的数据

                Thread.Sleep(20);
                Port.Read(ReadBuffer, 0, Port.BytesToRead);
            }

            string instring = new string(ReadBuffer);
            weibu = instring.Substring(5, 2);
            switch (weibu)
            {
                case "00":
                    return true;
                case "01":
                    MessageBox.Show("在RUN模式下不可执行！");
                    break;
                case "02":
                    MessageBox.Show("在MONITOR模式下不可执行");
                    break;
                case "04":
                    MessageBox.Show("地址越界");
                    break;
                case "0B":
                    MessageBox.Show("在PROGRAM模式下不可执行");
                    break;
                case "13":
                    MessageBox.Show("FCS错误");
                    break;
                case "14":
                    MessageBox.Show("格式错误");
                    break;
                case "15":
                    MessageBox.Show("入口号数据错误");
                    break;
                case "16":
                    MessageBox.Show("命令不支持");
                    break;
                case "18":
                    MessageBox.Show("帧长度错误");
                    break;
                case "19":
                    MessageBox.Show("不可执行");
                    break;
                case "23":
                    MessageBox.Show("用户存储区写保护");
                    break;
                case "A3":
                    MessageBox.Show("由于数据传送中FCS错误而终止");
                    break;
                case "A4":
                    MessageBox.Show("由于数据传送中格式错误而终止");
                    break;
                case "A5":
                    MessageBox.Show("由于数据传送中入口号数据错误而终止");
                    break;
                case "A8":
                    MessageBox.Show("由于数据传送中帧长错误而终止");
                    break;
                default:
                    break;
            }
            return false;

        }
        /// <summary>
        /// 生成奇偶校验位
        /// </summary>
        /// <param name="Value">数据</param>
        /// <returns>校验位</returns>
        private string FCS(String Value)
        {
            int i, f;
            byte[] x;
            f = 0;
            for (i = 0; i < Value.Length; i++)
            {
                x = ASCIIEncoding.ASCII.GetBytes(Value.Substring(i, 1));
                f = f ^ (int)x[0];
            }
            return f.ToString("X");
        }

        /// <summary>
        /// 无消息提示发送函数
        /// </summary>
        /// <param name="inStr"></param>
        /// <returns></returns>
        private string CPM2A(string inStr)               //无消息提示发送函数
        {
            char[] SendBuffer;
            char[] ReadBuffer;
            int length = 0;
            SendBuffer = inStr.ToCharArray();
            Port.Write(SendBuffer, 0, SendBuffer.Length);
            do
            {
                Thread.Sleep(5);
            } while (Port.BytesToRead < 11);                                //等待接收所有的数据
            Thread.Sleep(20);
            length = Port.BytesToRead;
            ReadBuffer = new char[length];
            Port.Read(ReadBuffer, 0, length);
            string instring = new string(ReadBuffer);
            return instring;
        }

        private string CPM2A_MultiCheck(string inStr, int bytesToReadCount)               //无消息提示发送函数
        {
            char[] SendBuffer;
            char[] ReadBuffer;
            int length = 0;
            SendBuffer = inStr.ToCharArray();
            try
            {
                Port.Write(SendBuffer, 0, SendBuffer.Length);
            }
            catch
            {
                return null;
            }
            int tick = 0;//修改
            do
            {
                tick++;
                if (tick > 200)
                {
                    return null;
                }
                Thread.Sleep(5);
            } while (Port.BytesToRead < bytesToReadCount);                                //等待接收所有的数据
            Thread.Sleep(20);
            length = Port.BytesToRead;
            ReadBuffer = new char[length];
            Port.Read(ReadBuffer, 0, length);
            string instring = new string(ReadBuffer);
            return instring;
        }


        /// <summary>
        /// get property value by channel id and position
        /// </summary>
        /// <param name="channelID"></param>
        /// <param name="position"></param>
        /// <returns>the setting value for specific property</returns>
        private string GetPropertyState(int channelID, int position)
        {
            string result = String.Empty;
            var currentChannel = channels.Where(channel => channel.ChannelID == channelID).FirstOrDefault();
            if (currentChannel != null)
            {
                // note : the convert, "15 - position", is used map position to correct setting-position
                result = currentChannel.Setting.ToCharArray()[15 - position].ToString();
            }

            return result;
        }

        /// <summary>
        /// get channel id and position by property value
        /// </summary>
        /// <param name="id"> return channel id </param>
        /// <param name="position"> return position </param>
        /// <param name="args"> property value </param>
        private void GetChannelIDandPosition(out int id, out int position, string args)
        {
            id = Int32.Parse(args.Substring(0, args.Length - 2));
            position = Int32.Parse(args.Substring(args.Length - 2, 2));
        }

        /// <summary>
        /// modify the property state accroding to the setting
        /// </summary>
        /// <param name="currentChannel">current channel model</param>
        /// <param name="currentVM">viewmodel where the property localed at</param>
        private void ManageChannelProState(TrackModel currentChannel, TrackViewModel currentVM)
        {
            int channelID;
            int positon;
            bool value;

            GetChannelIDandPosition(out channelID, out positon, currentChannel.LeftCubeAddress);
            value = GetPropertyState(channelID, positon) == "1";
            currentVM.LeftCubeAddressValue = value;

            GetChannelIDandPosition(out channelID, out positon, currentChannel.RightCubeAddress);
            value = GetPropertyState(channelID, positon) == "1";
            currentVM.RightCubeAddressValue = value;

            // get property channel id , and position
            GetChannelIDandPosition(out channelID, out positon, currentChannel.LeftUpArrowAddress);
            // get setting value for this property
            value = GetPropertyState(channelID, positon) == "1";
            // set value
            currentVM.LeftUpArrowAddressValue = value;

            // get property channel id , and position
            GetChannelIDandPosition(out channelID, out positon, currentChannel.LeftDownArrowAddress);
            // get setting value for this property
            value = GetPropertyState(channelID, positon) == "1";
            // set value
            currentVM.LeftDownArrowAddressValue = value;

            // get property channel id , and position
            GetChannelIDandPosition(out channelID, out positon, currentChannel.RightUpArrowAddress);
            // get setting value for this property
            value = GetPropertyState(channelID, positon) == "1";
            // set value
            currentVM.RightUpArrowAddressValue = value;

            // get property channel id , and position
            GetChannelIDandPosition(out channelID, out positon, currentChannel.RightDownArrowAddress);
            // get setting value for this property
            value = GetPropertyState(channelID, positon) == "1";
            // set value
            currentVM.RightDownArrowAddressValue = value;

            // get property channel id , and position
            GetChannelIDandPosition(out channelID, out positon, currentChannel.LeftApplyForUpDetachAddress);
            // get setting value for this property
            value = GetPropertyState(channelID, positon) == "1";
            // set value
            currentVM.LeftApplyForUpDetachAddressValue = value;

            // get property channel id , and position
            GetChannelIDandPosition(out channelID, out positon, currentChannel.LeftApplyForDownDetachAddress);
            // get setting value for this property
            value = GetPropertyState(channelID, positon) == "1";
            // set value
            currentVM.LeftApplyForDownDetachAddressValue = value;

            // get property channel id , and position
            GetChannelIDandPosition(out channelID, out positon, currentChannel.RightApplyForUpDetachAddress);
            // get setting value for this property
            value = GetPropertyState(channelID, positon) == "1";
            // set value 
            currentVM.RightApplyForUpDetachAddressValue = value;

            // get property channel id , and position
            GetChannelIDandPosition(out channelID, out positon, currentChannel.RightApplyForDownDetachAddress);
            // get setting value for this property
            value = GetPropertyState(channelID, positon) == "1";
            // set value
            currentVM.RightApplyForDownDetachAddressValue = value;

            // get property channel id , and position
            GetChannelIDandPosition(out channelID, out positon, currentChannel.AllowAddress);
            // get setting value for this property
            value = GetPropertyState(channelID, positon) == "1";
            // set value
            currentVM.Allow = value;

            // get property channel id , and position
            GetChannelIDandPosition(out channelID, out positon, currentChannel.PowerAddress);
            // get setting value for this property
            value = GetPropertyState(channelID, positon) == "1";
            // set value
            currentVM.Prower = value;

            // get property channel id , and position
            GetChannelIDandPosition(out channelID, out positon, currentChannel.RemoveAddress);
            // get setting value for this property
            value = GetPropertyState(channelID, positon) == "1";
            // set value
            currentVM.Remove = value;

            // get property channel id , and position
            //GetChannelIDandPosition(out channelID, out positon, currentChannel.AlertTechnicalMaintenanceTimeout);
            //// get setting value for this property
            //value = GetPropertyState(channelID, positon) == "1";
            //// set value
            //currentVM.TimeOutAddressValue = value;
        }

        /// <summary>
        /// modify the property state accroding to the setting
        /// </summary>
        /// <param name="currentChannel">current channel model</param>
        /// <param name="currentVM">viewmodel where the property localed at</param>
        private void ManageChannelNotificationState(TrackModel currentChannel, TrackWarningViewModel currentVM)
        {
            int channelID;
            int positon;
            bool value;
            #region 修改 刘永强

            // get property channel id , and position
            GetChannelIDandPosition(out channelID, out positon, currentChannel.NoteLeftCubeAddress);
            // get setting value for this property
            value = GetPropertyState(channelID, positon) == "1";
            // set value
            currentVM.NoteLeftCubeAddress = value;

            // get property channel id , and position
            GetChannelIDandPosition(out channelID, out positon, currentChannel.NoteRightCubeAddress);
            // get setting value for this property
            value = GetPropertyState(channelID, positon) == "1";
            // set value
            currentVM.NoteRightCubeAddress = value;

            GetChannelIDandPosition(out channelID, out positon, currentChannel.NoteFlagFault);
            value = GetPropertyState(channelID, positon) == "1";
            currentVM.NoteFlagFault = value;

            GetChannelIDandPosition(out channelID, out positon, currentChannel.NoteNotInPlace);
            value = GetPropertyState(channelID, positon) == "1";
            currentVM.NoteNotInPlace = value;

            GetChannelIDandPosition(out channelID, out positon, currentChannel.NoteTouch);
            value = GetPropertyState(channelID, positon) == "1";
            currentVM.NoteTouch = value;

            GetChannelIDandPosition(out channelID, out positon, currentChannel.NotePowerSupplyReset);
            value = GetPropertyState(channelID, positon) == "1";
            currentVM.NotePowerSupplyReset = value;

            GetChannelIDandPosition(out channelID, out positon, currentChannel.NoteAvulsionReset);
            value = GetPropertyState(channelID, positon) == "1";
            currentVM.NoteAvulsionReset = value;

            GetChannelIDandPosition(out channelID, out positon, currentChannel.NoteLeftApplyForDownDetach);
            value = GetPropertyState(channelID, positon) == "1";
            currentVM.NoteLeftApplyForDownDetach = value;

            // get property channel id , and position
            GetChannelIDandPosition(out channelID, out positon, currentChannel.NoteLeftApplyForUpDetach);
            // get setting value for this property
            value = GetPropertyState(channelID, positon) == "1";
            // set value
            currentVM.NoteLeftApplyForUpDetach = value;

            // get property channel id , and position
            GetChannelIDandPosition(out channelID, out positon, currentChannel.NoteLeftDownDetaching);
            // get setting value for this property
            value = GetPropertyState(channelID, positon) == "1";
            // set value
            currentVM.NoteLeftDownDetaching = value;

            // get property channel id , and position
            GetChannelIDandPosition(out channelID, out positon, currentChannel.NoteLeftUpDetaching);
            // get setting value for this property
            value = GetPropertyState(channelID, positon) == "1";
            // set value
            currentVM.NoteLeftUpDetaching = value;

            // get property channel id , and position
            GetChannelIDandPosition(out channelID, out positon, currentChannel.NoteRightApplyForDownDetach);
            // get setting value for this property
            value = GetPropertyState(channelID, positon) == "1";
            // set value
            currentVM.NoteRightApplyForDownDetach = value;

            // get property channel id , and position
            GetChannelIDandPosition(out channelID, out positon, currentChannel.NoteRightApplyForUpDetach);
            // get setting value for this property
            value = GetPropertyState(channelID, positon) == "1";
            // set value
            currentVM.NoteRightApplyForUpDetach = value;

            // get property channel id , and position
            GetChannelIDandPosition(out channelID, out positon, currentChannel.NoteRightDownDetaching);
            // get setting value for this property
            value = GetPropertyState(channelID, positon) == "1";
            // set value
            currentVM.NoteRightDownDetaching = value;

            // get property channel id , and position
            GetChannelIDandPosition(out channelID, out positon, currentChannel.NoteRightUpDetaching);
            // get setting value for this property
            value = GetPropertyState(channelID, positon) == "1";
            // set value
            currentVM.NoteRightUpDetaching = value;
            #endregion
        }

        private void ManageChannelOtherState()
        {
            int channelID;
            int positon;
            bool value;

            // get property channel id , and position
            GetChannelIDandPosition(out channelID, out positon, MWVM.CM.PrimaryWatchSetting);
            // get setting value for this property
            value = GetPropertyState(channelID, positon) == "1";
            // set value
            MWVM.FingerVM.PremFingerprintCheckSuccess = value;

            // get property channel id , and position
            GetChannelIDandPosition(out channelID, out positon, MWVM.CM.MinorWatchSetting);
            // get setting value for this property
            value = GetPropertyState(channelID, positon) == "1";
            // set value
            MWVM.FingerVM.ViceFingerprintCheckSuccess = value;

            // get property channel id , and position
            GetChannelIDandPosition(out channelID, out positon, MWVM.CM.Scene);
            // get setting value for this property
            value = GetPropertyState(channelID, positon) == "1";
            // set value
            MWVM.Scene = value;

            // get property channel id , and position
            //GetChannelIDandPosition(out channelID, out positon, MWVM.CM.Auto);
            //// get setting value for this property
            //value = GetPropertyState(channelID, positon) == "1";
            //// set value
            //MWVM.Auto = value;
        }

        /// <summary>
        /// manage all channel property states according to the setting
        /// </summary>
        public void ManageAllChannels()
        {
            List<TrackModel> LTM = MWVM.CM.LTM;

            for (int i = 0; i < LTM.Count; i++)
            {
                if (i >= MWVM.TracksVM.Count)
                {
                    break;
                }

                #region 管理通道状态
                ManageChannelProState((TrackModel)LTM[i], MWVM.TracksVM[i]);
                #endregion
                #region 通道状态改变后处理
                ManageChannelNotificationState((TrackModel)LTM[i], MWVM.WarningVM.TWVM[i]);
                #endregion
            }
            #region 其他管理通道状态
            ManageChannelOtherState();
            #endregion
            //InitState();
        }

        /// <summary>
        /// 置位
        /// </summary>
        /// <param name="Channel"></param>
        /// <param name="Index"></param>
        public bool SetChannel(string Address)
        {
            if (Connected)
            {
                string SendStr = "";

                SendStr = "@" + StationNumber + "KSCIO " + Address;
                SendStr = SendStr + FCS(SendStr) + "*" + "\r";
                return COM2CPM2A(SendStr);
            }
            return false;

        }

        /// <summary>
        /// 复位
        /// </summary>
        /// <param name="SetStation"></param>
        /// <param name="Channel"></param>
        public bool ReSetChannel(string Address)
        {
            if (Connected)
            {
                string SendStr = "";

                SendStr = "@" + StationNumber + "KRCIO " + Address;
                SendStr = SendStr + FCS(SendStr) + "*" + "\r";
                return COM2CPM2A(SendStr);
            }
            return false;
        }

    }
}
