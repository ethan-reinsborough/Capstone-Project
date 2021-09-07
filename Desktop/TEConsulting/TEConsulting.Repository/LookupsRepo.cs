using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEConsulting.Model;
using TEConsulting.Model.HREntities;
using TEConsulting.Types;

namespace TEConsulting.Repository
{
    public class LookupsRepo
    {
        DataAccess db = new DataAccess();

        public List<StatusLookupDTO> RetrieveStatuses()
        {
            DataTable dt = db.GetData("spGetStatusesForLookup");

            List<StatusLookupDTO> statuses = new List<StatusLookupDTO>();

            foreach (DataRow row in dt.Rows)
            {
                statuses.Add(
                    new StatusLookupDTO()
                    {
                        StatusID = Convert.ToInt32(row["StatusID"]),
                        StatusName = row["StatusName"].ToString()
                    }
                );
            }

            return statuses;
        }

        public List<DepartmentLookupDTO> RetrieveDepartments()
        {
            //Using inline queries for now, Khraig's Department is unique for CEO only.
            DataTable dt = db.GetData("SELECT DepartmentID, DepartmentName, DepartmentDescription, InvocationDate, RecordVersion FROM Department", null, CommandType.Text);

            List<DepartmentLookupDTO> departments = new List<DepartmentLookupDTO>();

            foreach (DataRow row in dt.Rows)
            {
                departments.Add(
                    new DepartmentLookupDTO()
                    {
                        DepartmentId = Convert.ToInt32(row["DepartmentID"]),
                        Name = row["DepartmentName"].ToString(),
                        Description = row["DepartmentDescription"].ToString(),
                        InvocationDate = Convert.ToDateTime(row["InvocationDate"]),
                        RecordVersion = (byte[])row["RecordVersion"]
                    }
                );
            }

            return departments;
        }

        //Change to departmentdto
        public Department RetrieveDepartment(int deptId)
        {
            //Using inline queries for now, Khraig's Department is unique for CEO only.
            DataTable dt = db.GetData($"SELECT DepartmentID, DepartmentName, DepartmentDescription, InvocationDate, RecordVersion FROM Department WHERE DepartmentID = {deptId}", null, CommandType.Text);

            return new Department()
            {
                DepartmentId = Convert.ToInt32(dt.Rows[0]["DepartmentID"]),
                Name = dt.Rows[0]["DepartmentName"].ToString(),
                Description = dt.Rows[0]["DepartmentDescription"].ToString(),
                InvocationDate = Convert.ToDateTime(dt.Rows[0]["InvocationDate"]),
                RecordVersion = (byte[])dt.Rows[0]["RecordVersion"]
            };  
        }

        public bool CheckEmptyDepartment(int deptId)
        {
            DataTable dt = db.GetData($"SELECT COUNT(*) AS Counter FROM Employee WHERE DepartmentID = {deptId}", null, CommandType.Text);
            
            if (Convert.ToInt32(dt.Rows[0]["Counter"]) > 0){
                return false;
            }
            else
            {
                return true;
            }
        }

        public List<JobLookupDTO> RetrieveJobs()
        {
            //Using inline queries for now, omit JobID 11 since Khraig is the only CEO
            DataTable dt = db.GetData("SELECT JobID, JobName FROM Job WHERE JobID <> 11", null, CommandType.Text);

            List<JobLookupDTO> jobs = new List<JobLookupDTO>();

            foreach (DataRow row in dt.Rows)
            {
                jobs.Add(
                    new JobLookupDTO()
                    {
                        JobId = Convert.ToInt32(row["JobID"]),
                        JobName = row["JobName"].ToString()
                    }
                );
            }

            return jobs;
        }

        public List<Review> RetrieveReviews(int employeeId)
        {
            //Using inline queries for now, omit JobID 11 since Khraig is the only CEO
            DataTable dt = db.GetData($"SELECT * FROM Review WHERE EmployeeID = {employeeId}", null, CommandType.Text);

            List<Review> reviews = new List<Review>();

            foreach (DataRow row in dt.Rows)
            {
                reviews.Add(
                    new Review()
                    {
                        ReviewId = Convert.ToInt32(row["ReviewID"]),
                        EmployeeId = Convert.ToInt32(row["EmployeeID"]),
                        SupervisorId = Convert.ToInt32(row["SupervisorID"]),
                        ReviewDate = Convert.ToDateTime(row["ReviewDate"]),
                        Comment = row["Comment"].ToString(),
                        PerformanceRating = row["PerformanceRating"].ToString()
                    }
                );
            }

            return reviews;
        }

