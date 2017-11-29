using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Vidly.Models
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var Customer = (Customer)validationContext.ObjectInstance;

            //old way
            //if (Customer.MembershipTypeId==0 ||Customer.MembershipTypeId == 1)
            //    return ValidationResult.Success;

            //more readable code
            if (Customer.MembershipTypeId == MembershipType.Unknown || Customer.MembershipTypeId == MembershipType.PayAsYouGo)
                return ValidationResult.Success;

            if (Customer.Birthdate == null)
                return new ValidationResult("Birthday is required");

            var age = System.DateTime.Now.Year - Customer.Birthdate.Value.Year;

            return (age > 18) ?
                     ValidationResult.Success
                : new ValidationResult("Customer must be above 18 years to go over this membership"); 
        }
    }
}