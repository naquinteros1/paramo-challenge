using Sat.Application.Resources;
using Sat.Domain.Helpers;
using Sat.Domain.Models;
using Sat.Domain.Repositories;
using Sat.Domain.Services;
using Sat.PersistenseFile.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(
            IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ResultModel<UserModel>> CreateUser(UserModel usuario)
        {
            var result = new ResultModel<UserModel>();
            try
            {
                ValidateErrors(usuario, result);

                if (result.Errors.Count() > 0)
                    return result;

                switch (usuario.UserType)
                {
                    case "SuperUser":
                        if (usuario.Money > 100)
                            usuario.Money = usuario.Money + (usuario.Money * Convert.ToDecimal(0.20));
                        break;
                    case "Premium":
                        if(usuario.Money > 100)
                            usuario.Money = usuario.Money * 2;
                        break;
                    default:
                        decimal gif = (usuario.Money < 100 && usuario.Money > 10) ? usuario.Money * Convert.ToDecimal(0.8) : usuario.Money * Convert.ToDecimal(0.12);
                        usuario.Money = usuario.Money + gif;
                        break;
                }

                var data = await _userRepository.CreateUser(usuario);

                if(data)
                {
                    result.Data = usuario;
                    Debug.WriteLine(string.Format(GeneralResources.Created, "User"));
                }
                else
                {
                    result.AddInputDataError(string.Format(GeneralResources.Duplicated, "User"));
                    Debug.WriteLine(string.Format(GeneralResources.Duplicated, "User"));
                }
                    
            }
            catch (Exception ex)
            {
                result.AddInternalError(GeneralResources.UnhandledError);
                Debug.WriteLine(ex.Message);
            }

            return result;
        }

        private ResultModel<UserModel> ValidateErrors(UserModel user, ResultModel<UserModel> result)
        {
            if (string.IsNullOrEmpty(user.Name))
                result.AddInputDataError(string.Format(GeneralResources.Required, "Name"));

            if (string.IsNullOrEmpty(user.Email))
                result.AddInputDataError(string.Format(GeneralResources.Required, "Email"));
            
            if (!IsValidEmail(user.Email))
                result.AddInputDataError(string.Format(GeneralResources.Format, "Email"));

            if (string.IsNullOrEmpty(user.Address))
                result.AddInputDataError(string.Format(GeneralResources.Required, "Address"));

            if (string.IsNullOrEmpty(user.Phone))
                result.AddInputDataError(string.Format(GeneralResources.Required, "Phone"));

            return result;
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

    }
}
