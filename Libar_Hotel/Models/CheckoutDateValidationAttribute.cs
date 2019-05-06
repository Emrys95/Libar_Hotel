using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Libar_Hotel.Models
{
    public class CheckoutDateValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ReservationRequest reservationRequest = null;
            Reservation reservation = null;

            if (value == null)
            {
                return new  ValidationResult("Morate uneti datum odjave.");
            }

            DateTime checkoutDate = (DateTime)value;
            DateTime checkinDate = new DateTime();


            //Provera da li je instanca validacionog konteksta zahtev rezervacije ili rezervacija kako bi postavili odgovarajuci datum da poredimo
            if (validationContext.ObjectInstance is ReservationRequest)
            {
                reservationRequest = (ReservationRequest)validationContext.ObjectInstance;
                checkinDate = reservationRequest.CheckInDate;
            }

            
            else if (validationContext.ObjectInstance is Reservation)
            {
                reservation = (Reservation)validationContext.ObjectInstance;
                checkinDate = reservation.CheckInDate;
            }
                

            if (checkoutDate > checkinDate)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Datum odjave mora biti posle datuma prijave.");
            
        }
    }
}