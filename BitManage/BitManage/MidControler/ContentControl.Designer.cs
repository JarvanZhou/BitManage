namespace BitManage.MidControler
{
    partial class ContentControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContentControl));
            this.pg_context = new System.Windows.Forms.PropertyGrid();
            this.btnUpload = new System.Windows.Forms.PictureBox();
            this.btnDownload = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnUpload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDownload)).BeginInit();
            this.SuspendLayout();
            // 
            // pg_context
            // 
            this.pg_context.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pg_context.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pg_context.LineColor = System.Drawing.SystemColors.ControlDark;
            this.pg_context.Location = new System.Drawing.Point(0, 0);
            this.pg_context.Name = "pg_context";
            this.pg_context.Size = new System.Drawing.Size(283, 398);
            this.pg_context.TabIndex = 0;
            // 
            // btnUpload
            // 
            this.btnUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpload.BackColor = System.Drawing.Color.Transparent;
            this.btnUpload.Image = ((System.Drawing.Image)(resources.GetObject("btnUpload.Image")));
            this.btnUpload.Location = new System.Drawing.Point(209, 0);
            this.btnUpload.Margin = new System.Windows.Forms.Padding(0);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(37, 26);
            this.btnUpload.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnUpload.TabIndex = 2;
            this.btnUpload.TabStop = false;
            this.btnUpload.Paint += new System.Windows.Forms.PaintEventHandler(this.btn_Paint);
            this.btnUpload.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnUpload.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btn_MouseMove);
            this.btnUpload.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnUpload_MouseUp);
            // 
            // btnDownload
            // 
            this.btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownload.BackColor = System.Drawing.Color.Transparent;
            this.btnDownload.Image = ((System.Drawing.Image)(resources.GetObject("btnDownload.Image")));
            this.btnDownload.Location = new System.Drawing.Point(246, 0);
            this.btnDownload.Margin = new System.Windows.Forms.Padding(0);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(37, 26);
            this.btnDownload.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnDownload.TabIndex = 3;
            this.btnDownload.TabStop = false;
            this.btnDownload.Paint += new System.Windows.Forms.PaintEventHandler(this.btn_Paint);
            this.btnDownload.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnDownload.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btn_MouseMove);
            this.btnDownload.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnDownload_MouseUp);
            // 
            // ContentControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.pg_context);
            this.Name = "ContentControl";
            this.Size = new System.Drawing.Size(283, 398);
            ((System.ComponentModel.ISupportInitialize)(this.btnUpload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDownload)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid pg_context;
        private System.Windows.Forms.PictureBox btnUpload;
        private System.Windows.Forms.PictureBox btnDownload;
    }
}
