using System;
using System.Collections.Generic;
using System.Text;
using Data.Tables;

namespace Data.Access.Interfaces
{
    public interface IUserRepository
    {
        void Add(User user);
        void Delete(Guid user);
        void Update(User user);
        ICollection<User> GetAll();
    }
}
