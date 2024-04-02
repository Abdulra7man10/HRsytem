using System.Runtime.Remoting.Messaging;

namespace HRsytem
{
    abstract class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string JobTitle { get; set; }
        public Department Depart { get; set; } //if deleted make there depart as '0'

        //public Benefit

        public Employee(int id) 
        {
            this.ID = id;
        }

        public virtual string displayDetails() => $"ID : {ID}\nName : {Name}\nJob title : {JobTitle}\nPhone number : {PhoneNumber}\nEmail : {Email}\n{getDetails()}\n";
        public virtual string getDetails() => "Employee Type : ";
        public abstract double getSalary();
        public virtual void ExtraMethod(double more)
        {

        }

    }

    class HourlyEmployee : Employee
    {
        public double HoursWorked { get; set; }
        public double Rate { get; set; }

        public HourlyEmployee(int id, double hw, double r) : base(id) 
        {
            this.HoursWorked = hw;
            this.Rate = r;
        }

        public override double getSalary()
        {
            return HoursWorked * Rate;
        }
        public override string getDetails() => base.getDetails() + "Hourly";
        public override string displayDetails() => base.displayDetails() + 
            $"Hours Worked : {HoursWorked}\nRate per hour : {Rate}\nTotal Salary : {this.getSalary()}\n";
        public void addHours(double moreHours) => HoursWorked += moreHours;
        public override void ExtraMethod(double more)
        {
            addHours(more);
        }
    }

    class SalariedEmployee : Employee
    {
        public double Salary { get; set; }

        public SalariedEmployee(int id, double s) : base(id) 
        {
            this.Salary = s;
        }

        public override double getSalary()
        {
            return Salary;
        }
        public override string getDetails() => base.getDetails() + "Salaried";
        public override string displayDetails() => base.displayDetails() +
            $"Total Salary : {this.getSalary()}\n";
    }

    class ManagerEmployee : SalariedEmployee
    {
        public double Bonus { get; set; }

        public ManagerEmployee(int id, double s, double b) : base(id,s) 
        {
            this.Bonus = b;
        }

        public override double getSalary()
        {
            return Salary + Bonus;
        }
        public override string getDetails() => base.getDetails() + "Manager";
        public override string displayDetails() => base.displayDetails() +
            $"Salary : {Salary}\nBonus : {Bonus}\nTotal Salary : {this.getSalary()}\n";
        public void addBonus(double moreBonus) => Bonus += moreBonus;
        public override void ExtraMethod(double more)
        {
            addBonus(more);
        }
    }

    class CommissionEmployee : Employee
    {
        public double Target { get; set; }
        public double Rate { get; set; }

        public CommissionEmployee(int id, double t, double r) : base(id) 
        {
            this.Target = t;
            this.Rate = r;
        }
        public override double getSalary()
        {
            return Target * Rate;
        }
        public override string getDetails() => base.getDetails() + "Commission";
        public override string displayDetails() => base.displayDetails() +
            $"Target : {Target}\nRate : {Rate}\nTotal Salary : {this.getSalary()}\n";
    }
}
