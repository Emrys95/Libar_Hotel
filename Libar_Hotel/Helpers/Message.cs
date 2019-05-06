using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace Libar_Hotel.Helpers
{
    static class Message
    {
        public static void Display(Controller controller, string message)
        {
            controller.TempData["Message"] = message;

        }

        public static void Display(Controller controller, string message, object obj)
        {
            controller.TempData["Message"] = $"{message} {obj}";

        }

        
    }
}