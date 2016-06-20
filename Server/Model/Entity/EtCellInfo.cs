/*******************************************************************
 * * 版权所有： 王建军
 * * 文件名称： EtCellInfo.cs
 * * 功   能： 数据库dbo.CellInfo表实体类,采用Dapper方法
 * * 作   者： 王建军
 * * 电子邮箱： 595303122@qq.com
 * * 创建日期： 2016/02/23 09:28:34
 * * 修改记录： 
 * * 日期时间： 2016/02/23 09:28:34  修改人：王建军  创建
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
    #region EtCellInfo
    /// <summary>
    ///  CellInfo实体.
    /// </summary>
    [Serializable()]
    public class EtCellInfo : EntityBase
    {
        /****1个主键***/
        protected int _CELLID;


        protected int _StationID;
        protected string _GUID = String.Empty;

        protected EtBaseStation _StationObj;//自动赋予初始值
        public EtCellInfo()
        {
            // Method = new EntityMethodClass();
        }
        #region 公开属性
        //public  EntityMethodClass Method = null;

        #region 主键
        /****1个主键***/
        public int CELLID
        {
            get { return _CELLID; }
            set { _CELLID = value; }
        }
        #endregion
        #region 其它字段
        public string GUID
        {
            get { return _GUID; }
            set
            {
                _GUID = value;
            }
        }
        #endregion

        #region 外键
        public int StationID
        {
            get { return _StationID; }
            set
            {
                if (_StationID != value)
                {
                    _StationID = value;
                }
            }
        }
        #endregion

        public EtBaseStation StationObj
        {
            get
            {
                if (Method != null)
                {
                    _StationObj = Method.GetEtBaseStationByID(StationID);
                }
                return _StationObj;
            }
        }
        #endregion
        /// <summary>
        /// DataRow转为对象
        /// </summary>
        /// <param name="row"></param>
        internal EtCellInfo(DataRow row)
        {
            try
            {
                /****1个主键***/
                _CELLID = int.Parse(row["CELLID"].ToString());
                _StationID = int.Parse(row["StationID"].ToString());
                _GUID = row["GUID"].ToString();
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
		internal EtCellInfo(int id)
        {
            using (SqlConnection conn = new SqlConnection(SQLDBHelper.ConnectionString))
            {
                EtCellInfo Relse = null;
                /****1个主键***/
                string sql = "SELECT * FROM [CellInfo] WHERE CELLID = '" + id.ToString() + "'";

                using (var cnn = new SqlConnection(SQLDBHelper.ConnectionString))
                {
                    cnn.Open();
                    Relse = cnn.Query<EtCellInfo>(sql, null).Single();
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
        protected void DeepCopy(EtCellInfo Oject)
        {
            try
            {
                if (Oject != null)
                {
                    this._CELLID = Oject.CELLID;
                    this._StationID = Oject.StationID;
                    this._GUID = Oject.GUID;
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
                _StationObj = Method.GetEtBaseStationByID(_StationID);
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
