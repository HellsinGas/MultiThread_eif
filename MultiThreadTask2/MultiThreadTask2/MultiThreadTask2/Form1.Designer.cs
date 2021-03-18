
namespace MultiThreadTask2
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.FolderBox = new System.Windows.Forms.ListBox();
            this.FileBox = new System.Windows.Forms.ListBox();
            this.EncryptButton = new System.Windows.Forms.Button();
            this.DecryptButton = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.EncryptBar = new System.Windows.Forms.ProgressBar();
            this.DecryptBar = new System.Windows.Forms.ProgressBar();
            this.EncryptTimer = new System.Windows.Forms.Label();
            this.DecryptTimer = new System.Windows.Forms.Label();
            this.ResumeButton = new System.Windows.Forms.Button();
            this.PauseButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.DecryptResume = new System.Windows.Forms.Button();
            this.DecryptPause = new System.Windows.Forms.Button();
            this.DecryptStop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FolderBox
            // 
            this.FolderBox.FormattingEnabled = true;
            this.FolderBox.ItemHeight = 16;
            this.FolderBox.Location = new System.Drawing.Point(12, 12);
            this.FolderBox.Name = "FolderBox";
            this.FolderBox.Size = new System.Drawing.Size(502, 260);
            this.FolderBox.TabIndex = 0;
            this.FolderBox.SelectedIndexChanged += new System.EventHandler(this.FolderBox_SelectedIndexChanged);
            // 
            // FileBox
            // 
            this.FileBox.FormattingEnabled = true;
            this.FileBox.ItemHeight = 16;
            this.FileBox.Location = new System.Drawing.Point(520, 12);
            this.FileBox.Name = "FileBox";
            this.FileBox.Size = new System.Drawing.Size(680, 260);
            this.FileBox.TabIndex = 1;
            // 
            // EncryptButton
            // 
            this.EncryptButton.Location = new System.Drawing.Point(12, 278);
            this.EncryptButton.Name = "EncryptButton";
            this.EncryptButton.Size = new System.Drawing.Size(188, 66);
            this.EncryptButton.TabIndex = 2;
            this.EncryptButton.Text = "Ecnrypt";
            this.EncryptButton.UseVisualStyleBackColor = true;
            this.EncryptButton.Click += new System.EventHandler(this.EncryptButton_Click);
            // 
            // DecryptButton
            // 
            this.DecryptButton.Location = new System.Drawing.Point(520, 278);
            this.DecryptButton.Name = "DecryptButton";
            this.DecryptButton.Size = new System.Drawing.Size(188, 66);
            this.DecryptButton.TabIndex = 3;
            this.DecryptButton.Text = "Decrypt";
            this.DecryptButton.UseVisualStyleBackColor = true;
            this.DecryptButton.Click += new System.EventHandler(this.DecryptButton_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(667, 516);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(188, 114);
            this.button3.TabIndex = 4;
            this.button3.Text = "Generate Hash";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(947, 516);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(188, 114);
            this.button4.TabIndex = 5;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(1012, 278);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(188, 114);
            this.button5.TabIndex = 6;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // EncryptBar
            // 
            this.EncryptBar.Location = new System.Drawing.Point(12, 350);
            this.EncryptBar.Name = "EncryptBar";
            this.EncryptBar.Size = new System.Drawing.Size(188, 36);
            this.EncryptBar.TabIndex = 7;
            // 
            // DecryptBar
            // 
            this.DecryptBar.Location = new System.Drawing.Point(520, 350);
            this.DecryptBar.Name = "DecryptBar";
            this.DecryptBar.Size = new System.Drawing.Size(188, 36);
            this.DecryptBar.TabIndex = 8;
            // 
            // EncryptTimer
            // 
            this.EncryptTimer.AutoSize = true;
            this.EncryptTimer.Location = new System.Drawing.Point(301, 327);
            this.EncryptTimer.Name = "EncryptTimer";
            this.EncryptTimer.Size = new System.Drawing.Size(46, 17);
            this.EncryptTimer.TabIndex = 9;
            this.EncryptTimer.Text = "label1";
            // 
            // DecryptTimer
            // 
            this.DecryptTimer.AutoSize = true;
            this.DecryptTimer.Location = new System.Drawing.Point(823, 327);
            this.DecryptTimer.Name = "DecryptTimer";
            this.DecryptTimer.Size = new System.Drawing.Size(46, 17);
            this.DecryptTimer.TabIndex = 10;
            this.DecryptTimer.Text = "label2";
            // 
            // ResumeButton
            // 
            this.ResumeButton.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.ResumeButton.Location = new System.Drawing.Point(12, 392);
            this.ResumeButton.Name = "ResumeButton";
            this.ResumeButton.Size = new System.Drawing.Size(94, 23);
            this.ResumeButton.TabIndex = 11;
            this.ResumeButton.Text = "Resume";
            this.ResumeButton.UseVisualStyleBackColor = true;
            this.ResumeButton.Click += new System.EventHandler(this.ResumeButton_Click);
            // 
            // PauseButton
            // 
            this.PauseButton.Location = new System.Drawing.Point(112, 392);
            this.PauseButton.Name = "PauseButton";
            this.PauseButton.Size = new System.Drawing.Size(88, 23);
            this.PauseButton.TabIndex = 12;
            this.PauseButton.Text = "Pause";
            this.PauseButton.UseVisualStyleBackColor = true;
            this.PauseButton.Click += new System.EventHandler(this.PauseButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(12, 421);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(188, 27);
            this.StopButton.TabIndex = 13;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // DecryptResume
            // 
            this.DecryptResume.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.DecryptResume.Location = new System.Drawing.Point(520, 392);
            this.DecryptResume.Name = "DecryptResume";
            this.DecryptResume.Size = new System.Drawing.Size(94, 23);
            this.DecryptResume.TabIndex = 14;
            this.DecryptResume.Text = "Resume";
            this.DecryptResume.UseVisualStyleBackColor = true;
            this.DecryptResume.Click += new System.EventHandler(this.DecryptResume_Click);
            // 
            // DecryptPause
            // 
            this.DecryptPause.Location = new System.Drawing.Point(620, 392);
            this.DecryptPause.Name = "DecryptPause";
            this.DecryptPause.Size = new System.Drawing.Size(88, 23);
            this.DecryptPause.TabIndex = 15;
            this.DecryptPause.Text = "Pause";
            this.DecryptPause.UseVisualStyleBackColor = true;
            this.DecryptPause.Click += new System.EventHandler(this.DecryptPause_Click);
            // 
            // DecryptStop
            // 
            this.DecryptStop.Location = new System.Drawing.Point(520, 421);
            this.DecryptStop.Name = "DecryptStop";
            this.DecryptStop.Size = new System.Drawing.Size(188, 27);
            this.DecryptStop.TabIndex = 16;
            this.DecryptStop.Text = "Stop";
            this.DecryptStop.UseVisualStyleBackColor = true;
            this.DecryptStop.Click += new System.EventHandler(this.DecryptStop_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1410, 700);
            this.Controls.Add(this.DecryptStop);
            this.Controls.Add(this.DecryptPause);
            this.Controls.Add(this.DecryptResume);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.PauseButton);
            this.Controls.Add(this.ResumeButton);
            this.Controls.Add(this.DecryptTimer);
            this.Controls.Add(this.EncryptTimer);
            this.Controls.Add(this.DecryptBar);
            this.Controls.Add(this.EncryptBar);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.DecryptButton);
            this.Controls.Add(this.EncryptButton);
            this.Controls.Add(this.FileBox);
            this.Controls.Add(this.FolderBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox FolderBox;
        private System.Windows.Forms.ListBox FileBox;
        private System.Windows.Forms.Button EncryptButton;
        private System.Windows.Forms.Button DecryptButton;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ProgressBar EncryptBar;
        private System.Windows.Forms.ProgressBar DecryptBar;
        private System.Windows.Forms.Label EncryptTimer;
        private System.Windows.Forms.Label DecryptTimer;
        private System.Windows.Forms.Button ResumeButton;
        private System.Windows.Forms.Button PauseButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button DecryptResume;
        private System.Windows.Forms.Button DecryptPause;
        private System.Windows.Forms.Button DecryptStop;
    }
}

