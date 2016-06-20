/*******************************************************************
 * * 版权所有： 王建军
 * * 文件名称： EtUser.cs
 * * 功   能： 数据库dbo.User表实体类,采用Dapper方法
 * * 作   者： 王建军
 * * 电子邮箱： 595303122@qq.com
 * * 创建日期： 2016/02/23 09:33:23
 * * 修改记录： 
 * * 日期时间： 2016/02/23 09:33:23  修改人：王建军  创建
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
    #region EtUser
    /// <summary>
    ///  User实体.
    /// </summary>
    [Serializable()]
    public class EtUser : EntityBase
    {
        /****1个主键***/
        protected int _UserID;


        protected int _EDSID;
        protected string _UserName = String.Empty;
        protected string _Password = String.Empty;

        protected EtProjectInfo _EDSObj;//自动赋予初始值
        public EtUser()
        {
            // Method = new EntityMethodClass();
        }
        #region 公开属性
        //public  EntityMethodClass Method = null;

        #region 主键
        /****1个主键***/
        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        #endregion
        #region 其它字段
        public string UserName
        {
            get { return _UserName; }
            set
            {
                _UserName = value;
            }
        }

        public string Password
        {
            get { return _Password; }
            set
            {
                _Password = value;
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
        internal EtUser(DataRow row)
        {
            try
            {
                /****1个主键***/
                _UserID = int.Parse(row["UserID"].ToString());
                _EDSID = int.Parse(row["EDSID"].ToString());
                _UserName = row["UserName"].ToString();
                _Password = row["Password"].ToString();
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
		internal EtUser(int id)
        {
            using (SqlConnection conn = new SqlConnection(SQLDBHelper.ConnectionString))
            {
                EtUser Relse = null;
                /****1个主键***/
                string sql = "SELECT * FROM [User] WHERE UserID = '" + id.ToString() + "'";

                using (var cnn = new SqlConnection(SQLDBHelper.ConnectionString))
                {
                    cnn.Open();
                    Relse = cnn.Query<EtUser>(sql, null).Single();
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
        protected void DeepCopy(EtUser Oject)
        {
            try
            {
                if (Oject != null)
                {
                    this._UserID = Oject.UserID;
                    this._EDSID = Oject.EDSID;
                    this._UserName = Oject.UserName;
                    this._Password = Oject.Password;
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
