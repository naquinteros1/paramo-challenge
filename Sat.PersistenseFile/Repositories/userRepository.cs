using Sat.Domain.Models;
using Sat.Domain.Repositories;
using Sat.PersistenseFile.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

namespace Sat.PersistenseFile.Repositories
{
    public class UserRepository : IUserRepository
    {
        private IConfiguration _Configuration;
        public UserRepository( IConfiguration configuration)
        {

            _Configuration = configuration;
        }

        public async Task<bool> CreateUser(UserModel usuario)
        {
            var user = usuario.Name + ',' + usuario.Email + ',' + usuario.Phone + ',' + usuario.Address + ',' + usuario.UserType + ',' + usuario.Money;

            var all = FileHelper.ReadAllFromFile(Directory.GetCurrentDirectory() + _Configuration["FilePaths:user"]);

            if (all.ToLower().Contains(usuario.Name.ToLower()) && all.ToLower().Contains(usuario.Address.ToLower()))
                return false;
            else if (all.ToLower().Contains(usuario.Email.ToLower()) || all.ToLower().Contains(usuario.Address.ToLower()))
                return false;

            await FileHelper.WriteFile(Directory.GetCurrentDirectory() + _Configuration["FilePaths:user"], user);

            return true;
        }

     }
}
