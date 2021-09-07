using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEConsulting.Model
{
    public class StatusLookupDTO
    {
        public int StatusID { get; set; }

        public string StatusName { get; set; }
    }

    public class EmployeeLookupDTO
    {
        public int EmployeeID { get; set; }

        public string Employee { get; set; }

        public string Department { get; set; }

        public string Supervisor { get; set; }
    }

    public class DepartmentLookupDTO
    {
        public int DepartmentId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime InvocationDate { get; set; }

        public byte[] RecordVersion { get; set; }
    }

    public class JobLookupDTO
    {
        public int JobId { get; set; }

        public string JobName { get; set; }
    }

    public class SupervisorLookupDTO
    {
        //This is an Employee ID that reports to the CEO (CEO ID = 10000000)
        public int SupervisorId { get; set; }

        //FName + LName of Supervisor
        public string SupervisorName { get; set; }

        //Determines the Department a Supervisor is responsible for
        public int SupervisorDepartment { get; set; }

        public string Email { get; set; }
    }

    public class SearchEmployeeLookupDTO
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }

        public string MiddleInitial { get; set; }

        public string LastName { get; set; }

        public string MailAddress { get; set; }

        public string WorkPhone { get; set; }

        public string CellPhone { get; set; }

        public string Email { get; set; }

        public int SupervisorID { get; set; }

        public string JobName { get; set; }

        public int DepartmentId { get; set; }
        
        public string DepartmentName { get; set; }

        public string Province { get; set; }

        public string Country { get; set; }

        public string Supervisor { get; set; }

        public string Department { get; set; }
    }

    public class ReviewEmployeeDTO
    {
        public int EmployeeId { get; set; }

        public int DepartmentId { get; set; }

        public string FullName { get; set; }

        //public int ReviewId { get; set; }

        //public DateTime ReviewDate { get; set; }
    
    }
}
