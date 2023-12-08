using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PikodAorfLayout.Class
{
    class massge
    {
        public massge()
        {

        }
        public massge(string data, double size,double FontSize)
        {
            this.data = data;
            this.Height = size;
            this.FontSize = FontSize;
   
            
        }
        public string data { get; set; }
        public double Height { get; set; }
        public double FontSize { get; set; }

    }
}
