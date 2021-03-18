using System;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;


namespace Daugiagijis1
{
    //static int counteris = 0 ;
   // public static int counteris = 0;
    public partial class Form1 : Form
    {
        public static int counteris = 0;
        private int _tcks;
        public Form1()
        {
          // public static int counteris = 0;

            InitializeComponent();
            foreach (DriveInfo e in DriveInfo.GetDrives())
            {
                DrivesBox.Items.Add(e);
            }
            Repository.FileFoundEvent += Repository_FileFoundEvent;
            Repository.DirectoryFoundEvent += Repository_DirectoryFoundEvent;


        }

        private void Repository_DirectoryFoundEvent(object sender, DirectoryInfo e)
        {
            //Thread t = new Thread(Repository_DirectoryFoundEvent)
            //MessageBox.Show($"OH SHAAAAAIT IT WORKED ASWELL : {e.Name}");
            // throw new NotImplementedException();
            DirectoryInfo b = new DirectoryInfo(e.FullName);
            object c = new object();
            c = e;
            Thread folders = new Thread(new ParameterizedThreadStart(pridejimas));
            // d.Start(c);


            /* Invoke(new Action(() =>
             {
                 FolderBox.Items.Add(e.FullName);
             }));*/
            pridejimas(c);
            // FolderBox.Items.Add(e.FullName);
        }
        void pridejimas2(object a)
        {
            FileInfo e = (FileInfo)a;            
            //string bbz = e.Name;
            /* MethodInvoker invoker = new MethodInvoker(delegate { richTextBox2.AppendText($"{e.Name}\n "); });
             richTextBox2.Invoke(invoker);
             */
            //  Richboxinvoker(e.FullName);
            
                this.Invoke(new Action(() =>
                 {                    
                    richTextBox2.Text += ($"{e.Name}\n ");
                    
                }));
               /* if (InvokeRequired)
                {
                    this.Invoke(new Action<object>(pridejimas2), new object[] { e.Name });
                    return;
                }
                richTextBox2.Text += e.Name;*/
           /* }
            catch (Exception c)
            {
                MessageBox.Show(c.Message);
            }*/
        }
        private void Richboxinvoker(string text)
        {
            MethodInvoker invoker = new MethodInvoker(delegate { richTextBox2.AppendText($"{text}\n "); });
            richTextBox2.Invoke(invoker);
        }
        void pridejimas(object b)
        {
            DirectoryInfo e = (DirectoryInfo)b;
            Invoke(new Action(() =>
            {
                //FolderBox.Items.Add(e.FullName);
                richTextBox1.Text += $"{e.FullName}\n ";
            }));
            // Thread.Sleep(50);

        }

        private void Repository_FileFoundEvent(object sender, FileInfo e)
        {

             //MessageBox.Show($"OH SHIT IT WORKED {e.FullName}");
            // throw new NotImplementedException();
          //  DirectoryInfo b = new DirectoryInfo(e.FullName);
            object c = new object();
            c = e;
            /*ParameterizedThreadStart f = new ParameterizedThreadStart(pridejimas2);
            Thread thread = new Thread(f);
            thread.Start(c);*/
            Thread f = new Thread(new ParameterizedThreadStart(pridejimas2));
            //f.Start(c);
           // f.Abort(c);
             pridejimas2(c);

            //pridejimas2(c);

            //pridejimas2(c);
            // SearchResults.Items.Add(e.Name);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Thread t = new Thread(new ThreadStart(Repository_DirectoryFoundEvent));
            SearchResults.Items.Clear();
            FolderBox.Items.Clear();
            
            if (DrivesBox.SelectedIndex == -1 && button1.Tag == null)
            {
                MessageBox.Show("nothing seleceted");
                foreach (DriveInfo currentdrive in DriveInfo.GetDrives())
                {
                    if (!currentdrive.IsReady)
                    {
                        MessageBox.Show($"The drive {currentdrive.Name} could not be read", currentdrive.Name);
                        continue;
                    }
                    System.IO.DirectoryInfo rootDir = currentdrive.RootDirectory;
                    timer1.Start();
                    Repository.WalkDirectoryTree(rootDir, textBox1.Text);
                    timer1.Stop();
                    //DirectoryInfo[] foundirs = currentdrive.RootDirectory.GetDirectories("*Div*.*", SearchOption.AllDirectories);

                }
            }
            else
            {
               // MessageBox.show
               DirectoryInfo searchdir = (DirectoryInfo)DrivesBox.SelectedItem;
                Button button = (Button)sender;
                DirectoryInfo selecteddir = (DirectoryInfo)button.Tag;
                MessageBox.Show($"{selecteddir.FullName}");
                timer1.Start();                
                Repository.WalkDirectoryTree(selecteddir, textBox1.Text);
                timer1.Stop();
                //MessageBox.Show($"{timer1.}")

            }

        }

        private void DrivesBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FolderBox.Items.Clear();
            try
            {
                if (DrivesBox.SelectedItem is DriveInfo)
                {
                    DriveInfo selectedDrive = (DriveInfo)DrivesBox.SelectedItem;
                    DirectoryInfo directory = selectedDrive.RootDirectory;
                    button1.Tag = directory;
                    DrivesBox.Items.Clear();
                    foreach (DirectoryInfo dir in directory.GetDirectories())
                        DrivesBox.Items.Add(dir);
                }
                else
                {
                    DirectoryInfo dir = (DirectoryInfo)DrivesBox.SelectedItem;
                    
                    button1.Tag = dir;
                    DrivesBox.Items.Clear();

                    

                    foreach (DirectoryInfo a in dir.GetDirectories())
                        DrivesBox.Items.Add(a);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show($"{ exc}");
                foreach (DriveInfo ees in DriveInfo.GetDrives())
                {
                    DrivesBox.Items.Add(ees);
                }
            }

        }

        private void DrivesBox_SelectedValueChanged(object sender, EventArgs e)
        {
        }

        private void FolderBox_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBox.Items.Clear();
            SearchResults.Items.Clear();
            DrivesBox.Items.Clear();
            foreach (DriveInfo eeee in DriveInfo.GetDrives())
            {
                DrivesBox.Items.Add(eeee);
            }

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _tcks++;
            label2.Text = _tcks.ToString();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            /*_tcks++;
            label2.Text = $"{_tcks.ToString()}";*/
        }
    }
}

    

