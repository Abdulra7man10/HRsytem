using System;

namespace HRsystem
{
    static class EmployeeExtensions
    {
        public static string ToFileString(this Employee employee)
        {
            var basicInfo = $"{employee.ID} {employee.Name.Replace(' ', '-')}";
            var departmentID = employee.Depart.ID;
            var jobTitle = employee.JobTitle.Replace(' ', '-');

            if (employee is HourlyEmployee hourly)
            {
                return $"{basicInfo} {employee.Email} {employee.PhoneNumber} {departmentID} 1 {jobTitle} {hourly.HoursWorked} {hourly.Rate}";
            }
            else if (employee is SalariedEmployee salaried)
            {
                return $"{basicInfo} {employee.Email} {employee.PhoneNumber} {departmentID} 2 {jobTitle} {salaried.Salary}";
            }
            else if (employee is ManagerEmployee manager)
            {
                return $"{basicInfo} {employee.Email} {employee.PhoneNumber} {departmentID} 3 {jobTitle} {manager.Salary} {manager.Bonus}";
            }
            else if (employee is CommissionEmployee commission)
            {
                return $"{basicInfo} {employee.Email} {employee.PhoneNumber} {departmentID} 4 {jobTitle} {commission.Target} {commission.Rate}";
            }
            else
            {
                throw new InvalidOperationException("Unknown employee type");
            }
        }
    }
}
