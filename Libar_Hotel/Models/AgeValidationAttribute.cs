using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Libar_Hotel.Models
{
    public class AgeValidationAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("Datum nije u ispravnom formatu.");

            DateTime birthdate = (DateTime)value;

            

            DateTime today = DateTime.Today;

            var age = today.Year - birthdate.Year;

            //Ako je prestupna godina smanji godinu za jedan
            if (birthdate.Date > today.AddYears(-age))
                age--;

            if (age < 18)
                return new ValidationResult("Morate biti punoletni.");

            else
                return ValidationResult.Success;
            

            
        }
    }
}





/*protected override ValidationResult IsValid(object value, ValidationContext validationContext)
{
    if (value == null)
    {
        return new ValidationResult("Datum je obavezno polje");
    }
    DateTime datum = (DateTime)value;
    if (datum > DateTime.Now.AddYears(-2) && datum <= DateTime.Now)
    {
        return ValidationResult.Success;
    }
    else
    {
        return new ValidationResult("Datum nije u dozvoljenom opsegu");
    }
}*/