using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;// dodato zbog anotacija

namespace Libar_Hotel.Models
{
    public class RoomStatus
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name="Status sobe")]
        public string Name { get; set; }
    }
}