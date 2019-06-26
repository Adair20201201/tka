using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TKA.Helper;

namespace TKA.Model
{
    public class MultiChannelInfo
    {
        public string ChannelStartPoint { get; set; }
        public int ChannelCount { get; set; }
    }

    public class ConfigModel
    {
        public List<TrackModel> LTM { get; set; }

        public List<string> ChannelCollection { get; set; }
        public List<MultiChannelInfo> MultiChannelCollections { get; set; }

        ///// <summary>
        ///// 自动
        ///// </summary>
        //public string Auto { get; set; }
        /// <summary>
        /// 室内操作
        /// </summary>
        public string IndoorOperation { get; set; }
        public string PrimaryWatchSetting { get; set; }
        public string MinorWatchSetting { get; set; }

        public string SpeekAddress { get; set; }

        public ConfigModel()
        {
            ChannelCollection = new List<string>();
            LTM = new List<TrackModel>();
        }

        //public void test()
        //{
        //    #region Single Check

        //    ChannelCollection.Add("0001");
        //    ChannelCollection.Add("0002");
        //    ChannelCollection.Add("0003");
        //    ChannelCollection.Add("0004");
        //    ChannelCollection.Add("0005");
        //    ChannelCollection.Add("0030");
        //    ChannelCollection.Add("0031");
        //    ChannelCollection.Add("0032");
        //    ChannelCollection.Add("0033");
        //    ChannelCollection.Add("0034");
        //    ChannelCollection.Add("0035");
        //    ChannelCollection.Add("0036");
        //    ChannelCollection.Add("0037");
        //    ChannelCollection.Add("0038");
        //    ChannelCollection.Add("0039");
        //    ChannelCollection.Add("0200");
        //    ChannelCollection.Add("0201");
        //    ChannelCollection.Add("0202");
        //    ChannelCollection.Add("0203");
        //    ChannelCollection.Add("0204");
        //    ChannelCollection.Add("0205");
        //    ChannelCollection.Add("0206");
        //    ChannelCollection.Add("0207");
        //    ChannelCollection.Add("0216");
        //    ChannelCollection.Add("0217");
        //    ChannelCollection.Add("0218");
        //    ChannelCollection.Add("0219");
        //    #endregion

        //    #region Multiple Check
        //    MultiChannelCollections = new List<MultiChannelInfo>() { 
        //        new MultiChannelInfo(){ ChannelStartPoint = "0001", ChannelCount = 5},
        //        new MultiChannelInfo(){ ChannelStartPoint = "0030", ChannelCount = 10},
        //        new MultiChannelInfo(){ ChannelStartPoint = "0200", ChannelCount = 8},
        //        new MultiChannelInfo(){ ChannelStartPoint = "0216", ChannelCount = 4}
        //    };
        //    #endregion

        //    AllowSceneSetting = "021915";
        //    BottonLightSetting = "000400";
        //    PrimaryWatchSetting = "020001";
        //    MinorWatchSetting = "020002";
        //    SpeekAddress = "020003";

        //    TrackModel TM1 = new TrackModel()
        //    {
        //        LeftTrackAddress = "021711",
        //        RightTrackAddress = "021811",
        //        LeftCubeAddress = "000512",
        //        RightCubeAddress = "000513",
        //        LeftUpArrowAddress = "020702",
        //        LeftDownArrowAddress = "020703",
        //        RightUpArrowAddress = "020706",
        //        RightDownArrowAddress = "020707",
        //        LeftApplyForUpDetachAddress1 = "000208",
        //        LeftApplyForUpDetachAddress2 = "000306",
        //        LeftApplyForDownDetachAddress = "020701",
        //        RightApplyForUpDetachAddress1 = "000209",
        //        RightApplyForUpDetachAddress2 = "000307",
        //        RightApplyForDownDetachAddress = "020705",
        //        CenterLeftCircleAddress = "000412",
        //        CenterRightCircleAddress = "000314",

        //        AllowLeftAddress = "020600",
        //        AllowRightAddress = "020604",
        //        UPBreakAddress = "021906",
        //        DownBreakAddress = "021606",

