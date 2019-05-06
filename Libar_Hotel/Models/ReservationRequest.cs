using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Libar_Hotel.Models
{
    public class ReservationRequest
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name ="Datum rezervacije")]
        public DateTime ReservationDate { get; set; }
        
        [Required(ErrorMessage ="Morate uneti datum prijave.")]
        [Display(Name = "Datum prijave")]
        [CheckInDateValidation]
        public DateTime CheckInDate { get; set; }

        [CheckoutDateValidation]
        [Required(ErrorMessage = "Morate uneti datum odjave.")]
        [Display(Name = "Datum odjave")]
        public DateTime CheckOutDate { get; set; }


        [Required]
        public int ServiceTypeId { get; set; }
        public ServiceType ServiceType {get; set;}

        [Required]
        public int RoomTypeId { get; set; }
        public RoomType RoomType { get; set; }

        [Required]
        public int RoomViewId { get; set; }
        public RoomView RoomView { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required(ErrorMessage = "Morate uneti broj gostiju.")]
        [Display(Name ="Broj gostiju")]
        [Range(1,5, ErrorMessage ="Broj osoba mora biti od 1 do 5.")]
        public int NumberOfGuests { get; set; }

        [Required]
        [Display(Name = "Cena")]
        public double Price { get; set; }


    }
}