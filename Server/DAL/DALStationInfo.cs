/*******************************************************************
 * * 版权所有： 王建军
 * * 文件名称： DALBaseStation.cs
 * * 功   能： 数据库dbo.BaseStation表访问类,采用Dapper方法
 * * 作   者： 王建军
 * * 电子邮箱： 595303122@qq.com
 * * 创建日期： 2016/02/23 13:04:03
 * * 修改记录： 
 * * 日期时间： 2016/02/23 13:04:03  修改人：王建军  创建
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
    #region DALBaseStation
    public static class DALBaseStation
    {
        #region 添加一个记录/对象
        /// <summary>
        /// 添加一个记录/对象,并返回新记录的主键值
        /// </summary>
        /// <param name="entityPointInfo"></param>
        /// <returns></returns>
        public static Int32 Add(EtBaseStation etBaseStation)
        {
            int result = -1;
            DynamicParameters p = new DynamicParameters();
            p.Add("@StationGUID", etBaseStation.StationGUID);
            p.Add("@TaskID", etBaseStation.TaskID);
            p.Add("@StationAlias", etBaseStation.StationAlias);
            p.Add("@Lng", etBaseStation.Lng);
            p.Add("@Lat", etBaseStation.Lat);
            p.Add("@StationType", etBaseStation.StationType);
            p.Add("@MastSave", etBaseStation.MastSave);
            p.Add("@CoverType", etBaseStation.CoverType);
            p.Add("@PlanLevel", etBaseStation.PlanLevel);
            p.Add("@StationID", 0, DbType.Int32, ParameterDirection.Output);

            using (var cnn = new SqlConnection(SQLDBHelper.ConnectionString))
            {
                cnn.Open();
                cnn.Execute("[dbo].[usp_InsertBaseStation]", p, null, null, CommandType.StoredProcedure);
                result = p.Get<int>("@StationID");
                cnn.Close();
            }
            return result;
        }
        #endregion
        #region 删除指定主键值的记录
        /// <summary>
        /// 删除指定主键值的记录,并返回受影响行数
        /// </summary>
        public static int DeleteByStationID(int stationID)
        {
            int result = 0;
            DynamicParameters p = new DynamicParameters();
            p.Add("@StationID", stationID);
            p.Add("@Rows", 0, DbType.Int32, ParameterDirection.Output);

            using (var cnn = new SqlConnection(SQLDBHelper.ConnectionString))
            {
                cnn.Open();
                cnn.Execute("[dbo].[usp_DeleteBaseStation]", p, null, null, CommandType.StoredProcedure);
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

        public static Int32 Modify(EtBaseStation etBaseStation)
        {
            Int32 result = 0;
            DynamicParameters p = new DynamicParameters();
            p.Add("@StationGUID", etBaseStation.StationGUID);
            p.Add("@TaskID", etBaseStation.TaskID);
            p.Add("@StationAlias", etBaseStation.StationAlias);
            p.Add("@Lng", etBaseStation.Lng);
            p.Add("@Lat", etBaseStation.Lat);
            p.Add("@StationType", etBaseStation.StationType);
            p.Add("@MastSave", etBaseStation.MastSave);
            p.Add("@CoverType", etBaseStation.CoverType);
            p.Add("@PlanLevel", etBaseStation.PlanLevel);
            p.Add("@StationID", etBaseStation.StationID);
            p.Add("@Rows", 0, DbType.Int32, ParameterDirection.Output);
            using (var cnn = new SqlConnection(SQLDBHelper.ConnectionString))
            {
                cnn.Open();
                cnn.Execute("[dbo].[usp_UpdateBaseStation]", p, null, null, CommandType.StoredProcedure);
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
        public static IList<int> GetAllStationIDs()
        {
            IList<int> Relse = null;
            using (var cnn = new SqlConnection(SQLDBHelper.ConnectionString))
            {
                string sql = "SELECT StationID FROM [BaseStation]";
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
        public static IList<EtBaseStation> GetAllBaseStations()
        {
            IList<EtBaseStation> Relse = null;
            using (var cnn = new SqlConnection(SQLDBHelper.ConnectionString))
            {
                string sql = "SELECT * FROM [BaseStation]";
                cnn.Open();
                Relse = cnn.Query<EtBaseStation>(sql, null).ToList();
                cnn.Close();

            }
            return Relse;
        }
        #endregion

        #region 返回条件查询所有数据
        /// <summary>
        /// 返回条件查询所有数据
        /// </summary>
        public static IList<EtBaseStation> GetAllBaseStationsWithDynamicCondition(string where)
        {
            IList<EtBaseStation> Relse = null;
            using (var cnn = new SqlConnection(SQLDBHelper.ConnectionString))
            {
                string sql = "SELECT * FROM [BaseStation] where " + where;
                cnn.Open();
                Relse = cnn.Query<EtBaseStation>(sql, null).ToList();
                cnn.Close();
            }
            return Relse;
        }
        #endregion

        #region 取得表的行数
        /// <summary>
        /// 取得表的行数
        /// </summary>
        public static int GetRowCountOfAllBaseStations()
        {
            return SQLDBHelper.GetRowCount("BaseStation", "");
        }
        #endregion
        #region 取得给定主键值集对应的记录集
        /// <summary>
        /// 取得给定主键值集对应的记录集
        /// </summary>
        public static IList<EtBaseStation> GetBaseStationsByBaseStationIDs(IList<int> listIDs)
        {

            IList<EtBaseStation> Relse = new List<EtBaseStation>();
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
                Relse = GetAllBaseStationsWithDynamicCondition(sql);
            }
            catch
            {
            }
            return Relse;
        }
        #endregion
        #region 清空表
        public static void ClearBaseStation()
        {
            string proName = "[dbo].[usp_ClearBaseStation]";
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
        public static IList<EtBaseStation> GetPageBaseStations(string DataTbleName, string ReturnFields, string SqlWhere, int pageIndex, string Sort, int pageSize, out Int32 AllRecords)
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
            SqlParam[0].Value = "BaseStation";
            SqlParam[1].Value = "StationID";
            SqlParam[2].Value = ReturnFields;
            SqlParam[3].Value = SqlWhere;
            SqlParam[4].Value = pageIndex;
            SqlParam[5].Value = pageSize;
            SqlParam[6].Value = Sort;
            DataSet ds = SQLDBHelper.RunProceGetDataSet("usp_GetPageData", SqlParam, DataTbleName);
            if (ds != null && ds.Tables.Count > 0)
            {
                AllRecords = Convert.ToInt32(ds.Tables[1].Rows[0]["RecordCount"]);
                return GetBaseStationsByDataTable(ds.Tables[DataTbleName]);
            }
            AllRecords = 0;
            return null;

        }
        #endregion
        #region ----
        public static void BulkToDB(IList<EtBaseStation> Datas)
        {
            DataTable dt = null;
            #region 创建表
            dt = new DataTable(TableName);
            dt.Columns.Add(new DataColumn("StationGUID", typeof(string)));
            dt.Columns.Add(new DataColumn("TaskID", typeof(int)));
            dt.Columns.Add(new DataColumn("StationAlias", typeof(string)));
            dt.Columns.Add(new DataColumn("Lng", typeof(double)));
            dt.Columns.Add(new DataColumn("Lat", typeof(double)));
            dt.Columns.Add(new DataColumn("StationType", typeof(byte)));
            dt.Columns.Add(new DataColumn("MastSave", typeof(bool)));
            dt.Columns.Add(new DataColumn("CoverType", typeof(byte)));
            dt.Columns.Add(new DataColumn("PlanLevel", typeof(byte)));
            #endregion

            #region 插入数据

            foreach (var data in Datas)
            {
                DataRow r = dt.NewRow();
                r["StationGUID"] = data.StationGUID.ToString();
                r["TaskID"] = data.TaskID;

                r["StationAlias"] = data.StationAlias.ToString();
                r["Lng"] = data.Lng;

                r["Lat"] = data.Lat;

                r["StationType"] = data.StationType;

                r["MastSave"] = data.MastSave;

                r["CoverType"] = data.CoverType;

                r["PlanLevel"] = data.PlanLevel;

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
        private static IList<EtBaseStation> GetBaseStationsByDataTable(DataTable table)
        {
            IList<EtBaseStation> listEtBaseStations = new List<EtBaseStation>();
            foreach (DataRow row in table.Rows)
            {
                listEtBaseStations.Add(new EtBaseStation(row));
            }
            return listEtBaseStations;
        }
        #endregion
        public static string TableName = "BaseStation";
        #region 列名称
        public static string Field_StationID = "StationID";

        public static string Field_StationGUID = "StationGUID";

        public static string Field_TaskID = "TaskID";

        public static string Field_StationAlias = "StationAlias";

        public static string Field_Lng = "Lng";

        public static string Field_Lat = "Lat";

        public static string Field_StationType = "StationType";

        public static string Field_MastSave = "MastSave";
        public static string Field_CoverType = "CoverType";
        public static string Field_PlanLevel = "PlanLevel";
        #endregion


    }
    #endregion
}