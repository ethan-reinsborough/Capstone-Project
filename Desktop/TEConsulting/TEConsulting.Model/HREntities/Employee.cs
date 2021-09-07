using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace TEConsulting.Model
{
    public class Employee : BaseEntity
    {
        public int EmployeeId { get; set; }

        [Display(Name = "Department")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a department.")]
        public int DepartmentId { get; set; }

        [Display(Name = "Supervisor")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a supervisor.")]
        public int SupervisorId { get; set; }

        public int JobId { get; set; }

        public string Password { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please enter First Name.")]
        [StringLength(100, ErrorMessage = "First Name cannot exceed 255 characters in length.")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Initial")]
        [Required(ErrorMessage = "Please enter Middle Initial.")]
        [StringLength(1, ErrorMessage = "Middle Initial cannot exceed 1 character in length.")]
        public string MiddleInitial { get; set; }

        
        [Required(ErrorMessage = "Please enter Last Name.")]
        [StringLength(1, ErrorMessage = "Last Name cannot exceed 255 characters in length.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        public virtual string FullName
        {
            get
            {
                string fullname = FirstName;
                fullname += !string.IsNullOrEmpty(MiddleInitial) ? " " + MiddleInitial + ". " : " ";
                fullname += LastName;
                return fullname;
            }
        }
        

        [Display(Name = "SIN")]
        [Required(ErrorMessage = "Please enter SIN.")]
        public string SIN { get; set; }

        public DateTime DOB { get; set; }

        public string StreetAddress { get; set; }

        public string Municipality { get; set; }

        public string Province { get; set; }

        public string Country { get; set; }
   
        public string PostalCode { get; set; }

        [Display(Name = "Seniority Date")]
        [Required(ErrorMessage = "Please enter Seniority Date.")]
        public DateTime SeniorityDate { get; set; }

        [Display(Name = "Job Start Date")]
        [Required(ErrorMessage = "Please enter Job Start Date.")]
        public DateTime JobStartDate { get; set; }

        [Phone]
        [Display(Name = "Work Phone")]
        [Required(ErrorMessage = "Please enter Work Phone.")]
        public string WorkPhone { get; set; }

        [Phone]
        [Display(Name = "Cell Phone")]
        [Required(ErrorMessage = "Please enter Cell Phone.")]
        public string CellPhone { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please enter Email.")]
        public string Email { get; set; }

        public DateTime RetirementDate { get; set; }

        public DateTime TerminationDate { get; set; }

        public int Active { get; set; }

        public string OfficeLocation { get; set; }

        public string HashedPass { get; set; }

        public byte[] RecordVersion { get; set; }

    }
}