        //        NoteUpDetaching = "003506",
        //        NoteDownDetaching = "003606",
        //        NoteWestApplyForUpDetach = "003706",
        //        NoteEastApplyForUpDetach = "003806",
        //        NoteWestDerailerUpExcepiton = "003906",
        //        NoteEastDerailerUpExcepiton = "003913",
        //        AlertBlooeySignal = "003006",
        //        AlertDerailerLessThenDegree = "003106",
        //        AlertRestoreEconductionSwitch = "003206",
        //        AlertRestoreRevokeSwitch = "003306",
        //        AlertDisturbanceApplictionSwitch = "003406",
        //        AlertRestoreScreenSwitch = "003513",
        //        AlertTechnicalMaintenanceTimeout = "003613"
        //    };
        //    LTM.Add(TM1);

        //    TrackModel TM2 = new TrackModel()
        //    {
        //        LeftTrackAddress = "021710",
        //        RightTrackAddress = "021810",
        //        LeftCubeAddress = "000510",
        //        RightCubeAddress = "000511",
        //        LeftUpArrowAddress = "020510",
        //        LeftDownArrowAddress = "020511",
        //        RightUpArrowAddress = "020514",
        //        RightDownArrowAddress = "020515",
        //        LeftApplyForUpDetachAddress1 = "000206",
        //        LeftApplyForUpDetachAddress2 = "000304",
        //        LeftApplyForDownDetachAddress = "020509",
        //        RightApplyForUpDetachAddress1 = "000207",
        //        RightApplyForUpDetachAddress2 = "000305",
        //        RightApplyForDownDetachAddress = "020513",
        //        CenterLeftCircleAddress = "000411",
        //        CenterRightCircleAddress = "000313",

        //        AllowLeftAddress = "020408",
        //        AllowRightAddress = "020412",
        //        UPBreakAddress = "021905",
        //        DownBreakAddress = "021605",

        //        NoteUpDetaching = "003505",
        //        NoteDownDetaching = "003605",
        //        NoteWestApplyForUpDetach = "003705",
        //        NoteEastApplyForUpDetach = "003805",
        //        NoteWestDerailerUpExcepiton = "003905",
        //        NoteEastDerailerUpExcepiton = "003912",
        //        AlertBlooeySignal = "003005",
        //        AlertDerailerLessThenDegree = "003105",
        //        AlertRestoreEconductionSwitch = "003205",
        //        AlertRestoreRevokeSwitch = "003305",
        //        AlertDisturbanceApplictionSwitch = "003405",
        //        AlertRestoreScreenSwitch = "003512",
        //        AlertTechnicalMaintenanceTimeout = "003612"
        //    };
        //    LTM.Add(TM2);

        //    TrackModel TM3 = new TrackModel()
        //    {
        //        LeftTrackAddress = "021709",
        //        RightTrackAddress = "021809",
        //        LeftCubeAddress = "000508",
        //        RightCubeAddress = "000509",
        //        LeftUpArrowAddress = "020502",
        //        LeftDownArrowAddress = "020503",
        //        RightUpArrowAddress = "020506",
        //        RightDownArrowAddress = "020507",
        //        LeftApplyForUpDetachAddress1 = "000204",
        //        LeftApplyForUpDetachAddress2 = "000302",
        //        LeftApplyForDownDetachAddress = "020501",
        //        RightApplyForUpDetachAddress1 = "000205",
        //        RightApplyForUpDetachAddress2 = "000303",
        //        RightApplyForDownDetachAddress = "020505",
        //        CenterLeftCircleAddress = "000410",
        //        CenterRightCircleAddress = "000312",

        //        AllowLeftAddress = "020400",
        //        AllowRightAddress = "020404",
        //        UPBreakAddress = "021904",
        //        DownBreakAddress = "021604",

