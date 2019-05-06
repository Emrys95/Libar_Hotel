using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libar_Hotel.Models
{
    public class ReservationRequestViewModel
    {
        public ReservationRequest ReservationRequest { get; set; }

        public List<RoomType> RoomTypes { get; set; }
        public List<ServiceType> ServiceTypes { get; set; }

        public List<RoomView> RoomViews { get; set; }

        public int UserId { get; set; }
        


    }
}