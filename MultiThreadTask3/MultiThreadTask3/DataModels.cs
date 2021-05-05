using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThreadTask3
{
    class DataModels
    {
        public string attribName;        
        public List<string> attribValues;

        public DataModels(string attribName, List<string> attribValues)
        {
            this.attribName = attribName;
            this.attribValues = attribValues;
        }
    }
}
