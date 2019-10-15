using System;
using System.Collections.Generic;
using System.Text;
using Data.Tables;

namespace Data.Access.Interfaces
{
    interface ITicketRepository
    {
        void Add(Ticket ticket);
        void Delete(Guid ticket);
        void Update(Ticket ticket);
        ICollection<Ticket> GetAll();
    }
}
