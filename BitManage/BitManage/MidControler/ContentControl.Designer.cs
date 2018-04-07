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
            this.pg_context = new System.Windows.Forms.PropertyGrid();
            this.btn_submit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pg_context
            // 
            this.pg_context.Location = new System.Drawing.Point(3, 0);
            this.pg_context.Name = "pg_context";
            this.pg_context.Size = new System.Drawing.Size(277, 545);
            this.pg_context.TabIndex = 0;
            // 
            // btn_submit
            // 
            this.btn_submit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_submit.Location = new System.Drawing.Point(67, 551);
            this.btn_submit.Name = "btn_submit";
            this.btn_submit.Size = new System.Drawing.Size(124, 42);
            this.btn_submit.TabIndex = 1;
            this.btn_submit.Text = "提交修改";
            this.btn_submit.UseVisualStyleBackColor = true;
            this.btn_submit.Click += new System.EventHandler(this.btn_submit_Click);
            // 
            // ContentControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_submit);
            this.Controls.Add(this.pg_context);
            this.Name = "ContentControl";
            this.Size = new System.Drawing.Size(283, 608);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid pg_context;
        private System.Windows.Forms.Button btn_submit;
    }
}
