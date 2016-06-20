using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar.Controls;
using JLIB.Utility;
using NetPlan.Model;

//using NetPlanClient.SvrREF.AirComSVR;

namespace NetPlanClient
{
    public partial class FrmSector : DevComponents.DotNetBar.Office2007Form
    {
        protected  List<AirComAntennaType>  _Antennas = new List<AirComAntennaType>();
        protected  BindingSource  _BindingSource = new BindingSource();

        public FrmSector()
        {
            InitializeComponent();

            
            InitionParam();

        }


        public void LoadSectorInfo(int SectorID, List<AirComAntennaType> AntennaTypes)
        {
            if (this.InvokeRequired)
            {
                object[] Params = new object[] {SectorID,AntennaTypes};
                this.Invoke(new Action<int,List<AirComAntennaType>>(this.LoadSectorInfo), Params);
                return;

            }
            try
            {
                txtSectorID.Enabled = false;
                txtSectorID.Text = SectorID.ToString();
                AntennaTypes.ForEach(fo =>
                {
                    _BindingSource.Add(fo);
                });
            }
            catch (Exception ex)
            {
                JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                    MethodBase.GetCurrentMethod().Module.Name);
            }
        }

        #region 私有方法
        

        protected void  InitionParam()
        {
            dataGridViewX1.AutoGenerateColumns = false;
            _BindingSource.DataSource = _Antennas;
            dataGridViewX1.DataSource = _BindingSource; 
            _BindingSource.Clear();
        }

        #endregion

        #region 窗体事件


        private void FrmSector_Load(object sender, EventArgs e)
        {

        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            try
            {

                var obj = ucLTEAntennaType1.BuildAntennaOjbect();
                if (obj != null)
                {
                    _BindingSource.Add(obj);
                }
            }
            catch (Exception ex)
            {
                JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                    MethodBase.GetCurrentMethod().Module.Name);
            }

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                string SectorID = txtSectorID.Text.Trim();
                int CellID = 0;
                if (int.TryParse(SectorID, out CellID))
                {


                    if (string.IsNullOrEmpty(SectorID))
                    {
                        return;
                    }
                    if (_BindingSource.Count < 1)
                    {
                        //return;
                    }
                    for (int index = 0; index < _Antennas.Count; index++)
                    {
                        var obj = _Antennas[index];
                        obj.SectorId = CellID;
                    }
                    RaiseAppendSectorEvent(CellID, _Antennas);
                    Close();
                }
                else
                {
                    MessageBox.Show("扇区编号输入有误");
                }
            }
            catch (Exception ex)
            {
                JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                    MethodBase.GetCurrentMethod().Module.Name);
            }

            
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            //JLIB.CSharp.JFileExten.ToXML(GlobalInfo.Instance.ConfigParam, "D:\\AppConfig.xml");

        }



        private void dataGridViewX1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {

                if (_BindingSource == null || _BindingSource.Count < 1)
                    return;
                if (e.RowIndex >= 0 && _BindingSource.Count > e.RowIndex)
                {
                    AirComAntennaType obj = _BindingSource[e.RowIndex] as AirComAntennaType;
                    ucLTEAntennaType1.LoadAntennaInfo(obj   );
                }
            }
            catch (Exception ex)
            {
                JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                    MethodBase.GetCurrentMethod().Module.Name);
            }
        }

        private void dataGridViewX1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (_BindingSource == null || _BindingSource.Count < 1)
                    return;

                if (dataGridViewX1.Columns[e.ColumnIndex].Name.Equals("colDelete"))
                {
                    DataGridViewButtonXCell cell = dataGridViewX1.CurrentCell as DataGridViewButtonXCell;
                    if (cell != null)
                    {
                        if (e.RowIndex >= 0 && _BindingSource.Count > e.RowIndex)
                        {
                            AirComAntennaType obj = _BindingSource[e.RowIndex] as AirComAntennaType;
                            _BindingSource.Remove(obj);

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                    MethodBase.GetCurrentMethod().Module.Name);
            }
        }


        #endregion

        #region 自定义事件


        #region AppendSectorEvent

        protected  Action<int,IList<AirComAntennaType>> AppendSectorEvent;

        protected void RaiseAppendSectorEvent(int SectorID, IList<AirComAntennaType> AntennaTypes)
        {
            if (AppendSectorEvent != null)
            {
                AppendSectorEvent.BeginInvoke( SectorID, AntennaTypes,null, null);
            }

        }

        public void RegistAppendSectorEvent(Action<int,IList<AirComAntennaType>> handle)
        {
            //DeRegistAppendSectorEvent(handle);
            AppendSectorEvent = handle;


        }

        public void DeRegistAppendSectorEvent(Action<int,IList<AirComAntennaType>> handle)
        {
            AppendSectorEvent = null;

        }

        #endregion

        private void txtSectorID_KeyPress(object sender, KeyPressEventArgs e)
        {

            try
            {
               int kc = (int)e.KeyChar;  
               if ((kc < 48 || kc > 57) && kc != 8)  
                   e.Handled = true;  
            }
            catch (Exception)
            {
                
                throw;
            }

        }





        #endregion


    }
}
