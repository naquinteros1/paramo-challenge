using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Domain.Helpers;
using Sat.Domain.Models;
using Sat.Domain.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {

        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Create new user
        /// Params UserModel
        /// </summary>
        [HttpPost]
        [Route("/create-user")]
        public async Task<ResultModel<UserModel>> CreateUser([FromBody] UserModel user)
        {
            var result = await _userService.CreateUser(user);
            return result;
        }
    }
}
