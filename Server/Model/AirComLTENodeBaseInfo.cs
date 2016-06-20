using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetPlan.Model
{
    [Serializable]
    /// <summary>
    /// AirComLTE基站基本信息
    /// </summary>
    public  class AirComLTENodeBaseInfo
    {
        private string _StationID = "基站号";
        private string _StationAlias = "基站别名";
        private double _Lng = 0;
        private double _Lat = 0;
        private EnumStationType _StationType = EnumStationType.Planning;
        private EnumSaveType _SaveType = EnumSaveType.Delete;
        private EnumCoverType _coverType = EnumCoverType.Outdoor;
        private  string _CityName = "城市名";
         /// <summary>
         /// 所属地市名称 --由浪潮提供
         /// </summary>
        public string CityName
        {
            get { return _CityName; }
            set { _CityName = value; }
        }
        /// <summary>
        /// 基站编号
        /// </summary>
        public string StationId
        {
            get { return _StationID; }
            set { _StationID = value; }
        }
        /// <summary>
        /// 基站别名
        /// </summary>
        public string StationAlias
        {
            get { return _StationAlias; }
            set { _StationAlias = value; }
        }
        /// <summary>
        /// 经度
        /// </summary>
        public double Lng
        {
            get { return _Lng; }
            set { _Lng = value; }
        }
        /// <summary>
        /// 纬度
        /// </summary>
        public double Lat
        {
            get { return _Lat; }
            set { _Lat = value; }
        }
        /// <summary>
        /// 基站类别
        /// </summary>
        public EnumStationType StationType
        {
            get { return _StationType; }
            set { _StationType = value; }
        }
        /// <summary>
        /// 保存类别，执行分析后是否保留
        /// </summary>
        public EnumSaveType SaveType
        {
            get { return _SaveType; }
            set { _SaveType = value; }
        }
        /// <summary>
        /// 覆盖类别
        /// </summary>
        public EnumCoverType CoverType
        {
            get { return _coverType; }
            set { _coverType = value; }
        }
        private int _PlanPrjID;
        /// <summary>
        /// 规划期数
        /// </summary>
        internal int PlanPrjID
        {
            get { return _PlanPrjID; }
            set { _PlanPrjID = value; }
        }

    }
}
