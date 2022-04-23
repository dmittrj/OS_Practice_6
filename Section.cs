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
                foreach (OS_Task task in Tasks)
                {
                    occupied += task.Size;
                }
                return (int)(Size - occupied);
            } 
        }

        private int Number { get; set; }
        public List<OS_Task> Tasks { get; set; }

        public Section(int size, int number)
        {
            if (size <= 0) throw new Exception("Size can't be negative or zero");
            Tasks = new();
            Size = size;
            Number = number;
        }

        public void AddTask(char id, int task)
        {
            Tasks.Add(new OS_Task(id, task));
            System.IO.File.AppendAllText($"Раздел {Number}.txt", $"Задача {id}\n");
        }

        public void UnloadTask(int task)
        {
            string textfromfile = System.IO.File.ReadAllText($"Раздел {Number}.txt");
            textfromfile = textfromfile.Replace($"Задача {Tasks[task].Identificator}\n", "");
            Tasks.RemoveAt(task);
            System.IO.File.WriteAllText($"Раздел {Number}.txt", textfromfile);
        }
    }
}
