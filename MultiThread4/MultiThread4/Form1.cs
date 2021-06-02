using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MultiThread4.Backend.Repo;
using System.IO;
using System.Threading;
using MultiThread4.Backend.Models;
using System.Diagnostics;


namespace MultiThread4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DirectoryInfo pictureDirectory=null;
        int maximumConcurrentThreads = 0;
        int filesPerThread = 2;
        bool workDone = false;
        Stopwatch stopWatch = new Stopwatch();
        public static int totalProgress = 0;
        public static List<FileInfo> processedFiles = new List<FileInfo>();
        public static List<FileInfo> inProgressFiles= new List<FileInfo>();
        public static List<FileInfo> notProcessedFiles;
        public static bool updateProcessing = false;
        public static bool updateProcessed = false;
        public static bool updateNotProcessedFile = false;
        int test = 0;


        private void button1_Click(object sender, EventArgs e)
        {
            TesseractImpl tesseract = new TesseractImpl();
            FileInfo file = new FileInfo(@"F:\MULTITHREAD4TESTSPACE\txt images\1Text.jpg");
            MessageBox.Show(Environment.ProcessorCount.ToString());
            // MessageBox.Show(ThreadPool.GetMinThreads()
            // tesseract.TesseractExtraction(file);
            // 
            MessageBox.Show(pictureDirectory.FullName);
            // ThreadPool.QueueUserWorkItem(s)
            progressBar1.Maximum = pictureDirectory.GetFiles().Length;
            ThreadPool.QueueUserWorkItem(s => UpgradeMethod());
            ThreadPool.QueueUserWorkItem(s => MasterThread());
        }

        private void MasterThread()
        {
            List<SlaveObjects> slaveObjects = new List<SlaveObjects>();            
            for (int i = 0; i<pictureDirectory.GetFiles().Length; i += filesPerThread)
            {
                var items = pictureDirectory.GetFiles().Skip(i).Take(filesPerThread);
                List<FileInfo> tempList = new List<FileInfo>();
                foreach(var FileInFo in items){
                    tempList.Add(FileInFo);
                }

                slaveObjects.Add(new SlaveObjects(tempList));
            }
            /*   foreach (SlaveObjects slaveObject in slaveObjects)
               {                 
                   Task.Run(() => tesseract.TesseractExtraction(slaveObject));
                   if()
                  // tesseract.TesseractExtraction(slaveObject);

               }*/



           // Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

          //  ThreadPool.QueueUserWorkItem(s => UpgradeMethod());

          //  ThreadPool.QueueUserWorkItem(s => TessaLaunch(slaveObjects));
            // workDone = true;

            //Cia istrintri jeigu kas ir atkomentint metodus virsuj

            List<string> fileOutputFull = new List<string>();
            TesseractImpl tesseract = new TesseractImpl();
            maximumConcurrentThreads = int.Parse(threadCountBox.Text);
            TextExtractionOutputToFile toFile = new TextExtractionOutputToFile();
            int testas = 0;



            /*  Parallel.ForEach(slaveObjects, new ParallelOptions { MaxDegreeOfParallelism = 3 },
              slaveObjects =>
              {
                  // logic                
                  testas++;
                  List<string> fileOutput = new List<string>();
                  fileOutput = tesseract.TesseractExtraction(slaveObjects);
                  fileOutputFull.AddRange(tesseract.TesseractExtraction(slaveObjects));
              });*/







            using (SemaphoreSlim concurrencySemaphore = new SemaphoreSlim(maximumConcurrentThreads))
            {
                List<Task> tasks = new List<Task>();
                foreach (SlaveObjects obj in slaveObjects)
                {
                    /*label3.Invoke((MethodInvoker)delegate
                    {
                        label3.Text = Process.GetCurrentProcess().Threads.Count.ToString();
                    });
                    progressBar1.Invoke((MethodInvoker)delegate { progressBar1.Value = progressBar1.Value + 10; });*/
                    concurrencySemaphore.Wait();


                    var t = Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            testas++;
                            //  List<string> fileOutput = new List<string>();
                            // fileOutput = tesseract.TesseractExtraction(obj);
                            fileOutputFull.AddRange(tesseract.TesseractExtraction(obj));


                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("Something happened");
                            MessageBox.Show(e.ToString());
                            slaveObjects.Add(obj);

                        }
                        finally
                        {
                            concurrencySemaphore.Release();
                        }
                    });

                    tasks.Add(t);
                }

                Task.WaitAll(tasks.ToArray());
                workDone = true;
                toFile.OutputText(fileOutputFull);
                /* MessageBox.Show(testas.ToString());
                 MessageBox.Show(totalProgress.ToString());*/

            }
            workDone = true;
            toFile.OutputText(fileOutputFull);
            /* MessageBox.Show(testas.ToString());
             MessageBox.Show(totalProgress.ToString());*/

        }

        private void TessaLaunch(List<SlaveObjects> slaveObjects)
        {
            List<string> fileOutputFull = new List<string>();
            TesseractImpl tesseract = new TesseractImpl();
            maximumConcurrentThreads = int.Parse(threadCountBox.Text);            
            TextExtractionOutputToFile toFile = new TextExtractionOutputToFile();
            int testas = 0;



            /*  Parallel.ForEach(slaveObjects, new ParallelOptions { MaxDegreeOfParallelism = 3 },
              slaveObjects =>
              {
                  // logic                
                  testas++;
                  List<string> fileOutput = new List<string>();
                  fileOutput = tesseract.TesseractExtraction(slaveObjects);
                  fileOutputFull.AddRange(tesseract.TesseractExtraction(slaveObjects));
              });*/







            using (SemaphoreSlim concurrencySemaphore = new SemaphoreSlim(maximumConcurrentThreads))
            {
                List<Task> tasks = new List<Task>();
                foreach (SlaveObjects obj in slaveObjects)
                {
                    /*label3.Invoke((MethodInvoker)delegate
                    {
                        label3.Text = Process.GetCurrentProcess().Threads.Count.ToString();
                    });
                    progressBar1.Invoke((MethodInvoker)delegate { progressBar1.Value = progressBar1.Value + 10; });*/
                    concurrencySemaphore.Wait();


                    var t = Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            testas++;
                          //  List<string> fileOutput = new List<string>();
                           // fileOutput = tesseract.TesseractExtraction(obj);
                            fileOutputFull.AddRange(tesseract.TesseractExtraction(obj));


                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("Something happened");
                            MessageBox.Show(e.ToString());
                            slaveObjects.Add(obj);

                        }
                        finally
                        {
                            concurrencySemaphore.Release();
                        }
                    });

                    tasks.Add(t);
                }

                Task.WaitAll(tasks.ToArray());
                workDone = true;
                toFile.OutputText(fileOutputFull);
               /* MessageBox.Show(testas.ToString());
                MessageBox.Show(totalProgress.ToString());*/

            }
            workDone = true;
            toFile.OutputText(fileOutputFull);
           /* MessageBox.Show(testas.ToString());
            MessageBox.Show(totalProgress.ToString());*/
        }

        private void UpgradeMethod()
        {
            /*Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();*/
           // Thread.Sleep(10000);
            // Get the elapsed time as a TimeSpan value.
            //TimeSpan ts = stopWatch.Elapsed;
            /*string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                        ts.Hours, ts.Minutes, ts.Seconds,
                        ts.Milliseconds / 10);*/

            while (workDone == false) {
                TimeSpan ts = stopWatch.Elapsed;
                label3.Invoke((MethodInvoker)delegate
                {
                    label3.Text = ts.ToString();
                });
                progressBar1.Invoke((MethodInvoker)delegate
                {
                    progressBar1.Value = totalProgress;
                });

               /* if (updateNotProcessedFile)
                {
                    listBox3.Invoke((MethodInvoker)delegate
                    {
                        listBox3.Items.Clear();
                    });

                    foreach (FileInfo file in TesseractImpl.fileInfos)
                    {

                        listBox3.Invoke((MethodInvoker)delegate
                        {
                            listBox3.Items.Add(file.FullName);
                        });
                    }
                    updateNotProcessedFile = false;

                }*/

                if (updateProcessed)
                {
                    listBox1.Invoke((MethodInvoker)delegate
                    {
                        listBox1.Items.Clear();                        
                    });
                   /* listBox3.Invoke((MethodInvoker)delegate
                    {
                        listBox3.Items.Clear();
                    });*/

                    //  listBox1.Items.Add(inProgressFiles[test].FullName);                                       
                    foreach (FileInfo file in processedFiles.ToList())
                    {

                        listBox2.Invoke((MethodInvoker)delegate
                        {
                            listBox1.Items.Add(file.FullName);
                        });
                    }


                    foreach (FileInfo file in processedFiles.ToList())
                    {

                        for (int n = listBox3.Items.Count - 1; n >= 0; --n)
                        {
                            string removelistitem = file.FullName;
                            if (listBox3.Items[n].ToString().Contains(removelistitem))
                            {
                                listBox3.Invoke((MethodInvoker)delegate
                               {
                                   listBox3.Items.RemoveAt(n);
                               });
                            }
                        }
                    }




                    /*  for (int n = listBox1.Items.Count - 1; n >= 0; --n)
                      {
                          string removelistitem = "OBJECT";
                          if (listBox1.Items[n].ToString().Contains(removelistitem))
                          {
                              listBox1.Items.RemoveAt(n);
                          }
                      }*/





                    test++;
                    updateProcessed = false;
                    /* foreach (FileInfo file in inProgressFiles)
                     {
                         listBox1.Items.Add(file.FullName);
                     }*/
                }
                if (updateProcessing)
                {
                    listBox2.Invoke((MethodInvoker)delegate
                    {
                        listBox2.Items.Clear();
                    });
                    foreach (FileInfo file in inProgressFiles.ToList())
                    {
                        
                        listBox2.Invoke((MethodInvoker)delegate
                        {
                            listBox2.Items.Add(file.FullName);
                        });
                    }
                    updateProcessing = false;
                }

                    // Format and display the TimeSpan value.
                    /*string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                        ts.Hours, ts.Minutes, ts.Seconds,
                        ts.Milliseconds / 10);
                    Console.WriteLine("RunTime " + elapsedTime);*/
                }
            stopWatch.Stop();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"F:\MULTITHREAD4TESTSPACE";
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = openFileDialog.FileName;
                pictureDirectory = new DirectoryInfo(Path.GetDirectoryName(path).ToString());
            }
            notProcessedFiles = new List<FileInfo>();
            foreach(FileInfo info in pictureDirectory.GetFiles())
            {
                notProcessedFiles.Add(info);

            }
            foreach (FileInfo file in notProcessedFiles)
            {
                                
                    listBox3.Items.Add(file.FullName);
                
            }
        }
    }
}
