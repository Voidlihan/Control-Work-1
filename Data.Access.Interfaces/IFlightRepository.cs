using System;
using System.Collections.Generic;
using System.Text;
using Data.Tables;

namespace Data.Access.Interfaces
{
    public interface IFlightRepository
    {
        void Add(Flight flight);
        void Delete(Guid flight);
        void Update(Flight flight);
        ICollection<Flight> GetAll();
    }
}
