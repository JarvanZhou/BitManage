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
        /// <summary>
        /// 切换目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dirTreeview_DirectoryChanged(object sender, LeftControler.DirectoryChangedEventArgs e)
        {
            if (Directory.Exists(e.DirectoryPath))
            {
                string[] png = Directory.GetFiles(e.DirectoryPath, "*.png");
                string[] jpg = Directory.GetFiles(e.DirectoryPath, "*.jpg");
                string[] jpeg = Directory.GetFiles(e.DirectoryPath, "*.jpeg");
                string[] bmp = Directory.GetFiles(e.DirectoryPath, "*.bmp");
                List<string> files = new List<string>();
                files.AddRange(png);
                files.AddRange(jpg);
                files.AddRange(jpeg);
                files.AddRange(bmp);
                files.Sort();
                imgListPanel.Load(files.ToArray());
                imgListPanel.Visible = true;
            }
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
