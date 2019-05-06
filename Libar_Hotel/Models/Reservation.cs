using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;// dodato zbog anotacija

namespace Libar_Hotel.Models
{
    public class Reservation
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Datum rezervacije")]
        public DateTime ReservationDate { get; set; }

        [Required (ErrorMessage ="Morate uneti datum prijave.")]
        [CheckInDateValidation]
        [Display(Name = "Datum prijave")]
        public DateTime CheckInDate { get; set; }

        [Required(ErrorMessage = "Morate uneti datum odjave.")]
        [CheckoutDateValidation]
        [Display(Name = "Datum odjave")]
        public DateTime CheckOutDate { get; set; }

        [Required]
        [Display(Name = "Broj gostiju")]
        public int NumberOfGuests { get; set; }

       
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Display(Name="Broj sobe")]
        public int? RoomId { get; set; }
        public Room Room { get; set; }

        [Required]
        public int ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; }


        [Required]
        [Display(Name = "Cena")]
        public double Price { get; set; }

        [Required]
        public bool IsActive { get; set; }



    }
}