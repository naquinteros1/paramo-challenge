using Sat.Domain.Helpers;
using Sat.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Domain.Services
{
    public interface IUserService
    {
        public Task<ResultModel<UserModel>> CreateUser(UserModel usuario);
    }
}
