using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEConsulting.Model;
using TEConsulting.Model.HREntities;
using TEConsulting.Repository;

namespace TEConsulting.Service
{
    public class LookupsService
    {
        private LookupsRepo repo;

        public LookupsService()
        {
            repo = new LookupsRepo();
        }

        public List<DepartmentLookupDTO> GetDepartments()
        {
            return repo.RetrieveDepartments();
        }

        public List<Review> GetReviews(int employeeId)
        {
           return repo.RetrieveReviews(employeeId);
        }
        public bool CheckEmptyDept(int deptId)
        {
            return repo.CheckEmptyDepartment(deptId);
        }
        public Department GetDepartment(int deptId)
        {
            return repo.RetrieveDepartment(deptId);
        }

        public List<ReviewEmployeeDTO> GetEmployeesForReview(int supervisorId)
        {
            return repo.RetrieveEmployeesForReview(supervisorId);
        }

        public List<Employee> GetEmployeesByDepartment(int departmentId)
        {
            return repo.RetrieveEmployeesByDepartment(departmentId);
        }

        public List<JobLookupDTO> GetJobs()
        {
            return repo.RetrieveJobs();
        }

        public List<SupervisorLookupDTO> GetSupervisors()
        {
            return repo.RetrieveSupervisors();
        }

        public List<SupervisorLookupDTO> GetSupervisorsByDepartment(int departmentId)
        {
            return repo.RetrieveSupervisorsByDepartment(departmentId);
        }

        public List<StatusLookupDTO> Statuses()
        {
            return repo.RetrieveStatuses();
        }

        public EmployeeLookupDTO GetSupDeps(int id)
        {
            //ceo check
            if(id == 10000000)
            {
                return new EmployeeLookupDTO
                {
                    Employee = "Khraig Bentley",
                    Supervisor = "N/A",
                    Department = "CEO"
                };
            }
            else 
            {
                return repo.RetrieveSupDep(id);
            }
            
        }

        public SearchEmployeeLookupDTO GetEmployeeSearchById(int employeeId)
        {
            return repo.SearchEmployeeById(employeeId);
        }

        public List<SearchEmployeeLookupDTO> GetEmployeeSearchByLName(string lastName)
        {
            return repo.SearchEmployeeByLName(lastName);
        }

        public List<SearchEmployeeLookupDTO> GetEmployeeSearchByDepartment(int departmentId)
        {
            return repo.SearchEmployeeByDepartment(departmentId);
        }


        public Employee GetEmployeeIdLogin(string email, string pass)
        {
            return repo.RetrieveEmployeeIdLogin(email, pass);
        }

    }
}
