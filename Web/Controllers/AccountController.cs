using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Web.Models;

namespace Web.Controllers
{
    /// <summary>
    /// Kullanıcı Oturum İşlmelerinden Sorumludur.
    /// </summary>
    [Authorize]
    public class AccountController : BaseController
    {
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    var user = await UserManager.FindByEmailAsync(model.Email);
                    if (user != null)
                    {
                        var isAuthenticated = UserManager.PasswordHasher.VerifyHashedPassword(user.PasswordHash, model.Password);
                        if (isAuthenticated.HasFlag(PasswordVerificationResult.Success))
                        {
                            await SignInManager.SignInAsync(user, true, true);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Lütfen Şifrenizi Doğru Giriniz!");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Böyle Bir Kullanıcı Bulunamadı!");
                    }
                }
            }
            return View();
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var password = Guid.NewGuid().ToString();
                    string hash = "";
                    for (int i = 0; i < 7; i++)
                    {
                        hash += password[i].ToString();
                    }
                    user.PasswordHash = UserManager.PasswordHasher.HashPassword(hash);
                    var request = await UserManager.UpdateAsync(user);
                    if (request.Succeeded)
                    {
                        var a = await Bll.DB_Action().Add(new DataAccessLayer.AdoModel.Action
                        {
                            User_Id = user.Id,
                            Message = hash,
                            Completed = false
                        });
                        if (a.success)
                            ModelState.AddModelError("", "Şifreniz Güncellendi! Size Mail Olarak Gönderilecektir.");
                        else
                            ModelState.AddModelError("", "Şifreniz Güncellendi! Fakat Mail Gönderilemiyor. Yönetici İle İletişime Geçiniz!");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Kullanıcı Bulunamadı!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı Bulunamadı!");
                }
            }
            else
            {
                ModelState.AddModelError("", "Şifreniz Değiştirilemedi!");
            }
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var who = await UserManager.FindByEmailAsync(model.email);
                if (who != null)
                {
                    ModelState.AddModelError("", "Böyle Bir Kullanıcı Zaten Mevcut!");
                }
                else
                {
                    ApplicationUser u = new ApplicationUser();
                    u.Email = model.email;
                    u.UserName = u.Email;
                    var registered = await UserManager.CreateAsync(u, model.password);
                    if (registered.Succeeded)
                    {
                        var registeredUser = await UserManager.FindByEmailAsync(model.email);
                        var addToRole = await UserManager.AddToRoleAsync(registeredUser.Id, "Kullanıcı");
                        if (addToRole.Succeeded)
                        {
                            ModelState.AddModelError("", "Kullanıcı Başarıyla Kayıt Edildi!");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Kullanıcı Oluşturuldu Fakat Yetkilendirme Esnasında Hata Oluştu Yöneticilerle İletişime Geçiniz!");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Kullanıcı Oluşturulamadı!");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Kullanıcı Oluşturulamadı!");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Profile()
        {
            var user = await UserManager.FindByEmailAsync(User.Identity.Name);
            var result = new LoginViewModel();
            result.Email = user.Email;
            return View(result);
        }

        [HttpPost]
        public async Task<ActionResult> Profile(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var u = await UserManager.FindByEmailAsync(User.Identity.Name);
                u.PasswordHash = UserManager.PasswordHasher.HashPassword(model.Password);
                var request = await UserManager.UpdateAsync(u);
                if (request.Succeeded)
                    ModelState.AddModelError("", "Şifreniz Başarıyla Değiştirildi!");
                else
                    ModelState.AddModelError("", "Şifreniz Değiştirilemedi!");
            }
            else
            {
                ModelState.AddModelError("", "Şifreniz Değiştirilemedi!");
            }
            var user = await UserManager.FindByEmailAsync(User.Identity.Name);
            var result = new LoginViewModel();
            result.Email = user.Email;
            return View(result);
        }

        // GET: /Account/LogOff
        [HttpGet]
        public ActionResult LogOff()
        {
            var AuthenticationManager = HttpContext.GetOwinContext().Authentication;
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}