using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libar_Hotel.Models
{
    public class UserReservationViewModel
    {
        public ReservationRequest ReservationRequest { get; set; }
        public Reservation Reservation { get; set; }
        public int UserId { get; set; }
    }
}