using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Tables
{
    public class Flight : Entity
    {
        public DateTime ArrivalDate { get; set; }
        public Guid UserID { get; set; }
        public DateTime DepartureDate { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string PlaneName { get; set; }
    }
}
