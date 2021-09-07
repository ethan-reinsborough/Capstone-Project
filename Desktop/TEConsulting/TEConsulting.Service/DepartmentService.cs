using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEConsulting.Model;
using TEConsulting.Repository;
using TEConsulting.Types;

namespace TEConsulting.Service
{
    public class DepartmentService
    {
        #region Fields and Constructors

        private DepartmentRepo repo = new DepartmentRepo();

        #endregion

        #region Public Methods

        public bool AddDepartment(Department department)
        {
            return repo.Add(department);
        }

        public bool ModifyDepartment(Department department)
        {
            return repo.ModifyDepartment(department);
        }

        public bool DeleteDepartment(int departmentId)
        {
            return repo.DeleteDepartment(departmentId);
        }

        #endregion

        #region Validation
        //Enable later after testing

        /*
        private bool Validate(Department department)
        {
            // Validate Entity

            List<ValidationResult> results = new List<ValidationResult>();
            Validator.TryValidateObject(department, new ValidationContext(department), results, true);

            foreach (ValidationResult e in results)
            {
                department.AddError(new ValidationError(e.ErrorMessage, ErrorType.Model));
            }

            return department.Errors.Count == 0;
        }
        */

        #endregion

        #region Business Rules


        #endregion
    }
}
