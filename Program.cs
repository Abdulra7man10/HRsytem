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
            Department d = new Department("Developer",1);
            Employee e = new HourlyEmployee(1,30,100);
            e.Name = "Abdo";
            e.JobTitle = "Junior";
            e.Email = "abdo144418@gmail.com";
            e.PhoneNumber = "01143685049";
            e.Depart = d;
            e.ExtraMethod(5);
            Console.WriteLine(e.displayDetails());
        }
    }

}
