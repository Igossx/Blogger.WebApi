using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task Register(User user);
        Task Delete(User user);
        Task<User> GetByEmail(string email);
        bool IsEmailUnique(string email);
        Task<IEnumerable<User>> GetAll();
    }
}
