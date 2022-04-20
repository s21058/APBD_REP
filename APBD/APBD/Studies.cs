using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD
{
    public class Studies
    {
        public Studies(string studiesName, string mode) {
            this.studiesName = studiesName;
            this.mode = mode;
        }

        public string studiesName { get; set; }
         public string mode { get; set; }
    }
}
