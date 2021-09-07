using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TEConsulting.Model.EmployeeEnums;

namespace TEConsulting.Model
{
    public class OrderItem : BaseEntity
    {
        public int OrderItemID { get; set; }

        [Required]
        public int PONumber { get; set; }

        public int StatusID { get; set; }

        public virtual string Status
        {
            get
            {
                switch (StatusID)
                {
                    case 2:
                        return "Approved";
                    case 3:
                        return "Denied";
                    default:
                        return "Pending";
                }

            }
            
        }

        public virtual string Unknown { get; set; }

        [Required(ErrorMessage = "Please enter an item name.")]
        public string OrderItemName { get; set; }

        [Required(ErrorMessage = "Please enter an item description.")]
        public string OrderItemDescription { get; set; }

        [Required(ErrorMessage = "Please enter an item justification.")]
        public string OrderItemJustification { get; set; }

        [Required(ErrorMessage = "Please enter an item location.")]
        public string OrderItemLocation { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter an item quantity.")]
        public int OrderItemQty { get; set; }

        [Required(ErrorMessage = "Please enter an item price.")]
        public decimal OrderItemPrice { get; set; }

        public byte[] RecordVersion { get; set; }

        public string Reason { get; set; } = "";
    }
}
