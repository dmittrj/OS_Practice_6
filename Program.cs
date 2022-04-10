using System;

namespace OS_Practice_6
{
    class Program
    {
        public static void OS_SelectType()
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
            Console.Write(" >");
        }
        static void Main(string[] args)
        {
            OS_SelectType();
        }
    }
}
