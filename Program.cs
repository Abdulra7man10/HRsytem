using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRsytem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Employee e = new Employee() { ID = 1 };
            Console.WriteLine(e.ID);
            Console.ReadLine();
        }
    }
}
