using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Libar_Hotel.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Zapamti ovaj pregledač?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        //dodat atribut KorisnickoIme(UserName)
        [Required(ErrorMessage ="Morate uneti korisničko ime.")]
        [Display(Name = "Korisničko ime")]
        public string UserName { get; set; }


        //[Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Morate uneti lozinku.")]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }

        [Display(Name = "Zapamti me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        //dodat atribut KorisnickoIme(UserName)

        [Required(ErrorMessage = "Morate uneti svoje ime.")]
        [Display(Name = "Ime")]
        [RegularExpression(@"^([A-Z]{1}[a-zA]{2,20})( [A-Z]{1}[a-z]{2,20})?$",
        ErrorMessage = "Ime mora da počinje velikim slovom i mora minimum da ima 3 slova do 20 slova.")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Morate uneti svoje prezime.")]
        [Display(Name = "Prezime")]
        [RegularExpression(@"^([A-Z]{1}[a-zA]{2,20})( [A-Z]{1}[a-z]{2,20})?$",
        ErrorMessage = "Prezime mora da počinje velikim slovom i mora minimum da ima 3 slova do 20 slova.")]
        public string LastName { get; set; }

        [Required(ErrorMessage ="Morate uneti korisničko ime")]
        [Display(Name = "Korisničko ime")]
        public string UserName { get; set; }

        //dodat datum rođenja
        

        [Required(ErrorMessage = "Morate uneti datum rođenja.")]
        [AgeValidation]
        [Display(Name = "Datum rođenja")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage ="Morate uneti email")]
        [EmailAddress(ErrorMessage ="Email nije u odgovarajućem formatu")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        // MENJATI NA SRPRSKI SVE DOLE
        [Required(ErrorMessage = "Morate uneti lozinku.")]
        [StringLength(100, ErrorMessage = "{0} mora sadržati najmanje {2} karaktera.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potvrdi lozinku")]
        [Compare("Password", ErrorMessage = "Lozinka i potvrda lozinke se ne podudaraju!")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potvrdi lozinku")]
        [Compare("Password", ErrorMessage = "Lozinka i potvrda lozinke se ne poklapaju!")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
