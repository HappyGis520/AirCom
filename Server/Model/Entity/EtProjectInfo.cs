/*******************************************************************
 * * 版权所有： 王建军
 * * 文件名称： EtProjectInfo.cs
 * * 功   能： 数据库dbo.ProjectInfo表实体类,采用Dapper方法
 * * 作   者： 王建军
 * * 电子邮箱： 595303122@qq.com
 * * 创建日期： 2016/02/23 09:31:52
 * * 修改记录： 
 * * 日期时间： 2016/02/23 09:31:52  修改人：王建军  创建
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

    #region EtProjectInfo
    /// <summary>
    ///  ProjectInfo实体.
    /// </summary>
    [Serializable()]
    public class EtProjectInfo
    {
        /****1个主键***/
        protected int _EDSID;


        protected string _CityName = String.Empty;
        protected string _CityDesc = String.Empty;
        protected string _ProjectName = String.Empty;
        protected string _Describe = String.Empty;

        public EtProjectInfo()
        {
        }
        #region 公开属性

        #region 主键
        /****1个主键***/
        public int EDSID
        {
            get { return _EDSID; }
            set { _EDSID = value; }
        }
        #endregion
        #region 其它字段
        public string CityName
        {
            get { return _CityName; }
            set
            {
                _CityName = value;
            }
        }

        public string CityDesc
        {
            get { return _CityDesc; }
            set
            {
                _CityDesc = value;
            }
        }

        public string ProjectName
        {
            get { return _ProjectName; }
            set
            {
                _ProjectName = value;
            }
        }

        public string Describe
        {
            get { return _Describe; }
            set
            {
                _Describe = value;
            }
        }
        #endregion

        #endregion
        /// <summary>
        /// DataRow转为对象
        /// </summary>
        /// <param name="row"></param>
        internal EtProjectInfo(DataRow row)
        {
            try
            {
                /****1个主键***/
                _EDSID = int.Parse(row["EDSID"].ToString());
                _CityName = row["CityName"].ToString();
                _CityDesc = row["CityDesc"].ToString();
                _ProjectName = row["ProjectName"].ToString();
                _Describe = row["Describe"].ToString();
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
		internal EtProjectInfo(int id)
        {
            using (SqlConnection conn = new SqlConnection(SQLDBHelper.ConnectionString))
            {
                EtProjectInfo Relse = null;
                /****1个主键***/
                string sql = "SELECT * FROM [ProjectInfo] WHERE EDSID = '" + id.ToString() + "'";

                using (var cnn = new SqlConnection(SQLDBHelper.ConnectionString))
                {
                    cnn.Open();
                    Relse = cnn.Query<EtProjectInfo>(sql, null).Single();
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
        protected void DeepCopy(EtProjectInfo Oject)
        {
            try
            {
                if (Oject != null)
                {
                    this._EDSID = Oject.EDSID;
                    this._CityName = Oject.CityName;
                    this._CityDesc = Oject.CityDesc;
                    this._ProjectName = Oject.ProjectName;
                    this._Describe = Oject.Describe;
                }
            }
            catch (Exception ex)
            {
                JLog.Instance.MethodName = MethodBase.GetCurrentMethod().Name;
                JLog.Instance.Error(ex.Message);
            }
        }



    }
    #endregion
}
