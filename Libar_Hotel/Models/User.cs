using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Libar_Hotel.Models
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage ="Morate uneti svoje ime.")]
        [Display(Name = "Ime")]
        [RegularExpression(@"^([A-Z]{1}[a-z]{2,20})( [A-Z]{1}[a-z]{2,20})?$",
        ErrorMessage = "Ime mora da počinje velikim slovom i mora minimum da ima 3 slova do 20 slova.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Morate uneti svoje prezime.")]
        [Display(Name = "Prezime")]
        [RegularExpression(@"^([A-Z]{1}[a-z]{2,20})( [A-Z]{1}[a-z]{2,20})?$",
        ErrorMessage = "Prezime mora da počinje velikim slovom i mora minimum da ima 3 slova do 20 slova.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Morate uneti datum rođenja.")]
        [Display(Name = "Datum rođenja")]
        [AgeValidation]
        public DateTime BirthDate { get; set; }

        [Required]
        [StringLength(128)]
        public string AspNetUserId { get; set; }
        public ApplicationUser AspNetUser { get; set; }

        [Required]
        public bool IsActive { get; set; }
   

    }
}