using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEConsulting.Model
{
    public class Order : BaseEntity
    {
        public int PONumber { get; set; }
        public virtual string PONumberFormatted
        {
            get
            {
                if(PONumber != 0)
                {
                    int length = (int)Math.Floor(Math.Log10(PONumber) + 1);
                    int needed = 8 - length;
                    return PONumber.ToString().PadLeft(PONumber.ToString().Length + needed, '0');
                }
                else
                {
                    return "0";
                }
                
            }
        }

        public int StatusID { get; set; }

        public virtual string Status
        {
            get
            {
                switch(StatusID)
                {
                    case 4:
                        return "Under Review";
                    case 5:
                        return "Closed";
                    default:
                        return "Pending";
                }
                
            }
        }

        public List<OrderItem> Items { get; set; }

        [POCreationDate]
        public DateTime POCreationDate { get; set; }

        public decimal POSubtotal { get; set; }

        public decimal POTax { get; set; }

        public decimal POTotal { get; set; }

        [Required]
        public int EmployeeID { get; set; }

        public byte[] RecordVersion { get; set; }

        public string EmployeeName { get; set; }
    }
}
