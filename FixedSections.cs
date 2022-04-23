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
        public Queue<int> Logs { get; set; }
        private char ID { get; set; }
        public FixedSections(int count)
        {
            int size = 64 * 1024 / count;
            Sections = new List<Section>();
            Logs = new();
            ID = 'A';
            for (int i = 0; i < count; i++)
            {
                try
                {
                    Sections.Add(new Section(size, i));
                } 
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            while (true)
            {
                DrawAkula();
            }
            
        }

        private void DrawAkula()
        {
            Console.Clear();
            Console.WriteLine("┌─ Фиксированные разделы ──────────────────────────────────────┐");
            for (int i = 0; i < Sections.Count; i++)
            {
                Console.Write("│ Раздел " + i.ToString());
                Console.Write(": ");
                int counter = 0;
                for (int j = 0; j < Sections[i].Tasks.Count; j++)
                {
                    for (int k = 0; k < 50 * Sections[i].Tasks[j].Size / Sections[i].Size; k++)
                    {
                        Console.Write(Sections[i].Tasks[j].Identificator.ToString());
                        counter++;
                    }
                }
                for (; counter < 50; counter++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine(" [" + (Sections[i].Size - Sections[i].Free).ToString() + "/" + Sections[i].Size.ToString() + "]");
            }
            Console.WriteLine("└──────────────────────────────────────────────────────────────┘");
            Console.WriteLine(" A - добавить задачу   D - удалить задачу");
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.A:
                    Console.Write("\n Размер задачи > ");
                    try
                    {
                        AddTask(OS_Inputing.OS_Int(0, OS_Inputing.Infinity));
                    }
                    catch (Exception ex)
                    {
                        Console.Write(" Подсистема управления памятью выдала следующую ошибку:\n ");
                        Console.WriteLine(ex.Message);
                        Console.ReadKey();
                    }
                    break;
                case ConsoleKey.D:
                    Console.Write("\n ID задачи > ");
                    try
                    {
                        UnloadTask(Console.ReadLine()[0]);
                    }
                    catch (Exception ex)
                    {
                        Console.Write(" Подсистема управления памятью выдала следующую ошибку:\n ");
                        Console.WriteLine(ex.Message);
                        Console.ReadKey();
                    }
                    break;
            }
        }


        public void AddTask(int task_size)
        {
            if (task_size > Sections[0].Size)
            {
                throw new Exception("Размер задачи слишком велик");
            }
            int max = Sections[0].Free;
            int max_number = 0;
            for (int i = 0; i < Sections.Count; i++)
            {
                if (max < Sections[i].Free)
                {
                    max = Sections[i].Free;
                    max_number = i;
                }
            }
            if (max < task_size)
            {
                throw new Exception("Размер задачи слишком велик");
            } else
            {
                Sections[max_number].AddTask(ID++, task_size);
            }
        }
    
        public void UnloadTask(char id)
        {
            foreach (Section section in Sections)
            {
                int counter = 0;
                foreach (OS_Task task in section.Tasks)
                {
                    if (task.Identificator == id)
                    {
                        section.UnloadTask(counter);
                        return;
                    }
                    counter++;
                }
            }
            throw new Exception("Нет задачи с таким ID");
        }
    }
}
