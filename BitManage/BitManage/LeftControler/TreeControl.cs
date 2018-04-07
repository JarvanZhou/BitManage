using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BitManage.LeftControler
{
    public partial class TreeControl : UserControl
    {
        public TreeControl()
        {
            InitializeComponent();
        }

        public event DirectoryChangedHandler DirectoryChanged
        {
            add { _directoryChanged += value; }
            remove { _directoryChanged -= value; }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            dirTreeview.Nodes.Clear();
            dirTreeview.Nodes.Add(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "桌面", 0);
            TreeNode node = dirTreeview.Nodes.Add("我的电脑", "我的电脑", 1);
            DriveInfo[] drive = DriveInfo.GetDrives();
            foreach (var dr in drive)
            {
                if (!dr.IsReady) continue;
                TreeNode tn = node.Nodes.Add(dr.Name, "本地磁盘 " + dr.Name.Substring(0, 1), 2);
                string[] ds = Directory.GetDirectories(dr.Name);
                if (ds.Length > 0)
                {
                    foreach (string d in ds)
                    {
                        DirectoryInfo di = new DirectoryInfo(d);
                        if ((di.Attributes & FileAttributes.System) == FileAttributes.System || (di.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden) continue;
                        tn.Nodes.Add(d, d.Substring(d.LastIndexOf("\\") + 1), 3);
                    }
                }
            }
            string[] dirs = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            foreach (string dir in dirs)
            {
                node = dirTreeview.Nodes.Add(dir, dir.Substring(dir.LastIndexOf("\\") + 1), 3);
                string[] ds = Directory.GetDirectories(dir);
                if (ds.Length > 0)
                {
                    foreach (string d in ds)
                    {
                        DirectoryInfo di = new DirectoryInfo(d);
                        if ((di.Attributes & FileAttributes.System) == FileAttributes.System || (di.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden) continue;
                        node.Nodes.Add(d, d.Substring(d.LastIndexOf("\\") + 1), 3);
                    }
                }
            }

        }
        private void dicTreeview_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            e.Node.SelectedImageIndex = e.Node.ImageIndex;
            if (!Directory.Exists(e.Node.Name))
            { return; }
        }

        private void dicTreeview_AfterSelect(object sender, TreeViewEventArgs e)
        {
            _directoryChanged?.Invoke(e.Node, new DirectoryChangedEventArgs(e.Node.Name));
        }
        private void dicTreeview_AfterExpand(object sender, TreeViewEventArgs e)
        {
            try
            {
                dirTreeview.SuspendLayout();
                foreach (TreeNode node in e.Node.Nodes)
                {
                    string[] ds = Directory.GetDirectories(node.Name);
                    if (ds.Length > 0)
                    {
                        foreach (string d in ds)
                        {
                            DirectoryInfo di = new DirectoryInfo(d);
                            if ((di.Attributes & FileAttributes.System) == FileAttributes.System || (di.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden) continue;
                            node.Nodes.Add(d, d.Substring(d.LastIndexOf("\\") + 1), 3);
                        }
                    }
                }
            }
            finally
            { dirTreeview.ResumeLayout(); }
        }

        private DirectoryChangedHandler _directoryChanged;

    }
    public delegate void DirectoryChangedHandler(object sender, DirectoryChangedEventArgs e);
    public class DirectoryChangedEventArgs : EventArgs
    {
        public DirectoryChangedEventArgs(string path)
        {
            DirectoryPath = path;
        }
        /// <summary>
        /// 文件夹路径
        /// </summary>
        public string DirectoryPath
        { get; private set; }
    }
}
