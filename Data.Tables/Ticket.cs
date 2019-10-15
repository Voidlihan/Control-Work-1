using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Tables
{
    public class Ticket : Entity
    {
        public int TicketNumber { get; set; }
        public Guid UserID { get; set; }
        public Guid FlightID { get; set; }
        public string ArrivalDateInfo { get; set; }
        public string DepartureDateInfo { get; set; }
        public int Price { get; set; }
        public bool Children { get; set; }
    }
}
