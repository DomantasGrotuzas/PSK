using System;
using System.Collections.Generic;
using System.Text;
using PSK.Domain.Enums;

namespace PSK.Domain
{
    public class EmployeeTrip
    {
        public int Id { get; set; }

        public PurchasableStatus PlaneTicketStatus { get; set; }

        public decimal? PlaneTicketPrice { get; set; }

        public PurchasableStatus CarReservationStatus { get; set; }

        public decimal? CarReservationPrice { get; set; }

        public Trip Trip { get; set; }

        public Employee Employee { get; set; }
    }
}
