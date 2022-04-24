using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Practice_6
{
    class PageMemory
    {
        private char ID { get; set; }
        private List<OS_Address> AdrTable { get; set; }
        private List<OS_VirtualAddress> VirtAdrTable { get; set; }
        private List<OS_Task> Tasks { get; set; }
        private bool Lite { get; set; }

        string OZU;
        public PageMemory(bool lite)
        {
            //int size = 64 * 1024 / count;
            AdrTable = new();
            Tasks = new();
            VirtAdrTable = new();
            ID = 'A';
            Lite = lite;
            while (true)
            {
                DrawAkula();
            }
        }

        private string PeekVirtualAddress(float adr)
        {
            foreach (OS_VirtualAddress item in VirtAdrTable)
            {
                if (item.Address <= adr &&
                    item.Address + item.Task.Size >= adr)
                {
                    return item.Task.Identificator.ToString();
                }
            }
            return " ";
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
            foreach (OS_VirtualAddress item in VirtAdrTable)
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
            Console.WriteLine("┌─ Страничная организация ───────────────────────────┐");
            for (int i = 0; i < 16; i++)
            {
                Console.Write("│ ");
                for (int j = 0; j < 50; j++)
                {
                    Console.Write(PeekAddress((j + 50 * i) * 81.92f));
                }
                Console.WriteLine(" │");
            }
            Console.WriteLine("├────────────────────────────────────────────────────┤");
            foreach (OS_Address item in AdrTable)
            {

            }
            Console.WriteLine(" A - добавить задачу   D - удалить задачу   G - обратиться");
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
            int page_count = task_size / 4096;
            if (task_size % 4096 != 0) page_count++;
            //AdrTable.Add(new OS_Address(new OS_Task(ID++, task_size), i));
            for (int i = 0; i < page_count; i++)
            {
                for (int j = 0; j < 16; j += 4096)
                {
                    if (FreeAddress(j * 4096, (j + 1) * 4096 - 1))
                    {
                        VirtAdrTable.Add(new OS_VirtualAddress(new OS_Task(ID, 4096), j * 4096, i));
                        //ComposeOZU();
                    }
                }
            }
            AdrTable.Add(new OS_Address(new OS_Task(ID++, task_size), 0));
            //for (int i = 0; i < 65536 - task_size; i++)
            //{
            //    if (FreeAddress(i, i + task_size))
            //    {
            //        AdrTable.Add(new OS_Address(new OS_Task(ID++, task_size), i));
            //        ComposeOZU();
            //        return;
            //    }
            //}
            //throw new Exception("Недостаточно свободного места");
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
