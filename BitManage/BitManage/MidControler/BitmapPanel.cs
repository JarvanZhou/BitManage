using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BitManage.MidControler
{
    public class BitmapPanel : Panel
    {
        public BitmapPanel()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            Zoom = 1;
            Loc = new PointF(0, 0);
            this.MouseWheel += BitmapPanel_MouseWheel;
        }

        public Bitmap Bit
        {
            get
            { return _bit; }
            set
            {
                if (_bit != null)
                { _bit.Dispose(); _bit = null; }
                _bit = value;
                if (_bit == null) return;
                InitBit();
                this.Invalidate();
            }
        }

        public float Zoom
        { get; set; }
        public PointF Loc
        { get; set; }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            _ismousedown = true;
            _point = new Point(e.X, e.Y);
            this.Focus();
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (_ismousedown)
            {
                Loc = new PointF(Loc.X + e.X - _point.X, Loc.Y + e.Y - _point.Y);
                _point = new Point(e.X, e.Y);
                this.Invalidate();
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _ismousedown = false;
        }
        private void BitmapPanel_MouseWheel(object sender, MouseEventArgs e)
        {
            float px = (e.X - Loc.X) / Zoom;
            float py = (e.Y - Loc.Y) / Zoom;
            if (e.Delta > 0)
            {
                Zoom *= 5.0f / 4;
                if (Zoom > 20)
                    Zoom = 20;
            }
            else
            {
                Zoom *= 4.0f / 5;
                if (Zoom < 0.01)
                    Zoom = 0.01f;
            }
            float pxx = (e.X - Loc.X) / Zoom;
            float pyy = (e.Y - Loc.Y) / Zoom;
            Loc = new PointF(Loc.X + (pxx - px) * Zoom, Loc.Y + (pyy - py) * Zoom);
            this.Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (_bit != null)
            {
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                e.Graphics.DrawImage(_bit, new Rectangle((int)Loc.X, (int)Loc.Y, (int)(_bit.Width * Zoom), (int)(_bit.Height * Zoom)), new Rectangle(0, 0, _bit.Width, _bit.Height), GraphicsUnit.Pixel);
            }
        }
        private void InitBit()
        {
            if (_bit != null)
            {
                if ((float)_bit.Width / _bit.Height > (float)this.Width / this.Height)
                {
                    Zoom = (float)this.Width / _bit.Width;
                    Loc = new PointF(0, (this.Height - _bit.Height * Zoom) / 2);
                }
                else
                {
                    Zoom = (float)this.Height / _bit.Height;
                    Loc = new PointF((this.Width - _bit.Width * Zoom) / 2, 0);
                }
            }
        }

        private bool _ismousedown;
        private Point _point;
        private Bitmap _bit;
    }
}
