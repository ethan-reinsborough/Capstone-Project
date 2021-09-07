using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEConsulting.Model.HREntities
{
    public class Review : BaseEntity
    {
        public int ReviewId { get; set; }

        public int EmployeeId { get; set; }

        public int SupervisorId { get; set; }

        public DateTime ReviewDate { get; set; }

        public string PerformanceRating { get; set; }

        public string Comment { get; set; }

        public byte[] RecordVersion { get; set; }
    }
}
