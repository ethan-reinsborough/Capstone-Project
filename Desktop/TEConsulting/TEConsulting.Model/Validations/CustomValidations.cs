using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEConsulting.Model
{
    internal class POCreationDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((DateTime)value <= DateTime.Now)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Please enter a correct value.");
        }
    }

    //internal class EnsureMinElementsAttribute : ValidationAttribute
    //{
    //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //    {
    //        List<OrderItem> list = value as List<OrderItem>;
    //        if (list != null && list.Any() && list.Count >= 1)
    //        {
    //            return ValidationResult.Success;
    //        }
    //        return new ValidationResult("An order must have at least one orderitem to be created");
    //    }
    //}
}
