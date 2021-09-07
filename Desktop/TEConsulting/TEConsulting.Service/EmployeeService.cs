using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEConsulting.Model;
using TEConsulting.Model.HREntities;
using TEConsulting.Repository;
using TEConsulting.Types;

namespace TEConsulting.Service
{
    public class EmployeeService
    {
        #region Fields and Constructors

        private EmployeeRepo repo = new EmployeeRepo();

        #endregion

        #region Public Methods

        public bool AddEmployee(Employee employee)
        {
            return repo.Add(employee);

            //return Validate(employee) ? repo.Add(employee) : employee;
        }

        public bool AddReview(Review review)
        {
            return repo.AddReview(review);

            //return Validate(employee) ? repo.Add(employee) : employee;
        }

        public int GetDepartmentEmployeeCount(int deptId)
        {
            return repo.GetNumEmpInDept(deptId);
        }

        public int GetDepartmentSupervisorCount(int deptId)
        {
            return repo.GetNumSupInDept(deptId);
        }

        public bool ModifyPersonalEmployeeInfo(Employee employee)
        {
            return repo.ModifyPersonalEmployee(employee);
        }

        public bool ModifyPersonalInformation(Employee employee)
        {
            return repo.ModifyPersonalInformation(employee);
        }
        public bool ModifyStatus(Employee employee)
        {
            return repo.ModifyStatus(employee);
        }

        public bool ModifyJobInfo(Employee employee)
        {
            return repo.ModifyJobInfo(employee);
        }

        public Employee RetrieveStatus(int employeeId)
        {
            return repo.GetEmployeeStatus(employeeId);
        }
        public Employee RetrieveEmployeeJobInfo(int employeeId)
        {
            return repo.GetEmployeeJobInfo(employeeId);
        }

        public Employee RetrieveEmployeeById(int employeeId)
        {
            return repo.GetEmployee(employeeId);
        }
        #endregion

        #region Validation
        //Enable later after testing

        /*
        private bool Validate(Employee employee)
        {
            // Validate Entity

            List<ValidationResult> results = new List<ValidationResult>();
            Validator.TryValidateObject(employee, new ValidationContext(employee), results, true);

            foreach (ValidationResult e in results)
            {
                employee.AddError(new ValidationError(e.ErrorMessage, ErrorType.Model));
            }

            return employee.Errors.Count == 0;
        }
        */

        #endregion

        #region Business Rules


        #endregion
    }
}
