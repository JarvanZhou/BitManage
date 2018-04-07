namespace BitManage
{
    partial class MainForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.picPanel = new BitManage.MidControler.BitmapPanel();
            this.imgListPanel = new BitManage.MidControler.ImageListControl();
            this.contentControl = new BitManage.MidControler.ContentControl();
            this.dirTreeview = new BitManage.LeftControler.TreeControl();
            this.tableLayoutPanel1.SuspendLayout();
            this.picPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 278F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 294F));
            this.tableLayoutPanel1.Controls.Add(this.picPanel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.contentControl, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.dirTreeview, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 44);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1128, 605);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // picPanel
            // 
            this.picPanel.Bit = null;
            this.picPanel.Controls.Add(this.imgListPanel);
            this.picPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picPanel.Loc = ((System.Drawing.PointF)(resources.GetObject("picPanel.Loc")));
            this.picPanel.Location = new System.Drawing.Point(283, 4);
            this.picPanel.Name = "picPanel";
            this.picPanel.Size = new System.Drawing.Size(546, 597);
            this.picPanel.TabIndex = 1;
            this.picPanel.Zoom = 1F;
            this.picPanel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.picPanel_MouseDoubleClick);
            // 
            // imgListPanel
            // 
            this.imgListPanel.AutoScroll = true;
            this.imgListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imgListPanel.Location = new System.Drawing.Point(0, 0);
            this.imgListPanel.Name = "imgListPanel";
            this.imgListPanel.Size = new System.Drawing.Size(546, 597);
            this.imgListPanel.TabIndex = 0;
            this.imgListPanel.ItemDoubleClick += new System.EventHandler(this.imgListPanel_ItemDoubleClick);
            this.imgListPanel.ItemClick += new System.EventHandler(this.imgListPanel_ItemClick);
            this.imgListPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.imgListPanel_MouseClick);
            // 
            // contentControl
            // 
            this.contentControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentControl.Location = new System.Drawing.Point(836, 4);
            this.contentControl.Name = "contentControl";
            this.contentControl.Size = new System.Drawing.Size(288, 597);
            this.contentControl.TabIndex = 0;
            // 
            // dirTreeview
            // 
            this.dirTreeview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dirTreeview.Location = new System.Drawing.Point(4, 4);
            this.dirTreeview.Name = "dirTreeview";
            this.dirTreeview.Size = new System.Drawing.Size(272, 597);
            this.dirTreeview.TabIndex = 2;
            this.dirTreeview.DirectoryChanged += new BitManage.LeftControler.DirectoryChangedHandler(this.dirTreeview_DirectoryChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1135, 650);
            this.Controls.Add(this.tableLayoutPanel1);
            this.IsAllowMenu = false;
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.NewBckColor = System.Drawing.Color.WhiteSmoke;
            this.NewLeftMenuColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(197)))), ((int)(((byte)(254)))));
            this.NewRightMenuColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(197)))), ((int)(((byte)(254)))));
            this.NewShadowWidth = 3;
            this.NewText = "图片标引工具";
            this.NewTextFont = new System.Drawing.Font("楷体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Text = "图片标引工具";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.picPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private MidControler.ImageListControl imgListPanel;
        private BitManage.MidControler.BitmapPanel picPanel;
        private MidControler.ContentControl contentControl;
        private LeftControler.TreeControl dirTreeview;
    }
}

