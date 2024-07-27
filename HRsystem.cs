using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace HRsystem
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
            biggestEmployee = 0;
            departments = new List<Department>();
            //departments.Add(new Department("No Department", 0));
            //biggestDepartment = 1;

            string path = "C:\\Users\\abdo1\\Desktop\\Current_track\\1-Back-end-track\\C#\\HRsytem\\d\\departments.txt";
            var result = File.ReadAllLines(path);
            List<int> managersID = LoadDepartments(result);

            path = "C:\\Users\\abdo1\\Desktop\\Current_track\\1-Back-end-track\\C#\\HRsytem\\d\\employees.txt";
            result  = File.ReadAllLines(path);
            LoadEmployees(result);

            for (int i=0; i<managersID.Count; i++)
            {
                int index = search<Employee>(e => e.ID == managersID[i]);
                if (index != -1) 
                    departments[i].Manager = employees[index];
            }
        }
        private List<int> LoadDepartments(string[] departmentData)
        {
            biggestDepartment = int.Parse(departmentData[0].Split(' ').Last());
            List<int> managersId = new List<int>();
            foreach (var line in departmentData.Skip(1)) // Skip the header line
            {
                var parts = line.Split(' ').Select(p => p.Trim()).ToArray();
                if (parts.Length < 3) // Ensure there are enough parts
                {
                    Console.WriteLine($"Skipping invalid line: {line}");
                    continue;
                }

                int id;
                if (!int.TryParse(parts[0], out id))
                {
                    Console.WriteLine($"Invalid department ID format: {parts[0]}");
                    continue;
                }

                string name = parts[1].Replace('-', ' ');

                int managerID;
                if (!int.TryParse(parts[2], out managerID))
                {
                    Console.WriteLine($"Invalid manager ID format: {parts[2]}");
                    continue;
                }

                managersId.Add(managerID);
                Department depart = new Department(name, id);
                departments.Add(depart);
            }
            return managersId;
        }
        private void LoadEmployees(string[] employeeData)
        {
            biggestEmployee = int.Parse(employeeData[0].Split(' ').Last());
            foreach (var line in employeeData.Skip(1)) // Skip the header line
            {
                var parts = line.Split(' ');

                int id = int.Parse(parts[0]);
                string name = parts[1].Replace('-', ' ');
                string email = parts[2];
                string phoneNumber = parts[3];
                int departmentID = int.Parse(parts[4]);
                Department depart = departments[search<Department>(d => d.ID == departmentID)];
                int type = int.Parse(parts[5]);
                string jobTitle = parts[6].Replace('-', ' ');

                Employee employee = null;

                switch (type)
                {
                    case 1:
                        {
                            int hours = int.Parse(parts[7]);
                            int rate = int.Parse(parts[8]);
                            employee = new HourlyEmployee(id, name, email, phoneNumber, jobTitle, depart, hours, rate);
                            break;
                        }
                    case 2:
                        {
                            int salary = int.Parse(parts[7]);
                            employee = new SalariedEmployee(id, name, email, phoneNumber, jobTitle, depart, salary);
                            break;
                        }
                    case 3: 
                        {
                            int salary = int.Parse(parts[7]);
                            int bonus = int.Parse(parts[8]);
                            employee = new ManagerEmployee(id, name, email, phoneNumber, jobTitle, depart, salary, bonus);
                            break;
                        }
                    case 4:
                        {
                            int target = int.Parse(parts[7]);
                            int rate = int.Parse(parts[8]);
                            employee = new CommissionEmployee(id, name, email, phoneNumber, jobTitle, depart, target, rate);
                            break;
                        }
                }
                if (employee != null)
                {
                    biggestDepartment++;
                    employees.Add(employee);
                    depart?.addEmployee(employee);
                }
            }
        }
        public void SaveEmployeesToFile()
        {
            string filePath = "C:\\Users\\abdo1\\Desktop\\Current_track\\1-Back-end-track\\C#\\HRsytem\\d\\employees.txt";
            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine($"ID Name Email PhoneNumber DepartmentID EmployeeType JobTitle {biggestEmployee}");
                foreach (var employee in employees)
                {
                    writer.WriteLine(employee.ToFileString());
                }
            }
        }
        public void SaveDepartmentsToFile()
        {
            string filePath = "C:\\Users\\abdo1\\Desktop\\Current_track\\1-Back-end-track\\C#\\HRsytem\\d\\departments.txt";
            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine($"DepartmentID Name ManagerID {biggestDepartment}");
                foreach (var depart in departments)
                {
                    writer.WriteLine(depart.ToFileString());
                }
            }
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
                    SaveEmployeesToFile();
                    SaveDepartmentsToFile();
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
                Console.WriteLine("|2- Job title          |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|3- Income             |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|4- Department         |");
                Console.WriteLine("|                      |");
                Console.WriteLine("|0- Back               |");
                Console.WriteLine("-----------------------");

                Console.Write("Choose order : ");
                choice = Console.ReadLine();
            } while (!int.TryParse(choice, out choiceInt) || choiceInt < 0 || choiceInt > 4);

            switch (choiceInt)
            {
                case 1:
                    printSort(employees, "Sorted by Name", CompareByName, true);
                    break;
                case 2:
                    {
                        string j;
                        Console.Write("Write a job title : ");
                        j = Console.ReadLine();
                        printReport(employees, $"Employees of Job-title : {j}", e => e.JobTitle == j);
                    }
                    break;
                case 3:
                    printSort(employees, "Sorted by Income", CompareBySalary, false);
                    break;
                case 4:
                    {
                        int d;
                        Console.Write("Write a Department ID : ");
                        d = int.Parse(Console.ReadLine());
                        if (!exist<Department>(de => de.ID == d))
                        {
                            Console.WriteLine("Sorry this ID is not exist :(");
                            break;
                        }
                        printReport(employees, $"Employees of Department ID : {d}", e => e.Depart.ID == d);
                    }
                    break;
                case 0:
                    return;
            }
            Console.ReadKey();
            ReportEmployee();
        }

        public delegate bool isIlegible(Employee e);
        public delegate int compareEmps(Employee e1, Employee e2, bool ascending);

        private void printReport(List<Employee> employees, string title, isIlegible legal)
        {
            Console.WriteLine($"ID  Name    Email    Phone-number    Job-title    Salary    Department");
            foreach (Employee e in employees)
            {
                if (legal(e))
                    Console.WriteLine(e);
            }
        }

        int CompareByName(Employee e1, Employee e2, bool ascending)
        {
            return ascending ? string.Compare(e1.Name, e2.Name) : string.Compare(e2.Name, e1.Name);
        }

        int CompareBySalary(Employee e1, Employee e2, bool ascending)
        {
            return ascending ? e1.getSalary().CompareTo(e2.getSalary()) : e2.getSalary().CompareTo(e1.getSalary());
        }


        private void printSort(List<Employee> e, string title, compareEmps compare, bool ascending) {
            for (int i = 0; i < employees.Count - 1; i++)
            {
                int flag = 0;
                for (int j = 0; j < employees.Count - 1 - i; j++)
                {
                    if (compare(e[j], e[j + 1], ascending) > 0);
                    {
                        Employee temp = e[j];
                        e[j] = e[j + 1];
                        e[j + 1] = temp;
                        flag = 1;
                    }
                }
                if (flag == 0) break;
            }
            printReport(e, title, em => true);
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
                        Console.WriteLine("The Department is deleted :)");
                    }
                    break;
                case 4:
                    {
                        int d;
                        Console.Write("Write a Department ID : ");
                        d = int.Parse(Console.ReadLine());
                        if (!exist<Department>(de => de.ID == d))
                        {
                            Console.WriteLine("Sorry this ID is not exist :(");
                            break;
                        }
                        printReport(employees, $"Employees of Department ID : {d}", e => e.Depart.ID == d);
                    }
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
