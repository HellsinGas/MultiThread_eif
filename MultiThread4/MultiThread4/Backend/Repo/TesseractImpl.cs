using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;
using System.Diagnostics;
using MultiThread4.Backend;
using System.IO;
using System.Windows.Forms;
using MultiThread4.Backend.Models;



namespace MultiThread4.Backend.Repo
{
    class TesseractImpl
    {
        
        public List<string> TesseractExtraction(SlaveObjects slaveObjects)
        {
         
            List<string> pulledFileText = new List<string>();

            foreach (FileInfo file in slaveObjects.fiveFilesForSlave)
            {
                
                Form1.inProgressFiles.Add(file);
                

                Form1.updateProcessing = true;

                var testImagePath = file.FullName;
                /*  if (args.Length > 0)
                  {
                      testImagePath = args[0];
                }*/
                
                string text = null;

                try
                {
                    var engine = new TesseractEngine(@"./TesseractFiles", "eng", EngineMode.Default);

                    var img = Pix.LoadFromFile(testImagePath);
                        
                            var page = engine.Process(img);
                            
                                text = page.GetText();                   



                }
                catch (Exception e)
                {
                    Trace.TraceError(e.ToString());
                    MessageBox.Show("Unexpected Error: " + e.Message);
                    MessageBox.Show("Details: ");
                    MessageBox.Show(e.ToString());
                }
                // MessageBox.Show(text);
                pulledFileText.Add(text);
                Form1.totalProgress++;
                Form1.processedFiles.Add(file);
                Form1.inProgressFiles.Remove(file);                
                Form1.updateProcessed = true;
                //Form1.notProcessedFiles.Clear();
                //               
               // Form1.notProcessedFiles.Remove(file);
                //fileInfos.Remove(file);
              //  Form1.updateNotProcessedFile = true;
                // Form1.totalProgress = Form1.totalProgress + 1;
            }




           // MessageBox.Show($"how many times run :{Form1.totalProgress}");            
            return pulledFileText;
        }
    }
}
