using System;

namespace OS_Practice_6
{
    enum MemoryType { 
        FixedSections = 1,
        RoamingSections = 2
    }
    class Program
    {
        public static MemoryType OS_SelectType()
        {
            Console.WriteLine(" Выберите алгоритм управления памятью:");
            Console.WriteLine("  1. Распределение памяти фиксированными разделами");
            Console.WriteLine("  2. Распределение памяти перемещаемыми разделами");
            Console.WriteLine("  3. Распределение памяти разделами переменной величины");
            Console.WriteLine("  4. Страничное распределение памяти");
            Console.WriteLine("  5. Страничное распределение памяти (упрощённый вариант)"); 
            Console.WriteLine("  6. Сегментное распределение памяти");
            Console.WriteLine("  7. Странично-сегметное распределение памяти");
            Console.WriteLine("  8. Свопинг");
            Console.Write(" > ");
            return (MemoryType)OS_Inputing.OS_Int(1, 8);
        }
        static void Main(string[] args)
        {
            MemoryType selection = OS_SelectType();
            switch (selection)
            {
                case MemoryType.FixedSections:
                    Console.Clear();
                    Console.WriteLine(" 1. Распределение памяти фиксированными разделами\n");
                    Console.WriteLine(" На сколько разделов вы хотите разделить оперативную память?");
                    Console.Write(" > ");
                    int c = OS_Inputing.OS_Int(1, OS_Inputing.Infinity);
                    FixedSections sys = new(c);
                    break;
                case MemoryType.RoamingSections:
                    break;
                default:
                    break;
            }
        }
    }
}
