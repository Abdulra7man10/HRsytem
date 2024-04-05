using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRsytem
{
    public delegate bool Filter<in T>(T value);
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
                Console.WriteLine("Main");
                Console.WriteLine("-----------------------");
                Console.WriteLine("|1- Employee           |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|2- Department         |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|0- End Program        |");
                Console.WriteLine("-----------------------");

                Console.Write("Choose Hub : ");
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
                Console.WriteLine("Employee");
                Console.WriteLine("-----------------------");
                Console.WriteLine("|1- Add                |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|2- Edit               |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|3- Remove             |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|4- Report             |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|5- Employee Details   |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|0- Back               |");
                Console.WriteLine("-----------------------");

                Console.Write("Choose order : ");
                choice = Console.ReadLine();
            } while (!int.TryParse(choice, out choiceInt) || choiceInt < 0 || choiceInt > 5);

            switch (choiceInt)
            {
                case 1:
                    AddEmployee();
                    break;
                case 2:
                    EditEmployee();
                    break;
                case 3:
                    DeleteEmployee();
                    break;
                case 4:
                    ReportEmployee();
                    break;
                case 5:
                    {
                        int id;
                        Console.WriteLine("Enter Employee ID : ");
                        int.TryParse(Console.ReadLine(), out id);
                        id = search<Employee>(e => e.ID == id);
                        if (id != -1)
                            Console.WriteLine(employees[id].displayDetails());
                        else
                            Console.WriteLine("This ID is not exist :(");
                    }
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
                Console.WriteLine("Employee");
                Console.WriteLine("Add");
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

                Console.Write("Choose type of Employee : ");
                choice = Console.ReadLine();
            } while (!int.TryParse(choice, out choiceInt) || choiceInt < 0 || choiceInt > 4);

            if (choiceInt == 0) return;

            string em="", ph="", jo="", na="";
            int de=0;
            Console.Write("Name : ");
            na = Console.ReadLine();
            Console.Write("Job title : ");
            jo = Console.ReadLine();
            Console.Write("Phone number : ");
            ph = Console.ReadLine();
            Console.Write("Email : ");
            em = Console.ReadLine();
            Console.Write("Department ID : ");
            //int dID;
            int.TryParse(Console.ReadLine(), out de);
            de = search<Department>(depart => depart.ID == de);
            if (de == -1) de = 0;

            Employee new_emp = null;
            switch (choiceInt)
            {
                case 1:
                    double hw, r;
                    Console.Write("hours worked : ");
                    hw = double.Parse(Console.ReadLine());
                    Console.Write("Rate : ");
                    r = double.Parse(Console.ReadLine());
                    new_emp = new HourlyEmployee(biggestEmployee, hw, r);
                    break;
                case 2:
                    double s;
                    Console.Write("Salary : ");
                    s = double.Parse(Console.ReadLine());
                    new_emp = new SalariedEmployee(biggestEmployee, s);
                    break;
                case 3:
                    double b;
                    Console.Write("Salary : ");
                    s = double.Parse(Console.ReadLine());
                    Console.Write("Bonus : ");
                    b = double.Parse(Console.ReadLine());
                    new_emp = new ManagerEmployee(biggestEmployee, s, b);
                    break;
                case 4:
                    double t;
                    Console.Write("Target : ");
                    t = double.Parse(Console.ReadLine());
                    Console.Write("Rate : ");
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
            Console.WriteLine($"\n  Done -->{na} ID is {biggestEmployee}");

            biggestEmployee++;
            employeeCounter++;
        }

        public void EditEmployee() 
        {
            string choice;
            int choiceInt;

            do
            {
                Console.Clear();
                Console.WriteLine("Employee");
                Console.WriteLine("Edit");
                Console.WriteLine("-----------------------");
                Console.WriteLine("|1- Name               |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|2- Email              |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|3- Phone number       |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|4- Job title          |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|5- Income             |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|6- Department         |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|0- Back               |");
                Console.WriteLine("-----------------------");

                Console.Write("Choose order : ");
                choice = Console.ReadLine();
            } while (!int.TryParse(choice, out choiceInt) || choiceInt < 0 || choiceInt > 6);

            if (choiceInt == 0) return;

            Console.Write("Enter Employee ID : ");
            int id = int.Parse(Console.ReadLine());
            id = search<Employee>(e => e.ID == id);
            if (id == -1)
            {
                Console.WriteLine("This ID is Invalid :(");
                return;
            }
            string s = "";
            switch (choiceInt)
            {
                case 1:
                    {
                        Console.WriteLine($"Old Name : {employees[id].Name}");
                        Console.Write("Enter new name : ");
                        s = Console.ReadLine();
                        if (search<Employee>(e => e.Name == s) == -1)
                        {
                            employees[id].Name = s;
                            Console.WriteLine("The name is EDITED :)");
                        }
                        else
                            Console.WriteLine("The name isn't modified, there are two name equals :(");
                    }
                    break;
                case 2:
                    {
                        Console.WriteLine($"Old Email : {employees[id].Email}");
                        Console.Write("Enter new Email : ");
                        s = Console.ReadLine();
                        employees[id].Email = s;
                        Console.WriteLine("The Email is EDITED :)");
                    }
                    break;
                case 3:
                    {
                        Console.WriteLine($"Old Phone number : {employees[id].PhoneNumber}");
                        Console.Write("Enter new Phone number : ");
                        s = Console.ReadLine();
                        employees[id].PhoneNumber = s;
                        Console.WriteLine("The Phone number is EDITED :)");
                    }
                    break;
                case 4:
                    {
                        Console.WriteLine($"Old Job title : {employees[id].JobTitle}");
                        Console.Write("Enter new Job title : ");
                        s = Console.ReadLine();
                        employees[id].JobTitle = s;
                        Console.WriteLine("The Job title is EDITED :)");
                    }
                    break;
                case 5:
                    {
                        if (employees[id] is HourlyEmployee)
                        {
                            Console.WriteLine($"Old Hours worked - Rate : {employees[id].getDetails()}");
                            Console.Write("Enter more hours : ");
                            double more = double.Parse(Console.ReadLine());
                            Console.Write("Enter new Rate : ");
                            double r = double.Parse(Console.ReadLine());
                            employees[id].ExtraMethod(more,r);
                        }
                        else if (employees[id] is SalariedEmployee)
                        {
                            Console.WriteLine($"Old Salary : {employees[id].getDetails()}");
                            Console.Write("Enter Salary : ");
                            double more = double.Parse(Console.ReadLine());
                            employees[id].ExtraMethod(more);
                        }
                        else if (employees[id] is ManagerEmployee)
                        {
                            Console.WriteLine($"Old Salary - Bonus : {employees[id].getDetails()}");
                            Console.Write("Enter Salary : ");
                            double ss = double.Parse(Console.ReadLine());
                            Console.Write("Enter Salary : ");
                            double more = double.Parse(Console.ReadLine());
                            employees[id].ExtraMethod(more,ss);
                        }
                        else
                        {
                            Console.WriteLine($"Old Target - Rate : {employees[id].getDetails()}");
                            Console.Write("Enter Target : ");
                            double more = double.Parse(Console.ReadLine());
                            Console.Write("Enter Rate : ");
                            double r = double.Parse(Console.ReadLine());
                            employees[id].ExtraMethod(more,r);
                        }
                        Console.WriteLine("The Income is EDITED :)");
                    }
                    break;
                case 6:
                    {
                        Console.WriteLine($"Old Department : {employees[id].Depart.Name} - {employees[id].Depart.ID}");
                        Console.Write("Enter new Department ID : ");
                        int d = int.Parse(Console.ReadLine());
                        d = search<Department>(dep => dep.ID == d);
                        if (d != -1)
                        {
                            employees[id].Depart = departments[d];
                            Console.WriteLine("Department was Edited :)");
                        }
                        else
                            Console.WriteLine("Department IS invalid :(");
                    }
                    break;
                case 0:
                    return;
            }
            Console.ReadKey();
            EditEmployee();
        }

        public void DeleteEmployee() 
        {
            Console.Write("Enter Employee's id to delete : ");
            int id = int.Parse(Console.ReadLine());
            int idx = search<Employee>(e => e.ID == id);
            if (idx == -1)
            {
                Console.WriteLine("This ID is not Exist :(");
                return;
            }
            employees.Remove(employees[idx]);
            employeeCounter--;
            Console.WriteLine("The Employee is deleted :)");
        }

        public void ReportEmployee() 
        {
            string choice;
            int choiceInt;

            do
            {
                Console.Clear();
                Console.WriteLine("-----------------------");
                Console.WriteLine("|1- Name               |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|2- Email              |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|3- Phone number       |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|4- Job title          |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|5- Income             |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|6- Department         |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|0- Back               |");
                Console.WriteLine("-----------------------");

                Console.Write("Choose order : ");
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
            EditEmployee();
        }

        public int search<T>(Filter<T> filter)
        {
            int index = 0;
            foreach (T item in typeof(T) == typeof(Employee) ? (IEnumerable<T>)employees : (IEnumerable<T>)departments)
            {
                if (filter(item))
                    return index;
                index++;
            }
            return -1;
        }
        public bool exist<T>(Predicate<T> filter)
        {
            foreach (T item in typeof(T) == typeof(Employee) ? (IEnumerable<T>)employees : (IEnumerable<T>)departments)
            {
                if (filter(item))
                    return true;
            }
            return false;
        }


        public void DepartHub()
        {
            string choice;
            int choiceInt;

            do
            {
                Console.Clear();
                Console.WriteLine("Department");
                Console.WriteLine("-----------------------");
                Console.WriteLine("|1- Add                |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|2- Edit               |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|3- Remove             |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|4- Report             |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|5- Depart Details     |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|0- Back               |");
                Console.WriteLine("-----------------------");

                Console.Write("Choose order : ");
                choice = Console.ReadLine();
            } while (!int.TryParse(choice, out choiceInt) || choiceInt < 0 || choiceInt > 5);

            switch (choiceInt)
            {
                case 1:
                    {
                        string dName = "";
                        do
                        {
                            Console.Write("Enter Department name : ");
                            dName = Console.ReadLine();
                        } while (search<Department>(d => d.Name == dName) != -1);
                        Department new_dep = new Department(dName, biggestDepartment);
                        departments.Add(new_dep);
                        Console.WriteLine($" Depratment ID is {biggestDepartment} :)");
                        biggestDepartment++;
                        departmentCounter++;
                    }
                    break;
                case 2:
                    EditDepart();
                    break;
                case 3:
                    {
                        Console.Write("Enter Department's id to delete : ");
                        int id = int.Parse(Console.ReadLine());
                        int idx = search<Employee>(e => e.ID == id);
                        if (idx == -1)
                        {
                            Console.WriteLine("This ID is not Exist :(");
                            return;
                        }
                        foreach (Employee emp in departments[idx].Employees)
                        {
                            emp.Depart = departments[0];
                            departments[idx].removeEmployee(emp);
                        }
                        departments.Remove(departments[idx]);
                        departmentCounter--;
                        Console.WriteLine("The Department is deleted :)");
                    }
                    break;
                case 4:
                    break;
                case 5:
                    {
                        {
                            int id;
                            Console.WriteLine("Enter Department's ID : ");
                            int.TryParse(Console.ReadLine(), out id);
                            id = search<Department>(d => d.ID == id);
                            if (id != -1)
                            {
                                Console.WriteLine(departments[id].ToString());
                                Console.Write("Manager : ");
                                if (departments[id].Manager != null)
                                    Console.WriteLine($"{departments[id].Manager.Name} - {departments[id].Manager.ID}");
                                else
                                    Console.WriteLine("No Manager added");
                                Console.WriteLine("Employees :- ");
                                departments[id].AllEmployees();
                            }
                            else
                                Console.WriteLine("This ID is not exist :(");
                        }

                    }
                    break;
                case 0:
                    return;
            }
            Console.ReadKey();
            DepartHub();
        }

        public void EditDepart()
        {
            string choice;
            int choiceInt;

            do
            {
                Console.Clear();
                Console.WriteLine("Department");
                Console.WriteLine("Edit");
                Console.WriteLine("--------------------------");
                Console.WriteLine("|1- Name                  |");
                Console.WriteLine("|                         |");
                Console.WriteLine("|2- Manager               |");
                Console.WriteLine("|                         |");
                Console.WriteLine("|3- Transfir Employees    |");
                Console.WriteLine("| From Depart to another  |");
                Console.WriteLine("|                         |");
                Console.WriteLine("|0- Back                  |");
                Console.WriteLine("-----------------------");

                Console.Write("Choose order : ");
                choice = Console.ReadLine();
            } while (!int.TryParse(choice, out choiceInt) || choiceInt < 0 || choiceInt > 3);

            int id;
            if (choiceInt == 0)
                return;
            Console.Write("Enter Department ID : ");
            id = int.Parse(Console.ReadLine());
            id = search<Department>(d => d.ID == id);
            if (id == -1)
            {
                Console.WriteLine("This ID is Invalid :(");
                return;
            }
            string s = "";
            switch (choiceInt)
            {
                case 1:
                    {
                        Console.WriteLine($"Old Name : {departments[id].Name}");
                        Console.Write("Enter new name : ");
                        s = Console.ReadLine();
                        if (search<Department>(d => d.Name == s) == -1)
                        {
                            departments[id].Name = s;
                            Console.WriteLine("The name is EDITED :)");
                        }
                        else
                            Console.WriteLine("The name isn't modified, there are two name equals :(");
                    }
                    break;
                case 2:
                    {
                        Console.Write("Old Manager : ");
                        if (departments[id].Manager != null)
                            Console.WriteLine($"{departments[id].Manager.Name} - {departments[id].Manager.ID}");
                        else
                            Console.WriteLine("No Manager added");
                        Console.Write("Enter new Manger ID : ");
                        int i = int.Parse(Console.ReadLine());
                        i = search<Employee>(e => e.ID == i);
                        if (i != -1)
                        {
                            departments[id].Manager = employees[i];
                            Console.WriteLine("The Manager is EDITED :)");
                        }
                        else
                            Console.WriteLine("The ID is not exist :(");
                    }
                    break;
                case 3:
                    {
                        Console.WriteLine($"All Employees in {departments[id].Name} :- ");
                        departments[id].AllEmployees();
                        Console.Write("Enter new Department's id to transfir : ");
                        int d = int.Parse(Console.ReadLine());
                        d = search<Department>(dep => dep.ID == d);
                        if (d != -1)
                        {
                            char ch;
                            Console.Write($"Are you sure to transfir all employees to {departments[d].Name} (y/n) ? ");
                            ch = Console.ReadKey().KeyChar;
                            Console.WriteLine();
                            if (ch == 'y' || ch == 'Y')
                            {
                                foreach(Employee emp in departments[id].Employees)
                                    departments[d].addEmployee(emp);
                                departments[id].Manager = null;
                                departments[id].Employees.Clear();
                                Console.WriteLine("Transfiring is Done :)");
                            }

                        }
                        else
                            Console.WriteLine("This ID is not Exist :(");
                    }
                    break;
                case 0:
                    return;
            }
            Console.ReadKey();
            EditDepart();
        }
    }
}
