using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TEConsulting.Model;
using TEConsulting.Model.HREntities;
using TEConsulting.Types;

namespace TEConsulting.Repository
{
    public class EmployeeRepo
    {
        #region Fields and Constructors

        DataAccess db = new DataAccess();

        #endregion

        #region Public Methods
        public bool Add(Employee employee)
        {
            List<ParmStruct> parms = new List<ParmStruct>();

            parms.Add(new ParmStruct("@EmployeeOutputPARM", employee.EmployeeId, SqlDbType.Int, 0, ParameterDirection.Output));
            parms.Add(new ParmStruct("@DepartmentID", employee.DepartmentId, SqlDbType.Int));
            parms.Add(new ParmStruct("@JobID", employee.JobId, SqlDbType.Int));
            parms.Add(new ParmStruct("@Password", employee.Password, SqlDbType.NVarChar, 255));
            parms.Add(new ParmStruct("@FirstName", employee.FirstName, SqlDbType.NVarChar, 255));
            parms.Add(new ParmStruct("@MI", employee.MiddleInitial, SqlDbType.NVarChar, 255));
            parms.Add(new ParmStruct("@LastName", employee.LastName, SqlDbType.NVarChar, 255));
            parms.Add(new ParmStruct("@SIN", employee.SIN, SqlDbType.Int));
            parms.Add(new ParmStruct("@DOB", employee.DOB, SqlDbType.DateTime2));
            parms.Add(new ParmStruct("@StreetAddress", employee.StreetAddress, SqlDbType.NVarChar, 255));
            parms.Add(new ParmStruct("@Municipality", employee.Municipality, SqlDbType.NVarChar, 255));
            parms.Add(new ParmStruct("@Province", employee.Province, SqlDbType.NVarChar, 255));
            parms.Add(new ParmStruct("@Country", employee.Country, SqlDbType.NVarChar, 255));
            parms.Add(new ParmStruct("@PostalCode", employee.PostalCode, SqlDbType.NVarChar, 255));
            parms.Add(new ParmStruct("@SeniorityDate", employee.SeniorityDate, SqlDbType.DateTime2));
            parms.Add(new ParmStruct("@JobStartDate", employee.JobStartDate, SqlDbType.DateTime2));
            parms.Add(new ParmStruct("@WorkPhone", employee.WorkPhone, SqlDbType.NVarChar, 255));
            parms.Add(new ParmStruct("@CellPhone", employee.CellPhone, SqlDbType.NVarChar, 255));
            parms.Add(new ParmStruct("@Email", employee.Email, SqlDbType.NVarChar, 255));
            parms.Add(new ParmStruct("@Supervisor", employee.SupervisorId, SqlDbType.Int));
            parms.Add(new ParmStruct("@Active", employee.Active, SqlDbType.Bit));
            parms.Add(new ParmStruct("@OfficeLocation", employee.OfficeLocation, SqlDbType.NVarChar, 50));
            parms.Add(new ParmStruct("@HashedPass", employee.HashedPass, SqlDbType.NVarChar, 255));

            if (db.SendData("spAddEmployee", parms) > 0)
            {
                employee.EmployeeId = (int)parms.Where(p => p.Name == "@EmployeeOutputPARM").FirstOrDefault().Value;
                return true;
            }

            return false;
        }

