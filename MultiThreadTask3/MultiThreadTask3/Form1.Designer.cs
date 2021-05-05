
namespace MultiThreadTask3
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.parsingProgressBar = new System.Windows.Forms.ProgressBar();
            this.pause_button = new System.Windows.Forms.Button();
            this.cancel_button = new System.Windows.Forms.Button();
            this.resume_button = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.open_File = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(796, 56);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(499, 818);
            this.checkedListBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(796, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(252, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select attributes you wish to pick out";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 685);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(316, 189);
            this.button1.TabIndex = 3;
            this.button1.Text = "Begin Parsing";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // parsingProgressBar
            // 
            this.parsingProgressBar.Location = new System.Drawing.Point(2, 555);
            this.parsingProgressBar.Name = "parsingProgressBar";
            this.parsingProgressBar.Size = new System.Drawing.Size(788, 97);
            this.parsingProgressBar.TabIndex = 6;
            // 
            // pause_button
            // 
            this.pause_button.Location = new System.Drawing.Point(2, 369);
            this.pause_button.Name = "pause_button";
            this.pause_button.Size = new System.Drawing.Size(243, 189);
            this.pause_button.TabIndex = 9;
            this.pause_button.Text = "Pause";
            this.pause_button.UseVisualStyleBackColor = true;
            this.pause_button.Click += new System.EventHandler(this.pause_button_Click);
            // 
            // cancel_button
            // 
            this.cancel_button.Location = new System.Drawing.Point(532, 369);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(243, 189);
            this.cancel_button.TabIndex = 10;
            this.cancel_button.Text = "Begin Parsing";
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
            // 
            // resume_button
            // 
            this.resume_button.Location = new System.Drawing.Point(268, 369);
            this.resume_button.Name = "resume_button";
            this.resume_button.Size = new System.Drawing.Size(243, 189);
            this.resume_button.TabIndex = 11;
            this.resume_button.Text = "Resume";
            this.resume_button.UseVisualStyleBackColor = true;
            this.resume_button.Click += new System.EventHandler(this.resume_button_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(445, 216);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // open_File
            // 
            this.open_File.Location = new System.Drawing.Point(504, 111);
            this.open_File.Name = "open_File";
            this.open_File.Size = new System.Drawing.Size(243, 189);
            this.open_File.TabIndex = 12;
            this.open_File.Text = "Open file";
            this.open_File.UseVisualStyleBackColor = true;
            this.open_File.Click += new System.EventHandler(this.open_File_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1307, 878);
            this.Controls.Add(this.open_File);
            this.Controls.Add(this.resume_button);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.pause_button);
            this.Controls.Add(this.parsingProgressBar);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkedListBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ProgressBar parsingProgressBar;
        private System.Windows.Forms.Button pause_button;
        private System.Windows.Forms.Button cancel_button;
        private System.Windows.Forms.Button resume_button;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button open_File;
    }
}

