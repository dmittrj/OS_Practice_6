using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Practice_6
{
    class RoamingSections
    {
        private char ID { get; set; }
        private List<OS_Address> AdrTable { get; set; }

        string OZU;
        public RoamingSections()
        {
            //int size = 64 * 1024 / count;
            AdrTable = new();
            ID = 'A';
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

        private int FreeAddress(int adrRight)
        {
            for (int i = 1; i < adrRight; i++)
            {
                if (!FreeAddress(adrRight - i, adrRight))
                {
                    return i - 1;
                }
            }
            return adrRight;
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
            Console.WriteLine("┌─ Разделы переменной величины ──────────────────────┐");
            for (int i = 0; i < 10; i++)
            {
                Console.Write("│ ");
                for (int j = 0; j < 50; j++)
                {
                    Console.Write(PeekAddress((j + 50 * i) * 131.072f));
                }
                Console.WriteLine(" │");
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

        private void Optimize()
        {
            bool not_avaliable_moves = true;
            while (not_avaliable_moves)
            {
                not_avaliable_moves = false;
                foreach (OS_Address item in AdrTable)
                {
                    if (FreeAddress(item.Address - 1, item.Address) && item.Address > 0)
                    {
                        item.Address -= FreeAddress(item.Address);
                        not_avaliable_moves = true;
                    }
                }
            }
        }


        public void AddTask(int task_size)
        {
            for (int i = 0; i < 65536 - task_size; i++)
            {
                if (FreeAddress(i, i + task_size))
                {
                    AdrTable.Add(new OS_Address(new OS_Task(ID++, task_size), i));
                    ComposeOZU();
                    return;
                }
            }
            throw new Exception("Недостаточно свободного места");
        }

        public void UnloadTask(char id)
        {
            foreach (OS_Address section in AdrTable)
            {
                if (section.Task.Identificator == id)
                {
                    AdrTable.Remove(section);
                    Optimize();
                    ComposeOZU();
                    return;
                }
            }
            throw new Exception("Нет задачи с таким ID");
        }
    }
}
