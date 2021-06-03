using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MulthiThreadMasterProgram.Backend.Models;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using MultiThreadMasterProgram.Backend.Receiver;
using MulthiThreadMasterProgram.Backend.Repository;


namespace MulthiThreadMasterProgram
{
    public partial class Form1 : Form
    {
         


        public Form1()
        {
            InitializeComponent();
        }
        List<SlaveItems> slaveObjects = new List<SlaveItems>();
        bool isCancelRequested = false;
        Receiver receiver = null;
        bool workDone = false;
        public static int testCase = 0;
        int slaveObjCount = 0;
        bool send = true;
        public static bool updateProcessed = false;
        public static List<string> processedFiles = new List<string>();
        private void dirButton2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"F:\MULTITHREAD4TESTSPACE";
            int filesPerThread = 5;       


            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = openFileDialog.FileName;
                DirectoryInfo pictureDirectory = new DirectoryInfo(Path.GetDirectoryName(path).ToString());
                progressBar1.Maximum = pictureDirectory.GetFiles().Length;

                foreach(FileInfo fileInfo in pictureDirectory.GetFiles())
                {
                    listBox2.Items.Add(fileInfo.Name);
                }

                for (int i = 0; i < pictureDirectory.GetFiles().Length; i += filesPerThread)
                {
                    var items = pictureDirectory.GetFiles().Skip(i).Take(filesPerThread);
                    List<FileInfo> tempList = new List<FileInfo>();
                    foreach (var FileInFo in items)
                    {
                        tempList.Add(FileInFo);
                    }

                    slaveObjects.Add(new SlaveItems(tempList));
                    slaveObjCount++;
                }
                
            }



            foreach(SlaveItems slaveItems in slaveObjects)
            {

                //Va cia turi buti senderis pirminis kuris turi failus paduot slave`ui

            }
            
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }



        private void beginSendButton1_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(s => UpgradeMethod());
            ThreadPool.QueueUserWorkItem(s => SendFiles());
           
        }

        private void SendFiles()
        {
            SendFilesToSlave sendFilesToSlave = new SendFilesToSlave();
            int whichTosend = 0;            
            while (!workDone)
            {
                if (send)
                {

                   // ThreadPool.QueueUserWorkItem(s => sendFilesToSlave.SendFiles(slaveObjects[whichTosend]));
                    sendFilesToSlave.SendFiles(slaveObjects[whichTosend]);
                    send = false;
                    whichTosend++;
                }

            }
        }

        private void startReceiverButton_Click(object sender, EventArgs e)
        {
            isCancelRequested = false;
            Thread receiverThread = new Thread(() =>
            {
                  receiver = new Receiver();
                  receiver.StartSlaveOutputReceiver();
            });

            receiverThread.Start();
            Thread receiverThreadUpdates = new Thread(() =>
            {
                receiver = new Receiver();
                receiver.StarSlaveProgressUpdateReceiver();
            });
            receiverThreadUpdates.Start();
            Thread uiThread = new Thread(() =>
            {
                while (true)
                {
                    /*
                    label4.Invoke((MethodInvoker)delegate
                    {
                        // Paeditina info labeli apie esama situacija
                        label4.Text = Receiver.Message;
                    });
                    */
                    Thread.Sleep(100);
                    if (isCancelRequested)
                        break;
                }
            });

            uiThread.Start();
        }


        private void UpgradeMethod()
        {
            int totalProgress = 0;

            while (workDone == false)
            {
               
                               

                if (updateProcessed)
                {
                    totalProgress++;
                    progressBar1.Invoke((MethodInvoker)delegate
                    {
                        progressBar1.Value = totalProgress;
                    });
                    listBox1.Invoke((MethodInvoker)delegate
                    {
                        listBox1.Items.Clear();
                    });
                                                          
                    foreach (string file in processedFiles.ToList())
                    {

                        listBox1.Invoke((MethodInvoker)delegate
                        {
                            listBox1.Items.Add(file);
                        });
                    }


                    foreach (string file in processedFiles.ToList())
                    {

                        for (int n = listBox2.Items.Count - 1; n >= 0; --n)
                        {
                            string removelistitem = file;
                            if (listBox2.Items[n].ToString().Contains(removelistitem))
                            {
                                listBox2.Invoke((MethodInvoker)delegate
                                {
                                    listBox2.Items.RemoveAt(n);
                                });
                            }
                        }
                    }                                  

                    updateProcessed = false;
                   
                }
                

                
            }
            
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            send = true;
        }
    }
}
