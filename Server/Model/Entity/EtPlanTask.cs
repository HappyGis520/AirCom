/*******************************************************************
 * * 版权所有： 王建军
 * * 文件名称： EtPlanTask.cs
 * * 功   能： 数据库dbo.PlanTask表实体类,采用Dapper方法
 * * 作   者： 王建军
 * * 电子邮箱： 595303122@qq.com
 * * 创建日期： 2016/02/23 09:30:55
 * * 修改记录： 
 * * 日期时间： 2016/02/23 09:30:55  修改人：王建军  创建
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
        #region EtPlanTask

        /// <summary>
        ///  PlanTask实体.
        /// </summary>
        [Serializable()]
        public class EtPlanTask : EntityBase
        {
            /****1个主键***/
            protected int _TaskID;


            protected int _UserID;
            protected int _CreateDateTime;

            protected EtUser _UserObj; //自动赋予初始值

            public EtPlanTask()
            {
                // Method = new EntityMethodClass();
            }

            #region 公开属性

            //public  EntityMethodClass Method = null;

            #region 主键

            /****1个主键***/

            public int TaskID
            {
                get { return _TaskID; }
                set { _TaskID = value; }
            }

            #endregion

            #region 其它字段

            public int CreateDateTime
            {
                get { return _CreateDateTime; }
                set { _CreateDateTime = value; }
            }

            #endregion

            #region 外键

            public int UserID
            {
                get { return _UserID; }
                set
                {
                    if (_UserID != value)
                    {
                        _UserID = value;
                    }
                }
            }

            #endregion

            public EtUser UserObj
            {
                get
                {
                    if (Method != null)
                    {
                        _UserObj = Method.GetEtUserByID(UserID);
                    }
                    return _UserObj;
                }
            }

            #endregion

            /// <summary>
            /// DataRow转为对象
            /// </summary>
            /// <param name="row"></param>
            internal EtPlanTask(DataRow row)
            {
                try
                {
                    /****1个主键***/
                    _TaskID = int.Parse(row["TaskID"].ToString());
                    _UserID = int.Parse(row["UserID"].ToString());
                    _CreateDateTime = int.Parse(row["CreateDateTime"].ToString());
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
            internal EtPlanTask(int id)
            {
                using (SqlConnection conn = new SqlConnection(SQLDBHelper.ConnectionString))
                {
                    EtPlanTask Relse = null;
                    /****1个主键***/
                    string sql = "SELECT * FROM [PlanTask] WHERE TaskID = '" + id.ToString() + "'";

                    using (var cnn = new SqlConnection(SQLDBHelper.ConnectionString))
                    {
                        cnn.Open();
                        Relse = cnn.Query<EtPlanTask>(sql, null).Single();
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
            protected void DeepCopy(EtPlanTask Oject)
            {
                try
                {
                    if (Oject != null)
                    {
                        this._TaskID = Oject.TaskID;
                        this._UserID = Oject.UserID;
                        this._CreateDateTime = Oject.CreateDateTime;
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
                    _UserObj = Method.GetEtUserByID(_UserID);
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

