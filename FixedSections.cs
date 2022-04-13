using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Practice_6
{
    class FixedSections
    {
        public List<Section> Sections { get; set; }
        public FixedSections(int count, params int[] sections)
        {
            int size = 64 * 1024 / count;
            Sections = new List<Section>();
            for (int i = 0; i < count; i++)
            {
                try
                {
                    Sections.Add(new Section(size));
                } catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            }
        }

        public void AddProgram(FileInfo file)
        {
            if (file.Length > Sections[0].Size)
            {
                throw new Exception("Too big task");
            }
            int max = Sections[0].Free;
            int max_number = 0;
            for (int i = 0; i < Sections.Count; i++)
            {
                if (max > Sections[i].Free)
                {
                    max = Sections[i].Free;
                    max_number = i;
                }
            }
            if (max < file.Length)
            {
                throw new Exception("Too big task");
            } else
            {
                Sections[max_number].AddTask(file);
            }
        }
    
        public void UnloadProgram(int section, int task)
        {
            Sections[section].UnloadTask(task);
        }
    }
}
