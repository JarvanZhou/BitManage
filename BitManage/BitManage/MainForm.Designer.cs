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
            this.button1 = new System.Windows.Forms.Button();
            this.picPanel = new BitManage.MidControler.BitmapPanel();
            this.imgListPanel = new BitManage.MidControler.ImageListControl();
            this.contentControl = new BitManage.MidControler.ContentControl();
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
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 331F));
            this.tableLayoutPanel1.Controls.Add(this.button1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.picPanel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.contentControl, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 42);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1118, 610);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(244, 65);
            this.button1.TabIndex = 1;
            this.button1.Text = "加载图片";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // picPanel
            // 
            this.picPanel.Bit = null;
            this.picPanel.Controls.Add(this.imgListPanel);
            this.picPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picPanel.Loc = ((System.Drawing.PointF)(resources.GetObject("picPanel.Loc")));
            this.picPanel.Location = new System.Drawing.Point(255, 4);
            this.picPanel.Name = "picPanel";
            this.picPanel.Size = new System.Drawing.Size(527, 602);
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
            this.imgListPanel.Size = new System.Drawing.Size(527, 602);
            this.imgListPanel.TabIndex = 0;
            this.imgListPanel.ItemDoubleClick += new System.EventHandler(this.imgListPanel_ItemDoubleClick);
            this.imgListPanel.ItemClick += new System.EventHandler(this.imgListPanel_ItemClick);
            this.imgListPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.imgListPanel_MouseClick);
            // 
            // contentControl
            // 
            this.contentControl.Location = new System.Drawing.Point(789, 4);
            this.contentControl.Name = "contentControl";
            this.contentControl.Size = new System.Drawing.Size(283, 602);
            this.contentControl.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1105, 654);
            this.Controls.Add(this.tableLayoutPanel1);
            this.IsAllowMenu = false;
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.NewBckColor = System.Drawing.Color.WhiteSmoke;
            this.NewLeftMenuColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(197)))), ((int)(((byte)(254)))));
            this.NewRightMenuColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(197)))), ((int)(((byte)(254)))));
            this.NewShadowWidth = 3;
            this.Text = "图片标引工具";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.picPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private MidControler.ImageListControl imgListPanel;
        private System.Windows.Forms.Button button1;
        private BitManage.MidControler.BitmapPanel picPanel;
        private MidControler.ContentControl contentControl;
    }
}