        public List<SupervisorLookupDTO> RetrieveSupervisors()
        {
            //Using inline queries for now
            DataTable dt = db.GetData("SELECT EmployeeID, DepartmentID, FirstName, LastName, Email FROM Employee WHERE Supervisor = 10000000", null, CommandType.Text);

            List<SupervisorLookupDTO> supervisors = new List<SupervisorLookupDTO>();

            foreach (DataRow row in dt.Rows)
            {
                supervisors.Add(
                    new SupervisorLookupDTO()
                    {
                        SupervisorId = Convert.ToInt32(row["EmployeeID"]),
                        SupervisorName = row["LastName"].ToString() + ", " + row["FirstName"].ToString(),
                        SupervisorDepartment = Convert.ToInt32(row["DepartmentID"]),
                        Email = row["Email"].ToString()
                    }
                );
            }

            return supervisors;
        }

        public List<ReviewEmployeeDTO> RetrieveEmployeesForReview(int supervisorId)
        {
            List<ParmStruct> parms = new List<ParmStruct>();
            parms.Add(new ParmStruct("@SupervisorID", supervisorId, SqlDbType.Int, 0));

            DataTable dt = db.GetData("spGetEmployeesForReview", parms);
            //DataTable dt = db.GetData("SELECT t1.EmployeeID, t1.FirstName, t1.LastName, t1.DepartmentID FROM Employee t1 LEFT JOIN Review t2 ON t2.EmployeeID = t1.EmployeeID WHERE (t2.EmployeeID IS NULL OR t2.ReviewDate < DATEADD(month, -3, GETDATE())) AND t1.Supervisor <> 10000000 AND t1.Supervisor <> 0", null, CommandType.Text);

            List<ReviewEmployeeDTO> reviewEmployees = new List<ReviewEmployeeDTO>();

            foreach (DataRow row in dt.Rows)
            {
                reviewEmployees.Add(
                    new ReviewEmployeeDTO()
                    {
                        EmployeeId = Convert.ToInt32(row["EmployeeID"]),
                        FullName = row["LastName"].ToString() + ", " + row["FirstName"].ToString(),
                        DepartmentId = Convert.ToInt32(row["DepartmentID"])
                    }
                );
            }

            return reviewEmployees;
        }

        public List<Employee> RetrieveEmployeesByDepartment(int departmentId)
        {
            DataTable dt = db.GetData($"SELECT EmployeeID, DepartmentID, FirstName, LastName, Email FROM Employee WHERE DepartmentID = {departmentId}", null, CommandType.Text);

            List<Employee> reviewEmployees = new List<Employee>();

            foreach (DataRow row in dt.Rows)
            {
                reviewEmployees.Add(
                    new Employee()
                    {
                        EmployeeId = Convert.ToInt32(row["EmployeeID"]),
                        FirstName = row["LastName"].ToString() + ", " + row["FirstName"].ToString(),
                        DepartmentId = Convert.ToInt32(row["DepartmentID"]),
                        Email = row["Email"].ToString()
                    }
                );
            }

            return reviewEmployees;
        }

        public List<SupervisorLookupDTO> RetrieveSupervisorsByDepartment(int departmentId)
        {
            //Using inline queries for now
            DataTable dt = db.GetData($"SELECT EmployeeID, DepartmentID, FirstName, LastName FROM Employee WHERE Supervisor = 10000000 AND DepartmentID = {departmentId}", null, CommandType.Text);

            List<SupervisorLookupDTO> supervisors = new List<SupervisorLookupDTO>();

            foreach (DataRow row in dt.Rows)
            {
                supervisors.Add(
                    new SupervisorLookupDTO()
                    {
                        SupervisorId = Convert.ToInt32(row["EmployeeID"]),
                        SupervisorName = row["LastName"].ToString() + ", " + row["FirstName"].ToString(),
                        SupervisorDepartment = Convert.ToInt32(row["DepartmentID"])
                    }
                );
            }

            return supervisors;
        }

