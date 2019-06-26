using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Windows.Input;
using System.Windows;
using TKA.Business;
using TKA.View;
using TKA.Model;
using System.Windows.Media;

namespace TKA.ViewModel
{
    public class TrackViewModel : ViewModelBase
    {
        VideoGroupViewModel VideoGroupVM;
        Access access;
        DispatcherTimer timer;
        private TimeSpan UpBreakConstantTimeSpan;
        public TrackViewModel(VideoGroupViewModel videoGroupVM)
        {
            VideoGroupVM = videoGroupVM;
            access = new Access();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(timer_Tick);

            RightTrackState = new SolidColorBrush(Colors.White);
            LeftTrackState = new SolidColorBrush(Colors.White);
        }
        void timer_Tick(object sender, EventArgs e)
        {
            UpBreakConstantTimeSpan = UpBreakConstantTimeSpan + TimeSpan.FromSeconds(1);
            UpBreakConstant = UpBreakConstantTimeSpan.ToString();
        }
        #region 属性

        private int m_TrackNum;
        /// <summary>
        /// 铁轨号
        /// </summary>
        public int TrackNum
        {
            get { return m_TrackNum; }
            set
            {
                if (m_TrackNum != value)
                {
                    m_TrackNum = value;
                    RaisePropertyChanged(() => TrackNum);
                }
            }
        }

        private bool m_LeftCubeAddressValue = false;
        /// <summary>
        /// 左侧方块灯显隐*
        /// </summary>
        public bool LeftCubeAddressValue
        {
            get { return m_LeftCubeAddressValue; }
            set
            {
                if (m_LeftCubeAddressValue != value)
                {
                    m_LeftCubeAddressValue = value;
                    RaisePropertyChanged(() => LeftCubeAddressValue);
                }
            }
        }

        private bool m_RightCubeAddressValue = false;
        /// <summary>
        /// 右侧方块灯显隐*
        /// </summary>
        public bool RightCubeAddressValue
        {
            get { return m_RightCubeAddressValue; }
            set
            {
                if (m_RightCubeAddressValue != value)
                {
                    m_RightCubeAddressValue = value;
                    RaisePropertyChanged(() => RightCubeAddressValue);
                }
            }
        }

