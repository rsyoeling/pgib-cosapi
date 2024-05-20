using COSAPI.NETC.PGIB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace COSAPI.NETC.PGIB.Controllers
{
    public class LogInViewModel
    {
        [Required]
        [Display(Name = "Usuario")]
        public string Usuario { get; set; }
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "CodeCR")]
        public string CodeCR { get; set; }
        [Display(Name = "Moneda")]
        public string Moneda { get; set; }
        [Display(Name = "NombreCR")]
        public string NombreCR { get; set; }
    }
    public class UserDbModel
    {
        public string Usuario { get; set; }
        public string Nombre { get; set; }
        public string CodigoCR { get; set; }
        public string UserPermissionType { get; set; }
        public string UserPassword { get; set; }
        public string NombreCR { get; set; }
        public string Moneda { get; set; }

    }
    public class AccountController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public ActionResult LogOn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogOn(LogInViewModel form) 
        {
            //authenticate
            var user = new UserDbModel()
            {
                Usuario = form.Usuario, //.ToLower(),
                UserPassword = form.Password
            };

            if (!string.IsNullOrEmpty(user.Usuario) && !string.IsNullOrEmpty(user.UserPassword))
            {
                string rpta = AccountServices.Usuario_Login_Sel(user.Usuario, user.UserPassword);
                Models.ObjectResult accountStarted = JsonConvert.DeserializeObject<Models.ObjectResult>(rpta);
                if (accountStarted.content.Count > 0) {

                    _httpContextAccessor.HttpContext.Session.SetString("oUsuario", JsonConvert.SerializeObject(accountStarted.content));
                    var oUsuario = _httpContextAccessor.HttpContext.Session.GetString("oUsuario");
                    //var oUsuarioDeserializado = JsonConvert.DeserializeObject <List<accountStarted>>(oUsuario);
                    
                    return RedirectToAction("Index", "Proyectos", null);
                }
                else
                {
                    return View(form);
                }
            }
            else {
                return View(form);
            }
        }

        public async Task<ActionResult> LogOut()
        {
            //await _userManager.SignOut(this.HttpContext);
            //await httpContext.SignOutAsync("Cookies");
            return RedirectToAction("LogOn", "Account");
        }

        //public ActionResult Cargar_Menu_SubMenu_Por_Rol()
        //{
        //    string menuSubmenu = AccountServices.Cargar_Menu_SubMenu_Por_Rol(1);
        //    Models.ObjectResultMS Model = JsonConvert.DeserializeObject<Models.ObjectResultMS>(menuSubmenu);
        //    return PartialView("_PartialPageMenu", Model);
        //}
        // GET: AccountController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AccountController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: AccountController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: AccountController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: AccountController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        // POST: AccountController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: AccountController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: AccountController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
