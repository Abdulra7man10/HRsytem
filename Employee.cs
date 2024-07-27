using System;

namespace HRsystem
{
    abstract class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string JobTitle { get; set; }
        public Department Depart { get; set; }

        public Employee() { }

        public Employee(int id)
        {
            this.ID = id;
        }

        public Employee(int id, string name, string email, string phoneNumber, string jobTitle, Department depart)
        {
            ID = id;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            JobTitle = jobTitle;
            Depart = depart;
        }

        public override string ToString()
        {
            return $"{ID}  {Name}  {Email}  {PhoneNumber}  {JobTitle}  {getSalary()}  {Depart.Name}";
        }

        public virtual string displayDetails() => $"ID : {ID}\nName : {Name}\nJob title : {JobTitle}\nDepartment : {Depart.Name} -> {Depart.ID}\nPhone number : {PhoneNumber}\nEmail : {Email}\n{getType()}\n";
        public virtual string getType() => "Employee Type : ";
        public abstract string getDetails();
        public abstract double getSalary();
        public abstract void ExtraMethod(double more, double r = 0);
        /*public virtual string ToFileString()
        {
            var basicInfo = $"{ID} {Name.Replace(' ', '-')}";
            var departmentID = Depart.ID;
            var jobTitle = JobTitle.Replace(' ', '-');

            if (this is HourlyEmployee hourly)
            {
                return $"{basicInfo} {Email} {PhoneNumber} {departmentID} 1 {jobTitle}";
            }
            else if (this is SalariedEmployee salaried)
            {
                return $"{basicInfo} {Email} {PhoneNumber} {departmentID} 2 {jobTitle}";
            }
            else if (this is ManagerEmployee manager)
            {
                return $"{basicInfo} {Email} {PhoneNumber} {departmentID} 3 {jobTitle}";
            }
            else if (this is CommissionEmployee commission)
            {
                return $"{basicInfo} {Email} {PhoneNumber} {departmentID} 4 {jobTitle}";
            }
            else
            {
                throw new InvalidOperationException("Unknown employee type");
            }
        }*/
    }

    class HourlyEmployee : Employee
    {
        public double HoursWorked { get; set; }
        public double Rate { get; set; }

        public HourlyEmployee(int id, string name, string email, string phoneNumber, string jobTitle, Department depart, double hoursWorked, double rate)
            : base(id, name, email, phoneNumber, jobTitle, depart)
        {
            this.HoursWorked = hoursWorked;
            this.Rate = rate;
        }
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
        public override void ExtraMethod(double more, double r = 0)
        {
            addHours(more);
            if (r != 0)
                Rate = r;
        }
       /*public override string ToFileString()
        {
            var baseString = base.ToFileString();
            return $"{baseString} {HoursWorked} {Rate}";
        }*/
    }

    class SalariedEmployee : Employee
    {
        public double Salary { get; set; }

        public SalariedEmployee(int id, string name, string email, string phoneNumber, string jobTitle, Department depart, double salary)
            : base(id, name, email, phoneNumber, jobTitle, depart)
        {
            this.Salary = salary;
        }
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
        public override void ExtraMethod(double more, double r = 0)
        {
            Salary = more;
        }
        /*public override string ToFileString()
        {
            var baseString = base.ToFileString();
            return $"{baseString} {Salary}";
        }*/
    }

    class ManagerEmployee : SalariedEmployee
    {
        public double Bonus { get; set; }

        public ManagerEmployee(int id, string name, string email, string phoneNumber, string jobTitle, Department depart, double salary, double bonus)
            : base(id, name, email, phoneNumber, jobTitle, depart, salary)
        {
            this.Bonus = bonus;
        }
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
        public override void ExtraMethod(double more, double more2 = 0)
        {
            addBonus(more);
            if (more2 != 0)
                Salary = more2;
        }
        /*public override string ToFileString()
        {
            var baseString = base.ToFileString();
            return $"{baseString} {Bonus}";
        }*/
    }

    class CommissionEmployee : Employee
    {
        public double Target { get; set; }
        public double Rate { get; set; }

        public CommissionEmployee(int id, string name, string email, string phoneNumber, string jobTitle, Department depart, double target, double rate)
            : base(id, name, email, phoneNumber, jobTitle, depart)
        {
            this.Target = target;
            this.Rate = rate;
        }

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
        /*public override string ToFileString()
        {
            var baseString = base.ToFileString();
            return $"{baseString} {Target} {Rate}";
        }*/
    }
}