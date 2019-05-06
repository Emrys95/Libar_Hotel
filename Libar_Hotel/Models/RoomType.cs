using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; // dodato zbog anotacija

namespace Libar_Hotel.Models
{
    public class RoomType
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required (ErrorMessage ="Morate uneti naziv tipa sobe.")]
        [Display(Name="Tip sobe")]
        [RegularExpression("^([A-Z]{1}[a-z ]{3,30})(-[a-z ]{3,30})?$", 
        ErrorMessage = "Naziv tipa sobe mora početi velikim slovom i mora sadržati od 3 do 30 slova.")]
        public string Name { get; set; }

        [Required (ErrorMessage = "Morate uneti cenu tipa sobe")]
        [Display(Name="Cena")]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "Cena tipa sobe mora biti broj i mora sadržati 3 cifre.")]
        [Range(100,999, ErrorMessage = "Cena tipa sobe mora biti u rasponu od 100 do 999.")]
        public double Price { get; set; }

        [Required]
        public bool IsActive { get; set; }


    }
}