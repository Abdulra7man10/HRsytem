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

        public Employee () { }

        public Employee(int id)
        {
            this.ID = id;
            //Depart = d;
        }

        public virtual string displayDetails() => $"ID : {ID}\nName : {Name}\nJob title : {JobTitle}\nDepartment : {Depart.Name} -> {Depart.ID}\nPhone number : {PhoneNumber}\nEmail : {Email}\n{getType()}\n";
        public virtual string getType() => "Employee Type : ";
        public abstract string getDetails();
        public abstract double getSalary();
        public abstract void ExtraMethod(double more, double r=0);

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
        public override string getType() => base.getType() + "Hourly";
        public override string getDetails() => $"{HoursWorked} - {Rate}";
        public override string displayDetails() => base.displayDetails() +
            $"Hours Worked : {HoursWorked}\nRate per hour : {Rate}\nTotal Salary : {this.getSalary()}\n";
        public void addHours(double moreHours) => HoursWorked += moreHours;
        public override void ExtraMethod(double more, double r=0)
        {
            addHours(more);
            if (r != 0)
                Rate = r;
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
        public override string getType() => base.getType() + "Salaried";
        public override string getDetails() => $"{Salary}";
        public override string displayDetails() => base.displayDetails() +
            $"Total Salary : {this.getSalary()}\n";
        public override void ExtraMethod(double more, double r=0)
        {
            Salary = more;
        }
    }

    class ManagerEmployee : SalariedEmployee
    {
        public double Bonus { get; set; }

        public ManagerEmployee(int id, double s, double b) : base(id, s)
        {
            this.Bonus = b;
        }

        public override double getSalary()
        {
            return Salary + Bonus;
        }
        public override string getType() => base.getType() + "Manager";
        public override string getDetails() => base.getDetails() + $" - {Bonus}";
        public override string displayDetails() => base.displayDetails() +
            $"Salary : {Salary}\nBonus : {Bonus}\nTotal Salary : {this.getSalary()}\n";
        public void addBonus(double moreBonus) => Bonus += moreBonus;
        public override void ExtraMethod(double more, double more2=0)
        {
            addBonus(more);
            if (more2 != 0)
                Salary = more2;
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
        public override string getType() => base.getType() + "Commission";
        public override string getDetails() => $"{Target} - {Rate}";
        public override string displayDetails() => base.displayDetails() +
            $"Target : {Target}\nRate : {Rate}\nTotal Salary : {this.getSalary()}\n";
        public override void ExtraMethod(double more, double r)
        {
            Target = more;
            Rate = r;
        }
    }
}