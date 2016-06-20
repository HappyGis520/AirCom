/*******************************************************************
 * * 版权所有： 王建军
 * * 文件名称： DALAntennalnfo.cs
 * * 功   能： 数据库dbo.Antennalnfo表访问类,采用Dapper方法
 * * 作   者： 王建军
 * * 电子邮箱： 595303122@qq.com
 * * 创建日期： 2016/02/23 09:47:11
 * * 修改记录： 
 * * 日期时间： 2016/02/23 09:47:11  修改人：王建军  创建
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
    #region DALAntennalnfo
    public static class DALAntennalnfo
    {
        #region 添加一个记录/对象
        /// <summary>
        /// 添加一个记录/对象,并返回新记录的主键值
        /// </summary>
        /// <param name="entityPointInfo"></param>
        /// <returns></returns>
        public static Int32 Add(EtAntennalnfo etAntennalnfo)
        {
            int result = -1;
            DynamicParameters p = new DynamicParameters();
            p.Add("@CELLID", etAntennalnfo.CELLID);
            p.Add("@AntennaType", etAntennalnfo.AntennaType);
            p.Add("@Lng", etAntennalnfo.Lng);
            p.Add("@Lat", etAntennalnfo.Lat);
            p.Add("@Direction", etAntennalnfo.Direction);
            p.Add("@DownTilt", etAntennalnfo.DownTilt);
            p.Add("@Height", etAntennalnfo.Height);
            p.Add("@CarrierID", etAntennalnfo.CarrierID);
            p.Add("@RsPower", etAntennalnfo.RsPower);
            p.Add("@Burthen", etAntennalnfo.Burthen);
            p.Add("@ModelType", etAntennalnfo.ModelType);
            p.Add("@RadiusKm", etAntennalnfo.RadiusKm);
            p.Add("@ResolutionMetres", etAntennalnfo.ResolutionMetres);
            p.Add("@AntennaID", 0, DbType.Int32, ParameterDirection.Output);

            using (var cnn = new SqlConnection(SQLDBHelper.ConnectionString))
            {
                cnn.Open();
                cnn.Execute("[dbo].[usp_InsertAntennalnfo]", p, null, null, CommandType.StoredProcedure);
                result = p.Get<int>("@AntennaID");
                cnn.Close();
            }
            return result;
        }
        #endregion
        #region 删除指定主键值的记录
        /// <summary>
        /// 删除指定主键值的记录,并返回受影响行数
        /// </summary>
        public static int DeleteByAntennaID(int antennaID)
        {
            int result = 0;
            DynamicParameters p = new DynamicParameters();
            p.Add("@AntennaID", antennaID);
            p.Add("@Rows", 0, DbType.Int32, ParameterDirection.Output);

            using (var cnn = new SqlConnection(SQLDBHelper.ConnectionString))
            {
                cnn.Open();
                cnn.Execute("[dbo].[usp_DeleteAntennalnfo]", p, null, null, CommandType.StoredProcedure);
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

        public static Int32 Modify(EtAntennalnfo etAntennalnfo)
        {
            Int32 result = 0;
            DynamicParameters p = new DynamicParameters();
            p.Add("@CELLID", etAntennalnfo.CELLID);
            p.Add("@AntennaType", etAntennalnfo.AntennaType);
            p.Add("@Lng", etAntennalnfo.Lng);
            p.Add("@Lat", etAntennalnfo.Lat);
            p.Add("@Direction", etAntennalnfo.Direction);
            p.Add("@DownTilt", etAntennalnfo.DownTilt);
            p.Add("@Height", etAntennalnfo.Height);
            p.Add("@CarrierID", etAntennalnfo.CarrierID);
            p.Add("@RsPower", etAntennalnfo.RsPower);
            p.Add("@Burthen", etAntennalnfo.Burthen);
            p.Add("@ModelType", etAntennalnfo.ModelType);
            p.Add("@RadiusKm", etAntennalnfo.RadiusKm);
            p.Add("@ResolutionMetres", etAntennalnfo.ResolutionMetres);
            p.Add("@AntennaID", etAntennalnfo.AntennaID);
            p.Add("@Rows", 0, DbType.Int32, ParameterDirection.Output);
            using (var cnn = new SqlConnection(SQLDBHelper.ConnectionString))
            {
                cnn.Open();
                cnn.Execute("[dbo].[usp_UpdateAntennalnfo]", p, null, null, CommandType.StoredProcedure);
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
        public static IList<int> GetAllAntennaIDs()
        {
            IList<int> Relse = null;
            using (var cnn = new SqlConnection(SQLDBHelper.ConnectionString))
            {
                string sql = "SELECT AntennaID FROM [Antennalnfo]";
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
        public static IList<EtAntennalnfo> GetAllAntennalnfos()
        {
            IList<EtAntennalnfo> Relse = null;
            using (var cnn = new SqlConnection(SQLDBHelper.ConnectionString))
            {
                string sql = "SELECT * FROM [Antennalnfo]";
                cnn.Open();
                Relse = cnn.Query<EtAntennalnfo>(sql, null).ToList();
                cnn.Close();

            }
            return Relse;
        }
        #endregion

        #region 返回条件查询所有数据
        /// <summary>
        /// 返回条件查询所有数据
        /// </summary>
        public static IList<EtAntennalnfo> GetAllAntennalnfosWithDynamicCondition(string where)
        {
            IList<EtAntennalnfo> Relse = null;
            using (var cnn = new SqlConnection(SQLDBHelper.ConnectionString))
            {
                string sql = "SELECT * FROM [Antennalnfo] where " + where;
                cnn.Open();
                Relse = cnn.Query<EtAntennalnfo>(sql, null).ToList();
                cnn.Close();
            }
            return Relse;
        }
        #endregion

        #region 取得表的行数
        /// <summary>
        /// 取得表的行数
        /// </summary>
        public static int GetRowCountOfAllAntennalnfos()
        {
            return SQLDBHelper.GetRowCount("Antennalnfo", "");
        }
        #endregion
        #region 取得给定主键值集对应的记录集
        /// <summary>
        /// 取得给定主键值集对应的记录集
        /// </summary>
        public static IList<EtAntennalnfo> GetAntennalnfosByAntennalnfoIDs(IList<int> listIDs)
        {

            IList<EtAntennalnfo> Relse = new List<EtAntennalnfo>();
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
                Relse = GetAllAntennalnfosWithDynamicCondition(sql);
            }
            catch
            {
            }
            return Relse;
        }
        #endregion
        #region 清空表
        public static void ClearAntennalnfo()
        {
            string proName = "[dbo].[usp_ClearAntennalnfo]";
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
        public static IList<EtAntennalnfo> GetPageAntennalnfos(string DataTbleName, string ReturnFields, string SqlWhere, int pageIndex, string Sort, int pageSize, out Int32 AllRecords)
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
            SqlParam[0].Value = "Antennalnfo";
            SqlParam[1].Value = "AntennaID";
            SqlParam[2].Value = ReturnFields;
            SqlParam[3].Value = SqlWhere;
            SqlParam[4].Value = pageIndex;
            SqlParam[5].Value = pageSize;
            SqlParam[6].Value = Sort;
            DataSet ds = SQLDBHelper.RunProceGetDataSet("usp_GetPageData", SqlParam, DataTbleName);
            if (ds != null && ds.Tables.Count > 0)
            {
                AllRecords = Convert.ToInt32(ds.Tables[1].Rows[0]["RecordCount"]);
                return GetAntennalnfosByDataTable(ds.Tables[DataTbleName]);
            }
            AllRecords = 0;
            return null;

        }
        #endregion
        #region ----
        public static void BulkToDB(IList<EtAntennalnfo> Datas)
        {
            DataTable dt = null;
            #region 创建表
            dt = new DataTable(TableName);
            dt.Columns.Add(new DataColumn("CELLID", typeof(int)));
            dt.Columns.Add(new DataColumn("AntennaType", typeof(byte)));
            dt.Columns.Add(new DataColumn("Lng", typeof(double)));
            dt.Columns.Add(new DataColumn("Lat", typeof(double)));
            dt.Columns.Add(new DataColumn("Direction", typeof(int)));
            dt.Columns.Add(new DataColumn("DownTilt", typeof(double)));
            dt.Columns.Add(new DataColumn("Height", typeof(double)));
            dt.Columns.Add(new DataColumn("CarrierID", typeof(byte)));
            dt.Columns.Add(new DataColumn("RsPower", typeof(double)));
            dt.Columns.Add(new DataColumn("Burthen", typeof(double)));
            dt.Columns.Add(new DataColumn("ModelType", typeof(byte)));
            dt.Columns.Add(new DataColumn("RadiusKm", typeof(double)));
            dt.Columns.Add(new DataColumn("ResolutionMetres", typeof(double)));
            #endregion

            #region 插入数据

            foreach (var data in Datas)
            {
                DataRow r = dt.NewRow();
                r["CELLID"] = data.CELLID;

                r["AntennaType"] = data.AntennaType;

                r["Lng"] = data.Lng;

                r["Lat"] = data.Lat;

                r["Direction"] = data.Direction;

                r["DownTilt"] = data.DownTilt;

                r["Height"] = data.Height;

                r["CarrierID"] = data.CarrierID;

                r["RsPower"] = data.RsPower;

                r["Burthen"] = data.Burthen;

                r["ModelType"] = data.ModelType;

                r["RadiusKm"] = data.RadiusKm;

                r["ResolutionMetres"] = data.ResolutionMetres;

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
        private static IList<EtAntennalnfo> GetAntennalnfosByDataTable(DataTable table)
        {
            IList<EtAntennalnfo> listEtAntennalnfos = new List<EtAntennalnfo>();
            foreach (DataRow row in table.Rows)
            {
                listEtAntennalnfos.Add(new EtAntennalnfo(row));
            }
            return listEtAntennalnfos;
        }
        #endregion
        public static string TableName = "Antennalnfo";
        #region 列名称
        public static string Field_AntennaID = "AntennaID";

        public static string Field_CELLID = "CELLID";

        public static string Field_AntennaType = "AntennaType";

        public static string Field_Lng = "Lng";

        public static string Field_Lat = "Lat";

        public static string Field_Direction = "Direction";

        public static string Field_DownTilt = "DownTilt";

        public static string Field_Height = "Height";

        public static string Field_CarrierID = "CarrierID";

        public static string Field_RsPower = "RsPower";

        public static string Field_Burthen = "Burthen";

        public static string Field_ModelType = "ModelType";
        public static string Field_RadiusKm = "RadiusKm";
        public static string Field_ResolutionMetres = "ResolutionMetres";
        #endregion


    }
    #endregion
}