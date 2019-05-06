using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;// dodato zbog anotacija


namespace Libar_Hotel.Models
{
    public class Room
    {
        [Key]
        [Required]
        [Display(Name ="Broj sobe")]
        public int Id { get; set; }

        [Required(ErrorMessage ="Morate uneti broj sprata")]
        [Display(Name ="Sprat")]
        [RegularExpression(@"^\d{1,1}$", 
        ErrorMessage = "Broj sprata mora da bude jednocifren broj")]
        [Range(1,9, ErrorMessage = "Broj sprata mora biti u granicama od 1 do 9")]
        public int Floor { get; set; }

        [Required]
        public int RoomTypeId { get; set; }
        public RoomType RoomType { get; set; }

        [Required]
        public int RoomStatusId { get; set; }
        public RoomStatus RoomStatus { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public int RoomViewId { get; set; }
        public RoomView RoomView { get; set; }



    }
}