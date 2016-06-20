using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace NetPlanClient.Componet
{
    /// <summary>
    /// 图形按钮类
    /// </summary>
    public class ImageButton : PictureBox, IButtonControl
    {
        #region  Fileds

        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;

        private DialogResult _DialogResult;
        private Color _FontColor;
        private Color _BackgroundColor;
        private Color _HoverColor;
        private Image _HoverImage;
        private Image _DownImage;
        private Image _NormalImage;
        private bool _hover = false;
        private bool _down = false;
        private bool _isDefault = false;
        private bool _holdingSpace = false;

        private ToolTip _toolTip = new ToolTip();

        #endregion

        #region Constructor

        public ImageButton()
            : base()
        {
            //base.BackColor = BackgroundColor;
            _FontColor = Color.Black;
            this.Size = new Size(75, 23);
        }

        #endregion

        #region IButtonControl Members

        public DialogResult DialogResult
        {
            get
            {
                return _DialogResult;
            }
            set
            {
                if (Enum.IsDefined(typeof(DialogResult), value))
                {
                    _DialogResult = value;
                }
            }
        }

        public void NotifyDefault(bool value)
        {
            if (_isDefault != value)
            {
                _isDefault = value;
            }
        }

        public void PerformClick()
        {
            base.OnClick(EventArgs.Empty);
        }

        #endregion

        #region  Properties

        [Category("Appearance")]
        [Description("按钮文字颜色")]
        public Color FontColor
        {
            get { return _FontColor; }
            set { _FontColor = value; }
        }
        [Category("Appearance")]
        [Description("按钮背景颜色")]
        public Color BackgroundColor
        {
            get { return _BackgroundColor; }
            set { _BackgroundColor = value; base.BackColor = value; }
        }
        [Category("Appearance")]
        [Description("鼠标移动到按钮时背景颜色变化")]
        public Color HoverColor
        {
            get { return _HoverColor; }
            set { _HoverColor = value;}
        }
        [Category("Appearance")]
        [Description("Image to show when the button is hovered over.")]
        public Image HoverImage
        {
            get { return _HoverImage; }
            set { _HoverImage = value; if (_hover) BackgroundImage = value;}
        }

        [Category("Appearance")]
        [Description("Image to show when the button is depressed.")]
        public Image DownImage
        {
            get { return _DownImage; }
            set { _DownImage = value; if (_down) Image = value; }
        }

        [Category("Appearance")]
        [Description("Image to show when the button is not in any other state.")]
        public Image NormalImage
        {
            get { return _NormalImage; }
            set { _NormalImage = value; if (!(_hover || _down)) Image = value ; }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Appearance")]
        [Description("The text associated with the control.")]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Appearance")]
        [Description("The font used to display text in the control.")]
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
            }
        }

        [Description("当鼠标放在控件可见处的提示文本")]
        public string ToolTipText { get; set; }
        /// <summary>
        /// 边框颜色
        /// </summary>
        public Color BorderColor { get; set; }     
        #endregion

        #region Description Changes
        [Description("Controls how the ImageButton will handle image placement and control sizing.")]
        public new PictureBoxSizeMode SizeMode { get { return base.SizeMode; } set { base.SizeMode = value; } }

        [Description("Controls what type of border the ImageButton should have.")]
        public new BorderStyle BorderStyle { get { return base.BorderStyle; } set { base.BorderStyle = value; } }
        #endregion

        #region Hiding

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Color BackColor { get { return base.BackColor; } set { base.BackColor = value; } }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Image Image { get { return base.Image; } set { base.Image = value; } }

        //[Browsable(false)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public new ImageLayout BackgroundImageLayout { get { return base.BackgroundImageLayout; } set { base.BackgroundImageLayout = value; } }

        //[Browsable(true)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public new Image BackgroundImage { get { return base.BackgroundImage; } set { base.BackgroundImage = value; } }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new String ImageLocation { get { return base.ImageLocation; } set { base.ImageLocation = value; } }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Image ErrorImage { get { return base.ErrorImage; } set { base.ErrorImage = value; } }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Image InitialImage { get { return base.InitialImage; } set { base.InitialImage = value; } }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool WaitOnLoad { get { return base.WaitOnLoad; } set { base.WaitOnLoad = value; } }
        #endregion

        #region override

        protected override void OnMouseEnter(EventArgs e)
        {
            //show tool tip 
            if (ToolTipText != string.Empty)
            {
                HideToolTip();
                ShowTooTip(ToolTipText);
            }
            base.OnMouseEnter(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            _hover = true;
            if (_down)
            {
                if ((_DownImage != null) && (Image != _DownImage))
                    Image = _DownImage;
            }
            else
            {
                if (_HoverImage != null)
                    BackgroundImage = _HoverImage;
                else
                    Image = _NormalImage;
            }
            if (_hover)
            {
                if (HoverColor != null && HoverColor!=Color.Empty)
                {
                    base.BackColor = HoverColor;
                }
              
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _hover = false;
            Image = _NormalImage;
            if (_HoverImage != null)
            {
                BackgroundImage = null;
            }
            base.BackColor = BackgroundColor;
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.Focus();
            OnMouseUp(null);
            _down = true;
            if (_DownImage != null)
                Image = _DownImage;
            if (e != null)
            {
                base.OnMouseDown(e);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _down = false;
            if (_hover)
            {
                if (_HoverImage != null)
                    BackgroundImage = _HoverImage;
            }
            else
            {
                Image = _NormalImage;
            }
            if (e != null)
            {
                base.OnMouseUp(e);
            }
        }

        public override bool PreProcessMessage(ref Message msg)
        {
            if (msg.Msg == WM_KEYUP)
            {
                if (_holdingSpace)
                {
                    if ((int)msg.WParam == (int)Keys.Space)
                    {
                        OnMouseUp(null);
                        PerformClick();
                    }
                    else if ((int)msg.WParam == (int)Keys.Escape
                        || (int)msg.WParam == (int)Keys.Tab)
                    {
                        _holdingSpace = false;
                        OnMouseUp(null);
                    }
                }
                return true;
            }
            else if (msg.Msg == WM_KEYDOWN)
            {
                if ((int)msg.WParam == (int)Keys.Space)
                {
                    _holdingSpace = true;
                    OnMouseDown(null);
                }
                else if ((int)msg.WParam == (int)Keys.Enter)
                {
                    PerformClick();
                }
                return true;
            }
            else
                return base.PreProcessMessage(ref msg);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            _holdingSpace = false;
            OnMouseUp(null);
            base.OnLostFocus(e);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            if ((!string.IsNullOrEmpty(Text)) && (pe != null) && (base.Font != null))
            {
                SizeF drawStringSize = pe.Graphics.MeasureString(base.Text, base.Font);
                PointF drawPoint;
                if (base.Image != null)
                {
                    this.Padding = new Padding(0, (base.Height - base.Image.Height) / 2, 0, 0);
                    drawPoint = new PointF(base.Image.Width,(base.Height - drawStringSize.Height)/ 2);
                }
                else
                {
                    drawPoint = new PointF((base.Width- drawStringSize.Width) / 2,base.Height / 2 - drawStringSize.Height / 2);
                }
                //      this.btnQuery.ItemTextAlignment = System.Drawing.StringAlignment.Near;
                using (SolidBrush drawBrush = new SolidBrush(this.FontColor))
                {
                    pe.Graphics.DrawString(base.Text, base.Font, drawBrush, drawPoint);
                }

            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_toolTip != null)
                    _toolTip.Dispose();
            }
            _toolTip = null;
            base.Dispose(disposing);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            Refresh();
            base.OnTextChanged(e);
        }

        #endregion

        #region Private

        private void ShowTooTip(string toolTipText)
        {
            _toolTip.Active = true;
            _toolTip.SetToolTip(this, toolTipText);
        }

        private void HideToolTip()
        {
            _toolTip.Active = false;
        }

        #endregion
    }
}
