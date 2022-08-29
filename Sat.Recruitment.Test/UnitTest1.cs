using System;
using Moq;
using Sat.Domain.Helpers;
using Sat.Domain.Models;
using Sat.Domain.Services;
using Sat.Recruitment.Api.Controllers;
using System.Collections.Generic;

using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Sat.Application.Services;
using Sat.Domain.Repositories;
using Sat.PersistenseFile.Repositories;
using Microsoft.Extensions.Configuration;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTest1
    {
        private IUserRepository uRepo;
        private IUserService uService;

        [Fact]
        public void Test1()
        {

            var services = new ServiceCollection();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IConfiguration>(sp =>
            {
                IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
                configurationBuilder.AddJsonFile("appsettings.json");
                return configurationBuilder.Build();
            });

            var serviceProvider = services.BuildServiceProvider();

            uRepo = serviceProvider.GetService<IUserRepository>();
            uService = serviceProvider.GetService<IUserService>();

            var user = new UserModel
            {
                Address = "Av. Juan v justo",
                Email = "mike@gmail.com",
                Money = Convert.ToDecimal(124),
                Name = "Mike",
                Phone = "+349 1122354216",
                UserType = "Normal"
            };


            var userController = new UsersController(uService);

            

            var result = userController.CreateUser(user).Result;


            Assert.Equal(true, result.IsSuccess);
        }

        [Fact]
        public void Test2()
        {

            var services = new ServiceCollection();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IConfiguration>(sp =>
            {
                IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
                configurationBuilder.AddJsonFile("appsettings.json");
                return configurationBuilder.Build();
            });

            var serviceProvider = services.BuildServiceProvider();


            uRepo = serviceProvider.GetService<IUserRepository>();
            uService = serviceProvider.GetService<IUserService>();

            var user = new UserModel
            {
                Address = "Av. Juan G",
                Email = "agustina@gmail.com",
                Money = Convert.ToDecimal(124),
                Name = "Agustina",
                Phone = "+349 1122354215",
                UserType = "Normal"
            };

            var userController = new UsersController(uService);

            var result = userController.CreateUser(user).Result;


            Assert.Equal(false, result.IsSuccess);
            foreach (var item in result.Errors)
            {
                Assert.Equal("The User is duplicated", item.Message);
            }
        }
    }
}
