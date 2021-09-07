using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using TEConsulting.Model;
using TEConsulting.Service;

namespace TEConsulting.API.Controllers
{
    [RoutePrefix("api/Login")]
    public class LoginController : ApiController
    {
        LookupsService service = new LookupsService();
        EmployeeService empService = new EmployeeService();

        //Can use cookies or session storage to maintain state

        /*
        [Route("{email}{pass}")]
        public HttpResponseMessage Get(string email, string pass)
        {
            var resp = new HttpResponseMessage();
            var nultiple = new NameValueCollection();
            Employee emp = service.GetEmployeeIdLogin(email, pass);
            nultiple["Employee"] = emp.EmployeeId.ToString();
            var cookie = new CookieHeaderValue("session", nultiple);
            resp.Headers.AddCookies(new CookieHeaderValue[] { cookie });
            return resp;
        }*/

        [HttpGet]
        [Route("{email}/{pass}")]
        public IHttpActionResult Get(string email, string pass)
        {
            try
            {
                Employee emp = service.GetEmployeeIdLogin(email, pass);

                if (emp == null)
                    return NotFound();

                return Ok(emp);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError,
                    "An internal error has occurred.  Please contact " +
                    "the system administrator.");
            }
        }
        
        [HttpGet]
        [Route("{empId}")]
        [ResponseType(typeof(SearchEmployeeLookupDTO))]
        public IHttpActionResult GetSupDeps(int empId)
        {
            try
            {
                SearchEmployeeLookupDTO emp = service.GetEmployeeSearchById(empId);
                EmployeeLookupDTO empL = service.GetSupDeps(empId);
                emp.Supervisor = empL.Supervisor;
                emp.Department = empL.Department;

                if (emp == null)
                    return NotFound();

                return Ok(emp);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError,
                    "An internal error has occurred.  Please contact " +
                    "the system administrator.");
            }
        }

        [HttpGet]
        [Route("Info/{empId}")]
        [ResponseType(typeof(Employee))]
        public IHttpActionResult GetAllEmployeeInfo(int empId)
        {
            try
            {
                Employee emp = empService.RetrieveEmployeeById(empId);

                if (emp == null)
                    return NotFound();

                return Ok(emp);
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
