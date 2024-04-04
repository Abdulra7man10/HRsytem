using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRsytem
{
    internal class HRsystem
    {
        private List<Employee> employees;
        private int employeeCounter;
        private int biggestEmployee;
        private List<Department> departments;
        private int departmentCounter;
        private int biggestDepartment;
        public HRsystem() 
        {
            employees = new List<Employee>();
            employeeCounter = 0;
            biggestEmployee = 0;
            departments = new List<Department>();
            departments.Add(new Department("No Department", 0));
            departmentCounter = 1;
            biggestDepartment = 1;
        }
        public void MainHub()
        {
            string choice;
            int choiceInt;

            do
            {
                Console.Clear();
                Console.WriteLine("-----------------------");
                Console.WriteLine("|1- Employee           |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|2- Department         |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|0- End Program        |");
                Console.WriteLine("-----------------------");

                choice = Console.ReadLine();
            } while (!int.TryParse(choice, out choiceInt) || choiceInt < 0 || choiceInt > 2);

            // Now choiceInt contains the valid choice
            switch (choiceInt)
            {
                case 1:
                    EmployeeHub();
                    break;
                case 2:
                    DepartHub();
                    break;
                case 0:
                    return;
            }
            Console.ReadKey();
            MainHub();
        }

        public void EmployeeHub()
        {
            string choice;
            int choiceInt;

            do
            {
                Console.Clear();
                Console.WriteLine("-----------------------");
                Console.WriteLine("|1- Add                |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|2- Edit               |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|3- Remove             |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|4- Salary             |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|5- Report             |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|6- Employee Details   |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|0- Back               |");
                Console.WriteLine("-----------------------");

                Console.WriteLine("Choose order : ");
                choice = Console.ReadLine();
            } while (!int.TryParse(choice, out choiceInt) || choiceInt < 0 || choiceInt > 6);

            switch (choiceInt)
            {
                case 1:
                    AddEmployee();
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 0:
                    return;
            }
            Console.ReadKey();
            EmployeeHub();
        }

        public void AddEmployee()
        {
            string choice;
            int choiceInt;
            do
            {
                Console.Clear();
                Console.WriteLine("-----------------------");
                Console.WriteLine("|1- Hourly             |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|2- Salaried           |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|3- Manager            |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|4- Commission         |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|0- Back               |");
                Console.WriteLine("-----------------------");

                Console.WriteLine("Choose type of Employee : ");
                choice = Console.ReadLine();
            } while (!int.TryParse(choice, out choiceInt) || choiceInt < 0 || choiceInt > 4);

            string em, ph, jo, na;
            int de;
            if (choiceInt != 0)
            {
                Console.WriteLine("Name : ");
                na = Console.ReadLine();
                Console.WriteLine("Job title : ");
                jo = Console.ReadLine();
                Console.WriteLine("Phone number : ");
                ph = Console.ReadLine();
                Console.WriteLine("Email : ");
                em = Console.ReadLine();
                //Console.WriteLine("Department ID : ");
                de = 0;
            }

            Employee new_emp = null;
            switch (choiceInt)
            {
                case 1:
                    double hw, r;
                    Console.WriteLine("hours worked : ");
                    hw = double.Parse(Console.ReadLine());
                    Console.WriteLine("Rate : ");
                    r = double.Parse(Console.ReadLine());
                    new_emp = new HourlyEmployee(biggestEmployee, hw, r);
                    break;
                case 2:
                    double s;
                    Console.WriteLine("Salary : ");
                    s = double.Parse(Console.ReadLine());
                    new_emp = new SalariedEmployee(biggestEmployee, s);
                    break;
                case 3:
                    double b;
                    Console.WriteLine("Salary : ");
                    s = double.Parse(Console.ReadLine());
                    Console.WriteLine("Bonus : ");
                    b = double.Parse(Console.ReadLine());
                    new_emp = new ManagerEmployee(biggestEmployee, s, b);
                    break;
                case 4:
                    double t;
                    Console.WriteLine("Target : ");
                    t = double.Parse(Console.ReadLine());
                    Console.WriteLine("Rate : ");
                    r = double.Parse(Console.ReadLine());
                    new_emp = new CommissionEmployee(biggestEmployee, t, r);
                    break;
                case 0:
                    return;
            }
            if (new_emp != null)
            {
                new_emp.Name = na;
                new_emp.JobTitle = jo;
                new_emp.PhoneNumber = ph;
                new_emp.Email = em;
                departments[de].addEmployee(new_emp);
            }

            employees.Add(new_emp);

            biggestEmployee++;
            employeeCounter++;
        }

        public void DepartHub()
        {
            string choice;
            int choiceInt;

            do
            {
                Console.Clear();
                Console.WriteLine("-----------------------");
                Console.WriteLine("|1- Add                |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|2- Edit               |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|3- Remove             |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|4- Salary             |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|5- Report             |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|6- Depart Details     |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|0- Back               |");
                Console.WriteLine("-----------------------");

                choice = Console.ReadLine();
            } while (!int.TryParse(choice, out choiceInt) || choiceInt < 0 || choiceInt > 6);

            switch (choiceInt)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 0:
                    return;
            }
            Console.ReadKey();
            DepartHub();
        }
    }
}
