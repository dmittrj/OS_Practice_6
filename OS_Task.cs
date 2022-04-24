using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Practice_6
{
    public class OS_Task
    {
        public char Identificator { get; set; }
        public int Size { get; set; }

        public OS_Task(char identificator, int size)
        {
            Identificator = identificator;
            Size = size;
        }
    }
}
