using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _02_Networking_Receiver_WPF
{
   public class SlaveObject
    {
        List<FileInfo> slaveObject;

        public SlaveObject(List<FileInfo> slaveObject)
        {
            this.slaveObject = slaveObject;
        }
    }
}
