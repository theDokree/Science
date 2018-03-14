using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using App.Library.Entity.DAO;
using Microsoft.Extensions.Logging;
using App.Library.Services.Users;
using App.Library.Entity.ViewModels.Account;
using Sakura.AspNetCore;
using Microsoft.AspNetCore.Authorization;

namespace App.Web.Controllers
{
    [Authorize(Roles = "Администратор системы")]
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UsersController(         
            ILogger<UsersController> logger, 
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
     
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index(string sortorder, string searchstring, int page = 1, int pageSize = 20)
        {
            ViewBag.searchstring = searchstring;
            ViewBag.UserNameSortParm = String.IsNullOrEmpty(sortorder) ? "username_desc" : "";

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

                _logger.LogInformation("Обработка запроса: {0}{1}", Request.Path, Request.QueryString);
                _logger.LogInformation("IP: {0}, Пользователь: {1}", Request.Host, User.Identity.IsAuthenticated ? User.Identity.Name : "Гость");

                return View(await users.ToPagedListAsync(pageSize, page));
            }
            catch (Exception ex)
            {
                _logger.LogError("Ошибка: {0}", ex.Message);
                ViewData["Error"] = ex.Message;
                return View("Error");
            }           
        }

        [HttpGet]  
        public IActionResult Create(string returnUrl = null)
        {
            RegisterViewModel model = new RegisterViewModel(){
                //Roles = new List<string>()

            };

            ViewData["ReturnUrl"] = returnUrl ?? "/Users";
            ViewData["Roles"] = _roleManager.Roles.OrderBy(p => p.Name);  
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterViewModel model, string returnUrl = null)
        {    
            if (ModelState.IsValid)
            {
                try
                {
                    var userexist = await _userManager.FindByEmailAsync(model.Email);
                    if (userexist != null)
                    {
                        ModelState.AddModelError("", "Пользователь с таким адресом электронной почты уже существует!");

                        ViewData["Roles"] = _roleManager.Roles.OrderBy(p => p.Name);
                        return View(model);
                    }
                    var user = new ApplicationUser { Id = Guid.NewGuid().ToString(), UserName = model.Email, Email = model.Email };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation(3, "Пользователь создан администратором системы с паролем.");
                        foreach (var item in model.Roles)
                        {
                            await _userManager.AddToRoleAsync(user, item);
                        }
                        _logger.LogInformation("IP: {0}, Пользователь: {1}, Создал нового пользователя: {2}", Request.Host, User.Identity.IsAuthenticated ? User.Identity.Name : "Гость", model.Email);
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("IP: {0}, Пользователь: {1}, Ошибка создания нового пользователя: {2}", Request.Host, User.Identity.IsAuthenticated ? User.Identity.Name : "Гость", model.Email);
                    ModelState.AddModelError("", ex.Message);
                    return View(model);
                }
                return RedirectToAction("Index");
            }

            ViewData["Roles"] = _roleManager.Roles.OrderBy(p => p.Name);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id, string returnUrl = null)
        {
            ViewData["Title"] = "Удаление пользователя";
            if (id != null)
            {
                var model = await _userManager.FindByIdAsync(id);
                EditViewModel user = new EditViewModel() {
                    Id = model.Id,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MiddleName = model.MiddleName,
                    Roles = await _userManager.GetRolesAsync(model)
            };

                ViewData["ReturnUrl"] = returnUrl ?? "/Users";
                ViewData["Roles"] = _roleManager.Roles.OrderBy(p => p.Name);
                if (model != null)
                    return View(user);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userexistbyId = await _userManager.FindByIdAsync(model.Id);
                    var userexistbyEmail = await _userManager.FindByEmailAsync(model.Email);

                    if (userexistbyEmail != null && userexistbyId != null)
                    {
                        if ((userexistbyEmail.Id != userexistbyId.Id) && (userexistbyEmail.Email == model.Email))
                        {
                            ModelState.AddModelError("", "Пользователь с таким адресом электронной почты уже существует!");

                            ViewData["Roles"] = _roleManager.Roles.OrderBy(p => p.Name);
                            return View(model);
                        }
                    }
                    userexistbyId.Email = model.Email;
                    userexistbyId.LastName = model.LastName;
                    userexistbyId.FirstName = model.FirstName;
                    userexistbyId.MiddleName = model.MiddleName;

                    var result = await _userManager.UpdateAsync(userexistbyId);
                    if (result.Succeeded)
                    {
                        var rolesAll = _roleManager.Roles.Select(p=>p.Name).ToList<string>();    
                        foreach (var roleName in rolesAll)
                        {
                           await _userManager.RemoveFromRoleAsync(userexistbyId, roleName);
                        }

                        foreach (var item in model.Roles)
                        {
                            await _userManager.AddToRoleAsync(userexistbyId, item);                            
                        }
                        if (!string.IsNullOrEmpty(model.Password))
                        {
                            await _userManager.RemovePasswordAsync(userexistbyId);
                            var resultResetPassword = await _userManager.AddPasswordAsync(userexistbyId, model.Password);
                            _logger.LogInformation("IP: {0}, Пользователь: {1}, Сменил пароль пользователя: {2}", Request.Host, User.Identity.IsAuthenticated ? User.Identity.Name : "Гость", model.Email);
                        }                        

                        _logger.LogInformation("IP: {0}, Пользователь: {1}, Редактировал данные пользователя: {2}", Request.Host, User.Identity.IsAuthenticated ? User.Identity.Name : "Гость", model.Email);
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("IP: {0}, Пользователь: {1}, Ошибка редактирования пользователя: {2}, Ошибка: {3}", Request.Host, User.Identity.IsAuthenticated ? User.Identity.Name : "Гость", model.Email, ex.Message);
                    ModelState.AddModelError("", ex.Message);
                    return View(model);
                }
                return RedirectToAction("Index");
            }

            ViewData["Roles"] = _roleManager.Roles.OrderBy(p => p.Name);
            return View(model);
        }


        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            ViewData["Title"] = "Удаление пользователя";
            if (id != null)
            {
                var user = await _userManager.FindByIdAsync(id);

                //var roles = await _userManager.GetRolesAsync(user);
                ViewData["Roles"] = await _userManager.GetRolesAsync(user);
                if (user != null)
                    return View(user);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (id != null)
            {
                var _user = await _userManager.FindByIdAsync(id);

                if (_user != null)
                {
                    if (_user.UserName == User.Identity.Name)
                    {
                        ViewData["Error"] = "Действие по удалению данного пользователя невозможно!";
                        return View("~/Views/Account/Accessdenied");
                    }

                    try
                    {

                        await _userManager.DeleteAsync(_user);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", ex.Message);
                        return View(_user);
                    }

                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }




        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

    }
}
