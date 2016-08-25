using NetPlanClient.UC;

namespace NetPlanClient
{
    partial class FrmSector
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSector));
            this.dataGridViewX1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colAntennaType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLng = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAngle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCarrier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPower = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colfh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colModelType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDelete = new DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn();
            this.btnOk = new DevComponents.DotNetBar.ButtonX();
            this.编号 = new DevComponents.DotNetBar.LabelX();
            this.txtSectorID = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ucLTEAntennaType1 = new NetPlanClient.UC.ucLTEAntennaType();
            this.btnAppend = new DevComponents.DotNetBar.ButtonX();
            this.dataGridViewButtonXColumn1 = new DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewX1
            // 
            this.dataGridViewX1.AllowUserToAddRows = false;
            this.dataGridViewX1.AllowUserToDeleteRows = false;
            this.dataGridViewX1.AllowUserToResizeRows = false;
            this.dataGridViewX1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridViewX1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAntennaType,
            this.colLng,
            this.colLat,
            this.colAngle,
            this.colTile,
            this.colHeight,
            this.colCarrier,
            this.colPower,
            this.colfh,
            this.colModelType,
            this.colDelete});
            this.tableLayoutPanel1.SetColumnSpan(this.dataGridViewX1, 5);
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewX1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewX1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewX1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridViewX1.Location = new System.Drawing.Point(5, 136);
            this.dataGridViewX1.Name = "dataGridViewX1";
            this.dataGridViewX1.ReadOnly = true;
            this.dataGridViewX1.RowHeadersVisible = false;
            this.dataGridViewX1.RowTemplate.Height = 23;
            this.dataGridViewX1.ShowEditingIcon = false;
            this.dataGridViewX1.Size = new System.Drawing.Size(809, 241);
            this.dataGridViewX1.TabIndex = 0;
            this.dataGridViewX1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewX1_CellContentClick);
            this.dataGridViewX1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewX1_CellMouseDown);
            // 
            // colAntennaType
            // 
            this.colAntennaType.DataPropertyName = "AntennaTypeName";
            this.colAntennaType.Frozen = true;
            this.colAntennaType.HeaderText = "天线型号";
            this.colAntennaType.Name = "colAntennaType";
            this.colAntennaType.ReadOnly = true;
            this.colAntennaType.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colAntennaType.Width = 80;
            // 
            // colLng
            // 
            this.colLng.DataPropertyName = "Lng";
            this.colLng.Frozen = true;
            this.colLng.HeaderText = "经度";
            this.colLng.Name = "colLng";
            this.colLng.ReadOnly = true;
            this.colLng.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colLng.Width = 70;
            // 
            // colLat
            // 
            this.colLat.DataPropertyName = "Lat";
            this.colLat.Frozen = true;
            this.colLat.HeaderText = "纬度";
            this.colLat.Name = "colLat";
            this.colLat.ReadOnly = true;
            this.colLat.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colLat.Width = 70;
            // 
            // colAngle
            // 
            this.colAngle.DataPropertyName = "Azimuth";
            this.colAngle.Frozen = true;
            this.colAngle.HeaderText = "方向角";
            this.colAngle.Name = "colAngle";
            this.colAngle.ReadOnly = true;
            this.colAngle.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colAngle.Width = 80;
            // 
            // colTile
            // 
            this.colTile.DataPropertyName = "MechanicalDownTilt";
            this.colTile.Frozen = true;
            this.colTile.HeaderText = "下倾角";
            this.colTile.Name = "colTile";
            this.colTile.ReadOnly = true;
            this.colTile.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colTile.Width = 80;
            // 
            // colHeight
            // 
            this.colHeight.DataPropertyName = "Height";
            this.colHeight.Frozen = true;
            this.colHeight.HeaderText = "挂高";
            this.colHeight.Name = "colHeight";
            this.colHeight.ReadOnly = true;
            this.colHeight.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colHeight.Width = 60;
            // 
            // colCarrier
            // 
            this.colCarrier.DataPropertyName = "CarrierAlias";
            this.colCarrier.Frozen = true;
            this.colCarrier.HeaderText = "载波";
            this.colCarrier.Name = "colCarrier";
            this.colCarrier.ReadOnly = true;
            this.colCarrier.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colCarrier.Width = 70;
            // 
            // colPower
            // 
            this.colPower.DataPropertyName = "Power";
            this.colPower.Frozen = true;
            this.colPower.HeaderText = "功率";
            this.colPower.Name = "colPower";
            this.colPower.ReadOnly = true;
            this.colPower.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colPower.Width = 60;
            // 
            // colfh
            // 
            this.colfh.DataPropertyName = "Burthen";
            this.colfh.Frozen = true;
            this.colfh.HeaderText = "负荷";
            this.colfh.Name = "colfh";
            this.colfh.ReadOnly = true;
            this.colfh.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colfh.Width = 60;
            // 
            // colModelType
            // 
            this.colModelType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colModelType.DataPropertyName = "ModelType";
            this.colModelType.HeaderText = "传播模型";
            this.colModelType.Name = "colModelType";
            this.colModelType.ReadOnly = true;
            this.colModelType.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // colDelete
            // 
            this.colDelete.HeaderText = "删除";
            this.colDelete.Image = ((System.Drawing.Image)(resources.GetObject("colDelete.Image")));
            this.colDelete.Name = "colDelete";
            this.colDelete.ReadOnly = true;
            this.colDelete.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colDelete.Text = null;
            this.colDelete.Width = 50;
            // 
            // btnOk
            // 
            this.btnOk.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOk.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOk.Location = new System.Drawing.Point(733, 383);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(81, 29);
            this.btnOk.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "确定";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // 编号
            // 
            // 
            // 
            // 
            this.编号.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tableLayoutPanel1.SetColumnSpan(this.编号, 2);
            this.编号.Dock = System.Windows.Forms.DockStyle.Fill;
            this.编号.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.编号.Location = new System.Drawing.Point(636, 3);
            this.编号.Name = "编号";
            this.编号.Size = new System.Drawing.Size(178, 29);
            this.编号.TabIndex = 3;
            this.编号.Text = "扇区号码：";
            // 
            // txtSectorID
            // 
            // 
            // 
            // 
            this.txtSectorID.Border.Class = "TextBoxBorder";
            this.txtSectorID.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tableLayoutPanel1.SetColumnSpan(this.txtSectorID, 2);
            this.txtSectorID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSectorID.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSectorID.Location = new System.Drawing.Point(636, 38);
            this.txtSectorID.Name = "txtSectorID";
            this.txtSectorID.Size = new System.Drawing.Size(178, 26);
            this.txtSectorID.TabIndex = 4;
            this.txtSectorID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSectorID_KeyPress);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 311F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 97F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 87F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.编号, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.ucLTEAntennaType1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridViewX1, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnOk, 5, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtSectorID, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnAppend, 4, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 63F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(819, 415);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // ucLTEAntennaType1
            // 
            this.ucLTEAntennaType1.BackColor = System.Drawing.SystemColors.Control;
            this.ucLTEAntennaType1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel1.SetColumnSpan(this.ucLTEAntennaType1, 3);
            this.ucLTEAntennaType1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucLTEAntennaType1.Location = new System.Drawing.Point(5, 3);
            this.ucLTEAntennaType1.Margin = new System.Windows.Forms.Padding(3, 3, 5, 3);
            this.ucLTEAntennaType1.MaximumSize = new System.Drawing.Size(2, 133);
            this.ucLTEAntennaType1.MinimumSize = new System.Drawing.Size(626, 131);
            this.ucLTEAntennaType1.Name = "ucLTEAntennaType1";
            this.ucLTEAntennaType1.Size = new System.Drawing.Size(626, 131);
            this.ucLTEAntennaType1.TabIndex = 5;
            // 
            // btnAppend
            // 
            this.btnAppend.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAppend.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.tableLayoutPanel1.SetColumnSpan(this.btnAppend, 2);
            this.btnAppend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAppend.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAppend.Location = new System.Drawing.Point(636, 73);
            this.btnAppend.Name = "btnAppend";
            this.btnAppend.Size = new System.Drawing.Size(178, 57);
            this.btnAppend.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAppend.TabIndex = 6;
            this.btnAppend.Text = "添加";
            this.btnAppend.Click += new System.EventHandler(this.btnAppend_Click);
            // 
            // dataGridViewButtonXColumn1
            // 
            this.dataGridViewButtonXColumn1.Frozen = true;
            this.dataGridViewButtonXColumn1.HeaderText = "删除";
            this.dataGridViewButtonXColumn1.Image = ((System.Drawing.Image)(resources.GetObject("dataGridViewButtonXColumn1.Image")));
            this.dataGridViewButtonXColumn1.Name = "dataGridViewButtonXColumn1";
            this.dataGridViewButtonXColumn1.ReadOnly = true;
            this.dataGridViewButtonXColumn1.Text = null;
            // 
            // FrmSector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 415);
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.Name = "FrmSector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "扇区信息";
            this.Load += new System.EventHandler(this.FrmSector_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1;
        private DevComponents.DotNetBar.ButtonX btnOk;
        private DevComponents.DotNetBar.LabelX 编号;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSectorID;
        private ucLTEAntennaType ucLTEAntennaType1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevComponents.DotNetBar.ButtonX btnAppend;
        private DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn dataGridViewButtonXColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAntennaType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLng;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLat;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAngle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTile;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHeight;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCarrier;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPower;
        private System.Windows.Forms.DataGridViewTextBoxColumn colfh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colModelType;
        private DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn colDelete;

    }
}