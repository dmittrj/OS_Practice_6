using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Practice_6
{
    class Section
    {

        public int Size { get; set; }
        public int Free { get {
                return 1;
            } 
        }

        public Section(int size)
        {
            if (size <= 0) throw new Exception("Size can't be negative or zero");
            Size = size;
        }
    }
}
