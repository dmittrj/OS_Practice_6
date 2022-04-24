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
        public int SectionsCount { get; set; }
        public List<int> SectionsSize { get; set; }
        private char ID { get; set; }
        private List<OS_Address> AdrTable { get; set; }

        string OZU;
        public FixedSections(int count, List<int> sizes)
        {
            //int size = 64 * 1024 / count;
            SectionsSize = new();
            AdrTable = new();
            ID = 'A';
            for (int i = 0; i < count; i++)
            {
                try
                {
                    SectionsSize.Add(sizes[i]);

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

        private string PeekAddress(float adr)
        {
            foreach (OS_Address item in AdrTable)
            {
                if (item.Address <= adr &&
                    item.Address + item.Task.Size >= adr)
                {
                    return item.Task.Identificator.ToString();
                }
            }
            return " ";
        }

        private bool FreeAddress(float adrStart, float adrFinish)
        {
            foreach (OS_Address item in AdrTable)
            {
                if (item.Address < adrFinish &&
                    item.Address + item.Task.Size > adrStart)
                {
                    return false;
                }
            }
            return true;
        }

        private void ComposeOZU()
        {
            OZU = "";
            for (int j = 0; j < 65536; j++)
            {
                OZU += PeekAddress(j);
            }
            System.IO.File.WriteAllText("ОЗУ", OZU);
        }

        private void DrawAkula()
        {
            Console.Clear();
            Console.WriteLine("┌─ Фиксированные разделы ────────────────────────────┐");
            for (int i = 0; i < 10; i++)
            {
                Console.Write("│ ");
                for (int j = 0; j < 50; j++)
                {
                    Console.Write(PeekAddress((j + 50 * i) * 131.072f));
                }
                Console.WriteLine(" │");


                //int counter = 0;
                //for (int j = 0; j < Sections[i].Tasks.Count; j++)
                //{
                //    for (int k = 0; k < 50 * Sections[i].Tasks[j].Size / Sections[i].Size; k++)
                //    {
                //        Console.Write(Sections[i].Tasks[j].Identificator.ToString());
                //        counter++;
                //    }
                //}
                //for (; counter < 50; counter++)
                //{
                //    Console.Write(" ");
                //}
                //Console.WriteLine(" [" + (Sections[i].Size - Sections[i].Free).ToString() + "/" + Sections[i].Size.ToString() + "]");
            }
            Console.WriteLine("└────────────────────────────────────────────────────┘");
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
            int start_address = 0;
            foreach (int item in SectionsSize)
            {
                if (FreeAddress(start_address, start_address + item) && task_size <= item)
                {
                    AdrTable.Add(new OS_Address(new OS_Task(ID++, task_size), start_address));
                    ComposeOZU();
                    return;
                } else
                {
                    start_address += item;
                }
            }
            throw new Exception("Нет ни одного подходящего раздела");
        }
    
        public void UnloadTask(char id)
        {
            foreach (OS_Address section in AdrTable)
            {
                if (section.Task.Identificator == id)
                {
                    AdrTable.Remove(section);
                    ComposeOZU();
                    return;
                }
            }
            throw new Exception("Нет задачи с таким ID");
        }
    }
}
