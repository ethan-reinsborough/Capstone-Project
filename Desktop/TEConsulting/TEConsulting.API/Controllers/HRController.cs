using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using TEConsulting.Model;
using TEConsulting.Model.HREntities;
using TEConsulting.Service;

namespace TEConsulting.API.Controllers
{
    [RoutePrefix("api/Employees")]
    public class HRController : ApiController
    {
        LookupsService service = new LookupsService();
        EmployeeService eService = new EmployeeService();
        DepartmentService dService = new DepartmentService();

        /// <summary>
        /// Returns an employee associated with an EmployeeID
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpGet]
        [ResponseType(typeof(SearchEmployeeLookupDTO))]
        public IHttpActionResult Get(int id)
        {
            try
            {
                SearchEmployeeLookupDTO searchedEmp = service.GetEmployeeSearchById(id);
                List<SearchEmployeeLookupDTO> searchedEmployees = new List<SearchEmployeeLookupDTO>();

                //If employee is not found, return an empty array for 
                if (searchedEmp == null)
                    //return Ok(searchedEmployees);
                    return NotFound();

                return Ok(searchedEmp);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError,
                    "An internal error has occurred.  Please contact " +
                    "the system administrator.");
            }
        }


        /// <summary>
        /// Returns an employee associated with an Employee's LastName property
        /// </summary>
        /// <param name="lastName"></param>
        /// <returns></returns>
        [Route("byName/{LName}")]
        [HttpGet]
        [ResponseType(typeof(List<SearchEmployeeLookupDTO>))]
        public IHttpActionResult Get(string lName)
        {
            try
            {
                if(lName == "zxy123")
                {
                    lName = "%";
                }
                List<SearchEmployeeLookupDTO> searchedEmp = service.GetEmployeeSearchByLName(lName);

               if (searchedEmp == null)
                   return NotFound();

                return Ok(searchedEmp);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError,
                    "An internal error has occurred.  Please contact " +
                    "the system administrator.");
            }
        }

        [Route("byDepartment/hi/{departmentId}")]
        [HttpGet]
        [ResponseType(typeof(List<SearchEmployeeLookupDTO>))]
        public IHttpActionResult GetByDepartment(int departmentId)
        {
            try
            {
                List<SearchEmployeeLookupDTO> searchedEmp = service.GetEmployeeSearchByDepartment(departmentId);

                if (searchedEmp == null)
                    return NotFound();

                return Ok(searchedEmp);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError,
                    "An internal error has occurred.  Please contact " +
                    "the system administrator.");
            }
        }

        [Route("Info/Departments")]
        [HttpGet]
        [ResponseType(typeof(List<DepartmentLookupDTO>))]
        public IHttpActionResult Get()
        {
            try
            {
                List<DepartmentLookupDTO> departments = service.GetDepartments();

                if (departments == null)
                    return NotFound();

                return Ok(departments);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError,
                    "An internal error has occurred.  Please contact " +
                    "the system administrator.");
            }
        }

        [Route("Info/Reviews/{employeeId}")]
        [HttpGet]
        [ResponseType(typeof(List<Review>))]
        public IHttpActionResult GetReviews(int employeeId)
        {
            try
            {
                List<Review> reviews = service.GetReviews(employeeId);

                if (reviews == null)
                    return NotFound();

                return Ok(reviews);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError,
                    "An internal error has occurred.  Please contact " +
                    "the system administrator.");
            }
        }


        [HttpPut]
        [Route("Update/Personal/{employeeId}")]
        public IHttpActionResult UpdatePEmp(int employeeId, [FromBody] Employee employee)
        {
            try
            {
                if (employeeId != employee.EmployeeId)
                    return BadRequest();

                eService.ModifyPersonalInformation(employee);
                return Ok(employee);

            }
            catch (SqlException ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError,
                    "An internal error has occurred.  Please contact " +
                    "the system administrator.");
            }
        }

        [HttpPut]
        [Route("Update/Department/{departmentId}")]
        public IHttpActionResult UpdateDepartment(int departmentId, [FromBody] Department department)
        {
            try
            {
                if (departmentId != department.DepartmentId)
                    return BadRequest();

                dService.ModifyDepartment(department);
                return Ok(department);

            }
            catch (SqlException ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError,
                    "An internal error has occurred.  Please contact " +
                    "the system administrator.");
            }
        }

        [HttpPost]
        [Route("AddReview")]
        public IHttpActionResult CreateReview([FromBody] Review review)
        {
            try
            {
                eService.AddReview(review);
                    
                return Ok(review);
                
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError,
                    "An internal error has occurred.  Please contact " +
                    "the system administrator.");
            }
        }

        [Route("EmployeesForReview/{supervisorId}")]
        [HttpGet]
        [ResponseType(typeof(List<ReviewEmployeeDTO>))]
        public IHttpActionResult GetEmployeesForReview(int supervisorId)
        {
            try
            {
                List<ReviewEmployeeDTO> reviewEmps = service.GetEmployeesForReview(supervisorId);

                if (reviewEmps == null)
                    return NotFound();

                return Ok(reviewEmps);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError,
                    "An internal error has occurred.  Please contact " +
                    "the system administrator.");
            }
        }
    }
}
