using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using TKA.Business;

namespace TKA.ViewModel
{
    public class TrackWarningViewModel : ViewModelBase
    {
        /// <summary>
        /// the information of notification
        /// </summary>
        private static string NoteLeftUpDetaching_NotifyInfo = "道已上脱";
        private static string NoteRightUpDetaching_NotifyInfo = "道已上脱";
        private static string NoteLeftDownDetaching_NotifyInfo = "道已下脱";
        private static string NoteRightDownDetaching_NotifyInfo = "道已下脱";

        private static string NoteLeftApplyForUpDetach_NotifyInfo = "道申请上脱";
        private static string NoteRightApplyForUpDetach_NotifyInfo = "道申请上脱";
        private static string NoteLeftApplyForDownDetach_NotifyInfo = "道申请下脱";
        private static string NoteRightApplyForDownDetach_NotifyInfo = "道申请下脱";

        private static string NoteLeftCubeAddress_NotifyInfo = "脱轨器上方异常";
        private static string NoteRightCubeAddress_NotifyInfo = "脱轨器上方异常";
        private static string NoteFlagFault_NotifyInfo = "道标志故障";
        private static string NoteNotInPlace_NotifyInfo = "道脱轨器未下到位";
        private static string NoteTouch_NotifyInfo = "道乱动申请开关";
        private static string NotePowerSupplyReset_NotifyInfo = "道请将供电开关复位";
        private static string NoteAvulsionReset_NotifyInfo = "道请将撤脱开关复位";

