using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libar_Hotel.Models
{
    public class ReservationViewModel
    {
        public Reservation Reservation { get; set; }

        public List<Room> Rooms { get; set; }
     
        public int ReservationRequestId { get; set; }
        

        

    }
}