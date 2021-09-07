using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEConsulting.Model;
using System.Data;
using TEConsulting.Types;

namespace TEConsulting.Repository
{
    public class DepartmentRepo
    {
        #region Fields and Constructors

        DataAccess db = new DataAccess();

        #endregion

        #region Public Methods
        public bool Add(Department department)
        {
            List<ParmStruct> parms = new List<ParmStruct>();

            parms.Add(new ParmStruct("@DepartmentOutputPARM", department.DepartmentId, SqlDbType.Int, 0, ParameterDirection.Output));
            parms.Add(new ParmStruct("@DepartmentName", department.Name, SqlDbType.NVarChar, 255));
            parms.Add(new ParmStruct("@DepartmentDescription", department.Description, SqlDbType.NVarChar, 255));
            parms.Add(new ParmStruct("@InvocationDate", department.InvocationDate, SqlDbType.DateTime2));

            if (db.SendData("spAddDepartment", parms) > 0)
            {
                department.DepartmentId = (int)parms.Where(p => p.Name == "@DepartmentOutputPARM").FirstOrDefault().Value;
                return true;
            }

            return false;
        }

        public bool ModifyDepartment(Department department)
        {
            List<ParmStruct> parms = new List<ParmStruct>();

            parms.Add(new ParmStruct("@RecordVersion", department.RecordVersion, SqlDbType.Timestamp, 0, ParameterDirection.InputOutput));
            parms.Add(new ParmStruct("@DepartmentID", department.DepartmentId, SqlDbType.Int, 0));         
            parms.Add(new ParmStruct("@DepartmentName", department.Name, SqlDbType.NVarChar, 255));
            parms.Add(new ParmStruct("@DepartmentDescription", department.Description, SqlDbType.NVarChar, 255));
            parms.Add(new ParmStruct("@InvocationDate", department.InvocationDate, SqlDbType.DateTime2));

            if (db.SendData("spUpdateDepartment", parms) > 0)
            {
                department.DepartmentId = (int)parms.Where(p => p.Name == "@DepartmentID").FirstOrDefault().Value;
                department.RecordVersion = (byte[])parms.Where(p => p.Name == "@RecordVersion").FirstOrDefault().Value;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteDepartment(int departmentId)
        {
            List<ParmStruct> parms = new List<ParmStruct>();
            parms.Add(new ParmStruct("@DepartmentID", departmentId, SqlDbType.Int, 0));

            if (db.SendData("spDeleteDepartment", parms) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Private Methods

        #endregion
    }
}
