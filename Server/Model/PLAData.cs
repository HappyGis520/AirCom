using System;
using System.Collections;
using System.Collections.Generic;

namespace NetPlan.Model
{
    [Serializable]
    public class PLAData
    {
        /// <summary>
        /// 工单号
        /// </summary>
        public long WorkOrder = 0;
        /// <summary>
        /// 基站信息
        /// </summary>
        public AirComLTENodeBaseInfo BaseInfo;
        /// <summary>
        /// 扇区信息
        /// </summary>
        public List<CellSector> CellSectors = new List<CellSector>();

        /// <summary>
        /// 保存目录
        /// </summary>
        internal string Savedir = "C:\\";
        /// <summary>
        /// 工程名称
        /// </summary>
        public string ProjectName ="工程名";
        /// <summary>
        /// 仿真范围
        /// </summary>
        internal GeoRegion RegionBound = new GeoRegion();
        /// <summary>
        /// 仿真半径，单位KM
        /// </summary>
        public double CoverRadius=1.00;


    }
}