        //        NoteUpDetaching = "003504",
        //        NoteDownDetaching = "003604",
        //        NoteWestApplyForUpDetach = "003704",
        //        NoteEastApplyForUpDetach = "003804",
        //        NoteWestDerailerUpExcepiton = "003904",
        //        NoteEastDerailerUpExcepiton = "003911",
        //        AlertBlooeySignal = "003004",
        //        AlertDerailerLessThenDegree = "003104",
        //        AlertRestoreEconductionSwitch = "003204",
        //        AlertRestoreRevokeSwitch = "003304",
        //        AlertDisturbanceApplictionSwitch = "003404",
        //        AlertRestoreScreenSwitch = "003511",
        //        AlertTechnicalMaintenanceTimeout = "003611"
        //    };
        //    LTM.Add(TM3);

        //    TrackModel TM4 = new TrackModel()
        //    {
        //        LeftTrackAddress = "021708",
        //        RightTrackAddress = "021808",
        //        LeftCubeAddress = "000506",
        //        RightCubeAddress = "000507",
        //        LeftUpArrowAddress = "020310",
        //        LeftDownArrowAddress = "020311",
        //        RightUpArrowAddress = "020314",
        //        RightDownArrowAddress = "020315",
        //        LeftApplyForUpDetachAddress1 = "000202",
        //        LeftApplyForUpDetachAddress2 = "000300",
        //        LeftApplyForDownDetachAddress = "020309",
        //        RightApplyForUpDetachAddress1 = "000203",
        //        RightApplyForUpDetachAddress2 = "000301",
        //        RightApplyForDownDetachAddress = "020313",
        //        CenterLeftCircleAddress = "000409",
        //        CenterRightCircleAddress = "000311",

        //        AllowLeftAddress = "020208",
        //        AllowRightAddress = "020212",
        //        UPBreakAddress = "021903",
        //        DownBreakAddress = "021603",

        //        NoteUpDetaching = "003503",
        //        NoteDownDetaching = "003603",
        //        NoteWestApplyForUpDetach = "003703",
        //        NoteEastApplyForUpDetach = "003803",
        //        NoteWestDerailerUpExcepiton = "003903",
        //        NoteEastDerailerUpExcepiton = "003910",
        //        AlertBlooeySignal = "003003",
        //        AlertDerailerLessThenDegree = "003103",
        //        AlertRestoreEconductionSwitch = "003203",
        //        AlertRestoreRevokeSwitch = "003303",
        //        AlertDisturbanceApplictionSwitch = "003403",
        //        AlertRestoreScreenSwitch = "003510",
        //        AlertTechnicalMaintenanceTimeout = "003610"
        //    };
        //    LTM.Add(TM4);

        //    TrackModel TM5 = new TrackModel()
        //    {
        //        LeftTrackAddress = "021706",
        //        RightTrackAddress = "021806",
        //        LeftCubeAddress = "000504",
        //        RightCubeAddress = "000505",
        //        LeftUpArrowAddress = "020302",
        //        LeftDownArrowAddress = "020303",
        //        RightUpArrowAddress = "020306",
        //        RightDownArrowAddress = "020307",
        //        LeftApplyForUpDetachAddress1 = "000200",
        //        LeftApplyForUpDetachAddress2 = "000214",
        //        LeftApplyForDownDetachAddress = "020301",
        //        RightApplyForUpDetachAddress1 = "000201",
        //        RightApplyForUpDetachAddress2 = "000215",
        //        RightApplyForDownDetachAddress = "020305",
        //        CenterLeftCircleAddress = "000408",
        //        CenterRightCircleAddress = "000310",

        //        AllowLeftAddress = "020200",
        //        AllowRightAddress = "020204",
        //        UPBreakAddress = "021902",
        //        DownBreakAddress = "021602",

        //        NoteUpDetaching = "003502",
        //        NoteDownDetaching = "003602",
        //        NoteWestApplyForUpDetach = "003702",
        //        NoteEastApplyForUpDetach = "003802",
        //        NoteWestDerailerUpExcepiton = "003902",
        //        NoteEastDerailerUpExcepiton = "003909",
        //        AlertBlooeySignal = "003002",
        //        AlertDerailerLessThenDegree = "003102",
        //        AlertRestoreEconductionSwitch = "003202",
        //        AlertRestoreRevokeSwitch = "003302",
        //        AlertDisturbanceApplictionSwitch = "003402",
        //        AlertRestoreScreenSwitch = "003509",
        //        AlertTechnicalMaintenanceTimeout = "003609"
        //    };
        //    LTM.Add(TM5);

