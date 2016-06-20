using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using JLIB.CSharp;
using JLIB.Utility;
using NetPlan.Model;
using NetPlanClient.Model;
//using NetPlanClient.SvrREF.AirComSVR;


namespace NetPlanClient.UC
{
    public partial class ucLTEStationType : UserControl
    {

        public ucLTEStationType()
        {
            InitializeComponent();
            //IntionParam();
            RegistEventHandle();
        }
        

        private void RegistEventHandle()
        {
            btnAddSector.Click += SubDoCreateNewStation_Click;
        }

        #region 私有方法

        private void IntionParam()
        {

            
            cmbStationType.Items.Clear();
            cmbCoverType.Items.Clear();
            cmbSaveType.Items.Clear();
          
            cmbStationType.Items.AddRange(Enum.GetNames(typeof(EnumStationType)));
            cmbCoverType.Items.AddRange(Enum.GetNames(typeof(EnumCoverType)));
            cmbSaveType.Items.AddRange(Enum.GetNames(typeof(EnumSaveType)));
        }
        
        #endregion

        #region 公有方法

        /// <summary>
        /// 根据界面参数生成基站基础信息
        /// </summary>
        /// <returns></returns>
        public AirComLTENodeBaseInfo BuildBasicInfo()
        {
            try
            {
                AirComLTENodeBaseInfo Info = new AirComLTENodeBaseInfo();
                Info.StationId = txtStationID.Text.Trim();
                Info.StationAlias = txtAlias.Text.Trim();
                Info.Lng = double.Parse(txtLng.Text.Trim());
                Info.Lat = double.Parse(txtLat.Text.Trim());
                Info.CoverType = (EnumCoverType)cmbCoverType.SelectedIndex;
                Info.StationType = (EnumStationType)cmbStationType.SelectedIndex;
                Info.SaveType = (EnumSaveType)cmbSaveType.SelectedIndex;
                return Info;
            }
            catch (Exception ex)
            {
                JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                    MethodBase.GetCurrentMethod().Module.Name);
                return null;
            }


        }

        //public AirComLTENodeBaseInfo BuildBasicInfo(EtBaseStation Station)
        //{
        //    try
        //    {
        //        AirComLTENodeBaseInfo Info = new AirComLTENodeBaseInfo();
        //        Info.StationId = Station.StationGUID;// txtStationID.Text.Trim();
        //        Info.StationAlias = Station.StationAlias;// txtAlias.Text.Trim();
        //        Info.Lng = Station.Lng;// double.Parse(txtLng.Text.Trim());
        //        Info.Lat = Station.Lat;// double.Parse(txtLat.Text.Trim());
        //        Info.CoverType = (EnumCoverType)Station.CoverType;//cmbCoverType.SelectedIndex;
        //        Info.StationType = (EnumStationType)Station.StationType;
        //        Info.SaveType =  Station.MastSave ? EnumSaveType.Save : EnumSaveType.Delete;
        //        return Info;
        //    }
        //    catch (Exception ex)
        //    {
        //        JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
        //            MethodBase.GetCurrentMethod().Module.Name);
        //        return null;
        //    }

        //}
        #endregion

        #region CreateNewStation_ClickEvent

        protected event EventHandler CreateNewStation_ClickEvent;

        protected void SubDoCreateNewStation_Click(object sender, EventArgs e)
        {
            RaiseCreateNewStation_ClickEvent( sender,  e);
        }

        protected void RaiseCreateNewStation_ClickEvent(object sender, EventArgs e)
        {
            if (CreateNewStation_ClickEvent != null)
            {
                CreateNewStation_ClickEvent.BeginInvoke( sender,  e,null, null);
            }

        }

        public void RegistCreateNewStation_ClickEvent(EventHandler handle)
        {
            CreateNewStation_ClickEvent += handle;


        }

        public void DeRegistCreateNewStation_ClickEvent(EventHandler handle)
        {
            CreateNewStation_ClickEvent -= handle;

        }

        #endregion



    }
}
