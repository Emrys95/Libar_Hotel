using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Libar_Hotel.Models
{
    //Pogledi za sobe Basta, More...
    public class RoomView
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength=4)]
        [Display(Name="Pogled")]
        public string Name { get; set; }
    }
}