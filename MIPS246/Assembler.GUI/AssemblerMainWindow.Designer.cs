namespace Assembler.GUI
{
    partial class AssemblerMainWindow
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssemblerMainWindow));
            this.Assemble = new System.Windows.Forms.Button();
            this.SourceRichTextBox = new System.Windows.Forms.RichTextBox();
            this.SourceFilePathTextBox = new System.Windows.Forms.TextBox();
            this.SourcePathFileButton = new System.Windows.Forms.Button();
            this.AssembleButton = new System.Windows.Forms.Button();
            this.SourceFileBrowseLabel = new System.Windows.Forms.Label();
            this.OutputRichTextBox = new System.Windows.Forms.RichTextBox();
            this.OutputFileButton = new System.Windows.Forms.Button();
            this.OutputFileCheckBox = new System.Windows.Forms.CheckBox();
            this.OutputPathTextBox = new System.Windows.Forms.TextBox();
            this.BinaryRadioButton = new System.Windows.Forms.RadioButton();
            this.HEXRadioButton = new System.Windows.Forms.RadioButton();
            this.SourceLabel = new System.Windows.Forms.Label();
            this.AssembledLabel = new System.Windows.Forms.Label();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // Assemble
            // 
            this.Assemble.Location = new System.Drawing.Point(697, 726);
            this.Assemble.Name = "Assemble";
            this.Assemble.Size = new System.Drawing.Size(75, 23);
            this.Assemble.TabIndex = 0;
            this.Assemble.Text = "Assemble";
            this.Assemble.UseVisualStyleBackColor = true;
            // 
            // SourceRichTextBox
            // 
            this.SourceRichTextBox.Location = new System.Drawing.Point(12, 61);
            this.SourceRichTextBox.Name = "SourceRichTextBox";
            this.SourceRichTextBox.Size = new System.Drawing.Size(377, 430);
            this.SourceRichTextBox.TabIndex = 1;
            this.SourceRichTextBox.Text = "";
            // 
            // SourceFilePathTextBox
            // 
            this.SourceFilePathTextBox.Location = new System.Drawing.Point(95, 12);
            this.SourceFilePathTextBox.Name = "SourceFilePathTextBox";
            this.SourceFilePathTextBox.Size = new System.Drawing.Size(596, 21);
            this.SourceFilePathTextBox.TabIndex = 2;
            // 
            // SourcePathFileButton
            // 
            this.SourcePathFileButton.Location = new System.Drawing.Point(697, 10);
            this.SourcePathFileButton.Name = "SourcePathFileButton";
            this.SourcePathFileButton.Size = new System.Drawing.Size(75, 23);
            this.SourcePathFileButton.TabIndex = 3;
            this.SourcePathFileButton.Text = "Browse";
            this.SourcePathFileButton.UseVisualStyleBackColor = true;
            this.SourcePathFileButton.Click += new System.EventHandler(this.SourcePathFileButton_Click);
            // 
            // AssembleButton
            // 
            this.AssembleButton.ForeColor = System.Drawing.Color.Red;
            this.AssembleButton.Location = new System.Drawing.Point(697, 526);
            this.AssembleButton.Name = "AssembleButton";
            this.AssembleButton.Size = new System.Drawing.Size(75, 23);
            this.AssembleButton.TabIndex = 4;
            this.AssembleButton.Text = "Assemble";
            this.AssembleButton.UseVisualStyleBackColor = true;
            this.AssembleButton.Click += new System.EventHandler(this.AssembleButton_Click);
            // 
            // SourceFileBrowseLabel
            // 
            this.SourceFileBrowseLabel.AutoSize = true;
            this.SourceFileBrowseLabel.Location = new System.Drawing.Point(12, 15);
            this.SourceFileBrowseLabel.Name = "SourceFileBrowseLabel";
            this.SourceFileBrowseLabel.Size = new System.Drawing.Size(77, 12);
            this.SourceFileBrowseLabel.TabIndex = 5;
            this.SourceFileBrowseLabel.Text = "Source File:";
            // 
            // OutputRichTextBox
            // 
            this.OutputRichTextBox.Location = new System.Drawing.Point(395, 61);
            this.OutputRichTextBox.Name = "OutputRichTextBox";
            this.OutputRichTextBox.ReadOnly = true;
            this.OutputRichTextBox.Size = new System.Drawing.Size(377, 430);
            this.OutputRichTextBox.TabIndex = 6;
            this.OutputRichTextBox.Text = "";
            // 
            // OutputFileButton
            // 
            this.OutputFileButton.Enabled = false;
            this.OutputFileButton.Location = new System.Drawing.Point(697, 497);
            this.OutputFileButton.Name = "OutputFileButton";
            this.OutputFileButton.Size = new System.Drawing.Size(75, 23);
            this.OutputFileButton.TabIndex = 7;
            this.OutputFileButton.Text = "Browse";
            this.OutputFileButton.UseVisualStyleBackColor = true;
            this.OutputFileButton.Click += new System.EventHandler(this.OutputFileButton_Click);
            // 
            // OutputFileCheckBox
            // 
            this.OutputFileCheckBox.AutoSize = true;
            this.OutputFileCheckBox.Location = new System.Drawing.Point(14, 501);
            this.OutputFileCheckBox.Name = "OutputFileCheckBox";
            this.OutputFileCheckBox.Size = new System.Drawing.Size(90, 16);
            this.OutputFileCheckBox.TabIndex = 8;
            this.OutputFileCheckBox.Text = "Output File";
            this.OutputFileCheckBox.UseVisualStyleBackColor = true;
            this.OutputFileCheckBox.CheckedChanged += new System.EventHandler(this.OutputFileCheckBox_CheckedChanged);
            // 
            // OutputPathTextBox
            // 
            this.OutputPathTextBox.Enabled = false;
            this.OutputPathTextBox.Location = new System.Drawing.Point(110, 499);
            this.OutputPathTextBox.Name = "OutputPathTextBox";
            this.OutputPathTextBox.Size = new System.Drawing.Size(581, 21);
            this.OutputPathTextBox.TabIndex = 9;
            // 
            // BinaryRadioButton
            // 
            this.BinaryRadioButton.AutoSize = true;
            this.BinaryRadioButton.Location = new System.Drawing.Point(713, 39);
            this.BinaryRadioButton.Name = "BinaryRadioButton";
            this.BinaryRadioButton.Size = new System.Drawing.Size(59, 16);
            this.BinaryRadioButton.TabIndex = 10;
            this.BinaryRadioButton.TabStop = true;
            this.BinaryRadioButton.Text = "Binary";
            this.BinaryRadioButton.UseVisualStyleBackColor = true;
            this.BinaryRadioButton.CheckedChanged += new System.EventHandler(this.BinaryRadioButton_CheckedChanged);
            // 
            // HEXRadioButton
            // 
            this.HEXRadioButton.AutoSize = true;
            this.HEXRadioButton.Checked = true;
            this.HEXRadioButton.Location = new System.Drawing.Point(666, 39);
            this.HEXRadioButton.Name = "HEXRadioButton";
            this.HEXRadioButton.Size = new System.Drawing.Size(41, 16);
            this.HEXRadioButton.TabIndex = 11;
            this.HEXRadioButton.TabStop = true;
            this.HEXRadioButton.Text = "HEX";
            this.HEXRadioButton.UseVisualStyleBackColor = true;
            this.HEXRadioButton.CheckedChanged += new System.EventHandler(this.HEXRadioButton_CheckedChanged);
            // 
            // SourceLabel
            // 
            this.SourceLabel.AutoSize = true;
            this.SourceLabel.Location = new System.Drawing.Point(12, 41);
            this.SourceLabel.Name = "SourceLabel";
            this.SourceLabel.Size = new System.Drawing.Size(41, 12);
            this.SourceLabel.TabIndex = 12;
            this.SourceLabel.Text = "Source";
            // 
            // AssembledLabel
            // 
            this.AssembledLabel.AutoSize = true;
            this.AssembledLabel.Location = new System.Drawing.Point(395, 40);
            this.AssembledLabel.Name = "AssembledLabel";
            this.AssembledLabel.Size = new System.Drawing.Size(59, 12);
            this.AssembledLabel.TabIndex = 13;
            this.AssembledLabel.Text = "Assembled";
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.FileName = "FileDialog";
            // 
            // AssemblerMainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.AssembledLabel);
            this.Controls.Add(this.SourceLabel);
            this.Controls.Add(this.HEXRadioButton);
            this.Controls.Add(this.BinaryRadioButton);
            this.Controls.Add(this.OutputPathTextBox);
            this.Controls.Add(this.OutputFileCheckBox);
            this.Controls.Add(this.OutputFileButton);
            this.Controls.Add(this.OutputRichTextBox);
            this.Controls.Add(this.SourceFileBrowseLabel);
            this.Controls.Add(this.AssembleButton);
            this.Controls.Add(this.SourcePathFileButton);
            this.Controls.Add(this.SourceFilePathTextBox);
            this.Controls.Add(this.SourceRichTextBox);
            this.Controls.Add(this.Assemble);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AssemblerMainWindow";
            this.Text = "MIPS246 Assembler";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Assemble;
        private System.Windows.Forms.RichTextBox SourceRichTextBox;
        private System.Windows.Forms.TextBox SourceFilePathTextBox;
        private System.Windows.Forms.Button SourcePathFileButton;
        private System.Windows.Forms.Button AssembleButton;
        private System.Windows.Forms.Label SourceFileBrowseLabel;
        private System.Windows.Forms.RichTextBox OutputRichTextBox;
        private System.Windows.Forms.Button OutputFileButton;
        private System.Windows.Forms.CheckBox OutputFileCheckBox;
        private System.Windows.Forms.TextBox OutputPathTextBox;
        private System.Windows.Forms.RadioButton BinaryRadioButton;
        private System.Windows.Forms.RadioButton HEXRadioButton;
        private System.Windows.Forms.Label SourceLabel;
        private System.Windows.Forms.Label AssembledLabel;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog;
    }
}

