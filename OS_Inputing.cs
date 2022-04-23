using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Practice_6
{
    public class OS_Inputing
    {
        public const int Infinity = int.MaxValue;
        public static int OS_Int(int min, int max)
        {
            int c;
        _OS_TryInput:
            try
            {
                c = int.Parse(Console.ReadLine());
                if (c > max) throw new Exception();
                if (c < min) throw new Exception();
            }
            catch
            {
                Console.Write("Повторите ввод\n > ");
                goto _OS_TryInput;
            }
            return c;
        }
    }
}
