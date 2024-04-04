using System.Collections.Generic;

namespace HRsytem
{
    internal class Department
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Employee> Employees { get; set; }
        public Employee Manager { get; set; } 


        public Department(string name="", int i=0)
        {
            this.Name = name;
            this.ID = i;
            this.Employees = new List<Employee>();
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
    }
}