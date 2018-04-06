using BitManage.MidControler;
using MQXS.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BitManage
{
    public partial class MainForm : MenuForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles("H:\\", "*.jpg");
            imgListPanel.Load(files);
        }

        private void imgListPanel_ItemDoubleClick(object sender, EventArgs e)
        {
            ImgPanel img = (sender as ImgPanel);
            if (!string.IsNullOrEmpty(img.File))
            {
                Bitmap bit = new Bitmap(img.File);
                picPanel.Bit = bit;
                imgListPanel.Visible = false;
            }
        }

        private void picPanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            imgListPanel.Visible = true;
            picPanel.Bit = null;
        }
    }
}