        private bool m_NoteFlagFault = false;
        /// <summary>
        /// 标志故障
        /// </summary>
        public bool NoteFlagFault
        {
            get
            {
                return m_NoteFlagFault;
            }
            set
            {
                TKA.ViewModel.WarningViewModel.Notification notification = new TKA.ViewModel.WarningViewModel.Notification() { IsWarning = true, TrackNumber = TrackNum, NotifyInfo = NoteFlagFault_NotifyInfo, DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), NeedSpeech = true };
                if (value)
                {
                    PLCControler.Instence.MWVM.WarningVM.SetWarning(notification);
                }
                if (value != m_NoteFlagFault)
                {
                    m_NoteFlagFault = value;
                    if (m_NoteFlagFault)
                    {
                        PLCControler.Instence.MWVM.WarningVM.InsertDb(notification);
                        // create a notification
                        //TKA.ViewModel.WarningViewModel.Notification notification = new TKA.ViewModel.WarningViewModel.Notification() { IsWarning = true, TrackNumber = TrackNum, NotifyInfo = NoteFlagFault_NotifyInfo, DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), NeedSpeech = true };
                        //// push notification
                        ////PLCControler.Instence.MWVM.WarningVM.SetWarning(notification);
                        //// record notification
                        //PLCControler.Instence.MWVM.WarningVM.AddNotification(notification);
                    }
                    else
                    {
                        // remove notification
                        //PLCControler.Instence.MWVM.WarningVM.RemoveNotification(NoteFlagFault_NotifyInfo, TrackNum);
                        //// push last notification
                        //TKA.ViewModel.WarningViewModel.Notification lastNotification = PLCControler.Instence.MWVM.WarningVM.GetlastNtification();
                        //PLCControler.Instence.MWVM.WarningVM.SetLastWarning(lastNotification);
                    }
                    RaisePropertyChanged(() => NoteFlagFault);
                }
            }
        }
        private bool m_NoteNotInPlace = false;
        /// <summary>
        /// 脱轨器未下到位
        /// </summary>
        public bool NoteNotInPlace
        {
            get
            {
                return m_NoteNotInPlace;
            }
            set
            {
                TKA.ViewModel.WarningViewModel.Notification notification = new TKA.ViewModel.WarningViewModel.Notification() { IsWarning = true, TrackNumber = TrackNum, NotifyInfo = NoteNotInPlace_NotifyInfo, DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), NeedSpeech = true };
                if (value)
                {
                    PLCControler.Instence.MWVM.WarningVM.SetWarning(notification);
                }
                if (value != m_NoteNotInPlace)
                {
                    m_NoteNotInPlace = value;

                    if (m_NoteNotInPlace)
                    {
                        PLCControler.Instence.MWVM.WarningVM.InsertDb(notification);
                        // create a notification

                        // push notification
                        //PLCControler.Instence.MWVM.WarningVM.SetWarning(notification);
                        //// record notification
                        //PLCControler.Instence.MWVM.WarningVM.AddNotification(notification);
                    }
                    else
                    {
                        //// remove notification
                        //PLCControler.Instence.MWVM.WarningVM.RemoveNotification(NoteNotInPlace_NotifyInfo, TrackNum);
                        //// push last notification
                        //TKA.ViewModel.WarningViewModel.Notification lastNotification = PLCControler.Instence.MWVM.WarningVM.GetlastNtification();
                        //PLCControler.Instence.MWVM.WarningVM.SetLastWarning(lastNotification);
                    }
                    RaisePropertyChanged(() => NoteNotInPlace);
                }
            }
        }
        private bool m_NoteTouch = false;
        /// <summary>
        /// 乱动申请开关
        /// </summary>
        public bool NoteTouch
        {
            get
            {
                return m_NoteTouch;
            }
            set
            {
                TKA.ViewModel.WarningViewModel.Notification notification = new TKA.ViewModel.WarningViewModel.Notification() { IsWarning = true, TrackNumber = TrackNum, NotifyInfo = NoteTouch_NotifyInfo, DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), NeedSpeech = true };
                if (value)
                {
                    PLCControler.Instence.MWVM.WarningVM.SetWarning(notification);
                }
                if (value != m_NoteTouch)
                {
                    m_NoteTouch = value;

                    if (m_NoteTouch)
                    {
                        PLCControler.Instence.MWVM.WarningVM.InsertDb(notification);
                        // create a notification
                        //TKA.ViewModel.WarningViewModel.Notification notification = new TKA.ViewModel.WarningViewModel.Notification() { IsWarning = true, TrackNumber = TrackNum, NotifyInfo = NoteTouch_NotifyInfo, DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), NeedSpeech = true };
                        //// push notification
                        //PLCControler.Instence.MWVM.WarningVM.SetWarning(notification);
                        //// record notification
                        //PLCControler.Instence.MWVM.WarningVM.AddNotification(notification);
                    }
                    else
                    {
                        // remove notification
                        //PLCControler.Instence.MWVM.WarningVM.RemoveNotification(NoteTouch_NotifyInfo, TrackNum);
                        //// push last notification
                        //TKA.ViewModel.WarningViewModel.Notification lastNotification = PLCControler.Instence.MWVM.WarningVM.GetlastNtification();
                        //PLCControler.Instence.MWVM.WarningVM.SetLastWarning(lastNotification);
                    }
                    RaisePropertyChanged(() => NoteTouch);
                }
            }
        }

        private bool m_NoteAvulsionReset = false;
        /// <summary>
        /// 请将撤脱开关复位
        /// </summary>
        public bool NoteAvulsionReset
        {
            get
            {
                return m_NoteAvulsionReset;
            }
            set
            {
                TKA.ViewModel.WarningViewModel.Notification notification = new TKA.ViewModel.WarningViewModel.Notification() { IsWarning = true, TrackNumber = TrackNum, NotifyInfo = NoteAvulsionReset_NotifyInfo, DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), NeedSpeech = true };
                if (value)
                {
                    PLCControler.Instence.MWVM.WarningVM.SetWarning(notification);
                }
                if (value != m_NoteAvulsionReset)
                {
                    m_NoteAvulsionReset = value;

                    if (m_NoteAvulsionReset)
                    {
                        PLCControler.Instence.MWVM.WarningVM.InsertDb(notification);
                        // create a notification

                        // push notification

                        // record notification
                        //PLCControler.Instence.MWVM.WarningVM.AddNotification(notification);
                    }
                    else
                    {
                        // remove notification
                        //PLCControler.Instence.MWVM.WarningVM.RemoveNotification(NoteAvulsionReset_NotifyInfo, TrackNum);
                        //// push last notification
                        //TKA.ViewModel.WarningViewModel.Notification lastNotification = PLCControler.Instence.MWVM.WarningVM.GetlastNtification();
                        //PLCControler.Instence.MWVM.WarningVM.SetLastWarning(lastNotification);
                    }
                    RaisePropertyChanged(() => NoteAvulsionReset);
                }
            }
        }
        private bool m_NotePowerSupplyReset = false;
        /// <summary>
        /// 请将供电开关复位
        /// </summary>
        public bool NotePowerSupplyReset
        {
            get
            {
                return m_NotePowerSupplyReset;
            }
            set
            {
                TKA.ViewModel.WarningViewModel.Notification notification = new TKA.ViewModel.WarningViewModel.Notification() { IsWarning = true, TrackNumber = TrackNum, NotifyInfo = NotePowerSupplyReset_NotifyInfo, DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), NeedSpeech = true };
                if (value)
                {
                    PLCControler.Instence.MWVM.WarningVM.SetWarning(notification);
                }
                if (value != m_NotePowerSupplyReset)
                {
                    m_NotePowerSupplyReset = value;

                    if (m_NotePowerSupplyReset)
                    {
                        PLCControler.Instence.MWVM.WarningVM.InsertDb(notification);
                        // create a notification

                        // push notification

                        // record notification
                        //PLCControler.Instence.MWVM.WarningVM.AddNotification(notification);
                    }
                    else
                    {
                        // remove notification
                        //PLCControler.Instence.MWVM.WarningVM.RemoveNotification(NotePowerSupplyReset_NotifyInfo, TrackNum);
                        //// push last notification
                        //TKA.ViewModel.WarningViewModel.Notification lastNotification = PLCControler.Instence.MWVM.WarningVM.GetlastNtification();
                        //PLCControler.Instence.MWVM.WarningVM.SetLastWarning(lastNotification);
                    }
                    RaisePropertyChanged(() => NotePowerSupplyReset);
                }
            }
        }

        public int TrackNum = 0;

        private bool m_NoteRightCubeAddress = false;
        /// <summary>
        /// 脱轨器上方异常
        /// </summary>
        public bool NoteRightCubeAddress
        {
            get
            {
                return m_NoteRightCubeAddress;
            }
            set
            {
                TKA.ViewModel.WarningViewModel.Notification notification = new TKA.ViewModel.WarningViewModel.Notification() { IsWarning = true, TrackNumber = TrackNum, NotifyInfo = NoteRightCubeAddress_NotifyInfo, DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), NeedSpeech = true };
                if (!value)
                {
                    PLCControler.Instence.MWVM.WarningVM.SetWarning(notification);
                }
                if (value != m_NoteRightCubeAddress)
                {
                    m_NoteRightCubeAddress = value;

                    if (!m_NoteRightCubeAddress)
                    {
                        PLCControler.Instence.MWVM.WarningVM.InsertDb(notification);
                        // create a notification

                        // push notification

                        // record notification
                        //PLCControler.Instence.MWVM.WarningVM.AddNotification(notification);
                    }
                    else
                    {
                        // remove notification
                        //PLCControler.Instence.MWVM.WarningVM.RemoveNotification(NoteRightCubeAddress_NotifyInfo, TrackNum);
                        //// push last notification
                        //TKA.ViewModel.WarningViewModel.Notification lastNotification = PLCControler.Instence.MWVM.WarningVM.GetlastNtification();
                        //PLCControler.Instence.MWVM.WarningVM.SetLastWarning(lastNotification);
                    }
                    RaisePropertyChanged(() => NoteLeftCubeAddress);
                }
            }
        }
        private bool m_NoteLeftCubeAddress = false;
        /// <summary>
        /// 脱轨器上方异常
        /// </summary>
        public bool NoteLeftCubeAddress
        {
            get
            {
                return m_NoteLeftCubeAddress;
            }
            set
            {
                TKA.ViewModel.WarningViewModel.Notification notification = new TKA.ViewModel.WarningViewModel.Notification() { IsWarning = true, TrackNumber = TrackNum, NotifyInfo = NoteLeftCubeAddress_NotifyInfo, DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), NeedSpeech = true };
                if (!value)
                {
                    PLCControler.Instence.MWVM.WarningVM.SetWarning(notification);
                }
                if (value != m_NoteLeftCubeAddress)
                {
                    m_NoteLeftCubeAddress = value;

                    if (!m_NoteLeftCubeAddress)
                    {
                        PLCControler.Instence.MWVM.WarningVM.InsertDb(notification);
                        // create a notification

                        // push notification

                        // record notification
                        //PLCControler.Instence.MWVM.WarningVM.AddNotification(notification);
                    }
                    else
                    {
                        // remove notification
                        //PLCControler.Instence.MWVM.WarningVM.RemoveNotification(NoteLeftCubeAddress_NotifyInfo, TrackNum);
                        //// push last notification
                        //TKA.ViewModel.WarningViewModel.Notification lastNotification = PLCControler.Instence.MWVM.WarningVM.GetlastNtification();
                        //PLCControler.Instence.MWVM.WarningVM.SetLastWarning(lastNotification);
                    }
                    RaisePropertyChanged(() => NoteLeftCubeAddress);
                }
            }
        }
        private bool m_NoteLeftUpDetaching = false;
        /// <summary>
        /// 道已上脱
        /// </summary>
        public bool NoteLeftUpDetaching
        {
            get
            {
                return m_NoteLeftUpDetaching;
            }
            set
            {
                if (value != m_NoteLeftUpDetaching)
                {
                    m_NoteLeftUpDetaching = value;

                    if (m_NoteLeftUpDetaching)
                    {
                        // create a notification
                        TKA.ViewModel.WarningViewModel.Notification notification = new TKA.ViewModel.WarningViewModel.Notification() { IsWarning = false, TrackNumber = TrackNum, NotifyInfo = NoteLeftUpDetaching_NotifyInfo, DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), NeedSpeech = true };
                        // push notification
                        PLCControler.Instence.MWVM.WarningVM.SetWarning(notification);
                        PLCControler.Instence.MWVM.WarningVM.InsertDb(notification);
                        // record notification
                        //PLCControler.Instence.MWVM.WarningVM.AddNotification(notification);
                    }
                    else
                    {
                        // remove notification
                        //PLCControler.Instence.MWVM.WarningVM.RemoveNotification(NoteLeftUpDetaching_NotifyInfo, TrackNum);
                        //// push last notification
                        //TKA.ViewModel.WarningViewModel.Notification lastNotification = PLCControler.Instence.MWVM.WarningVM.GetlastNtification();
                        //PLCControler.Instence.MWVM.WarningVM.SetLastWarning(lastNotification);
                    }
                    RaisePropertyChanged(() => NoteLeftUpDetaching);
                }
            }
        }
        private bool m_NoteRightUpDetaching = false;
        /// <summary>
        /// 道已上脱
        /// </summary>
        public bool NoteRightUpDetaching
        {
            get
            {
                return m_NoteRightUpDetaching;
            }
            set
            {
                if (value != m_NoteRightUpDetaching)
                {
                    m_NoteRightUpDetaching = value;

                    if (m_NoteRightUpDetaching)
                    {
                        // create a notification
                        TKA.ViewModel.WarningViewModel.Notification notification = new TKA.ViewModel.WarningViewModel.Notification() { IsWarning = false, TrackNumber = TrackNum, NotifyInfo = NoteRightUpDetaching_NotifyInfo, DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), NeedSpeech = true };
                        // push notification
                        PLCControler.Instence.MWVM.WarningVM.SetWarning(notification);
                        PLCControler.Instence.MWVM.WarningVM.InsertDb(notification);
                        // record notification
                        //PLCControler.Instence.MWVM.WarningVM.AddNotification(notification);
                    }
                    else
                    {
                        //// remove notification
                        //PLCControler.Instence.MWVM.WarningVM.RemoveNotification(NoteRightUpDetaching_NotifyInfo, TrackNum);
                        //// push last notification
                        //TKA.ViewModel.WarningViewModel.Notification lastNotification = PLCControler.Instence.MWVM.WarningVM.GetlastNtification();
                        //PLCControler.Instence.MWVM.WarningVM.SetLastWarning(lastNotification);
                    }
                    RaisePropertyChanged(() => NoteRightUpDetaching);
                }
            }
        }
        private bool m_NoteLeftDownDetaching = true;
        /// <summary>
        /// 道已下脱
        /// </summary>
        public bool NoteLeftDownDetaching
        {
            get
            {
                return m_NoteLeftDownDetaching;
            }
            set
            {
                if (value != m_NoteLeftDownDetaching)
                {
                    m_NoteLeftDownDetaching = value;

                    if (m_NoteLeftDownDetaching)
                    {
                        // create a notification
                        TKA.ViewModel.WarningViewModel.Notification notification = new TKA.ViewModel.WarningViewModel.Notification() { IsWarning = false, TrackNumber = TrackNum, NotifyInfo = NoteLeftDownDetaching_NotifyInfo, DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), NeedSpeech = true };
                        // push notification
                        PLCControler.Instence.MWVM.WarningVM.SetWarning(notification);
                        PLCControler.Instence.MWVM.WarningVM.InsertDb(notification);
                        // record notification
                        //PLCControler.Instence.MWVM.WarningVM.AddNotification(notification);
                    }
                    else
                    {
                        //// remove notification
                        //PLCControler.Instence.MWVM.WarningVM.RemoveNotification(NoteLeftDownDetaching_NotifyInfo, TrackNum);
                        //// push last notification
                        //TKA.ViewModel.WarningViewModel.Notification lastNotification = PLCControler.Instence.MWVM.WarningVM.GetlastNtification();
                        //PLCControler.Instence.MWVM.WarningVM.SetLastWarning(lastNotification);
                    }

                    RaisePropertyChanged(() => NoteLeftDownDetaching);
                }
            }
        }
        private bool m_NoteRightDownDetaching = true;
        /// <summary>
        /// 道已下脱
        /// </summary>
        public bool NoteRightDownDetaching
        {
            get
            {
                return m_NoteRightDownDetaching;
            }
            set
            {
                if (value != m_NoteRightDownDetaching)
                {
                    m_NoteRightDownDetaching = value;

                    if (m_NoteRightDownDetaching)
                    {
                        // create a notification
                        TKA.ViewModel.WarningViewModel.Notification notification = new TKA.ViewModel.WarningViewModel.Notification() { IsWarning = false, TrackNumber = TrackNum, NotifyInfo = NoteRightDownDetaching_NotifyInfo, DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), NeedSpeech = true };
                        // push notification
                        PLCControler.Instence.MWVM.WarningVM.SetWarning(notification);
                        PLCControler.Instence.MWVM.WarningVM.InsertDb(notification);
                        // record notification
                        //PLCControler.Instence.MWVM.WarningVM.AddNotification(notification);
                    }
                    else
                    {
                        //// remove notification
                        //PLCControler.Instence.MWVM.WarningVM.RemoveNotification(NoteRightDownDetaching_NotifyInfo, TrackNum);
                        //// push last notification
                        //TKA.ViewModel.WarningViewModel.Notification lastNotification = PLCControler.Instence.MWVM.WarningVM.GetlastNtification();
                        //PLCControler.Instence.MWVM.WarningVM.SetLastWarning(lastNotification);
                    }

                    RaisePropertyChanged(() => NoteRightDownDetaching);
                }
            }
        }
        private bool m_NoteLeftApplyForUpDetach = false;
        /// <summary>
        /// 道申请上脱
        /// </summary>
        public bool NoteLeftApplyForUpDetach
        {
            get
            {
                return m_NoteLeftApplyForUpDetach;
            }
            set
            {
                if (value != m_NoteLeftApplyForUpDetach)
                {
                    m_NoteLeftApplyForUpDetach = value;

                    if (m_NoteLeftApplyForUpDetach)
                    {
                        // create a notification
                        TKA.ViewModel.WarningViewModel.Notification notification = new TKA.ViewModel.WarningViewModel.Notification() { IsWarning = false, TrackNumber = TrackNum, NotifyInfo = NoteLeftApplyForUpDetach_NotifyInfo, DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), NeedSpeech = true };
                        // push notification
                        PLCControler.Instence.MWVM.WarningVM.SetWarning(notification);
                        PLCControler.Instence.MWVM.WarningVM.InsertDb(notification);
                        // record notification
                        //PLCControler.Instence.MWVM.WarningVM.AddNotification(notification);
                    }
                    else
                    {
                        //// remove notification
                        //PLCControler.Instence.MWVM.WarningVM.RemoveNotification(NoteLeftApplyForUpDetach_NotifyInfo, TrackNum);
                        //// push last notification
                        //TKA.ViewModel.WarningViewModel.Notification lastNotification = PLCControler.Instence.MWVM.WarningVM.GetlastNtification();
                        //PLCControler.Instence.MWVM.WarningVM.SetLastWarning(lastNotification);
                    }

                    RaisePropertyChanged(() => NoteLeftApplyForUpDetach);
                }
            }
        }

        private bool m_NoteRightApplyForUpDetach = false;
        /// <summary>
        /// 道申请上脱
        /// </summary>
        public bool NoteRightApplyForUpDetach
        {
            get
            {
                return m_NoteRightApplyForUpDetach;
            }
            set
            {
                if (value != m_NoteRightApplyForUpDetach)
                {
                    m_NoteRightApplyForUpDetach = value;

                    if (m_NoteRightApplyForUpDetach)
                    {
                        // create a notification
                        TKA.ViewModel.WarningViewModel.Notification notification = new TKA.ViewModel.WarningViewModel.Notification() { IsWarning = false, TrackNumber = TrackNum, NotifyInfo = NoteRightApplyForUpDetach_NotifyInfo, DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), NeedSpeech = true };
                        // push notification
                        PLCControler.Instence.MWVM.WarningVM.SetWarning(notification);
                        PLCControler.Instence.MWVM.WarningVM.InsertDb(notification);
                        // record notification
                        //PLCControler.Instence.MWVM.WarningVM.AddNotification(notification);
                    }
                    else
                    {
                        //// remove notification
                        //PLCControler.Instence.MWVM.WarningVM.RemoveNotification(NoteRightApplyForUpDetach_NotifyInfo, TrackNum);
                        //// push last notification
                        //TKA.ViewModel.WarningViewModel.Notification lastNotification = PLCControler.Instence.MWVM.WarningVM.GetlastNtification();
                        //PLCControler.Instence.MWVM.WarningVM.SetLastWarning(lastNotification);
                    }

                    RaisePropertyChanged(() => NoteRightApplyForUpDetach);
                }
            }
        }

        private bool m_NoteLeftApplyForDownDetach = false;
        /// <summary>
        /// 道申请下脱
        /// </summary>
        public bool NoteLeftApplyForDownDetach
        {
            get
            {
                return m_NoteLeftApplyForDownDetach;
            }
            set
            {
                if (value != m_NoteLeftApplyForDownDetach)
                {
                    m_NoteLeftApplyForDownDetach = value;

                    if (m_NoteLeftApplyForDownDetach)
                    {
                        // create a notification
                        TKA.ViewModel.WarningViewModel.Notification notification = new TKA.ViewModel.WarningViewModel.Notification() { IsWarning = false, TrackNumber = TrackNum, NotifyInfo = NoteLeftApplyForDownDetach_NotifyInfo, DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), NeedSpeech = true };
                        // push notification
                        PLCControler.Instence.MWVM.WarningVM.SetWarning(notification);
                        PLCControler.Instence.MWVM.WarningVM.InsertDb(notification);
                        // record notification
                        //PLCControler.Instence.MWVM.WarningVM.AddNotification(notification);
                    }
                    else
                    {
                        //// remove notification
                        //PLCControler.Instence.MWVM.WarningVM.RemoveNotification(NoteLeftApplyForDownDetach_NotifyInfo, TrackNum);
                        //// push last notification
                        //TKA.ViewModel.WarningViewModel.Notification lastNotification = PLCControler.Instence.MWVM.WarningVM.GetlastNtification();
                        //PLCControler.Instence.MWVM.WarningVM.SetLastWarning(lastNotification);
                    }

                    RaisePropertyChanged(() => NoteLeftApplyForDownDetach);
                }
            }
        }

        private bool m_NoteRightApplyForDownDetach = false;
        /// <summary>
        /// 道申请下脱
        /// </summary>
        public bool NoteRightApplyForDownDetach
        {
            get
            {
                return m_NoteRightApplyForDownDetach;
            }
            set
            {
                if (value != m_NoteRightApplyForDownDetach)
                {
                    m_NoteRightApplyForDownDetach = value;

                    if (m_NoteRightApplyForDownDetach)
                    {
                        // create a notification
                        TKA.ViewModel.WarningViewModel.Notification notification = new TKA.ViewModel.WarningViewModel.Notification() { IsWarning = false, TrackNumber = TrackNum, NotifyInfo = NoteRightApplyForDownDetach_NotifyInfo, DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), NeedSpeech = true };
                        // push notification
                        PLCControler.Instence.MWVM.WarningVM.SetWarning(notification);
                        PLCControler.Instence.MWVM.WarningVM.InsertDb(notification);
                        // record notification
                        //PLCControler.Instence.MWVM.WarningVM.AddNotification(notification);
                    }
                    else
                    {
                        // remove notification
                        //PLCControler.Instence.MWVM.WarningVM.RemoveNotification(NoteRightApplyForDownDetach_NotifyInfo, TrackNum);
                        //// push last notification
                        //TKA.ViewModel.WarningViewModel.Notification lastNotification = PLCControler.Instence.MWVM.WarningVM.GetlastNtification();
                        //PLCControler.Instence.MWVM.WarningVM.SetLastWarning(lastNotification);
                    }

                    RaisePropertyChanged(() => NoteRightApplyForDownDetach);
                }
            }
        }
    }
}
