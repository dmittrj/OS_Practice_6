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
        public int Free { 
            get {
                long occupied = 0;
                List<long> emprySectors = new();
                foreach (OS_Task task in Tasks)
                {
                    if (task.Identificator == ' ')
                    {
                        emprySectors.Add(task.Size);
                    } else
                    {
                        occupied += task.Size;
                    }
                }
                emprySectors.Add(Size - occupied);
                long max = 0;
                foreach (long item in emprySectors)
                {
                    if (max < item) max = item;
                }
                return (int)max;
            } 
        }

        private int Number { get; set; }
        public List<OS_Task> Tasks { get; set; }

        public List<Tuple<int, int>> GetGaps()
        {
            int occupied = 0;
            List<Tuple<int, int>> emprySectors = new();
            int counter = 0;
            foreach (OS_Task task in Tasks)
            {
                if (task.Identificator == ' ')
                {
                    emprySectors.Add(new (task.Size, counter));
                }
                else
                {
                    occupied += task.Size;
                }
                counter++;
            }
            emprySectors.Add(new(Size - occupied, counter));
            return emprySectors;
        }

        public Section(int size, int number)
        {
            if (size <= 0) throw new Exception("Size can't be negative or zero");
            Tasks = new();
            Size = size;
            Number = number;
        }

        public void AddTask(char id, int task)
        {
            List<Tuple<int, int>> gaps = GetGaps();
            foreach (Tuple<int, int> item in gaps)
            {
                if (item.Item1 >= task)
                {
                    if (item.Item2 >= Tasks.Count)
                    {
                        Tasks.Add(new OS_Task(id, task));
                    } else
                    {
                        int old_size = Tasks[item.Item2].Size;
                        Tasks[item.Item2].Identificator = id;
                        Tasks[item.Item2].Size = task;
                        if (old_size - task > 0)
                            Tasks.Insert(item.Item2 + 1, new OS_Task(' ', old_size - task));
                    }
                    break;
                }
            }
            string str = "";
            for (int i = 0; i < Tasks[^1].Size; i++)
            {
                str += $"{id}";
            }
            System.IO.File.AppendAllText($"Раздел {Number}.txt", str);
        }

        public void UnloadTask(int task)
        {
            string textfromfile = System.IO.File.ReadAllText($"Раздел {Number}.txt");
            textfromfile = textfromfile.Replace($"Задача {Tasks[task].Identificator}\n", " ");
            Tasks[task].Identificator = ' ';
            System.IO.File.WriteAllText($"Раздел {Number}.txt", textfromfile);
        }
    }
}
