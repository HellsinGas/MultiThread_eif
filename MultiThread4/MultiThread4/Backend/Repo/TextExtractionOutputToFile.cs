using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MultiThread4.Backend.Repo
{
   public class TextExtractionOutputToFile
    {


        public void OutputText(List<string> text)
        {
            var path = @"F:\MULTITHREAD4TESTSPACE\OutputDirectory\outputFile.txt";
            File.Create(path).Dispose();

            foreach(string singleFileText in text)
            {
                File.AppendAllText(path, singleFileText);
                File.AppendAllText(path, Environment.NewLine);

            }

        }

    }
}