        private bool m_LeftUpArrowAddressValue = false;
        /// <summary>
        /// 左侧上箭头显隐*
        /// </summary>
        public bool LeftUpArrowAddressValue
        {
            get { return m_LeftUpArrowAddressValue; }
            set
            {
                if (m_LeftUpArrowAddressValue != value)
                {
                    m_LeftUpArrowAddressValue = value;
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        if (m_LeftUpArrowAddressValue == true && m_RightUpArrowAddressValue == false)
                        {
                            UpBreakTime = DateTime.Now.ToString("HH:mm:ss");
                            UpBreakConstantTimeSpan = TimeSpan.FromSeconds(0);
                            timer.Start();
                        }

                        if (m_LeftUpArrowAddressValue == false && m_RightUpArrowAddressValue == false)
                        {
                            timer.Stop();
                            access.InsertOperation(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), TrackNum.ToString("d2") + "道作业完成,持续时间:" + UpBreakConstant);
                            UpBreakTime = "";
                            UpBreakConstant = "";
                        }
                        if (value)
                        {
                            LeftTrackState = GolbalColors.Red;
                            LeftTrackNumLightState = GolbalColors.Red;
                        }
                        else
                        {
                            LeftTrackState = GolbalColors.White;
                            LeftTrackNumLightState = GolbalColors.White;// new SolidColorBrush();
                        }
                    }));
                    RaisePropertyChanged(() => LeftUpArrowAddressValue);
                }
            }
        }

        private bool m_LeftDownArrowAddressValue = false;
        /// <summary>
        /// 左侧下箭头显隐*
        /// </summary>
        public bool LeftDownArrowAddressValue
        {
            get { return m_LeftDownArrowAddressValue; }
            set
            {
                if (m_LeftDownArrowAddressValue != value)
                {
                    m_LeftDownArrowAddressValue = value;
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        if (value)
                        {
                            LeftTrackNumLightState = GolbalColors.White;
                        }
                        else
                        {
                            LeftTrackNumLightState = new SolidColorBrush();
                        }
                    }));
                    RaisePropertyChanged(() => LeftDownArrowAddressValue);
                }
            }
        }

        private bool m_RightUpArrowAddressValue = false;
        /// <summary>
        /// 右侧上箭头灯显隐*
        /// </summary>
        public bool RightUpArrowAddressValue
        {
            get { return m_RightUpArrowAddressValue; }
            set
            {
                if (m_RightUpArrowAddressValue != value)
                {
                    m_RightUpArrowAddressValue = value;
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        if (m_LeftUpArrowAddressValue == false && m_RightUpArrowAddressValue == true)
                        {
                            UpBreakTime = DateTime.Now.ToString("HH:mm:ss");
                            UpBreakConstantTimeSpan = TimeSpan.FromSeconds(0);
                            timer.Start();
                        }
                        if (m_LeftUpArrowAddressValue == false && m_RightUpArrowAddressValue == false)
                        {
                            timer.Stop();
                            access.InsertOperation(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), TrackNum.ToString("d2") + "道作业完成,持续时间:" + UpBreakConstant);
                            UpBreakTime = "";
                            UpBreakConstant = "";
                        }
                        if (value)
                        {
                            RightTrackState = GolbalColors.Red;
                            RightTrackNumLightState = GolbalColors.Red;
                        }
                        else
                        {
                            RightTrackState = GolbalColors.White;
                            RightTrackNumLightState = GolbalColors.White;// new SolidColorBrush();
                        }
                    }));
                    RaisePropertyChanged(() => RightUpArrowAddressValue);
                }
            }
        }

        private bool m_RightDownArrowAddressValue = false;
        /// <summary>
        /// 右侧下箭头灯显隐*
        /// </summary>
        public bool RightDownArrowAddressValue
        {
            get { return m_RightDownArrowAddressValue; }
            set
            {
                if (m_RightDownArrowAddressValue != value)
                {
                    m_RightDownArrowAddressValue = value;
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        if (value)
                        {
                            RightTrackNumLightState = GolbalColors.White;
                        }
                        else
                        {
                            RightTrackNumLightState = new SolidColorBrush();
                        }
                    }));
                    RaisePropertyChanged(() => RightDownArrowAddressValue);
                }
            }
        }

        private bool m_LeftApplyForUpDetachAddressValue = false;
        /// <summary>
        /// 左侧申请上脱
        /// </summary>
        public bool LeftApplyForUpDetachAddressValue
        {
            get { return m_LeftApplyForUpDetachAddressValue; }
            set
            {
                if (m_LeftApplyForUpDetachAddressValue != value)
                {
                    m_LeftApplyForUpDetachAddressValue = value;
                    RaisePropertyChanged(() => LeftApplyForUpDetachAddressValue);
                }
            }
        }


        private bool m_LeftApplyForDownDetachAddressValue = false;
        /// <summary>
        /// 左侧申请下脱
        /// </summary>
        public bool LeftApplyForDownDetachAddressValue
        {
            get { return m_LeftApplyForDownDetachAddressValue; }
            set
            {
                if (m_LeftApplyForDownDetachAddressValue != value)
                {
                    m_LeftApplyForDownDetachAddressValue = value;
                    if (value)
                    {
                        LeftTrackState = GolbalColors.Green;
                    }
                    else
                    {
                        LeftTrackState = GolbalColors.White;
                    }
                    RaisePropertyChanged(() => LeftApplyForDownDetachAddressValue);
                }
            }
        }


        private bool m_RightApplyForUpDetachAddressValue = false;
        /// <summary>
        /// 右侧申请上脱
        /// </summary>
        public bool RightApplyForUpDetachAddressValue
        {
            get { return m_RightApplyForUpDetachAddressValue; }
            set
            {
                if (m_RightApplyForUpDetachAddressValue != value)
                {
                    m_RightApplyForUpDetachAddressValue = value;
                    RaisePropertyChanged(() => RightApplyForUpDetachAddressValue);
                }
            }
        }


        private bool m_RightApplyForDownDetachAddressValue = false;
        /// <summary>
        /// 右侧申请下脱
        /// </summary>
        public bool RightApplyForDownDetachAddressValue
        {
            get { return m_RightApplyForDownDetachAddressValue; }
            set
            {
                if (m_RightApplyForDownDetachAddressValue != value)
                {
                    m_RightApplyForDownDetachAddressValue = value;
                    if (value)
                    {
                        RightTrackState = GolbalColors.Green;
                    }
                    else
                    {
                        RightTrackState = GolbalColors.White;
                    }
                    RaisePropertyChanged(() => RightApplyForDownDetachAddressValue);
                }
            }
        }


        private SolidColorBrush m_LeftTrackState;
        /// <summary>
        ///左侧铁轨颜色 
        /// </summary>
        public SolidColorBrush LeftTrackState
        {
            get { return m_LeftTrackState; }
            set
            {
                if (m_LeftTrackState != value)
                {
                    m_LeftTrackState = value;
                    RaisePropertyChanged(() => LeftTrackState);
                }
            }
        }

        private SolidColorBrush m_RightTrackState;
        /// <summary>
        ///右侧铁轨颜色 
        /// </summary>
        public SolidColorBrush RightTrackState
        {
            get { return m_RightTrackState; }
            set
            {
                if (m_RightTrackState != value)
                {
                    m_RightTrackState = value;
                    RaisePropertyChanged(() => RightTrackState);
                }
            }
        }


        private SolidColorBrush m_LeftTrackNumLightState=GolbalColors.White;
        /// <summary>
        /// 左边轨道灯的颜色
        /// </summary>
        public SolidColorBrush LeftTrackNumLightState
        {
            get { return m_LeftTrackNumLightState; }
            set
            {
                if (m_LeftTrackNumLightState != value)
                {
                    m_LeftTrackNumLightState = value;
                    RaisePropertyChanged(() => LeftTrackNumLightState);
                }
            }
        }

        private SolidColorBrush m_RightTrackNumLightState;
        /// <summary>
        /// 右边轨道灯的颜色
        /// </summary>
        public SolidColorBrush RightTrackNumLightState
        {
            get { return m_RightTrackNumLightState; }
            set
            {
                if (m_RightTrackNumLightState != value)
                {
                    m_RightTrackNumLightState = value;
                    RaisePropertyChanged(() => RightTrackNumLightState);
                }
            }
        }


        private string m_UpBreakTime = "";
        /// <summary>
        /// 上脱时间
        /// </summary>
        public string UpBreakTime
        {
            get { return m_UpBreakTime; }
            set
            {
                if (m_UpBreakTime != value)
                {
                    m_UpBreakTime = value;
                    RaisePropertyChanged(() => UpBreakTime);
                }
            }
        }



        private string m_UpBreakConstant = "";
        /// <summary>
        /// 上脱持续时间
        /// </summary>
        public string UpBreakConstant
        {
            get { return m_UpBreakConstant; }
            set
            {
                if (m_UpBreakConstant != value)
                {
                    m_UpBreakConstant = value;
                    RaisePropertyChanged(() => UpBreakConstant);
                }
            }
        }

        private bool m_Allow = false;
        /// <summary>
        /// 允许对应
        /// </summary>
        public bool Allow
        {
            get { return m_Allow; }
            set
            {
                if (m_Allow != value)
                {
                    m_Allow = value;
                    if (value)
                    {
                        VideoGroupVM.ChangeTrack(m_TrackNum);
                    }
                    RaisePropertyChanged(() => Allow);
                }
            }
        }

        private bool m_Prower = false;
        /// <summary>
        /// 供电对应
        /// </summary>
        public bool Prower
        {
            get { return m_Prower; }
            set
            {
                if (m_Prower != value)
                {
                    m_Prower = value;
                    RaisePropertyChanged(() => Prower);
                }
            }
        }

        private bool m_Remove = false;
        /// <summary>
        /// 撤对应
        /// </summary>
        public bool Remove
        {
            get { return m_Remove; }
            set
            {
                if (m_Remove != value)
                {
                    m_Remove = value;
                    RaisePropertyChanged(() => Remove);
                }
            }
        }


        private bool m_TimeOutAddressValue = false;
        /// <summary>
        /// 技检超时*
        /// </summary>
        public bool TimeOutAddressValue
        {
            get { return m_TimeOutAddressValue; }
            set
            {
                if (m_TimeOutAddressValue != value)
                {
                    m_TimeOutAddressValue = value;
                    RaisePropertyChanged(() => TimeOutAddressValue);
                }
            }
        }
        #endregion
    }
}
