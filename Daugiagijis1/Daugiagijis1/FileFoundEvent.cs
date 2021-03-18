using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Daugiagijis1
{
    public class FileFoundEvent : EventArgs
    {
        public FileInfo Failas {get ; private set;}
        public FileFoundEvent(FileInfo failas)
        {
            Failas = failas;
        }
    }
}
