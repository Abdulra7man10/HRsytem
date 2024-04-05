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
            HRsystem h = new HRsystem();
            h.MainHub();
            /*
            Department d = new Department("Developer",1);
            Employee e = new HourlyEmployee(1,30,100);
            e.Name = "Abdo";
            e.JobTitle = "Senior";
            e.Email = "abdo144418@gmail.com";
            e.PhoneNumber = "01143685049";
            e.ExtraMethod(5);
            Employee e2 = new SalariedEmployee(2,3000);
            e2.Name = "Ali";
            e2.JobTitle = "Junior";
            e2.Email = "ali22@gmail.com";
            e2.PhoneNumber = "0546924998";
            d.addEmployee(e2);
            d.changeManager(e);
            e2.PhoneNumber = "11111";
            Console.WriteLine(d.Employees[0].displayDetails());
            */
            //int idx1 = h.search<Employee>(emp=>emp.ID==1);
            //int idx2 = h.search<Department>(depart => depart.ID == 2);


        }
    }

}
