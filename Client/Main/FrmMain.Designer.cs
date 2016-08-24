namespace NetPlanClient
{
    partial class FrmMain
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.superTabStrip1 = new DevComponents.DotNetBar.SuperTabStrip();
            this.tabCreateTask = new DevComponents.DotNetBar.SuperTabItem();
            this.tabTaskInfo = new DevComponents.DotNetBar.SuperTabItem();
            this.superTabItem1 = new DevComponents.DotNetBar.SuperTabItem();
            this.pnlContainer = new DevComponents.DotNetBar.PanelEx();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvStation = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colSectorID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAntennaType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.collng = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAngle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCarrier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPower = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colfh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ucLTEStationType1 = new NetPlanClient.UC.ucLTEStationType();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnLoadXML = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtXMLFile = new System.Windows.Forms.TextBox();
            this.buttonX3 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.txtCityName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtPrjName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.btnConfig = new DevComponents.DotNetBar.ButtonX();
            this.txtCoverRadius = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.btnCallWebSerivce = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.superTabStrip1)).BeginInit();
            this.pnlContainer.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStation)).BeginInit();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // superTabStrip1
            // 
            this.superTabStrip1.AutoSelectAttachedControl = false;
            // 
            // 
            // 
            this.superTabStrip1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.superTabStrip1.ContainerControlProcessDialogKey = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.superTabStrip1.ControlBox.CloseBox.Name = "";
            // 
            // 
            // 
            this.superTabStrip1.ControlBox.MenuBox.Name = "";
            this.superTabStrip1.ControlBox.Name = "";
            this.superTabStrip1.ControlBox.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.superTabStrip1.ControlBox.MenuBox,
            this.superTabStrip1.ControlBox.CloseBox});
            this.superTabStrip1.Dock = System.Windows.Forms.DockStyle.Top;
            this.superTabStrip1.Location = new System.Drawing.Point(0, 0);
            this.superTabStrip1.Name = "superTabStrip1";
            this.superTabStrip1.ReorderTabsEnabled = true;
            this.superTabStrip1.SelectedTabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.superTabStrip1.SelectedTabIndex = 0;
            this.superTabStrip1.Size = new System.Drawing.Size(939, 28);
            this.superTabStrip1.TabCloseButtonHot = null;
            this.superTabStrip1.TabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.superTabStrip1.TabIndex = 0;
            this.superTabStrip1.Tabs.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.tabCreateTask,
            this.tabTaskInfo,
            this.superTabItem1});
            this.superTabStrip1.Text = "superTabStrip1";
            // 
            // tabCreateTask
            // 
            this.tabCreateTask.GlobalItem = false;
            this.tabCreateTask.Name = "tabCreateTask";
            this.tabCreateTask.Tag = "CreateTask";
            this.tabCreateTask.Text = "创建任务";
            this.tabCreateTask.Click += new System.EventHandler(this.tabCreateTask_Click);
            // 
            // tabTaskInfo
            // 
            this.tabTaskInfo.GlobalItem = false;
            this.tabTaskInfo.Name = "tabTaskInfo";
            this.tabTaskInfo.Text = "任务进度";
            // 
            // superTabItem1
            // 
            this.superTabItem1.GlobalItem = false;
            this.superTabItem1.Name = "superTabItem1";
            this.superTabItem1.Text = "superTabItem1";
            // 
            // pnlContainer
            // 
            this.pnlContainer.CanvasColor = System.Drawing.SystemColors.Control;
            this.pnlContainer.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.pnlContainer.Controls.Add(this.tableLayoutPanel1);
            this.pnlContainer.DisabledBackColor = System.Drawing.Color.Empty;
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(0, 28);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(939, 501);
            this.pnlContainer.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pnlContainer.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.pnlContainer.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.pnlContainer.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlContainer.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pnlContainer.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnlContainer.Style.GradientAngle = 90;
            this.pnlContainer.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dgvStation, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.ucLTEStationType1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelEx1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 122F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(939, 501);
            this.tableLayoutPanel1.TabIndex = 1;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // dgvStation
            // 
            this.dgvStation.AllowUserToAddRows = false;
            this.dgvStation.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStation.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvStation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStation.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSectorID,
            this.colAntennaType,
            this.collng,
            this.colLat,
            this.colAngle,
            this.colTile,
            this.colHeight,
            this.colCarrier,
            this.colPower,
            this.colfh});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvStation.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvStation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStation.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvStation.Location = new System.Drawing.Point(3, 84);
            this.dgvStation.Name = "dgvStation";
            this.dgvStation.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStation.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvStation.RowHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgvStation.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvStation.RowTemplate.Height = 23;
            this.dgvStation.Size = new System.Drawing.Size(933, 292);
            this.dgvStation.TabIndex = 1;
            this.dgvStation.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewX1_CellMouseDoubleClick);
            // 
            // colSectorID
            // 
            this.colSectorID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colSectorID.DataPropertyName = "SectorId";
            this.colSectorID.HeaderText = "扇区ID";
            this.colSectorID.Name = "colSectorID";
            this.colSectorID.ReadOnly = true;
            // 
            // colAntennaType
            // 
            this.colAntennaType.DataPropertyName = "AntennaTypeName";
            this.colAntennaType.HeaderText = "天线型号";
            this.colAntennaType.Name = "colAntennaType";
            this.colAntennaType.ReadOnly = true;
            // 
            // collng
            // 
            this.collng.DataPropertyName = "Lng";
            this.collng.HeaderText = "经度";
            this.collng.Name = "collng";
            this.collng.ReadOnly = true;
            this.collng.Width = 70;
            // 
            // colLat
            // 
            this.colLat.DataPropertyName = "Lat";
            this.colLat.HeaderText = "纬度";
            this.colLat.Name = "colLat";
            this.colLat.ReadOnly = true;
            this.colLat.Width = 70;
            // 
            // colAngle
            // 
            this.colAngle.DataPropertyName = "Azimuth";
            this.colAngle.HeaderText = "方向角";
            this.colAngle.Name = "colAngle";
            this.colAngle.ReadOnly = true;
            this.colAngle.Width = 70;
            // 
            // colTile
            // 
            this.colTile.DataPropertyName = "MechanicalDownTilt";
            this.colTile.HeaderText = "下倾角";
            this.colTile.Name = "colTile";
            this.colTile.ReadOnly = true;
            this.colTile.Width = 70;
            // 
            // colHeight
            // 
            this.colHeight.DataPropertyName = "Height";
            this.colHeight.HeaderText = "挂高";
            this.colHeight.Name = "colHeight";
            this.colHeight.ReadOnly = true;
            this.colHeight.Width = 70;
            // 
            // colCarrier
            // 
            this.colCarrier.DataPropertyName = "CarrierTypeName";
            this.colCarrier.HeaderText = "载波";
            this.colCarrier.Name = "colCarrier";
            this.colCarrier.ReadOnly = true;
            this.colCarrier.Width = 70;
            // 
            // colPower
            // 
            this.colPower.DataPropertyName = "Power";
            this.colPower.HeaderText = "功率";
            this.colPower.Name = "colPower";
            this.colPower.ReadOnly = true;
            this.colPower.Width = 70;
            // 
            // colfh
            // 
            this.colfh.DataPropertyName = "Burthen";
            this.colfh.HeaderText = "负荷";
            this.colfh.Name = "colfh";
            this.colfh.ReadOnly = true;
            this.colfh.Width = 70;
            // 
            // ucLTEStationType1
            // 
            this.ucLTEStationType1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucLTEStationType1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucLTEStationType1.Location = new System.Drawing.Point(6, 6);
            this.ucLTEStationType1.Margin = new System.Windows.Forms.Padding(6);
            this.ucLTEStationType1.Name = "ucLTEStationType1";
            this.ucLTEStationType1.Size = new System.Drawing.Size(927, 69);
            this.ucLTEStationType1.TabIndex = 2;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.btnCallWebSerivce);
            this.panelEx1.Controls.Add(this.btnStart);
            this.panelEx1.Controls.Add(this.btnLoadXML);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Controls.Add(this.txtXMLFile);
            this.panelEx1.Controls.Add(this.buttonX3);
            this.panelEx1.Controls.Add(this.buttonX2);
            this.panelEx1.Controls.Add(this.txtCityName);
            this.panelEx1.Controls.Add(this.labelX2);
            this.panelEx1.Controls.Add(this.txtPrjName);
            this.panelEx1.Controls.Add(this.labelX1);
            this.panelEx1.Controls.Add(this.buttonX1);
            this.panelEx1.Controls.Add(this.btnConfig);
            this.panelEx1.Controls.Add(this.txtCoverRadius);
            this.panelEx1.Controls.Add(this.labelX4);
            this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 379);
            this.panelEx1.Margin = new System.Windows.Forms.Padding(0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(939, 122);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 3;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(497, 72);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 10;
            this.btnStart.Text = "启动";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnLoadXML
            // 
            this.btnLoadXML.Location = new System.Drawing.Point(417, 72);
            this.btnLoadXML.Name = "btnLoadXML";
            this.btnLoadXML.Size = new System.Drawing.Size(75, 23);
            this.btnLoadXML.TabIndex = 10;
            this.btnLoadXML.Text = "加载XML";
            this.btnLoadXML.UseVisualStyleBackColor = true;
            this.btnLoadXML.Click += new System.EventHandler(this.btnLoadXML_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "label1";
            // 
            // txtXMLFile
            // 
            this.txtXMLFile.Location = new System.Drawing.Point(98, 72);
            this.txtXMLFile.Name = "txtXMLFile";
            this.txtXMLFile.Size = new System.Drawing.Size(311, 21);
            this.txtXMLFile.TabIndex = 8;
            // 
            // buttonX3
            // 
            this.buttonX3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX3.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX3.Location = new System.Drawing.Point(553, 3);
            this.buttonX3.Name = "buttonX3";
            this.buttonX3.Size = new System.Drawing.Size(30, 23);
            this.buttonX3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX3.TabIndex = 7;
            this.buttonX3.Text = "buttonX2";
            this.buttonX3.Click += new System.EventHandler(this.buttonX3_Click_1);
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.Location = new System.Drawing.Point(507, 5);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(30, 23);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 7;
            this.buttonX2.Text = "buttonX2";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click_1);
            // 
            // txtCityName
            // 
            // 
            // 
            // 
            this.txtCityName.Border.Class = "TextBoxBorder";
            this.txtCityName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCityName.Location = new System.Drawing.Point(401, 5);
            this.txtCityName.Name = "txtCityName";
            this.txtCityName.PreventEnterBeep = true;
            this.txtCityName.Size = new System.Drawing.Size(100, 21);
            this.txtCityName.TabIndex = 6;
            this.txtCityName.Text = "changzhou";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(343, 3);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(37, 23);
            this.labelX2.TabIndex = 5;
            this.labelX2.Text = "城市:";
            // 
            // txtPrjName
            // 
            // 
            // 
            // 
            this.txtPrjName.Border.Class = "TextBoxBorder";
            this.txtPrjName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPrjName.Location = new System.Drawing.Point(221, 6);
            this.txtPrjName.Name = "txtPrjName";
            this.txtPrjName.PreventEnterBeep = true;
            this.txtPrjName.Size = new System.Drawing.Size(100, 21);
            this.txtPrjName.TabIndex = 6;
            this.txtPrjName.Text = "auto_changzhou";
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(148, 4);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(67, 23);
            this.labelX1.TabIndex = 5;
            this.labelX1.Text = "工程名称:";
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(852, 4);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(75, 23);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 4;
            this.buttonX1.Text = "开始仿真";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // btnConfig
            // 
            this.btnConfig.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnConfig.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnConfig.Location = new System.Drawing.Point(12, 3);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(130, 23);
            this.btnConfig.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnConfig.TabIndex = 3;
            this.btnConfig.Text = "配置文件格式生成";
            this.btnConfig.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtCoverRadius
            // 
            // 
            // 
            // 
            this.txtCoverRadius.Border.Class = "TextBoxBorder";
            this.txtCoverRadius.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCoverRadius.Location = new System.Drawing.Point(731, 6);
            this.txtCoverRadius.Name = "txtCoverRadius";
            this.txtCoverRadius.Size = new System.Drawing.Size(106, 21);
            this.txtCoverRadius.TabIndex = 1;
            this.txtCoverRadius.Text = "1";
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(625, 5);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(100, 23);
            this.labelX4.TabIndex = 0;
            this.labelX4.Text = "仿真范围(KM):";
            // 
            // btnCallWebSerivce
            // 
            this.btnCallWebSerivce.Location = new System.Drawing.Point(809, 53);
            this.btnCallWebSerivce.Name = "btnCallWebSerivce";
            this.btnCallWebSerivce.Size = new System.Drawing.Size(118, 23);
            this.btnCallWebSerivce.TabIndex = 11;
            this.btnCallWebSerivce.Text = "调用WEBSERVICE";
            this.btnCallWebSerivce.UseVisualStyleBackColor = true;
            this.btnCallWebSerivce.Click += new System.EventHandler(this.btnCallWebSerivce_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(111)))), ((int)(((byte)(134)))));
            this.ClientSize = new System.Drawing.Size(939, 529);
            this.Controls.Add(this.pnlContainer);
            this.Controls.Add(this.superTabStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmMain";
            this.Text = "主界面";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.superTabStrip1)).EndInit();
            this.pnlContainer.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStation)).EndInit();
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.SuperTabStrip superTabStrip1;
        private DevComponents.DotNetBar.SuperTabItem tabCreateTask;
        private DevComponents.DotNetBar.PanelEx pnlContainer;
        private DevComponents.DotNetBar.SuperTabItem tabTaskInfo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvStation;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSectorID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAntennaType;
        private System.Windows.Forms.DataGridViewTextBoxColumn collng;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLat;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAngle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTile;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHeight;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCarrier;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPower;
        private System.Windows.Forms.DataGridViewTextBoxColumn colfh;
        private UC.ucLTEStationType ucLTEStationType1;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCoverRadius;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.ButtonX btnConfig;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPrjName;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCityName;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private DevComponents.DotNetBar.SuperTabItem superTabItem1;
        private DevComponents.DotNetBar.ButtonX buttonX3;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnLoadXML;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtXMLFile;
        private System.Windows.Forms.Button btnCallWebSerivce;
    }
}