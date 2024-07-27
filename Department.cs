using System;
using System.Collections.Generic;

namespace HRsystem
{
    internal class Department
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Employee> Employees { get; set; }
        public Employee Manager { get; set; }

        public Department(string name = "", int id = 0)
        {
            this.Name = name;
            this.ID = id;
            this.Employees = new List<Employee>();
            Manager = null;
        }

        public override string ToString() => $"Name : {Name}\nID : {ID}\n";

        public void AllEmployees()
        {
            foreach (Employee emp in Employees)
                Console.WriteLine($"{emp.ID}  {emp.Name}");
        }

        public void addEmployee(Employee em)
        {
            em.Depart = this;
            this.Employees.Add(em);
        }

        public void removeEmployee(Employee em) => this.Employees.Remove(em);

        public void changeManager(Employee em)
        {
            em.Depart = this;
            this.Manager = em;
            foreach (Employee e in this.Employees)
                if (e == em)
                    return;
            this.Employees.Add(em);
        }
        public string ToFileString()
        {
            string name = Name.Replace(' ', '-');
            string managerId = Manager != null ? Manager.ID.ToString() : "0";
            return $"{ID} {name} {managerId}";
        }
    }
}