        public bool AddReview(Review review)
        {
            List<ParmStruct> parms = new List<ParmStruct>();
            parms.Add(new ParmStruct("@EmployeeID", review.EmployeeId, SqlDbType.Int));
            parms.Add(new ParmStruct("@SupervisorID", review.SupervisorId, SqlDbType.Int));
            parms.Add(new ParmStruct("@ReviewDate", review.ReviewDate, SqlDbType.DateTime2));
            parms.Add(new ParmStruct("@PerformanceRating", review.PerformanceRating, SqlDbType.NVarChar, 255));
            parms.Add(new ParmStruct("@Comment", review.Comment, SqlDbType.NVarChar, 255));

            if (db.SendData("spAddReview", parms) > 0)
            {
                return true;
            }

            return false;
        }
        public bool ModifyPersonalEmployee(Employee e)
        {
            List<ParmStruct> parms = new List<ParmStruct>();

            parms.Add(new ParmStruct("@RecordVersion", e.RecordVersion, SqlDbType.Timestamp, 0, ParameterDirection.InputOutput));
            parms.Add(new ParmStruct("@EmployeeID", e.EmployeeId, SqlDbType.Int, 0));
            parms.Add(new ParmStruct("@FirstName", e.FirstName, SqlDbType.NVarChar, 255));
            parms.Add(new ParmStruct("@MI", e.MiddleInitial, SqlDbType.NVarChar, 255));
            parms.Add(new ParmStruct("@LastName", e.LastName, SqlDbType.NVarChar, 255));
            parms.Add(new ParmStruct("@StreetAddress", e.StreetAddress, SqlDbType.NVarChar, 255));
            parms.Add(new ParmStruct("@Municipality", e.Municipality, SqlDbType.NVarChar, 255));
            parms.Add(new ParmStruct("@Province", e.Province, SqlDbType.NVarChar, 255));
            parms.Add(new ParmStruct("@Country", e.Country, SqlDbType.NVarChar, 255));
            parms.Add(new ParmStruct("@PostalCode", e.PostalCode, SqlDbType.NVarChar, 255));
            parms.Add(new ParmStruct("@WorkNum", e.WorkPhone, SqlDbType.NVarChar, 255));
            parms.Add(new ParmStruct("@CellNum", e.CellPhone, SqlDbType.NVarChar, 255));
            parms.Add(new ParmStruct("@Email", e.Email, SqlDbType.NVarChar, 255));
            parms.Add(new ParmStruct("@Password", e.Password, SqlDbType.NVarChar, 255));
            parms.Add(new ParmStruct("@DOB", e.DOB, SqlDbType.DateTime2));

            if (db.SendData("spUpdatePersonalEmployee", parms) > 0)
            {
                //department.DepartmentId = (int)parms.Where(p => p.Name == "@DepartmentID").FirstOrDefault().Value;
                e.RecordVersion = (byte[])parms.Where(p => p.Name == "@RecordVersion").FirstOrDefault().Value;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ModifyPersonalInformation(Employee e)
        {
            List<ParmStruct> parms = new List<ParmStruct>();
            parms.Add(new ParmStruct("@RecordVersion", e.RecordVersion, SqlDbType.Timestamp, 0, ParameterDirection.InputOutput));
            parms.Add(new ParmStruct("@EmployeeID", e.EmployeeId, SqlDbType.Int, 0));
            parms.Add(new ParmStruct("@StreetAddress", e.StreetAddress, SqlDbType.NVarChar, 255));
            parms.Add(new ParmStruct("@Municipality", e.Municipality, SqlDbType.NVarChar, 255));
            parms.Add(new ParmStruct("@Province", e.Province, SqlDbType.NVarChar, 255));
            parms.Add(new ParmStruct("@Country", e.Country, SqlDbType.NVarChar, 255));        
            parms.Add(new ParmStruct("@WorkNum", e.WorkPhone, SqlDbType.NVarChar, 255));
            parms.Add(new ParmStruct("@CellNum", e.CellPhone, SqlDbType.NVarChar, 255));


            if (db.SendData("spUpdatePersonalInformation", parms) > 0)
            {
                //department.DepartmentId = (int)parms.Where(p => p.Name == "@DepartmentID").FirstOrDefault().Value;
                e.RecordVersion = (byte[])parms.Where(p => p.Name == "@RecordVersion").FirstOrDefault().Value;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ModifyJobInfo(Employee e)
        {
            List<ParmStruct> parms = new List<ParmStruct>();

            parms.Add(new ParmStruct("@RecordVersion", e.RecordVersion, SqlDbType.Timestamp, 0, ParameterDirection.InputOutput));
            parms.Add(new ParmStruct("@EmployeeID", e.EmployeeId, SqlDbType.Int, 0));            
            parms.Add(new ParmStruct("@StartDate", e.JobStartDate, SqlDbType.DateTime2));
            parms.Add(new ParmStruct("@DepartmentID", e.DepartmentId, SqlDbType.Int, 0));
            parms.Add(new ParmStruct("@SupervisorID", e.SupervisorId, SqlDbType.Int, 0));
            parms.Add(new ParmStruct("@JobID", e.JobId, SqlDbType.Int, 0));
            if (db.SendData("spUpdateEmployeeJobInfo", parms) > 0)
            {
                //department.DepartmentId = (int)parms.Where(p => p.Name == "@DepartmentID").FirstOrDefault().Value;
                e.RecordVersion = (byte[])parms.Where(p => p.Name == "@RecordVersion").FirstOrDefault().Value;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ModifyStatus(Employee e)
        {
            List<ParmStruct> parms = new List<ParmStruct>();

            parms.Add(new ParmStruct("@RecordVersion", e.RecordVersion, SqlDbType.Timestamp, 0, ParameterDirection.InputOutput));
            parms.Add(new ParmStruct("@EmployeeID", e.EmployeeId, SqlDbType.Int, 0));
            parms.Add(new ParmStruct("@RetirementDate", e.RetirementDate, SqlDbType.DateTime2));
            parms.Add(new ParmStruct("@TerminationDate", e.TerminationDate, SqlDbType.DateTime2));
            parms.Add(new ParmStruct("@Active", e.Active, SqlDbType.Int, 0));
            if (db.SendData("spUpdateEmployeeStatus", parms) > 0)
            {
                //department.DepartmentId = (int)parms.Where(p => p.Name == "@DepartmentID").FirstOrDefault().Value;
                e.RecordVersion = (byte[])parms.Where(p => p.Name == "@RecordVersion").FirstOrDefault().Value;
                return true;
            }
            else
            {
                return false;
            }
        }

        public Employee GetEmployee(int employeeId)
        {
            DataTable dt = db.GetData($"SELECT EmployeeID, FirstName, MI, LastName, SIN, DOB, StreetAddress, Municipality, Province, Country, PostalCode, WorkPhone, CellPhone, Email, Supervisor, DepartmentID, Password, RecordVersion FROM Employee WHERE EmployeeID = {employeeId}", null, CommandType.Text);

            return new Employee
            {
                EmployeeId = Convert.ToInt32(dt.Rows[0]["EmployeeID"]),
                Password = dt.Rows[0]["Password"].ToString(),
                FirstName = dt.Rows[0]["FirstName"].ToString(),
                MiddleInitial = dt.Rows[0]["MI"].ToString(),
                LastName = dt.Rows[0]["LastName"].ToString(),
                SIN = dt.Rows[0]["SIN"].ToString(),
                DOB = Convert.ToDateTime(dt.Rows[0]["DOB"]),
                StreetAddress = dt.Rows[0]["StreetAddress"].ToString(),
                Municipality = dt.Rows[0]["Municipality"].ToString(),
                Province = dt.Rows[0]["Province"].ToString(),
                Country = dt.Rows[0]["Country"].ToString(),
                PostalCode = dt.Rows[0]["PostalCode"].ToString(),
                WorkPhone = dt.Rows[0]["WorkPhone"].ToString(),
                CellPhone = dt.Rows[0]["CellPhone"].ToString(),
                SupervisorId = Convert.ToInt32(dt.Rows[0]["Supervisor"]),
                DepartmentId = Convert.ToInt32(dt.Rows[0]["DepartmentID"]),
                Email = dt.Rows[0]["Email"].ToString(),
                RecordVersion = (byte[])dt.Rows[0]["RecordVersion"]
            };
        }

        public Employee GetEmployeeJobInfo(int employeeId)
        {
            //Using inline queries for now, Khraig's Department is unique for CEO only.
            DataTable dt = db.GetData($"SELECT SIN, JobStartDate, DepartmentID, Supervisor, JobID, SeniorityDate, RecordVersion FROM Employee WHERE EmployeeID = {employeeId}", null, CommandType.Text);

            return new Employee
            {
                SIN = dt.Rows[0]["SIN"].ToString(),
                SeniorityDate = Convert.ToDateTime(dt.Rows[0]["SeniorityDate"]),
                JobStartDate = Convert.ToDateTime(dt.Rows[0]["JobStartDate"]),
                DepartmentId = Convert.ToInt32(dt.Rows[0]["DepartmentID"]),
                SupervisorId = Convert.ToInt32(dt.Rows[0]["Supervisor"]),
                JobId = Convert.ToInt32(dt.Rows[0]["JobID"]),
                RecordVersion = (byte[])dt.Rows[0]["RecordVersion"]
            };
        }
        public Employee GetEmployeeStatus(int employeeId)
        {
            DataTable dt = db.GetData($"SELECT RetirementDate, TerminationDate, Active FROM Employee WHERE EmployeeID = {employeeId}", null, CommandType.Text);

            DateTime retirementDate;
            DateTime terminationDate;

            if (dt.Rows[0]["RetirementDate"] == DBNull.Value)
            {
                retirementDate = default;
            }
            else
            {
                retirementDate = Convert.ToDateTime(dt.Rows[0]["RetirementDate"]);
            }
            if (dt.Rows[0]["TerminationDate"] == DBNull.Value)
            {
                terminationDate = default;
            }
            else
            {
                terminationDate = Convert.ToDateTime(dt.Rows[0]["TerminationDate"]);
            }
            return new Employee
            {
                
                RetirementDate = retirementDate,
                TerminationDate = terminationDate,
                Active = Convert.ToInt32(dt.Rows[0]["Active"])
            };
        }

        //Using inline queries to save time, convert to stored procs later
        public int GetNumEmpInDept(int departmentId)
        {
            //Get the number of employees (including supervisors) from a given department.
            string query = $"SELECT COUNT(*) FROM Employee WHERE DepartmentID = {departmentId}";
            return Convert.ToInt32(db.GetValue(query, null, CommandType.Text));
        }

        public int GetNumSupInDept(int departmentId)
        {
            //Get the number of supervisors that are from a given department.
            string query = $"SELECT COUNT(*) FROM Employee WHERE DepartmentID = {departmentId} AND Supervisor = 10000000";
            return Convert.ToInt32(db.GetValue(query, null, CommandType.Text));
        }


        #endregion

        #region Private Methods
        //Using front-end validation for now
        private Employee PopulateEmployee(DataRow row)
        {
            return new Employee
            {
                EmployeeId = Convert.ToInt32(row["EmployeeID"]),
                DepartmentId = Convert.ToInt32(row["DepartmentID"]),
                JobId = Convert.ToInt32(row["JobID"]),
                Password = row["Password"].ToString(),
                FirstName = row["FirstName"].ToString(),
                MiddleInitial = row["MI"].ToString(),
                LastName = row["LastName"].ToString(),
                SIN = row["SIN"].ToString(),
                DOB = Convert.ToDateTime(row["DOB"]),
                StreetAddress = row["StreetAddress"].ToString(),
                Municipality = row["Municipality"].ToString(),
                PostalCode = row["PostalCode"].ToString(),
                SeniorityDate = Convert.ToDateTime(row["SeniorityDate"]),
                JobStartDate = Convert.ToDateTime(row["JobStartDate"]),
                WorkPhone = row["WorkPhone"].ToString(),
                CellPhone = row["CellPhone"].ToString(),
                Email = row["Email"].ToString(),
                SupervisorId = Convert.ToInt32(row["Supervisor"])
            };
        }

        private Employee PopulatePersonalEmployee(DataRow row)
        {
            return new Employee
            {
                Password = row["Password"].ToString(),
                FirstName = row["FirstName"].ToString(),
                MiddleInitial = row["MI"].ToString(),
                LastName = row["LastName"].ToString(),
                SIN = row["SIN"].ToString(),
                DOB = Convert.ToDateTime(row["DOB"]),
                StreetAddress = row["StreetAddress"].ToString(),
                Municipality = row["Municipality"].ToString(),
                Province = row["Province"].ToString(),
                Country = row["Country"].ToString(),
                PostalCode = row["PostalCode"].ToString(),
                WorkPhone = row["WorkPhone"].ToString(),
                CellPhone = row["CellPhone"].ToString(),
                Email = row["Email"].ToString(),
            };
        }
        #endregion
    }


}
