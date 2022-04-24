using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Practice_6
{
    public class OS_VirtualAddress
    {
        public OS_Task Task { get; set; }
        public int Address { get; set; }
        public int Page { get; set; }
        public OS_VirtualAddress(OS_Task task, int address, int page)
        {
            Task = task;
            Address = address;
            Page = page;
        }
    }
}
