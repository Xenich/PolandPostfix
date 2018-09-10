using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PolandPostfix
{
    class Program
    {
        static void Main(string[] args)
        {

            string str = "1 2 / 2 3 + 9 2 - sin 2 ^ 6 7 / - / +";
            //string str = "1 +  2*(sqrt( 6)*sin 5)";
            //1 2 / 2 3 + 9 2 - sin 2 * 6 7 / - / +

            //1 2 / 2 3 + 802,54 sin 2 * 6 7 / - / +
            
            Console.WriteLine(str);
            Console.WriteLine(PolandPostfix.Calculate(str));

            Console.WriteLine(Math.Sin(7));
            Console.ReadKey();
        }
    }
}
