using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Domain
{
    public interface IUserRepository
    {
        Task<User> Get(string login);
    }
}
