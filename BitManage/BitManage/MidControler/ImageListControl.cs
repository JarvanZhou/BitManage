using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BitManage.MidControler
{
    public class ImageListControl : FlowLayoutPanel
    {
        public ImageListControl()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.AutoScroll = true;
        }
        public event EventHandler ItemDoubleClick
        {
            add { _itemdoubleclick += value; }
            remove{ _itemdoubleclick -= value; }
        }
        public void Load(string[] files)
        {
            this.Controls.Clear();
            this.SuspendLayout();
            foreach (string str in files)
            {
                ImgPanel img = new ImgPanel();
                img.MouseClick += Img_MouseClick;
                img.MouseDoubleClick += (sender, e) => { _itemdoubleclick?.Invoke(sender, e); };
                this.Controls.Add(img);
            }
            this.ResumeLayout();
            _index = 0;
            _count = files.Length;
            _threadindex = 0;
            _files = files;
            _start = 0;
            Thread[] thread = new Thread[_threadcount];
            for (int i = 0; i < _threadcount; ++i)
            {
                thread[i] = new Thread(new ThreadStart(Start));
                thread[i].IsBackground = true;
                thread[i].Start();
            }
        }
        /// <summary>
        /// 控件选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Img_MouseClick(object sender, MouseEventArgs e)
        {
            ImgPanel img = sender as ImgPanel;
            if (Control.ModifierKeys == Keys.Control)
            {
                img.Selected = !img.Selected;
                _start = this.Controls.GetChildIndex(img);
            }
            else if (Control.ModifierKeys == Keys.Shift)
            {
                int index = this.Controls.GetChildIndex(img);
                int start = _start > index ? index : _start;
                int end= _start > index ? _start : index;
                int len = this.Controls.Count;
                for (int i = 0; i < len; ++i)
                {
                    if (i >= start && i <= end)
                    { (this.Controls[i] as ImgPanel).Selected = true; }
                    else
                    { (this.Controls[i] as ImgPanel).Selected = false; }
                }
            }
            else
            {
                foreach (ImgPanel panel in this.Controls)
                    panel.Selected = false;
                img.Selected = true;
                _start = this.Controls.GetChildIndex(img);
            }
        }

        private void Start()
        {
            while (_index < _count)
            {
                string file = "";
                int index = 0;
                lock (this)
                {
                    if (_index >= _count)
                        break;
                    file = _files[_index];
                    index = _index;
                    _index++;
                }
                (this.Controls[index] as ImgPanel).File = file;
            }
            lock (this)
            {
                if (_threadindex < _threadcount)
                    _threadindex++;
            }
        }

        private int _index;
        private int _count;
        private int _threadindex;
        private int _threadcount = 6;
        private string[] _files;
        private int _start = 0;
        private EventHandler _itemdoubleclick;
    }
}
