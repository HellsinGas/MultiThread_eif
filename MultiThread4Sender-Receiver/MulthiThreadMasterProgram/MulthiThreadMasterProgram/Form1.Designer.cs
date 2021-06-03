
namespace MulthiThreadMasterProgram
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
            this.beginSendButton1 = new System.Windows.Forms.Button();
            this.dirButton2 = new System.Windows.Forms.Button();
            this.itemsPerSlaveBox = new System.Windows.Forms.TextBox();
            this.startReceiverButton = new System.Windows.Forms.Button();
            this.testButton = new System.Windows.Forms.Button();
            this.launch = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // beginSendButton1
            // 
            this.beginSendButton1.Location = new System.Drawing.Point(466, 507);
            this.beginSendButton1.Name = "beginSendButton1";
            this.beginSendButton1.Size = new System.Drawing.Size(94, 56);
            this.beginSendButton1.TabIndex = 0;
            this.beginSendButton1.Text = "Begin";
            this.beginSendButton1.UseVisualStyleBackColor = true;
            this.beginSendButton1.Click += new System.EventHandler(this.beginSendButton1_Click);
            // 
            // dirButton2
            // 
            this.dirButton2.Location = new System.Drawing.Point(566, 507);
            this.dirButton2.Name = "dirButton2";
            this.dirButton2.Size = new System.Drawing.Size(101, 56);
            this.dirButton2.TabIndex = 1;
            this.dirButton2.Text = "Dir Selection";
            this.dirButton2.UseVisualStyleBackColor = true;
            this.dirButton2.Click += new System.EventHandler(this.dirButton2_Click);
            // 
            // itemsPerSlaveBox
            // 
            this.itemsPerSlaveBox.Location = new System.Drawing.Point(466, 474);
            this.itemsPerSlaveBox.Name = "itemsPerSlaveBox";
            this.itemsPerSlaveBox.Size = new System.Drawing.Size(201, 27);
            this.itemsPerSlaveBox.TabIndex = 2;
            // 
            // startReceiverButton
            // 
            this.startReceiverButton.Location = new System.Drawing.Point(1113, 517);
            this.startReceiverButton.Name = "startReceiverButton";
            this.startReceiverButton.Size = new System.Drawing.Size(94, 56);
            this.startReceiverButton.TabIndex = 3;
            this.startReceiverButton.Text = "Start Receiver";
            this.startReceiverButton.UseVisualStyleBackColor = true;
            this.startReceiverButton.Click += new System.EventHandler(this.startReceiverButton_Click);
            // 
            // testButton
            // 
            this.testButton.Location = new System.Drawing.Point(3, 544);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(94, 29);
            this.testButton.TabIndex = 4;
            this.testButton.Text = "Test";
            this.testButton.UseVisualStyleBackColor = true;
            this.testButton.Click += new System.EventHandler(this.testButton_Click);
            // 
            // launch
            // 
            this.launch.Location = new System.Drawing.Point(1113, 440);
            this.launch.Name = "launch";
            this.launch.Size = new System.Drawing.Size(99, 71);
            this.launch.TabIndex = 5;
            this.launch.Text = "Receiver";
            this.launch.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Location = new System.Drawing.Point(12, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(337, 284);
            this.listBox1.TabIndex = 6;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 20;
            this.listBox2.Location = new System.Drawing.Point(687, 12);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(337, 284);
            this.listBox2.TabIndex = 7;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 348);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1195, 86);
            this.progressBar1.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1219, 575);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.launch);
            this.Controls.Add(this.testButton);
            this.Controls.Add(this.startReceiverButton);
            this.Controls.Add(this.itemsPerSlaveBox);
            this.Controls.Add(this.dirButton2);
            this.Controls.Add(this.beginSendButton1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button beginButton1;
        private System.Windows.Forms.Button dirButton2;
        private System.Windows.Forms.Button beginSendButton1;
        private System.Windows.Forms.TextBox itemsPerSlaveBox;
        private System.Windows.Forms.TextBox itemsPerSlaveBox1;
        private System.Windows.Forms.Button startReceiverButton;
        private System.Windows.Forms.Button testButton;
        private System.Windows.Forms.Button launch;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

