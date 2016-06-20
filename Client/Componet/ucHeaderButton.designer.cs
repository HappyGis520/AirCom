namespace NetPlanClient.Componet
{
    partial class ucHeaderButton
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlMain = new DevComponents.DotNetBar.PanelEx();
            this.lblButton = new DevComponents.DotNetBar.LabelX();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.CanvasColor = System.Drawing.Color.Transparent;
            this.pnlMain.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.pnlMain.Controls.Add(this.lblButton);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(100, 48);
            this.pnlMain.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pnlMain.Style.BackColor1.Alpha = ((byte)(0));
            this.pnlMain.Style.BackColor1.Color = System.Drawing.Color.Gold;
            this.pnlMain.Style.BackgroundImagePosition = DevComponents.DotNetBar.eBackgroundImagePosition.Tile;
            this.pnlMain.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(84)))), ((int)(((byte)(100)))));
            this.pnlMain.Style.BorderSide = DevComponents.DotNetBar.eBorderSide.Right;
            this.pnlMain.Style.BorderWidth = 2;
            this.pnlMain.Style.CornerDiameter = 0;
            this.pnlMain.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.pnlMain.Style.GradientAngle = 90;
            this.pnlMain.StyleMouseDown.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlMain.TabIndex = 0;
            // 
            // lblButton
            // 
            this.lblButton.AllowDrop = true;
            this.lblButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            // 
            // 
            // 
            this.lblButton.BackgroundStyle.Class = "";
            this.lblButton.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblButton.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblButton.ForeColor = System.Drawing.Color.Black;
            this.lblButton.Location = new System.Drawing.Point(0, 0);
            this.lblButton.Margin = new System.Windows.Forms.Padding(0);
            this.lblButton.Name = "lblButton";
            this.lblButton.Size = new System.Drawing.Size(100, 48);
            this.lblButton.TabIndex = 5;
            this.lblButton.Text = "按钮文字";
            this.lblButton.Click += new System.EventHandler(this.lblButton_Click);
            this.lblButton.DragLeave += new System.EventHandler(this.lblButton_DragLeave);
            this.lblButton.DoubleClick += new System.EventHandler(this.lblButton_DoubleClick);
            this.lblButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblButton_MouseDown);
            this.lblButton.MouseEnter += new System.EventHandler(this.lblButton_MouseEnter);
            this.lblButton.MouseLeave += new System.EventHandler(this.lblButton_MouseLeave);
            this.lblButton.MouseHover += new System.EventHandler(this.lblButton_MouseHover);
            this.lblButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lblButton_MouseUp);
            // 
            // ucHeaderButton
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.pnlMain);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MaximumSize = new System.Drawing.Size(150, 100);
            this.MinimumSize = new System.Drawing.Size(100, 25);
            this.Name = "ucHeaderButton";
            this.Size = new System.Drawing.Size(100, 48);
            this.Load += new System.EventHandler(this.ucHeaderButton_Load);
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx pnlMain;
        private DevComponents.DotNetBar.LabelX lblButton;

    }
}
