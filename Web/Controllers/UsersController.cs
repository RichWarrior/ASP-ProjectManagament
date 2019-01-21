using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var dbResult = await Bll.DB_AspNetUsers().ListAsync("10");
            if (!String.IsNullOrEmpty(dbResult.exceptionMessage))
            {
                return HttpNotFound();
            }
            else
            {
                var result = new List<UsersViewModel>();
                foreach (var item in dbResult.resultItem)
                {
                    result.Add(new UsersViewModel
                    {
                        Id = item.Id,
                        email = item.Email,
                        role = item.AspNetRoles.FirstOrDefault().Name
                    });
                }
                return View(result);
            }
        }
        [HttpGet]
        public async Task<JsonResult> Filter(string args)
        {
            var dbResult = await Bll.DB_AspNetUsers().ListAsync(args);
            var result = new List<UsersViewModel>();
            foreach (var item in dbResult.resultItem)
            {
                result.Add(new UsersViewModel
                {
                    Id = item.Id,
                    email = item.Email,
                    role = item.AspNetRoles.FirstOrDefault().Name
                });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var dbModel = await Bll.DB_AspNetRoles().ListAsync();
            if (!String.IsNullOrEmpty(dbModel.exceptionMessage))
            {
                return HttpNotFound();
            }
            else
            {
                var result = new UsersCreateModel();
                foreach (var item in dbModel.resultItem)
                {
                    result.roles.Add(new RoleViewModel
                    {
                        Id = item.Id,
                        Name = item.Name
                    });
                }
                return View(result);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UsersCreateModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser _user = new ApplicationUser();
                _user.Email = model.email;
                _user.UserName = _user.Email;
                var _guid = Guid.NewGuid().ToString();
                string password = "";
                for (int i = 0; i < 7; i++)
                {
                    password += _guid[i].ToString();
                }
                var userCreated = await UserManager.CreateAsync(_user, password);
                if (userCreated.Succeeded)
                {
                    var role = await _roleManager.FindByIdAsync(model.role_id);
                    var createUser = await UserManager.FindByEmailAsync(model.email);
                    var addToRole = await UserManager.AddToRoleAsync(createUser.Id, role.Name);
                    if (addToRole.Succeeded)
                    {
                        var action = await Bll.DB_Action().Add(new DataAccessLayer.AdoModel.Action
                        {
                            User_Id = createUser.Id,
                            Message = password,
                            Completed = false
                        });
                        if (action.success)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Kullanıcı Şifresi Gönderilmek Üzere Hazırlanırken Bir Hata Oluştu!");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Kullanıcı Oluşturuldu Fakat Yetkilendirilirken Hataya Rastlandı!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı Oluşturulamadı!");
                }

            }
            else
            {
                ModelState.AddModelError("", "Kullanıcı Oluşturulamadı!");
            }
            var dbModel = await Bll.DB_AspNetRoles().ListAsync();
            var result = new UsersCreateModel();
            foreach (var item in dbModel.resultItem)
            {
                result.roles.Add(new RoleViewModel
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            return View(result);
        }
        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            var dbResult = await Bll.DB_AspNetUsers().FindByIdAsync(id);
            if (!String.IsNullOrEmpty(dbResult.exceptionMessage))
            {
                return HttpNotFound();
            }
            if (dbResult.resultItem != null)
            {
                var result = new UsersViewModel();
                result.email = dbResult.resultItem.Email;
                result.role = dbResult.resultItem.AspNetRoles.FirstOrDefault().Name;
                return View(result);
            }
            return HttpNotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id,UsersViewModel model)
        {
            var user = await UserManager.FindByIdAsync(id);
            var deleted = await UserManager.DeleteAsync(user);
            if (deleted.Succeeded)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("","Kullanıcı Silinemedi");
            return View(model);
        }
    }
}