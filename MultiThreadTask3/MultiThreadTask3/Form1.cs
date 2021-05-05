using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;



namespace MultiThreadTask3
{
    public partial class Form1 : Form
    {
        public List<string> attributeListForValidation = new List<string>();
        public List<FileInfo> validFileList = new List<FileInfo>();
        public List<AttributeAndPosition> checkedItemsList = new List<AttributeAndPosition>();
        public string[] valuesForValidation = null;
        bool isPaused = false;
        bool isStopped = false;
        bool preventClose = false;
        public Form1()
        {
            InitializeComponent();           
                      
            
                       

        }

       

        private void fileValidation(DirectoryInfo directoryInfo)
        {
                       
            string attrib = "@attribute";
            int filecount = 0;            
            button1.Invoke((MethodInvoker)delegate { button1.Visible = false; });
            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                bool validation=false;
                int i = 0;
                string last = ",";

                foreach (string line in File.ReadLines(file.FullName))
                {
                    
                    if (line.Contains(attrib))
                    {

                        string s = line;
                        if (!String.Equals(s,attributeListForValidation[i]))
                        {
                            validation = true;
                            MessageBox.Show($"File not valid : {file.Name}");
                        }                        
                        i++;

                    }else if (line.Contains(last))
                    
                    {
                        string s = line;
                        string[] stringArray = s.Split(',');
                        if(stringArray.Length != valuesForValidation.Length)
                        {
                            validation = true;                           
                        }
                       // MessageBox.Show($"File not valid : {file.Name}");
                    }
                    
                }
                filecount++;
                if (!validation)
                {
                    validFileList.Add(file);
                }
                validation = false;
            }
            button1.Invoke((MethodInvoker)delegate { button1.Visible = true; });
            
            

        }

        private void populateList(string file , DirectoryInfo directoryInfo)        {
            
            checkedListBox1.Invoke((MethodInvoker)delegate { checkedListBox1.Items.Clear(); });
            string attrib = "@attribute";
            string last = "'";

            foreach (string line in File.ReadLines(file)) {
                if (line.Contains(attrib))
                {
                    
                    string s = line;
                    attributeListForValidation.Add(s);

                    string[] res = s.Split(new string[] { "@attribute " }, StringSplitOptions.None);
                    string os = null;
                    foreach (string ss in res)
                    {
                        os += ss;
                    }                  
                                       
                   checkedListBox1.Invoke((MethodInvoker)delegate { checkedListBox1.Items.Add(os); });

                }
                if (line.Contains(last))
                {
                    valuesForValidation = line.Split(',');
                }
               
            }

            ThreadPool.QueueUserWorkItem(p => fileValidation(directoryInfo));


        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> checkedAttributes = new List<string>();
            
            foreach(object checkedItem in checkedListBox1.CheckedItems)
            {
                
                checkedAttributes.Add(checkedItem.ToString());
                string fullLine = "@attribute " + checkedItem.ToString();
                checkedItemsList.Add(new AttributeAndPosition(fullLine,checkedListBox1.Items.IndexOf(checkedItem)));                

            }

            CancellationTokenSource cts = new CancellationTokenSource();
            ThreadPool.QueueUserWorkItem(new WaitCallback(s=>threadWorkloadAssignment(cts)),cts.Token);
        }

        private void threadWorkloadAssignment(CancellationTokenSource cts)
        {
            preventClose = true;
            Parsing_Utility customParser = new Parsing_Utility();
            try
            {
                

                parsingProgressBar.Invoke((MethodInvoker)delegate { parsingProgressBar.Step = 1; parsingProgressBar.Maximum = validFileList.Count; });
                int j = 0;
                for (int i = 0; i < validFileList.Count(); i++) {                    
                    while (isPaused)
                    {
                        Thread.Sleep(1);
                    }
                    if (isStopped)
                    {
                        cts.Cancel();

                    }
                    if (cts.IsCancellationRequested)
                    {
                        MessageBox.Show("Thread canceled , Work exited");
                        parsingProgressBar.Invoke((MethodInvoker)delegate { parsingProgressBar.Value=1; });
                        isStopped = false;
                        break;
                    }
                    customParser.ParseAttributeMain(validFileList[j], checkedItemsList);
                        j++;
                    parsingProgressBar.Invoke((MethodInvoker)delegate { parsingProgressBar.PerformStep(); });

                }
                if (!cts.IsCancellationRequested)
                {
                    customParser.outputFileGeneration();                    
                }
                cts.Dispose();
                
            }
            catch (ThreadAbortException e)
            {
                MessageBox.Show(ToString());
            }
            preventClose = false;
            parsingProgressBar.Invoke((MethodInvoker)delegate { parsingProgressBar.Value = 1; });
            MessageBox.Show("File parsing done");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (preventClose)
            {
                e.Cancel = true;
            }
        }

        private void pause_button_Click(object sender, EventArgs e)
        {
            isPaused = true;
        }

        private void resume_button_Click(object sender, EventArgs e)
        {
            isPaused = false;
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            isStopped = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void open_File_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.InitialDirectory = @"F:\MULTHREAD3TESTPACE";
            openFile.Filter = "arff files (*.arff)|*.arff";
            openFile.FilterIndex = 1;
            openFile.RestoreDirectory = true;
            string filePath;
            
            if(openFile.ShowDialog() == DialogResult.OK)
            {
                filePath = openFile.FileName;
                DirectoryInfo directoryPath = new DirectoryInfo(Path.GetDirectoryName(filePath).ToString());                
                ThreadPool.QueueUserWorkItem(p => populateList(filePath, directoryPath));               
                

            }
        }
    }
}
