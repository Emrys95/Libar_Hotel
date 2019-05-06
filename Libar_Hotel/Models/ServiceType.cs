using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;// dodato zbog anotacija

namespace Libar_Hotel.Models
{
    public class ServiceType
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required (ErrorMessage= "Morate uneti naziv usluge")]
        [Display(Name="Usluga")]
        [RegularExpression(@"^[A-Z]{1}[a-z ]{3,20}$", ErrorMessage="Naziv usluge mora početi velikim slovom i mora sadržati od 3 do 20 slova")]
        public string Name { get; set; }

        [Required (ErrorMessage ="Morate uneti cenu")]
        [Display(Name ="Cena")]
        [RegularExpression(@"^\d{2,3}$", ErrorMessage="Cena usluge mora biti dvocifren ili trocifren broj, manji od 1000")]
        [Range(10, 999, ErrorMessage ="Cena usluge mora biti u rasponu od 10 do 999")]
        public double Price { get; set; }

        [Required]
        public bool IsActive { get; set; }


    }
}