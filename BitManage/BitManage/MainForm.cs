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
            string[] files = Directory.GetFiles(@"C:\Users\jarvan\Desktop\pic", "*.png");
            imgListPanel.Load(files);
        }

        private void imgListPanel_ItemDoubleClick(object sender, EventArgs e)
        {
            ImgPanel img = (sender as ImgPanel);
            if (!string.IsNullOrEmpty(img.File))
            {
                Bitmap bit = new Bitmap(img.File);
                picPanel.Bit = bit.Clone() as Bitmap;
                imgListPanel.Visible = false;
                bit.Dispose();
                bit = null;
            }
        }

        private void picPanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            imgListPanel.Visible = true;
            picPanel.Bit = null;
        }

        private void imgListPanel_ItemClick(object sender, EventArgs e)
        {
            try
            {
                this.imgListPanel.Focus();

                string[] imgArray = this.imgListPanel.GetSelectedImg();
                if (imgArray.Length > 0)
                {
                    this.contentControl.SetBitContent(imgArray);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void imgListPanel_MouseClick(object sender, MouseEventArgs e)
        {
            this.imgListPanel.Focus();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                this.imgListPanel.CtrlA();
                imgListPanel_ItemClick(null, null);
            }
        }
    }
}
