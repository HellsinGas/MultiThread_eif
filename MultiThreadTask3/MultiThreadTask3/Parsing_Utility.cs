using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;

namespace MultiThreadTask3
{
    class Parsing_Utility
    {
        public static string valuesList;
        public static List<DataModels> outputList = new List<DataModels>();
        public bool firstTime=true;
        public int filecount = 0;
        public void ParseAttributeMain(FileInfo filePath , List<AttributeAndPosition> attributePosition)
        {
            List<DataModels> fileParsingList = new List<DataModels>();

            if (firstTime)
            {
                firstRunTime(attributePosition);
            }
            
                        
            string lastline = "'";

            foreach(string line in File.ReadLines(filePath.FullName))
            {
                
                    if (line.Contains(lastline))
                    {
                        string s = line;                  
                        
                    string[] stringArray = s.Split(',');
                     int i = 0;
                        foreach(AttributeAndPosition entry in attributePosition)
                    {
                        outputList[i].attribValues.Add(stringArray[entry.attributePosition]);
                        
                        i++;
                    }
                      
                     

                    }

                   
                
            }


            filecount++;
        }

        private void firstRunTime(List<AttributeAndPosition> attributePosition)
        {
            foreach(AttributeAndPosition entry in attributePosition)
            {
                outputList.Add(new DataModels(entry.attributeName, new List<string>()));
            }
            firstTime = false;
        }

        public void outputFileGeneration()
        {
            string outputPath = @"F:\MULTHREAD3TESTPACE\MyOutputFile.arff";
            try
            {
                File.Create(outputPath).Dispose();
                
                
                using (StreamWriter sw = new StreamWriter(outputPath))
                {
                    
                    sw.WriteLine("@relation SMILEfeatures");
                    sw.WriteLine(" ");                    
                    foreach (DataModels dataModels in outputList)
                    {
                        
                            sw.WriteLine(dataModels.attribName);
                        
                    }
                    sw.WriteLine(" ");
                    sw.WriteLine("@data");
                    sw.WriteLine(" ");
                    
                    for (int i = 0; i <filecount; i++)
                    {
                        //string something="";
                        foreach (DataModels dataModels in outputList)
                        {

                            
                            sw.Write($"{dataModels.attribValues[i]},");
                            
                        }                       
                        sw.Write("\r\n");
                    }
                    sw.Close();

                }
                
                
                
            }
            catch (Exception e)
            {

                MessageBox.Show(e.ToString());
            }
            outputList.Clear();
        }
    }
}
