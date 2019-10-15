using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Tables
{
    public class User : Entity
    {
        public string Phonenumber { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public int IIN { get; set; }     
        public int TicketNumber_ID { get; set; }
    }
}
