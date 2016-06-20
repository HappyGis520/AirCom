/*******************************************************************
 * * 版权所有： 王建军
 * * 文件名称： EtAtennalType.cs
 * * 功   能： 数据库dbo.AtennalType表实体类,采用Dapper方法
 * * 作   者： 王建军
 * * 电子邮箱： 595303122@qq.com
 * * 创建日期： 2016/02/23 10:20:51
 * * 修改记录： 
 * * 日期时间： 2016/02/23 10:20:51  修改人：王建军  创建
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
    #region EtAtennalType
    /// <summary>
    ///  AtennalType实体.
    /// </summary>
    [Serializable()]
    public class EtAtennalType : EntityBase
    {
        /****1个主键***/
        protected int _ID;


        protected int _EDSID;
        protected string _Name = String.Empty;

        protected EtProjectInfo _EDSObj;//自动赋予初始值
        public EtAtennalType()
        {
            // Method = new EntityMethodClass();
        }
        #region 公开属性
        //public  EntityMethodClass Method = null;

        #region 主键
        /****1个主键***/
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        #endregion
        #region 其它字段
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
            }
        }
        #endregion

        #region 外键
        public int EDSID
        {
            get { return _EDSID; }
            set
            {
                if (_EDSID != value)
                {
                    _EDSID = value;
                }
            }
        }
        #endregion

        public EtProjectInfo EDSObj
        {
            get
            {
                if (Method != null)
                {
                    _EDSObj = Method.GetEtProjectInfoByID(EDSID);
                }
                return _EDSObj;
            }
        }
        #endregion
        /// <summary>
        /// DataRow转为对象
        /// </summary>
        /// <param name="row"></param>
        internal EtAtennalType(DataRow row)
        {
            try
            {
                /****1个主键***/
                _ID = int.Parse(row["ID"].ToString());
                _EDSID = int.Parse(row["EDSID"].ToString());
                _Name = row["Name"].ToString();
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
		internal EtAtennalType(int id)
        {
            using (SqlConnection conn = new SqlConnection(SQLDBHelper.ConnectionString))
            {
                EtAtennalType Relse = null;
                /****1个主键***/
                string sql = "SELECT * FROM [AtennalType] WHERE ID = '" + id.ToString() + "'";

                using (var cnn = new SqlConnection(SQLDBHelper.ConnectionString))
                {
                    cnn.Open();
                    Relse = cnn.Query<EtAtennalType>(sql, null).Single();
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
        protected void DeepCopy(EtAtennalType Oject)
        {
            try
            {
                if (Oject != null)
                {
                    this._ID = Oject.ID;
                    this._EDSID = Oject.EDSID;
                    this._Name = Oject.Name;
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
                _EDSObj = Method.GetEtProjectInfoByID(_EDSID);
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
