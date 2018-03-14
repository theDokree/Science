using System.Threading.Tasks;
using System.Collections.Generic;
using Sakura.AspNetCore;
using App.Library.Entity.DAO;
using App.Library.Entity.ViewModels.Account;
using Microsoft.AspNetCore.Identity;

namespace App.Library.Services.Users
{
    public interface IUsersService
    {
        /// <summary>
        /// Выборка пользователей
        /// </summary>
        /// <param name="sortorder"></param>
        /// <param name="searchstring"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<IPagedList<ApplicationUser>> GetUsers(string sortorder, string searchstring, int page = 1, int pageSize = 20);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApplicationUser> GetUsersById(string id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteUsersById(string id);

        Task<IdentityResult> CreateUser(RegisterViewModel model, ApplicationUser user);
    }
}