        public SearchEmployeeLookupDTO SearchEmployeeById(int employeeId)
        {
            //Stored procedure is not working for some reason, replace later
            //List<ParmStruct> parms = new List<ParmStruct>();
            //Fixed this, re-enable later
            //parms.Add(new ParmStruct("@EmployeeID", employeeId, SqlDbType.Int, 0, ParameterDirection.Input));
            DataTable dt = db.GetData($"SELECT EmployeeId, FirstName, MI, LastName, StreetAddress + ', ' + Municipality + ', ' + Province + ', ' + Country + ' ' + PostalCode AS [MailAddress], WorkPhone, CellPhone, Email, Supervisor, DepartmentID FROM Employee WHERE EmployeeID = {employeeId}", null, CommandType.Text);
            //DataTable dt = db.GetData("spSearchEmployeeByID", parms);
            return dt.Rows.Count > 0 ? PopulateSearchEmployeeDTO(dt.Rows[0]) : null;
        }

        public List<SearchEmployeeLookupDTO> SearchEmployeeByLName(string lastName)
        {
            //Fixed, enable SP later
            //List<ParmStruct> parms = new List<ParmStruct>();
            //parms.Add(new ParmStruct("@LastName", lastName, SqlDbType.NVarChar, 255, ParameterDirection.Input));
            DataTable dt = db.GetData($"SELECT Employee.EmployeeId, Employee.FirstName, Employee.MI, Employee.LastName, Employee.StreetAddress + ', ' + Employee.Municipality + ', ' + Employee.Province + ', ' + Employee.Country + ' ' + Employee.PostalCode AS [MailAddress], Employee.WorkPhone, Employee.Country, Employee.Province, Employee.CellPhone, Employee.Email, Employee.DepartmentID, Employee.Supervisor, Job.JobName, Department.DepartmentName FROM Employee INNER JOIN Job ON Employee.JobID = Job.JobID INNER JOIN Department ON Employee.DepartmentID = Department.DepartmentID WHERE LastName LIKE '{lastName}' + '%' ORDER BY Employee.LastName", null, CommandType.Text);
            //DataTable dt = db.GetData("spSearchEmployeeByLName", parms);
            List<SearchEmployeeLookupDTO> employeesByLastName = new List<SearchEmployeeLookupDTO>();

            foreach (DataRow row in dt.Rows)
            {
                employeesByLastName.Add(
                    new SearchEmployeeLookupDTO()
                    {
                        EmployeeId = Convert.ToInt32(row["EmployeeID"]),
                        FirstName = row["FirstName"].ToString(),
                        MiddleInitial = row["MI"].ToString(),
                        LastName = row["LastName"].ToString(),
                        MailAddress = row["MailAddress"].ToString(),
                        WorkPhone = row["WorkPhone"].ToString(),
                        CellPhone = row["CellPhone"].ToString(),
                        Email = row["Email"].ToString(),
                        SupervisorID = Convert.ToInt32(row["Supervisor"]),
                        JobName = row["JobName"].ToString(),
                        DepartmentId = Convert.ToInt32(row["DepartmentID"]),
                        DepartmentName = row["DepartmentName"].ToString(),
                        Province = row["Province"].ToString(),
                        Country = row["Country"].ToString()

                    }
                );
            }
            return employeesByLastName;
        }

        public List<SearchEmployeeLookupDTO> SearchEmployeeByDepartment(int departmentId)
        {
            //Fixed, enable SP later
            //List<ParmStruct> parms = new List<ParmStruct>();
            //parms.Add(new ParmStruct("@LastName", lastName, SqlDbType.NVarChar, 255, ParameterDirection.Input));
            DataTable dt = db.GetData($"SELECT Employee.EmployeeId, Employee.FirstName, Employee.MI, Employee.LastName, Employee.StreetAddress + ', ' + Employee.Municipality + ', ' + Employee.Province + ', ' + Employee.Country + ' ' + Employee.PostalCode AS [MailAddress], Employee.WorkPhone, Employee.Country, Employee.Province, Employee.CellPhone, Employee.Email, Employee.DepartmentID, Employee.Supervisor, Job.JobName, Department.DepartmentName FROM Employee INNER JOIN Job ON Employee.JobID = Job.JobID INNER JOIN Department ON Employee.DepartmentID = Department.DepartmentID WHERE Employee.DepartmentID = {departmentId} ORDER BY Employee.LastName", null, CommandType.Text);
            //DataTable dt = db.GetData("spSearchEmployeeByLName", parms);
            List<SearchEmployeeLookupDTO> employeesByLastName = new List<SearchEmployeeLookupDTO>();

            foreach (DataRow row in dt.Rows)
            {
                employeesByLastName.Add(
                    new SearchEmployeeLookupDTO()
                    {
                        EmployeeId = Convert.ToInt32(row["EmployeeID"]),
                        FirstName = row["FirstName"].ToString(),
                        MiddleInitial = row["MI"].ToString(),
                        LastName = row["LastName"].ToString(),
                        MailAddress = row["MailAddress"].ToString(),
                        WorkPhone = row["WorkPhone"].ToString(),
                        CellPhone = row["CellPhone"].ToString(),
                        Email = row["Email"].ToString(),
                        SupervisorID = Convert.ToInt32(row["Supervisor"]),
                        JobName = row["JobName"].ToString(),
                        DepartmentId = Convert.ToInt32(row["DepartmentID"]),
                        DepartmentName = row["DepartmentName"].ToString(),
                        Province = row["Province"].ToString(),
                        Country = row["Country"].ToString()

                    }
                );
            }
            return employeesByLastName;
        }