        //    TrackModel TM6 = new TrackModel()
        //    {
        //        LeftTrackAddress = "021705",
        //        RightTrackAddress = "021805",
        //        LeftCubeAddress = "000502",
        //        RightCubeAddress = "000503",
        //        LeftUpArrowAddress = "020110",
        //        LeftDownArrowAddress = "020111",
        //        RightUpArrowAddress = "020114",
        //        RightDownArrowAddress = "020115",
        //        LeftApplyForUpDetachAddress1 = "000114",
        //        LeftApplyForUpDetachAddress2 = "000212",
        //        LeftApplyForDownDetachAddress = "020109",
        //        RightApplyForUpDetachAddress1 = "000115",
        //        RightApplyForUpDetachAddress2 = "000213",
        //        RightApplyForDownDetachAddress = "020113",
        //        CenterLeftCircleAddress = "000407",
        //        CenterRightCircleAddress = "000309",

        //        AllowLeftAddress = "020008",
        //        AllowRightAddress = "020012",
        //        UPBreakAddress = "021901",
        //        DownBreakAddress = "021601",

        //        NoteUpDetaching = "003501",
        //        NoteDownDetaching = "003601",
        //        NoteWestApplyForUpDetach = "003701",
        //        NoteEastApplyForUpDetach = "003801",
        //        NoteWestDerailerUpExcepiton = "003901",
        //        NoteEastDerailerUpExcepiton = "003908",
        //        AlertBlooeySignal = "003001",
        //        AlertDerailerLessThenDegree = "003101",
        //        AlertRestoreEconductionSwitch = "003201",
        //        AlertRestoreRevokeSwitch = "003301",
        //        AlertDisturbanceApplictionSwitch = "003401",
        //        AlertRestoreScreenSwitch = "003508",
        //        AlertTechnicalMaintenanceTimeout = "003608"
        //    };
        //    LTM.Add(TM6);

        //    TrackModel TM7 = new TrackModel()
        //    {
        //        LeftTrackAddress = "021704",
        //        RightTrackAddress = "021804",
        //        LeftCubeAddress = "000500",
        //        RightCubeAddress = "000501",
        //        LeftUpArrowAddress = "020102",
        //        LeftDownArrowAddress = "020103",
        //        RightUpArrowAddress = "020106",
        //        RightDownArrowAddress = "020107",
        //        LeftApplyForUpDetachAddress1 = "000112",
        //        LeftApplyForUpDetachAddress2 = "000210",
        //        LeftApplyForDownDetachAddress = "020101",
        //        RightApplyForUpDetachAddress1 = "000113",
        //        RightApplyForUpDetachAddress2 = "000211",
        //        RightApplyForDownDetachAddress = "020105",
        //        CenterLeftCircleAddress = "000406",
        //        CenterRightCircleAddress = "000308",

        //        AllowLeftAddress = "020000",
        //        AllowRightAddress = "020004",
        //        UPBreakAddress = "021900",
        //        DownBreakAddress = "021600",

        //        NoteUpDetaching = "003500",
        //        NoteDownDetaching = "003600",
        //        NoteWestApplyForUpDetach = "003700",
        //        NoteEastApplyForUpDetach = "003800",
        //        NoteWestDerailerUpExcepiton = "003900",
        //        NoteEastDerailerUpExcepiton = "003907",
        //        AlertBlooeySignal = "003000",
        //        AlertDerailerLessThenDegree = "003100",
        //        AlertRestoreEconductionSwitch = "003200",
        //        AlertRestoreRevokeSwitch = "003300",
        //        AlertDisturbanceApplictionSwitch = "003400",
        //        AlertRestoreScreenSwitch = "003507",
        //        AlertTechnicalMaintenanceTimeout = "003607"
        //    };
        //    LTM.Add(TM7);

        //    XMLHelper.Write("d:/TrackModelConfig.xml", this, typeof(ConfigModel));
        //}
    }
}
