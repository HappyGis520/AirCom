using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.ComponentModel.Design;

namespace NetPlanClient.Componet
{
    public partial class ucHeaderButton : UserControl
    {
        #region 属性_事件_变量

        private bool _IsEnable = true;
        public bool IsEnable
        {
            get { return _IsEnable; }
            set
            {
                _IsEnable = value;
                this.lblButton.Enabled = value;
            }
        }

        protected bool _Visible = true;

        public bool Visible
        {
            get { return _Visible; }
            set
            {
                _Visible = value;
                base.Visible = _Visible;
            }
        }

        private string m_ButtonText="按钮";              
        /// <summary>
        /// 按钮文字
        /// </summary>
        [DefaultValue("按钮"), Description("按钮文字"), Category("自定义属性")]
        public string ItemText
        {
            get
            {
                return m_ButtonText;
            }
            set
            {
                this.lblButton.Text = value;
                m_ButtonText = value;
            }
        }
       
        private int m_PaddingLeft = 0;
        [DefaultValue(0), Description("按钮PaddingLeft"), Category("自定义属性")]
        public int ItemPaddingLeft
        {
            get
            {
                return m_PaddingLeft;
            }
            set
            {
                this.lblButton.PaddingLeft = value;
                m_PaddingLeft = value;
            }
        }
        private int m_PaddingTop = 0;
        [DefaultValue(0), Description("按钮PaddingTop"), Category("自定义属性")]
        public int ItemPaddingTop
        {
            get
            {
                return m_PaddingTop;
            }
            set
            {
                this.lblButton.PaddingTop = value;
                m_PaddingTop = value;
            }
        }
       
        public Image m_ItemImage;
        [Description("按钮Image"), Category("自定义属性")]
        public Image ItemImage
        {
            get
            {
                return m_ItemImage;
            }
            set
            {
                if (value != null)
                {
                    m_ItemImage = value;
                    this.lblButton.Image = m_ItemImage;                  
                }
              
            }
        }

        public Image m_ItemHoverImage;
        [DefaultValue(null), Description("按钮背景图片"), Category("自定义属性")]
        public Image ItemHoverImage
        {
            get
            {
                return m_ItemHoverImage;
            }
            set
            {
                m_ItemHoverImage = value;
            }
        }

        public ImageLayout m_ItemHoverImageLayout = ImageLayout.Center;
        [DefaultValue(ImageLayout.Center), Description("按钮背景图片布局方式"), Category("自定义属性")]
        public ImageLayout ItemHoverImageLayout
        {
            get
            {
                return m_ItemHoverImageLayout;
            }
            set
            {
                this.lblButton.BackgroundImageLayout = value;
                m_ItemHoverImageLayout = value;
            }
        }

        private int m_ItemWidth = 0;
         [DefaultValue(0), Description("按钮宽度"), Category("自定义属性")]
        public int ItemWidth
        {
            get
            {
                return m_ItemWidth;
            }
            set
            {
                if (value > 0)
                {
                    this.Width = value;
                }
                m_ItemWidth = value;
            }
        }
         private int m_ItemHeight = 0;
         [DefaultValue(0), Description("按钮高度"), Category("自定义属性")]
         public int ItemHeight
         {
             get
             {
                 return m_ItemHeight;
             }
             set
             {
                 if (value > 0)
                 {
                     this.Height = value;
                 }
                 m_ItemHeight = value;
             }
         }

         private StringAlignment m_ItemTextAlignment = StringAlignment.Center;
        [DefaultValue(StringAlignment.Center), Description("文字显示位置"), Category("自定义属性")]
         public StringAlignment ItemTextAlignment
         {
             get
             {
                 return m_ItemTextAlignment;
             }
             set
             {
                 if (value !=null)
                 {
                     this.lblButton.TextAlignment = value;
                 }
                 m_ItemTextAlignment = value;
             }
         }

