/*******************************************************************
 * * 版权所有： 王建军
 * * 文件名称： DALCellInfo.cs
 * * 功   能： 数据库dbo.CellInfo表访问类,采用Dapper方法
 * * 作   者： 王建军
 * * 电子邮箱： 595303122@qq.com
 * * 创建日期： 2016/02/23 09:48:32
 * * 修改记录： 
 * * 日期时间： 2016/02/23 09:48:32  修改人：王建军  创建
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

using NetPlan.Model;
namespace NetPlan.DAL
{
    #region DALCellInfo
    public static class DALCellInfo
    {
        #region 添加一个记录/对象
        /// <summary>
        /// 添加一个记录/对象,并返回新记录的主键值
        /// </summary>
        /// <param name="entityPointInfo"></param>
        /// <returns></returns>
        public static Int32 Add(EtCellInfo etCellInfo)
        {
            int result = -1;
            DynamicParameters p = new DynamicParameters();
            p.Add("@StationID", etCellInfo.StationID);
            p.Add("@GUID", etCellInfo.GUID);
            p.Add("@CELLID", 0, DbType.Int32, ParameterDirection.Output);

            using (var cnn = new SqlConnection(SQLDBHelper.ConnectionString))
            {
                cnn.Open();
                cnn.Execute("[dbo].[usp_InsertCellInfo]", p, null, null, CommandType.StoredProcedure);
                result = p.Get<int>("@CELLID");
                cnn.Close();
            }
            return result;
        }
        #endregion
        #region 删除指定主键值的记录
        /// <summary>
        /// 删除指定主键值的记录,并返回受影响行数
        /// </summary>
        public static int DeleteByCELLID(int cELLID)
        {
            int result = 0;
            DynamicParameters p = new DynamicParameters();
            p.Add("@CELLID", cELLID);
            p.Add("@Rows", 0, DbType.Int32, ParameterDirection.Output);

            using (var cnn = new SqlConnection(SQLDBHelper.ConnectionString))
            {
                cnn.Open();
                cnn.Execute("[dbo].[usp_DeleteCellInfo]", p, null, null, CommandType.StoredProcedure);
                result = p.Get<int>("@Rows");
                cnn.Close();
            }
            return result;
        }
        #endregion

        #region 修改主键值一致的记录
        /// <summary>
        /// 修改主键值一致的记录，并返回受影响行数
        /// </summary>

        public static Int32 Modify(EtCellInfo etCellInfo)
        {
            Int32 result = 0;
            DynamicParameters p = new DynamicParameters();
            p.Add("@StationID", etCellInfo.StationID);
            p.Add("@GUID", etCellInfo.GUID);
            p.Add("@CELLID", etCellInfo.CELLID);
            p.Add("@Rows", 0, DbType.Int32, ParameterDirection.Output);
            using (var cnn = new SqlConnection(SQLDBHelper.ConnectionString))
            {
                cnn.Open();
                cnn.Execute("[dbo].[usp_UpdateCellInfo]", p, null, null, CommandType.StoredProcedure);
                result = p.Get<int>("@Rows");
                cnn.Close();
            }
            return result;

        }
        #endregion
        #region 获取主键记录集
        /// <summary>
        /// 获取主键记录集
        /// </summary>
        public static IList<int> GetAllCELLIDs()
        {
            IList<int> Relse = null;
            using (var cnn = new SqlConnection(SQLDBHelper.ConnectionString))
            {
                string sql = "SELECT CELLID FROM [CellInfo]";
                cnn.Open();
                Relse = cnn.Query<int>(sql, null).ToList();
                cnn.Close();
            }
            return Relse;
        }
        #endregion
        #region 获取所有记录对象
        /// <summary>
        /// 获取所有记录对象
        /// </summary>
        public static IList<EtCellInfo> GetAllCellInfos()
        {
            IList<EtCellInfo> Relse = null;
            using (var cnn = new SqlConnection(SQLDBHelper.ConnectionString))
            {
                string sql = "SELECT * FROM [CellInfo]";
                cnn.Open();
                Relse = cnn.Query<EtCellInfo>(sql, null).ToList();
                cnn.Close();

            }
            return Relse;
        }
        #endregion

        #region 返回条件查询所有数据
        /// <summary>
        /// 返回条件查询所有数据
        /// </summary>
        public static IList<EtCellInfo> GetAllCellInfosWithDynamicCondition(string where)
        {
            IList<EtCellInfo> Relse = null;
            using (var cnn = new SqlConnection(SQLDBHelper.ConnectionString))
            {
                string sql = "SELECT * FROM [CellInfo] where " + where;
                cnn.Open();
                Relse = cnn.Query<EtCellInfo>(sql, null).ToList();
                cnn.Close();
            }
            return Relse;
        }
        #endregion

        #region 取得表的行数
        /// <summary>
        /// 取得表的行数
        /// </summary>
        public static int GetRowCountOfAllCellInfos()
        {
            return SQLDBHelper.GetRowCount("CellInfo", "");
        }
        #endregion
        #region 取得给定主键值集对应的记录集
        /// <summary>
        /// 取得给定主键值集对应的记录集
        /// </summary>
        public static IList<EtCellInfo> GetCellInfosByCellInfoIDs(IList<int> listIDs)
        {

            IList<EtCellInfo> Relse = new List<EtCellInfo>();
            string sql = string.Empty;
            int i = 0;
            foreach (int id in listIDs)
            {
                string formtstr = i == 0 ? "DeptID = {0}" : " or DeptID = {0}";
                sql += string.Format(formtstr, id);
                i++;
            }
            try
            {
                Relse = GetAllCellInfosWithDynamicCondition(sql);
            }
            catch
            {
            }
            return Relse;
        }
        #endregion
        #region 清空表
        public static void ClearCellInfo()
        {
            string proName = "[dbo].[usp_ClearCellInfo]";
            List<SqlParameter> listParas = new List<SqlParameter>();
            // listParas.Add(SQLDBHelper.CreateSqlParameter("@Id", id));
            SQLDBHelper.RunProcGetAffected(proName, listParas.ToArray());
        }
        #endregion

        #region 使用存储过程分页返回记录对象集
        /// <summary>
        /// 使用存储过程分页返回记录对象集
        /// </summary>
        /// <param name="DataTbleName">输出表名称</param>
        /// <param name="ReturnFields">返回的字段，多个时，有逗号隔开</param>
        /// <param name="Where">条件语句，可以为空，表示不使用条件</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="Sort">排序字段，可以为空,使用默认</param>
        /// <param name="pageSize">每页的行数</param>
        /// <param name="AllRecords">满足条件的记录数</param>
        /// <returns></returns>
        public static IList<EtCellInfo> GetPageCellInfos(string DataTbleName, string ReturnFields, string SqlWhere, int pageIndex, string Sort, int pageSize, out Int32 AllRecords)
        {

            SqlParameter[] SqlParam = new SqlParameter[]
            {
                new SqlParameter("@TableName",SqlDbType.NVarChar,1000),
                new SqlParameter("@PrimaryKey",SqlDbType.NVarChar,200),
                new SqlParameter("@ReturnFields",SqlDbType.NVarChar,3000),
                new SqlParameter("@Where",SqlDbType.NVarChar,3000),
                new SqlParameter("@PageIndex",SqlDbType.Int,4),
                new SqlParameter("@PageSize",SqlDbType.Int,4),
                new SqlParameter("@Sort",SqlDbType.NVarChar,200)
            };
            SqlParam[0].Value = "CellInfo";
            SqlParam[1].Value = "CELLID";
            SqlParam[2].Value = ReturnFields;
            SqlParam[3].Value = SqlWhere;
            SqlParam[4].Value = pageIndex;
            SqlParam[5].Value = pageSize;
            SqlParam[6].Value = Sort;
            DataSet ds = SQLDBHelper.RunProceGetDataSet("usp_GetPageData", SqlParam, DataTbleName);
            if (ds != null && ds.Tables.Count > 0)
            {
                AllRecords = Convert.ToInt32(ds.Tables[1].Rows[0]["RecordCount"]);
                return GetCellInfosByDataTable(ds.Tables[DataTbleName]);
            }
            AllRecords = 0;
            return null;

        }
        #endregion
        #region ----
        public static void BulkToDB(IList<EtCellInfo> Datas)
        {
            DataTable dt = null;
            #region 创建表
            dt = new DataTable(TableName);
            dt.Columns.Add(new DataColumn("StationID", typeof(int)));
            dt.Columns.Add(new DataColumn("GUID", typeof(string)));
            #endregion

            #region 插入数据

            foreach (var data in Datas)
            {
                DataRow r = dt.NewRow();
                r["StationID"] = data.StationID;

                r["GUID"] = data.GUID.ToString();
                dt.Rows.Add(r);
            }


            #endregion
            SqlConnection sqlConn = new SqlConnection(SQLDBHelper.ConnectionString);
            SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConn);
            bulkCopy.DestinationTableName = TableName;
            bulkCopy.BatchSize = dt.Rows.Count;
            try
            {
                sqlConn.Open();
                if (dt != null && dt.Rows.Count != 0)
                    bulkCopy.WriteToServer(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConn.Close();
                if (bulkCopy != null)
                    bulkCopy.Close();
            }
        }
        #endregion
        #region 从DataTable中取得所有记录的对象集
        /// <summary>
        /// 从DataTable中取得所有记录的对象集
        /// </summary>
        private static IList<EtCellInfo> GetCellInfosByDataTable(DataTable table)
        {
            IList<EtCellInfo> listEtCellInfos = new List<EtCellInfo>();
            foreach (DataRow row in table.Rows)
            {
                listEtCellInfos.Add(new EtCellInfo(row));
            }
            return listEtCellInfos;
        }
        #endregion
        public static string TableName = "CellInfo";
        #region 列名称
        public static string Field_CELLID = "CELLID";
        public static string Field_StationID = "StationID";
        public static string Field_GUID = "GUID";
        #endregion


    }
    #endregion
}