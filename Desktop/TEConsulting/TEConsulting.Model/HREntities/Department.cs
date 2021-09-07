using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace TEConsulting.Model
{
    public class Department : BaseEntity 
    {
        public int DepartmentId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime InvocationDate { get; set; }

        public byte[] RecordVersion { get; set; }
    }
}
