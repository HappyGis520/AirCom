using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Xml.Serialization;
using JLIB.CSharp;
using NetPlan.Model;

namespace NetPlan.Model
{
    [Serializable]
    /// <summary>
    /// AirCom馈线信息
    /// </summary>
    public class AirComAntennaType
    {
        protected int _SectorID = 0;
        /// <summary>
        /// 扇区编号
        /// </summary>
        public int SectorId
        {
            get { return _SectorID; }
            set { _SectorID = value; }
        }

        /// <summary>
        /// 天线型号
        /// </summary>
        private string _AntenTypeName = string.Empty;

        public　string AntennaTypeName 
        {
            get { return _AntenTypeName; }
            set { _AntenTypeName = value; }
        }
        /// <summary>
        /// 方向角
        /// </summary>
        private int _Azimuth;
        /// <summary>
        /// 挂高
        /// </summary>
        private double _Height;
        /// <summary>
        /// 机械
        /// </summary>
        private double _MechanicalDownTilt;

        /// <summary>
        /// 电子
        /// </summary>
        private double _ElectricalDownTilt;

        private string _ModelType = string.Empty;

        public double Lng
        {
            get { return Location.LongitudeField; }
        }

        public double Lat
        {
            get { return Location.Latitude; }
        }
        /// <summary>
        /// 地理位置
        /// </summary>
        public AirComLocationType Location;

        /// <summary>
        /// 方向角
        /// </summary>
        public int Azimuth
        {
            get { return _Azimuth; }
            set { _Azimuth = value; }
        }

        /// <summary>
        /// 挂高
        /// </summary>
        public double Height
        {
            get { return _Height; }
            set { _Height = value; }
        }

        /// <summary>
        /// 机械
        /// </summary>
        public double MechanicalDownTilt
        {
            get { return _MechanicalDownTilt; }
            set { _MechanicalDownTilt = value; }
        }

        /// <summary>
        /// 电子
        /// </summary>
        public double ElectricalDownTilt
        {
            get { return _ElectricalDownTilt; }
            set { _ElectricalDownTilt = value; }
        }

        /// <summary>
        /// 功率
        /// </summary>
        public double Power
        {
            get { return _Power; }
            set { _Power = value; }
        }

        /// <summary>
        /// 负荷
        /// </summary>
        public double Burthen
        {
            get { return _burthen; }
            set { _burthen = value; }
        }
        /// <summary>
        /// 传播模型
        /// </summary>
        public string ModelType
        {
            get { return _ModelType; }
            set { _ModelType = value; }
        }
        /// <summary>
        /// 载波别名
        /// </summary>
        public string CarrierAlias
        {
            get { return _CarrierAlias; }
            set { _CarrierAlias = value; }
        }
        /// <summary>
        /// 载波编号　
        /// </summary>
        public int CarrierId
        {
            get { return _CarrierID; }
            set { _CarrierID = value; }
        }

        /// <summary>
        /// 覆盖半径
        /// </summary>
        public double RadiusKm
        {
            get { return _RadiusKM; }
            set { _RadiusKM = value; }
        }
        /// <summary>
        /// 计算精度
        /// </summary>
        public double ResolutionMetres
        {
            get { return _ResolutionMetres; }
            set { _ResolutionMetres = value; }
        }


        //载波
        private string _CarrierAlias = string.Empty;

        private int _CarrierID = 0;



        /// <summary>
        /// 功率
        /// </summary>
        private double _Power;
        /// <summary>
        /// 负荷
        /// </summary>
        private double _burthen;

        /// <summary>
        /// 覆盖半径
        /// </summary>
        private double _RadiusKM = 2.00;
        /// <summary>
        /// 计算精度
        /// </summary>
        private double _ResolutionMetres = 10.00;





    }
}
