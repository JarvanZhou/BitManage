using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BitManage.MinControler
{
    public class ImgPanel : Panel
    {
        public ImgPanel()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.Size = new System.Drawing.Size(165, 145);
        }
        /// <summary>
        /// 获取或设置是否选中
        /// </summary>
        public bool Selected
        {
            get { return _selected; }
            set { _selected = value; this.Invalidate(); }
        }
        /// <summary>
        /// 获取或设置图片路径
        /// </summary>
        public string File
        {
            get { return _file; }
            set
            {
                if (_bit != null)
                { _bit.Dispose(); _bit = null; }
                if (_file != value)
                {
                    _file = value;
                    Bitmap bit = new Bitmap(_file);
                    if ((float)bit.Width / bit.Height > (float)6 / 5)
                    {
                        _bit = new Bitmap(bit, new Size(120, 120 * bit.Height / bit.Width));
                    }
                    else
                    { _bit = new Bitmap(bit, new Size(100 * bit.Width / bit.Height, 100)); }
                    this.Invalidate();
                }
            }
        }
        /// <summary>
        /// 绘画
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Pen pen = new Pen(Color.Gray, 2))
                e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, this.Width - 2, this.Height - 2));
            if (_bit != null)
            {
                Rectangle rect = new Rectangle((this.Width - _bit.Width) / 2, (this.Height - _bit.Height) / 2, _bit.Width, _bit.Height);
                rect.Offset(1, 1);
                e.Graphics.DrawImage(_bit, rect);
                e.Graphics.DrawRectangle(Pens.Gold, rect);
            }
            if (!string.IsNullOrEmpty(_file))
            {
                e.Graphics.DrawString(Path.GetExtension(_file).ToUpper(), this.Font, Brushes.Red, new Point(this.Width - 35, 10));
            }
            if (_selected)
            {
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(50, Color.Blue)))
                    e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }


        private string _file;
        private bool _selected;
        private Bitmap _bit;
    }
}