        public EmployeeLookupDTO RetrieveSupDep(int id)
        {
            List<ParmStruct> parms = new List<ParmStruct>();
            parms.Add(new ParmStruct("@EmployeeID", id, SqlDbType.Int, 0));

            DataTable dt = db.GetData("spGetSupDep", parms);

            return new EmployeeLookupDTO
            {
                Employee = dt.Rows[0]["Employee"].ToString(),
                Supervisor = dt.Rows[0]["Supervisor"].ToString(),
                Department = dt.Rows[0]["Department"].ToString()           
            };
        }

        public Employee RetrieveEmployeeIdLogin(string email, string pass)
        {
            List<ParmStruct> parms = new List<ParmStruct>();
            parms.Add(new ParmStruct("@Email", email, SqlDbType.VarChar, 255));
            parms.Add(new ParmStruct("@Password", pass, SqlDbType.VarChar, 255));

            DataTable dt = db.GetData("spEmployeeLogin", parms);

            return new Employee
            {
                EmployeeId = Convert.ToInt32(dt.Rows[0]["EmployeeID"]),
                DepartmentId = Convert.ToInt32(dt.Rows[0]["DepartmentID"]),
                JobId = Convert.ToInt32(dt.Rows[0]["JobID"]),
                Password = dt.Rows[0]["Password"].ToString(),
                FirstName = dt.Rows[0]["FirstName"].ToString(),
                MiddleInitial = dt.Rows[0]["MI"].ToString(),
                LastName = dt.Rows[0]["LastName"].ToString(),
                SIN = dt.Rows[0]["SIN"].ToString(),
                DOB = Convert.ToDateTime(dt.Rows[0]["DOB"]),
                StreetAddress = dt.Rows[0]["StreetAddress"].ToString(),
                Municipality = dt.Rows[0]["Municipality"].ToString(),
                PostalCode = dt.Rows[0]["PostalCode"].ToString(),
                SeniorityDate = Convert.ToDateTime(dt.Rows[0]["SeniorityDate"]),
                JobStartDate = Convert.ToDateTime(dt.Rows[0]["JobStartDate"]),
                WorkPhone = dt.Rows[0]["WorkPhone"].ToString(),
                CellPhone = dt.Rows[0]["CellPhone"].ToString(),
                Email = dt.Rows[0]["Email"].ToString(),
                SupervisorId = Convert.ToInt32(dt.Rows[0]["Supervisor"]),
                Active = Convert.ToInt32(dt.Rows[0]["Active"])
            };
        }

        private SearchEmployeeLookupDTO PopulateSearchEmployeeDTO(DataRow row)
        {
            return new SearchEmployeeLookupDTO
            {
                EmployeeId = Convert.ToInt32(row["EmployeeID"]),
                FirstName = row["FirstName"].ToString(),
                MiddleInitial = row["MI"].ToString(),
                LastName = row["LastName"].ToString(),
                MailAddress = row["MailAddress"].ToString(),
                WorkPhone = row["WorkPhone"].ToString(),
                CellPhone = row["CellPhone"].ToString(),
                Email = row["Email"].ToString(),
                SupervisorID = Convert.ToInt32(row["Supervisor"])

            };
        }
    }
}
