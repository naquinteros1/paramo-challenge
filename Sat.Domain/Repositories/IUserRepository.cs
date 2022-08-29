using Sat.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<bool> CreateUser(UserModel usuario);
    }
}
