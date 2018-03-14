using System;
using App.Library.Entity.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Library.Entity;
using AutoMapper;
using App.Library.Entity.ViewModels.Shared;
using App.Library.Entity.DAO;
using Microsoft.EntityFrameworkCore;
using App.Library.Entity.ViewModels.News;
using App.Library.Database.Context;
using System.Linq;
using Sakura.AspNetCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using App.Library.Entity.ViewModels.Account;

namespace App.Library.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly ILogger<UsersService> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;


        public UsersService(
            ILogger<UsersService> logger,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<IdentityResult> CreateUser(RegisterViewModel model, ApplicationUser user)
        {
            var result = await _userManager.CreateAsync(user, model.Password);
            return result;
        }

        public Task<bool> DeleteUsersById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Sakura.AspNetCore.IPagedList<ApplicationUser>> GetUsers(string sortorder, string searchstring, int page = 1, int pageSize = 20)
        {
            try
            {
                var users = _userManager.Users;
                if (!String.IsNullOrEmpty(searchstring))
                {
                    users = users.Where(s => s.UserName.Contains(searchstring));
                }
                switch (sortorder)
                {
                    case "username_desc":
                        users = users.OrderByDescending(s => s.UserName);
                        break;
                    default:
                        users = users.OrderBy(s => s.UserName);
                        break;
                }
                return await users.ToPagedListAsync(pageSize, page);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ошибка: {0}", ex.Message);                
                return null;
            }
        }



        public Task<ApplicationUser> GetUsersById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
