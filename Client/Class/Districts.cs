using PikodAorfLayout.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PikodAorfLayout.data
{
    class Districts : IComparable
    {
        public string name { get; set; }
        public IList<string> cities { get; set; }

        public int CompareTo(object? obj)
        {
            Districts other = obj as Districts;
            return this.name.CompareTo(other.name);
        }
    }
}
