using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEConsulting.API
{
    public class SearchParam
    {
        public int empId { get; set; }
        public int poNumber { get; set; }
        public string searchEmp { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public int statusId { get; set; }
    }

}