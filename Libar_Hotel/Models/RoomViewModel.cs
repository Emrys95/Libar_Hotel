using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Libar_Hotel.Models;

namespace Libar_Hotel.Models
{
    public class RoomViewModel
    {
        public List<RoomType> RoomTypes { get; set; }

        public List<RoomStatus> RoomStatuses { get; set; }

        public Room Room { get; set; }

        public List<RoomView> RoomViews { get; set; }
        
        
    }
}