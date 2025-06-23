namespace DDLParserWV
{
    partial class DDLParserForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.scanBinaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.hb1 = new Be.Windows.Forms.HexBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.rtb1 = new System.Windows.Forms.RichTextBox();
            this.radioButtonJson = new System.Windows.Forms.RadioButton();
            this.radioButtonMarkdown = new System.Windows.Forms.RadioButton();
            this.radioButtonDebug = new System.Windows.Forms.RadioButton();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scanBinaryToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(515, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // scanBinaryToolStripMenuItem
            // 
            this.scanBinaryToolStripMenuItem.Name = "scanBinaryToolStripMenuItem";
            this.scanBinaryToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.scanBinaryToolStripMenuItem.Text = "Scan binary";
            this.scanBinaryToolStripMenuItem.Click += new System.EventHandler(this.ScanBinaryToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(515, 366);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.hb1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(507, 340);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Hex View";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // hb1
            // 
            this.hb1.BoldFont = null;
            this.hb1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hb1.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hb1.LineInfoForeColor = System.Drawing.Color.Empty;
            this.hb1.LineInfoVisible = true;
            this.hb1.Location = new System.Drawing.Point(3, 3);
            this.hb1.Name = "hb1";
            this.hb1.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.hb1.Size = new System.Drawing.Size(501, 334);
            this.hb1.StringViewVisible = true;
            this.hb1.TabIndex = 0;
            this.hb1.UseFixedBytesPerLine = true;
            this.hb1.VScrollBarVisible = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.rtb1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(507, 340);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Parsed Output";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // rtb1
            // 
            this.rtb1.DetectUrls = false;
            this.rtb1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb1.Font = new System.Drawing.Font("Courier New", 8.25F);
            this.rtb1.HideSelection = false;
            this.rtb1.Location = new System.Drawing.Point(3, 3);
            this.rtb1.Name = "rtb1";
            this.rtb1.Size = new System.Drawing.Size(501, 334);
            this.rtb1.TabIndex = 1;
            this.rtb1.Text = "";
            this.rtb1.WordWrap = false;
            // 
            // radioButtonJson
            // 
            this.radioButtonJson.AutoSize = true;
            this.radioButtonJson.Checked = true;
            this.radioButtonJson.Location = new System.Drawing.Point(94, 3);
            this.radioButtonJson.Name = "radioButtonJson";
            this.radioButtonJson.Size = new System.Drawing.Size(53, 17);
            this.radioButtonJson.TabIndex = 2;
            this.radioButtonJson.TabStop = true;
            this.radioButtonJson.Text = "JSON";
            this.radioButtonJson.UseVisualStyleBackColor = true;
            this.radioButtonJson.CheckedChanged += new System.EventHandler(this.RadioButtonJson_CheckedChanged);
            // 
            // radioButtonMarkdown
            // 
            this.radioButtonMarkdown.AutoSize = true;
            this.radioButtonMarkdown.Location = new System.Drawing.Point(154, 3);
            this.radioButtonMarkdown.Name = "radioButtonMarkdown";
            this.radioButtonMarkdown.Size = new System.Drawing.Size(75, 17);
            this.radioButtonMarkdown.TabIndex = 3;
            this.radioButtonMarkdown.Text = "Markdown";
            this.radioButtonMarkdown.UseVisualStyleBackColor = true;
            this.radioButtonMarkdown.CheckedChanged += new System.EventHandler(this.RadioButtonMarkdown_CheckedChanged);
            // 
            // radioButtonDebug
            // 
            this.radioButtonDebug.AutoSize = true;
            this.radioButtonDebug.Location = new System.Drawing.Point(234, 3);
            this.radioButtonDebug.Name = "radioButtonDebug";
            this.radioButtonDebug.Size = new System.Drawing.Size(57, 17);
            this.radioButtonDebug.TabIndex = 4;
            this.radioButtonDebug.TabStop = true;
            this.radioButtonDebug.Text = "Debug";
            this.radioButtonDebug.UseVisualStyleBackColor = true;
            this.radioButtonDebug.CheckedChanged += new System.EventHandler(this.RadioButtonDebug_CheckedChanged);
            // 
            // DDLParserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 390);
            this.Controls.Add(this.radioButtonDebug);
            this.Controls.Add(this.radioButtonMarkdown);
            this.Controls.Add(this.radioButtonJson);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "DDLParserForm";
            this.Text = "Quazal DDL Parser";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox rtb1;
        private Be.Windows.Forms.HexBox hb1;
        private System.Windows.Forms.ToolStripMenuItem scanBinaryToolStripMenuItem;
        private System.Windows.Forms.RadioButton radioButtonJson;
        private System.Windows.Forms.RadioButton radioButtonMarkdown;
        private System.Windows.Forms.RadioButton radioButtonDebug;
    }
}