        private int m_BorderWidth = 0;
        [DefaultValue(0), Description("按钮宽度"), Category("自定义属性")]
        public int ItemBorderWidth
        {
            get
            {
                return m_BorderWidth;
            }
            set
            {
                if (value > 0)
                {                    
                    this.pnlMain.Style.BorderWidth = value;
                }
                m_BorderWidth = value;
            }
        }
        private Color m_BorderColor;
        [DefaultValue(null), Description("按钮边框颜色"), Category("自定义属性")]
        public Color ItemBorderColor
        {
            get
            {
                return m_BorderColor;
            }
            set
            {
                if (value!=null)
                {
                    this.pnlMain.Style.BorderColor.ColorSchemePart = eColorSchemePart.Custom;
                    this.pnlMain.Style.BorderColor.Color = value;
                }
                m_BorderColor = value;
            }
        }    
        private eBorderType m_Border = eBorderType.None;
        [DefaultValue(eBorderType.None), Description("按钮边框"), Category("自定义属性")]
        public eBorderType ItemBorder
        {
            get
            {
                return m_Border;
            }
            set
            {
                if (value != null)
                {
                    this.pnlMain.Style.Border = value;
                }
                m_Border = value;
            }
        }
        private Color m_ItemForeColor=Color.White;
        [Description("按钮文字颜色"), Category("自定义属性")]
        public Color ItemForeColor
        {
            get
            {
                return m_ItemForeColor;
            }
            set
            {
                if (value != null)
                {
                    m_ItemForeColor = value;
                    this.lblButton.ForeColor = m_ItemForeColor;
                }            
            }
        }
        #endregion 属性_事件_变量

        #region 构造函数
        public ucHeaderButton()
        {
            InitializeComponent();
             if (LicenseManager.UsageMode != LicenseUsageMode.Runtime)
            {
                return;
            }
                this.lblButton.TextAlignment = m_ItemTextAlignment;
                this.lblButton.BackgroundImageLayout = m_ItemHoverImageLayout;
                this.lblButton.Text = ItemText;
                this.lblButton.PaddingLeft = ItemPaddingLeft;
                this.lblButton.Image = ItemImage;
                if (ItemBorderColor != null)
                {
                    this.pnlMain.Style.Border = ItemBorder;
                    this.pnlMain.Style.BorderWidth = ItemBorderWidth;
                    this.pnlMain.Style.BorderColor.ColorSchemePart = eColorSchemePart.Custom;
                    this.pnlMain.Style.BorderColor.Color = ItemBorderColor;
                }            
        }
        #endregion 构造函数

        #region 窗体事件
        private void ucHeaderButton_Load(object sender, EventArgs e)
        {
           
        }

        private void lblButton_MouseEnter(object sender, EventArgs e)
        {
         
        }

        private void lblButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (_IsEnable)
            {
                LabelX lblSender = ((LabelX) sender);
                lblSender.PaddingLeft = ItemPaddingLeft + 1;
                lblSender.PaddingTop = ItemPaddingTop + 1;
            }

            //ButtonClick(e);           
        }
        private void lblButton_Click(object sender, EventArgs e)
        {
            if (_IsEnable)
            {
                OnClick(e);
            }
            
        }
        private void lblButton_DoubleClick(object sender, EventArgs e)
        {
            if (_IsEnable)
            {
                OnDoubleClick(e);
            }
        }
        private void lblButton_DragLeave(object sender, EventArgs e)
        {
            
        }

        private void lblButton_MouseHover(object sender, EventArgs e)
        {
            if (ItemHoverImage != null && _IsEnable)
            {
                LabelX lblSender = ((LabelX)sender);
                lblSender.BackgroundImage = ItemHoverImage;
                lblSender.BackgroundImageLayout = ItemHoverImageLayout;
            }
        }

        private void lblButton_MouseLeave(object sender, EventArgs e)
        {
            if (_IsEnable)
            {
                LabelX lblSender = ((LabelX) sender);
                lblSender.BackgroundImage = null;
            }
        }

        private void lblButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsEnable)
            {
                LabelX lblSender = ((LabelX) sender);
                lblSender.PaddingLeft = ItemPaddingLeft;
                lblSender.PaddingTop = ItemPaddingTop;
            }

        }       
        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);
        }
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);           
        }
        protected override void OnDragLeave(EventArgs e)
        {
            base.OnDragLeave(e);
        }
        #endregion 窗体事件       
    }
}
