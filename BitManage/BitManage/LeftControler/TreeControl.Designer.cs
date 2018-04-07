namespace BitManage.LeftControler
{
    partial class TreeControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TreeControl));
            this.dirTreeview = new System.Windows.Forms.TreeView();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // dirTreeview
            // 
            this.dirTreeview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dirTreeview.ImageIndex = 0;
            this.dirTreeview.ImageList = this.imgList;
            this.dirTreeview.Location = new System.Drawing.Point(0, 0);
            this.dirTreeview.Name = "dirTreeview";
            this.dirTreeview.SelectedImageIndex = 0;
            this.dirTreeview.Size = new System.Drawing.Size(247, 339);
            this.dirTreeview.TabIndex = 0;
            this.dirTreeview.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.dicTreeview_AfterExpand);
            this.dirTreeview.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.dicTreeview_AfterSelect);
            this.dirTreeview.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.dicTreeview_NodeMouseClick);
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "桌面.png");
            this.imgList.Images.SetKeyName(1, "我的电脑.png");
            this.imgList.Images.SetKeyName(2, "磁盘.png");
            this.imgList.Images.SetKeyName(3, "文件夹.png");
            // 
            // TreeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dirTreeview);
            this.Name = "TreeControl";
            this.Size = new System.Drawing.Size(247, 339);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView dirTreeview;
        private System.Windows.Forms.ImageList imgList;
    }
}
