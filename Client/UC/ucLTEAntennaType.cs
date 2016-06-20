using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

using JLIB.CSharp;
using JLIB.Utility;



using System.Windows.Forms;
using DevComponents.DotNetBar;
using NetPlan.Model;
using NetPlanClient.Model;
//using NetPlanClient.SvrREF.AirComSVR;


namespace NetPlanClient.UC
{
    public partial class ucLTEAntennaType : UserControl
    {
        private AirComAntennaType _Anten = null;

        public ucLTEAntennaType()
        {
            InitializeComponent();
            IntionParam();
        }

        public void IntionParam()
        {
            try
            {
                cmbAntenType.Items.Clear();
                cmbCarrierType.Items.Clear();
                cmbModelType.Items.Clear();
                ComboBoxItem item = new ComboBoxItem();
               
                
                cmbAntenType.Items.AddRange(ClientGlobalInfo.Instance.ConfigParam.AntennaTypes.ToArray());
                foreach (var carrierItem in ClientGlobalInfo.Instance.ConfigParam.CarrierItems)
                {
                    cmbCarrierType.Items.Add(new ComboBoxItem() { Tag = carrierItem.ID, Text = carrierItem.Alias });
                }
                cmbModelType.Items.AddRange(ClientGlobalInfo.Instance.ConfigParam.ModelTypes.ToArray())
;
                _Anten = null;
            }
            catch (Exception ex)
            {
                JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                    MethodBase.GetCurrentMethod().Module.Name);
            }


        }


        public void LoadAntennaInfo(AirComAntennaType Antennas )
        {
            try
            {
               
                
                //型号
                cmbAntenType.Text = Antennas.AntennaTypeName;
             
                txtLat.Text = Antennas.Location.Latitude.ToString();
                txtLng.Text = Antennas.Location.Latitude.ToString();

                txtAngle.Text = Antennas.Azimuth.ToString();
                txtTile.Text = Antennas.MechanicalDownTilt.ToString();

                txtHeight.Text = Antennas.Height.ToString()    ;
                //载波
                cmbCarrierType.Text = Antennas.CarrierAlias;

                cmbModelType.Text = Antennas.ModelType;

                txtRS.Text = Antennas.Power.ToString();

                txtfh.Text = Antennas.Burthen.ToString();


            }
            catch (Exception ex)
            {
                JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                    MethodBase.GetCurrentMethod().Module.Name);
            }
            
        }

        public AirComAntennaType BuildAntennaOjbect()
        {
            try
            {
                AirComAntennaType Obj = new AirComAntennaType();
                if (ValidateParam())
                {
                    Obj.AntennaTypeName = cmbAntenType.Text;
                    Obj.Location = new AirComLocationType();
                    Obj.Location.Latitude = double.Parse(txtLat.Text);
                    Obj.Location.LongitudeField = double.Parse(txtLng.Text);
                    Obj.Azimuth = int.Parse(txtAngle.Text);
                    Obj.MechanicalDownTilt = double.Parse(txtTile.Text );
                    Obj.Height = int.Parse(txtHeight.Text);

                    ComboBoxItem Item = cmbCarrierType.SelectedItem as ComboBoxItem;
                    Obj.CarrierAlias = Item.Text;
                    Obj.CarrierId = (int)Item.Tag;

                    Obj.Power = double.Parse(txtRS.Text);
                    Obj.Burthen = double.Parse(txtfh.Text);
                    Obj.ModelType = cmbModelType.Text;

                    Obj.ResolutionMetres =Double.Parse( txtResoluMeters.Text);
                    Obj.RadiusKm =Double.Parse( txtRS.Text);
                    return Obj;
                }
                return null;

            }
            catch (Exception ex)
            {
                JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                    MethodBase.GetCurrentMethod().Module.Name);
                return null;
            }

            
        }


        private bool ValidateParam()
        {
            if (string.IsNullOrEmpty(cmbAntenType.Text.Trim()))
            {
                return false;
            }
            if (string.IsNullOrEmpty(txtLng.Text.Trim()))
            {
                return false;
            }
            if (string.IsNullOrEmpty(txtLat.Text.Trim()))
            {
                return false;
            }
            if (string.IsNullOrEmpty(txtAngle.Text.Trim()))
            {
                return false;
            }
            if (string.IsNullOrEmpty(txtTile.Text.Trim()))
            {
                return false;
            }
            if (string.IsNullOrEmpty(txtHeight.Text.Trim()))
            {
                return false;
            }
            if (string.IsNullOrEmpty(cmbCarrierType.Text.Trim()))
            {
                return false;
            }
            if (string.IsNullOrEmpty(txtRS.Text.Trim()))
            {
                return false;
            }

            if (string.IsNullOrEmpty(txtfh.Text.Trim()))
            {
                return false;
            }
            return true;


        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
