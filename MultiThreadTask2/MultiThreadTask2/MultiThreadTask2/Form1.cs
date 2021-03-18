using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.IO.Compression;


namespace MultiThreadTask2
{
    public partial class Form1 : Form
    {

        

        
        bool IsEncryptPause = false;        
        bool IsDecryptPause = false;
        bool IsEncryptStopped = false;
        bool IsDecryptStopped = false;
        
        public static string password = "MULTHREADING_101";
        public Form1()
        {
            InitializeComponent();
            DirectoryInfo dir = new DirectoryInfo("F:/MULTHREAD2TESTPACE");


            AES_Utility.SetParams(password);

            foreach (DirectoryInfo c in dir.GetDirectories())
            {
                FolderBox.Items.Add(c.FullName);
            }
            foreach(FileInfo b in dir.GetFiles())
            {
                FileBox.Items.Add(b.Name);
            }
        }


        // UI events
        //---------------------------------
        private void EncryptButton_Click(object sender, EventArgs e)
        {        
                    
        DirectoryInfo encryptdir = new DirectoryInfo(FolderBox.SelectedItem.ToString());              
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
            
            string selected = FolderBox.SelectedItem.ToString();
            DirectoryInfo encryptdir = new DirectoryInfo(selected);
            ThreadPool.QueueUserWorkItem(s => DecryptCategory(encryptdir));
           
        }

        // For debbuging purposes only
        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Nemusk, skauda");           
        }


       

     

        //UI eventai
                   

        private void EncryptCategory(DirectoryInfo dir)
        {
            try
            {
                EncryptBar.Invoke((MethodInvoker)delegate { EncryptBar.Step = 1; EncryptBar.Maximum = dir.GetFiles().Length; });

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
                 EncryptBar.Invoke((MethodInvoker)delegate { EncryptBar.Value = 0; });
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
                EncryptBar.Invoke((MethodInvoker)delegate { EncryptBar.Value=0; });
                MessageBox.Show($"Thread exited, Files restored\n{e}");
            }

        }

        private void DecryptCategory(DirectoryInfo dir)
        {
            try
            {
                DecryptBar.Invoke((MethodInvoker)delegate { DecryptBar.Step = 1; DecryptBar.Maximum = dir.GetFiles().Length; });
                foreach (FileInfo c in dir.GetFiles())
                {
                    while (IsDecryptPause)
                    {
                        Thread.Sleep(1);
                    }

                    if (IsDecryptStopped)
                    {
                        Thread.CurrentThread.Abort();
                    }
                    AES_Utility.AesDecryptas(c);
                    DecryptBar.Invoke((MethodInvoker)delegate { DecryptBar.PerformStep(); });
                }
                foreach (FileInfo c in dir.GetFiles())
                {
                    if (string.Compare(c.Extension, ".zip") == 0)
                    {
                        ZipFile.ExtractToDirectory(c.FullName, dir.FullName + $"/{c.Name.Replace(".zip", "")}");
                        c.Delete();

                    }
                }
                DecryptBar.Invoke((MethodInvoker)delegate { DecryptBar.Value = 0; });
            }
            catch (ThreadAbortException e)
            {
                IsDecryptStopped = false;
                foreach (FileInfo c in dir.GetFiles())
                {
                    if (String.Compare(c.Extension, ".aes") == 0 || String.Compare(c.Extension,".HASH")==0)
                    {
                        continue;
                    }
                    else AES_Utility.AesEncryptas(c);
                }               
                DecryptBar.Invoke((MethodInvoker)delegate { DecryptBar.Value = 0; });
                MessageBox.Show($"Thread exited, Files ReEncrypted\n{e}");
            }
        }
              

        private void ResumeButton_Click(object sender, EventArgs e)
        {
            
            IsEncryptPause = false;
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            
            IsEncryptPause = true;
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            IsEncryptStopped = true;
        }

        private void DecryptPause_Click(object sender, EventArgs e)
        {
            
            IsDecryptPause = true;
        }

        private void DecryptResume_Click(object sender, EventArgs e)
        {
            IsDecryptPause = false;
        }

        private void DecryptStop_Click(object sender, EventArgs e)
        {
            IsDecryptStopped = true;
        }
    }


}
