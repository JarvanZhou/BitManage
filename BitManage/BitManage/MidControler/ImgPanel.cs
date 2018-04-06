using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BitManage.MidControler
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
                    Bitmap dst = null;
                    if ((float)bit.Width / bit.Height > (float)6 / 5)
                    {
                        dst = new Bitmap(120, 120 * bit.Height / bit.Width);
                    }
                    else
                    { dst = new Bitmap(100 * bit.Width / bit.Height, 100); }
                    Imaging.BitmapThum.GetThum(bit, dst);
                    _bit = dst;
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
            using (Pen pen = new Pen(Color.YellowGreen, 2))
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
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString(Path.GetFileNameWithoutExtension(_file), this.Font, Brushes.Black, new Rectangle(10, this.Height- 25,this.Width-10,25), sf);
                sf.Dispose();
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
