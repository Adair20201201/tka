using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TKA.Model
{
    public class TrackModel
    {
        /// <summary>
        /// 轨道号
        /// </summary>
        public string TrackID { get; set; }

       

        /// <summary>
        /// 左侧上箭头
        /// </summary>
        public string LeftUpArrowAddress { get; set; }

        /// <summary>
        /// 左侧下箭头
        /// </summary>
        public string LeftDownArrowAddress { get; set; }

        /// <summary>
        /// 右侧上箭头
        /// </summary>
        public string RightUpArrowAddress { get; set; }

        /// <summary>
        /// 右侧下箭头
        /// </summary>
        public string RightDownArrowAddress { get; set; }

        /// <summary>
        /// 左侧申请上脱
        /// </summary>
        public string LeftApplyForUpDetachAddress { get; set; }

        /// <summary>
        /// 左侧申请下脱
        /// </summary>
        public string LeftApplyForDownDetachAddress { get; set; }

        /// <summary>
        /// 右侧申请上脱
        /// </summary>
        public string RightApplyForUpDetachAddress { get; set; }

        /// <summary>
        /// 右侧申请下脱
        /// </summary>
        public string RightApplyForDownDetachAddress { get; set; }

        /// <summary>
        /// 允许对应
        /// </summary>
        public string AllowAddress { get; set; }

        /// <summary>
        /// 供电对应
        /// </summary>
        public string PowerAddress { get; set; }

        /// <summary>
        /// 撤对应
        /// </summary>
        public string RemoveAddress { get; set; }
        /// <summary>
        /// 右允许对应
        /// </summary>
        public string RightAllowAddress { get; set; }

        #region 语音文字提示
        /// <summary>
        /// 提示，道已上脱
        /// </summary>
        public string NoteLeftUpDetaching { get; set; }
        /// <summary>
        /// 提示，道已上脱
        /// </summary>
        public string NoteRightUpDetaching { get; set; }
        /// <summary>
        /// 提示，道已下脱
        /// </summary>
        public string NoteLeftDownDetaching { get; set; }
        /// <summary>
        /// 提示，道已下脱
        /// </summary>
        public string NoteRightDownDetaching { get; set; }

        /// <summary>
        /// 提示，道申请上脱
        /// </summary>
        public string NoteLeftApplyForUpDetach { get; set; }
        /// <summary>
        /// 提示，道申请上脱
        /// </summary>
        public string NoteRightApplyForUpDetach { get; set; }
        /// <summary>
        /// 提示，道申请下脱
        /// </summary>
        public string NoteLeftApplyForDownDetach { get; set; }
        /// <summary>
        /// 提示，道申请下脱
        /// </summary>
        public string NoteRightApplyForDownDetach { get; set; }
        
        /// <summary>
        /// 标志故障  added by 刘永强
        /// </summary>
        public string NoteFlagFault { get; set; }
        /// <summary>
        /// 脱轨器未下到位  added by 刘永强
        /// </summary>
        public string NoteNotInPlace { get; set; }
        /// <summary>
        /// 乱动申请开关  added by 刘永强
        /// </summary>
        public string NoteTouch { get; set; }
        /// <summary>
        /// 请将供电开关复位  added by 刘永强
        /// </summary>
        public string NotePowerSupplyReset { get; set; }
        /// <summary>
        /// 请将撤脱开关复位  added by 刘永强
        /// </summary>
        public string NoteAvulsionReset { get; set; }
        #endregion

        #region 语音文字告警
        /// <summary>
        /// 警告，标志故障
        /// </summary>
        public string AlertBlooeySignal { get; set; }

        /// <summary>
        /// 警告，脱轨器未下到位
        /// </summary>
        public string AlertDerailerLessThenDegree { get; set; }

        /// <summary>
        /// 警告，供电开关复位
        /// </summary>
        public string AlertRestoreEconductionSwitch { get; set; }

        /// <summary>
        /// 警告，撤开关复位
        /// </summary>
        public string AlertRestoreRevokeSwitch { get; set; }

        /// <summary>
        /// 警告，乱动申请开关
        /// </summary>
        public string AlertDisturbanceApplictionSwitch { get; set; }

        /// <summary>
        /// 警告，屏幕开关复位
        /// </summary>
        public string AlertRestoreScreenSwitch { get; set; }

        /// <summary>
        /// 警告，技检时间超时
        /// </summary>
        public string AlertTechnicalMaintenanceTimeout { get; set; }
        #endregion
    }
}
