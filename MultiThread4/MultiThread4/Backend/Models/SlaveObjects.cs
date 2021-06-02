using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MultiThread4.Backend.Models
{
    public class SlaveObjects
    {
       public List<FileInfo> fiveFilesForSlave;

        public SlaveObjects(List<FileInfo> fiveFilesForSlave)
        {
            this.fiveFilesForSlave = fiveFilesForSlave;
        }
    }
}
