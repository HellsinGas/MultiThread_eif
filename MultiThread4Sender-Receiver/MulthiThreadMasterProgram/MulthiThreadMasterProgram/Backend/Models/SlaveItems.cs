using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MulthiThreadMasterProgram.Backend.Models
{
    class SlaveItems
    {
        public List<FileInfo> filesForSlave;

        public SlaveItems(List<FileInfo> filesForSlave)
        {
            this.filesForSlave = filesForSlave;
        }

    }
}
