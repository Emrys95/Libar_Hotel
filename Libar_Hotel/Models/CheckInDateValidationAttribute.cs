using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Libar_Hotel.Models
{
    public class CheckInDateValidationAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value == null)
                return new ValidationResult("Datum nije u ispravnom formatu.");

            var today = DateTime.Today;
            var checkinDate = (DateTime)value;


            if (checkinDate > today)
                return ValidationResult.Success;


            return new ValidationResult("Datum prijave mora biti posle danasnjeg datuma.");
           
        }
    }
}