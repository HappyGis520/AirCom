/*******************************************************************
 * * 版权所有： 王建军
 * * 文件名称： EtAntennalnfo.cs
 * * 功   能： 数据库dbo.Antennalnfo表实体类,采用Dapper方法
 * * 作   者： 王建军
 * * 电子邮箱： 595303122@qq.com
 * * 创建日期： 2016/02/23 08:58:53
 * * 修改记录： 
 * * 日期时间： 2016/02/23 08:58:53  修改人：王建军  创建
 * *******************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Reflection;
using JLIB.CSharp;
using JLIB.Utility;
using Dapper;
namespace NetPlan.Model
{

    #region EtAntennalnfo
    /// <summary>
    ///  Antennalnfo实体.
    /// </summary>
    [Serializable()]
    public class EtAntennalnfo : EntityBase
    {
        /****1个主键***/
        protected int _AntennaID;


        protected int _CELLID;
        protected byte _AntennaType;
        protected double _Lng;
        protected double _Lat;
        protected int _Direction;
        protected double _DownTilt;
        protected double _Height;
        protected byte _CarrierID;
        protected double _RsPower;
        protected double _Burthen;
        protected byte _ModelType;
        protected double _RadiusKm;
        protected double _ResolutionMetres;

        protected EtCellInfo _CELLObj;//自动赋予初始值
        public EtAntennalnfo()
        {
            // Method = new EntityMethodClass();
        }
        #region 公开属性
        //public  EntityMethodClass Method = null;

        #region 主键
        /****1个主键***/
        public int AntennaID
        {
            get { return _AntennaID; }
            set { _AntennaID = value; }
        }
        #endregion
        #region 其它字段
        public byte AntennaType
        {
            get { return _AntennaType; }
            set
            {
                _AntennaType = value;
            }
        }

        public double Lng
        {
            get { return _Lng; }
            set
            {
                _Lng = value;
            }
        }

        public double Lat
        {
            get { return _Lat; }
            set
            {
                _Lat = value;
            }
        }

        public int Direction
        {
            get { return _Direction; }
            set
            {
                _Direction = value;
            }
        }

        public double DownTilt
        {
            get { return _DownTilt; }
            set
            {
                _DownTilt = value;
            }
        }

        public double Height
        {
            get { return _Height; }
            set
            {
                _Height = value;
            }
        }

        public byte CarrierID
        {
            get { return _CarrierID; }
            set
            {
                _CarrierID = value;
            }
        }

        public double RsPower
        {
            get { return _RsPower; }
            set
            {
                _RsPower = value;
            }
        }

        public double Burthen
        {
            get { return _Burthen; }
            set
            {
                _Burthen = value;
            }
        }

        public byte ModelType
        {
            get { return _ModelType; }
            set
            {
                _ModelType = value;
            }
        }

        public double RadiusKm
        {
            get { return _RadiusKm; }
            set
            {
                _RadiusKm = value;
            }
        }

        public double ResolutionMetres
        {
            get { return _ResolutionMetres; }
            set
            {
                _ResolutionMetres = value;
            }
        }
        #endregion

        #region 外键
        public int CELLID
        {
            get { return _CELLID; }
            set
            {
                if (_CELLID != value)
                {
                    _CELLID = value;
                }
            }
        }
        #endregion

        public EtCellInfo CELLObj
        {
            get
            {
                if (Method != null)
                {
                    _CELLObj = Method.GetEtCellInfoByID(CELLID);
                }
                return _CELLObj;
            }
        }
        #endregion
        /// <summary>
        /// DataRow转为对象
        /// </summary>
        /// <param name="row"></param>
        internal EtAntennalnfo(DataRow row)
        {
            try
            {
                /****1个主键***/
                _AntennaID = int.Parse(row["AntennaID"].ToString());
                _CELLID = int.Parse(row["CELLID"].ToString());
                _AntennaType = byte.Parse(row["AntennaType"].ToString());
                _Lng = double.Parse(row["Lng"].ToString());
                _Lat = double.Parse(row["Lat"].ToString());
                _Direction = int.Parse(row["Direction"].ToString());
                _DownTilt = double.Parse(row["DownTilt"].ToString());
                _Height = double.Parse(row["Height"].ToString());
                _CarrierID = byte.Parse(row["CarrierID"].ToString());
                _RsPower = double.Parse(row["RsPower"].ToString());
                _Burthen = double.Parse(row["Burthen"].ToString());
                _ModelType = byte.Parse(row["ModelType"].ToString());
                _RadiusKm = double.Parse(row["RadiusKm"].ToString());
                _ResolutionMetres = double.Parse(row["ResolutionMetres"].ToString());
            }
            catch (Exception ex)
            {
                JLog.Instance.MethodName = MethodBase.GetCurrentMethod().Name;
                JLog.Instance.Error(ex.Message);
            }

        }
        /****1个主键***/
        /// <summary>
        /// 根据主键获取对象
        /// </summary>
        /// <param name="id"></param>
		internal EtAntennalnfo(int id)
        {
            using (SqlConnection conn = new SqlConnection(SQLDBHelper.ConnectionString))
            {
                EtAntennalnfo Relse = null;
                /****1个主键***/
                string sql = "SELECT * FROM [Antennalnfo] WHERE AntennaID = '" + id.ToString() + "'";

                using (var cnn = new SqlConnection(SQLDBHelper.ConnectionString))
                {
                    cnn.Open();
                    Relse = cnn.Query<EtAntennalnfo>(sql, null).Single();
                    cnn.Close();
                    if (Relse != null)
                        DeepCopy(Relse);

                }
            }
        }
        /// <summary>
        /// 拷贝对象内容
        /// </summary>
        /// <param name="Oject"></param>
        protected void DeepCopy(EtAntennalnfo Oject)
        {
            try
            {
                if (Oject != null)
                {
                    this._AntennaID = Oject.AntennaID;
                    this._CELLID = Oject.CELLID;
                    this._AntennaType = Oject.AntennaType;
                    this._Lng = Oject.Lng;
                    this._Lat = Oject.Lat;
                    this._Direction = Oject.Direction;
                    this._DownTilt = Oject.DownTilt;
                    this._Height = Oject.Height;
                    this._CarrierID = Oject.CarrierID;
                    this._RsPower = Oject.RsPower;
                    this._Burthen = Oject.Burthen;
                    this._ModelType = Oject.ModelType;
                    this._RadiusKm = Oject.RadiusKm;
                    this._ResolutionMetres = Oject.ResolutionMetres;
                }
            }
            catch (Exception ex)
            {
                JLog.Instance.MethodName = MethodBase.GetCurrentMethod().Name;
                JLog.Instance.Error(ex.Message);
            }
        }

        /// <summary>
        /// 加载引用对象
        /// </summary>
        public override void LoadRefrenceObject()
        {
            try
            {
                _CELLObj = Method.GetEtCellInfoByID(_CELLID);
            }
            catch (Exception ex)
            {
                JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                    MethodBase.GetCurrentMethod().Module.Name);
            }
        }


    }
    #endregion
}
