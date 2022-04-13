using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Practice_6
{
    class Section
    {
        /// <summary>
        /// Task size, bytes
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// Free space, bytes
        /// </summary>
        public int Free { get {
                long occupied = 0;
                foreach (FileInfo task in Tasks)
                {
                    occupied += task.Length;
                }
                return (int)(Size - occupied);
            } 
        }
        public List<FileInfo> Tasks { get; set; }

        public Section(int size)
        {
            if (size <= 0) throw new Exception("Size can't be negative or zero");
            Tasks = new List<FileInfo>();
            Size = size;
        }

        public void AddTask(FileInfo task)
        {
            Tasks.Add(task);
        }
    }
}
