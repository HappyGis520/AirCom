/*******************************************************************
 * * 版权所有： 王建军
 * * 文件名称： EtBaseStation.cs
 * * 功   能： 数据库dbo.BaseStation表实体类,采用Dapper方法
 * * 作   者： 王建军
 * * 电子邮箱： 595303122@qq.com
 * * 创建日期： 2016/02/23 11:32:46
 * * 修改记录： 
 * * 日期时间： 2016/02/23 11:32:46  修改人：王建军  创建
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
    #region EtBaseStation
    /// <summary>
    ///  BaseStation实体.
    /// </summary>
    [Serializable()]
    public class EtBaseStation : EntityBase
    {
        /****1个主键***/
        protected int _StationID;


        protected string _StationGUID = String.Empty;
        protected int _TaskID;
        protected string _StationAlias = String.Empty;
        protected double _Lng;
        protected double _Lat;
        protected byte _StationType;
        protected bool _MastSave;
        protected byte _CoverType;
        protected byte _PlanLevel;

        protected EtPlanTask _TaskObj;//自动赋予初始值
        public EtBaseStation()
        {
            // Method = new EntityMethodClass();
        }
        #region 公开属性
        //public  EntityMethodClass Method = null;

        #region 主键
        /****1个主键***/
        public int StationID
        {
            get { return _StationID; }
            set { _StationID = value; }
        }
        #endregion
        #region 其它字段
        public string StationGUID
        {
            get { return _StationGUID; }
            set
            {
                _StationGUID = value;
            }
        }

        public string StationAlias
        {
            get { return _StationAlias; }
            set
            {
                _StationAlias = value;
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

        public byte StationType
        {
            get { return _StationType; }
            set
            {
                _StationType = value;
            }
        }

        public bool MastSave
        {
            get { return _MastSave; }
            set
            {
                _MastSave = value;
            }
        }

        public byte CoverType
        {
            get { return _CoverType; }
            set
            {
                _CoverType = value;
            }
        }

        public byte PlanLevel
        {
            get { return _PlanLevel; }
            set
            {
                _PlanLevel = value;
            }
        }
        #endregion

        #region 外键
        public int TaskID
        {
            get { return _TaskID; }
            set
            {
                if (_TaskID != value)
                {
                    _TaskID = value;
                }
            }
        }
        #endregion

        public EtPlanTask TaskObj
        {
            get
            {
                if (Method != null)
                {
                    _TaskObj = Method.GetEtPlanTaskByID(TaskID);
                }
                return _TaskObj;
            }
        }
        #endregion
        /// <summary>
        /// DataRow转为对象
        /// </summary>
        /// <param name="row"></param>
        internal EtBaseStation(DataRow row)
        {
            try
            {
                /****1个主键***/
                _StationID = int.Parse(row["StationID"].ToString());
                _StationGUID = row["StationGUID"].ToString();
                _TaskID = int.Parse(row["TaskID"].ToString());
                _StationAlias = row["StationAlias"].ToString();
                _Lng = double.Parse(row["Lng"].ToString());
                _Lat = double.Parse(row["Lat"].ToString());
                _StationType = byte.Parse(row["StationType"].ToString());
                _MastSave = bool.Parse(row["MastSave"].ToString());
                _CoverType = byte.Parse(row["CoverType"].ToString());
                _PlanLevel = byte.Parse(row["PlanLevel"].ToString());
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
		internal EtBaseStation(int id)
        {
            using (SqlConnection conn = new SqlConnection(SQLDBHelper.ConnectionString))
            {
                EtBaseStation Relse = null;
                /****1个主键***/
                string sql = "SELECT * FROM [BaseStation] WHERE StationID = '" + id.ToString() + "'";

                using (var cnn = new SqlConnection(SQLDBHelper.ConnectionString))
                {
                    cnn.Open();
                    Relse = cnn.Query<EtBaseStation>(sql, null).Single();
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
        protected void DeepCopy(EtBaseStation Oject)
        {
            try
            {
                if (Oject != null)
                {
                    this._StationID = Oject.StationID;
                    this._StationGUID = Oject.StationGUID;
                    this._TaskID = Oject.TaskID;
                    this._StationAlias = Oject.StationAlias;
                    this._Lng = Oject.Lng;
                    this._Lat = Oject.Lat;
                    this._StationType = Oject.StationType;
                    this._MastSave = Oject.MastSave;
                    this._CoverType = Oject.CoverType;
                    this._PlanLevel = Oject.PlanLevel;
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
                _TaskObj = Method.GetEtPlanTaskByID(_TaskID);
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
