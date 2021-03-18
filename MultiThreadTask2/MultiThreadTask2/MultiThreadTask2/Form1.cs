using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.IO.Compression;
//using System.Security.Cry

namespace MultiThreadTask2
{// folder stringas : F:/MULTHREAD2TESTPACE
    public partial class Form1 : Form
    {

        //private static ManualResetEvent ResetEvent = new ManualResetEvent(false);

        bool IsEncryptResume = true;
        bool IsEncryptPause = false;
        bool IsDecryptResume = true;
        bool IsDecryptPause = false;
        bool IsEncryptStopped = false;
        bool IsDecryptStopped = false;

        private static readonly byte[] Salt =
        new byte[] { 10, 20, 30, 40, 50, 60, 70, 80 };
        public static string password = "password!#$%!@#";
        public Form1()
        {
            InitializeComponent();
            DirectoryInfo dir = new DirectoryInfo("C:/ENCRYPTION FILES");


            AES_Utility.SetParams(password, Salt);

            foreach (DirectoryInfo c in dir.GetDirectories())
            {
                FolderBox.Items.Add(c.FullName);
            }
            foreach(FileInfo b in dir.GetFiles())
            {
                FileBox.Items.Add(b.Name);
            }
        }


        // UI eventai  
        //---------------------------------
        private void EncryptButton_Click(object sender, EventArgs e)
        {
        ThreadPool.QueueUserWorkItem(ThreadProc2);

        string selected = FolderBox.SelectedItem.ToString();
        DirectoryInfo encryptdir = new DirectoryInfo(selected);
        MessageBox.Show(encryptdir.FullName);
        int encryptedFileCount = 0;
        foreach(DirectoryInfo b in encryptdir.GetDirectories())
            {
                ZipFile.CreateFromDirectory(b.FullName, b.FullName + ".zip");
                b.Delete(true);
            }
        ThreadPool.QueueUserWorkItem(s => EncryptCategory(encryptdir));

        }

        private void FolderBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FileBox.Items.Clear();
            string selected = FolderBox.SelectedItem.ToString();
            DirectoryInfo encryptdir = new DirectoryInfo(selected);
            foreach (FileInfo c in encryptdir.GetFiles()) {
                FileBox.Items.Add(c.FullName);
                    }
        }

        private void DecryptButton_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(ThreadProc1);
            string selected = FolderBox.SelectedItem.ToString();
            DirectoryInfo encryptdir = new DirectoryInfo(selected);
            MessageBox.Show(encryptdir.FullName);
            ThreadPool.QueueUserWorkItem(s => DecryptCategory(encryptdir));
           
        }

        // For debbuging purposes only
        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Shits borke");           
        }


       

     

        //UI eventai


        private void ThreadProc1(object stateInfo)
        {
            int i = 0;
            while(true)
            {
                DecryptTimer.Invoke((MethodInvoker)delegate
                {
                    DecryptTimer.Text = i.ToString();
                });
                Thread.Sleep(1);
                i++;
            }
        }

        private void EncryptCategory(DirectoryInfo dir)
        {
            try
            {
                EncryptBar.Invoke((MethodInvoker)delegate { EncryptBar.Value = 0; EncryptBar.Step = 1; EncryptBar.Maximum = dir.GetFiles().Length; });

                foreach (FileInfo c in dir.GetFiles())
                {
                    while (IsEncryptPause)
                    {
                        Thread.Sleep(1);
                    }
                    if (IsEncryptStopped)
                    {
                        Thread.CurrentThread.Abort();
                    }
                    AES_Utility.AesEncryptas(c);
                    EncryptBar.Invoke((MethodInvoker)delegate { EncryptBar.PerformStep(); });

                }
            }
            catch(ThreadAbortException e)
            {
                IsEncryptStopped = false;
               foreach (FileInfo c in dir.GetFiles())
                {
                    if(String.Compare(c.Extension, ".aes") == 0)
                    {
                        AES_Utility.AesDecryptas(c);
                    }
                }
                foreach (FileInfo c in dir.GetFiles())
                {
                    if (string.Compare(c.Extension, ".zip") == 0)
                    {
                        ZipFile.ExtractToDirectory(c.FullName, dir.FullName + $"/{c.Name.Replace(".zip", "")}");
                        c.Delete();
                    }
                }
            }

        }

        private void DecryptCategory(DirectoryInfo dir)
        {
            DecryptBar.Invoke((MethodInvoker)delegate { DecryptBar.Value = 0; DecryptBar.Step = 1; DecryptBar.Maximum = dir.GetFiles().Length; });
            foreach (FileInfo c in dir.GetFiles())
            {
                while (IsDecryptPause)
                {
                    Thread.Sleep(1);
                }
                AES_Utility.AesDecryptas(c);
                DecryptBar.Invoke((MethodInvoker)delegate { DecryptBar.PerformStep(); });
            }
            foreach (FileInfo c in dir.GetFiles())
            {
                if (string.Compare(c.Extension, ".zip") == 0)
                {
                    ZipFile.ExtractToDirectory(c.FullName, dir.FullName + $"/{c.Name.Replace(".zip","")}");
                    c.Delete();
                    //+ $"{c.Name}"
                }
            }
        }


        private void ThreadProc2(object stateInfo)
        {
            int i = 0;
            while (true)
            {
                EncryptTimer.Invoke((MethodInvoker)delegate
                {
                    EncryptTimer.Text = i.ToString();
                });
                Thread.Sleep(1);
                i++;
            }
        }

        private void ResumeButton_Click(object sender, EventArgs e)
        {
            IsEncryptResume = true;
            IsEncryptPause = false;
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            IsEncryptResume = false;
            IsEncryptPause = true;
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            IsEncryptStopped = true;
        }

        private void DecryptPause_Click(object sender, EventArgs e)
        {
            IsDecryptResume = false;
            IsDecryptPause = true;
        }

        private void DecryptResume_Click(object sender, EventArgs e)
        {
            IsDecryptResume = true;
            IsDecryptPause = false;
        }

        private void DecryptStop_Click(object sender, EventArgs e)
        {
            IsDecryptStopped = true;
        }
    }


}
