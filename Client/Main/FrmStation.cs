using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar.Controls;
using EDSProxy.EDSClient;
using JLIB.Utility;
using NetPlan.BLL;
using NetPlan.Model;
using NetPlanClient;
//using NetPlanClient.SvrREF.AirComSVR;


namespace NetPlanClient
{
    public partial class FrmStation : DevComponents.DotNetBar.Office2007Form
    {
        /// <summary>
        /// 天线列表
        /// </summary>
        private Dictionary<int, List<AirComAntennaType>> Sectors = new Dictionary<int, List<AirComAntennaType>>();
        /// <summary>
        /// 天线列表
        /// </summary>
        private List<AirComAntennaType> _AllAntennas = new List<AirComAntennaType>();
        private BindingSource _bindingSource = new BindingSource();
        /// <summary>
        /// 地市名称，服务端根据该名称获取工程名称
        /// </summary>
        private string CityName = string.Empty;


        public FrmStation()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            InitionParam();
            RegistEventHandle();

        }
        #region 私有方法
        /// <summary>
        /// 初始化参数
        /// </summary>
        private void InitionParam()
        {


            _bindingSource.DataSource = _AllAntennas;
            dataGridViewX1.AutoGenerateColumns = false;
            dataGridViewX1.DataSource = _bindingSource;
        }


        private void RegistEventHandle()
        {
            try
            {
                ucLTEStationType1.RegistCreateNewStation_ClickEvent(SubDoCreateNewSector);

            }
            catch (Exception ex)
            {
                JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                    MethodBase.GetCurrentMethod().Module.Name);
            }

        }



        private void SubDoAppenSectorEvent(int SectorID, IList<AirComAntennaType> AntennaTypes)
        {
            if (this.InvokeRequired)
            {
                object[] Params = new object[] { SectorID, AntennaTypes };
                this.Invoke(new Action<int, IList<AirComAntennaType>>(this.SubDoAppenSectorEvent), Params);
                return;

            }
            try
            {
                AppendSectorInfo(SectorID, AntennaTypes);
            }
            catch (Exception ex)
            {
                JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                    MethodBase.GetCurrentMethod().Module.Name);
            }
        }

        private void AppendSectorInfo(int SectorID, IList<AirComAntennaType> AntennaTypes)
        {
            try
            {
                if (Sectors.ContainsKey(SectorID))
                {
                    Sectors.Remove(SectorID);
                    var obj = _AllAntennas.FindAll(fo => fo.SectorId == SectorID);
                    if (obj.Count > 0)
                    {
                        obj.ForEach(Fo =>
                        {
                            _bindingSource.Remove(Fo);
                        });
                    }
                }
                foreach (var obj in AntennaTypes)
                {
                    _bindingSource.Add(obj);
                }
                Sectors.Add(SectorID, AntennaTypes.ToList());
            }
            catch (Exception ex)
            {
                JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                    MethodBase.GetCurrentMethod().Module.Name);
            }




        } 

        protected void EditSectors(int SectorID, List<AirComAntennaType> MyAntennaTypes)
        {
            FrmSector _frmSector = new FrmSector();
            _frmSector.LoadSectorInfo(SectorID,MyAntennaTypes);
            _frmSector.RegistAppendSectorEvent(SubDoAppenSectorEvent);
            _frmSector.ShowDialog();
           
        }
        #endregion

        #region 窗体事件处理　

        private void btnBuildXML_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    LTENodeType _LteNode = new LTENodeType();
            //    var BaseInfo = ucLTEStationType1.BuildBasicInfo();
            //    LteNodeBuildFactory.BuildStationInfo(BaseInfo, ref _LteNode);//

            //    LteNodeBuildFactory.BuildCarrierInfo(Sectors, ref _LteNode);
            //    LteNodeBuildFactory.BuildAntennaInfo(_AllAntennas, ref _LteNode);
            //    LteNodeBuildFactory.BuildCellInfo(Sectors, ref _LteNode);

            //    #region 生成XML
            //    List<RootEntityType> Nodes = new List<RootEntityType>();
            //    List<LTENodeType> nodesList = new List<LTENodeType>();
            //    Nodes.Add(_LteNode);
            //    nodesList.Add(_LteNode);
            //    string dir = GlobalInfo.Instance.ConfigParam.XmlFileSaveDir.Trim();
            //    if (!Directory.Exists(dir))
            //    {
            //       DirectoryInfo dirInfo = Directory.CreateDirectory(dir);

            //    }
            //    LteNodeBuildFactory.BuildLteNodeXML(Nodes, dir);
            //    LteNodeBuildFactory.BuilLteNodeDeleteXML(nodesList, dir);
            //    LteNodeBuildFactory.BuildLocationXML(BaseInfo,dir);
            //    LteNodeBuildFactory.BuildLocationDeleteXML(BaseInfo, dir); 
            //    #endregion


            //}
            //catch (Exception ex)
            //{
            //    JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
            //        MethodBase.GetCurrentMethod().Module.Name);
            //}



        }


        private void dataGridViewX1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {

                if (_bindingSource == null || _bindingSource.Count < 1)
                    return;
                if (e.RowIndex >= 0 && _bindingSource.Count > e.RowIndex)
                {
                    AirComAntennaType obj = _bindingSource[e.RowIndex] as AirComAntennaType;
                    //string SectorID = obj.SectorId;
                    List<AirComAntennaType> SelectAntenna = null;
                    if (Sectors.TryGetValue(obj.SectorId, out SelectAntenna))
                    {
                        EditSectors(obj.SectorId, SelectAntenna);
                    }

                }
            }
            catch (Exception ex)
            {
                JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                    MethodBase.GetCurrentMethod().Module.Name);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {



            //if (FrmLogin.GetIntance().ShowDialog() != DialogResult.OK) //登录失败
            //{
            //    //SubDoLoginAck(false, "");
            //}
            //else
            //{
            //    //SubDoLoginAck(true, "");
            //}

            //UserConfigParam pp = new UserConfigParam();
            //pp.EDSLoadAppFile = @"c:\aa\tt.exe";
            //JLIB.CSharp.JFileExten.ToXML(pp, @".\test.xml");
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            //BLLMain.Instance.DoPlanTask();
            //string XMLSaveDir = GlobalInfo.Instance.ConfigParam.XmlFileSaveDir.Trim();

        }
        #endregion

        #region 自定义事件处理

        private void SubDoCreateNewSector(object Sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                object[] Params = new object[] { Sender, e };
                this.Invoke(new Action<object, EventArgs>(this.SubDoCreateNewSector), Params);
                return;

            }

            FrmSector _frmSector = new FrmSector();
            _frmSector.RegistAppendSectorEvent(SubDoAppenSectorEvent);
            _frmSector.ShowDialog();

        }
        
        #endregion





    }
}
