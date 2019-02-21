using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Attiributes;
using Web.Models;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProtectionController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var dbResult = await Bll.DB_AspNetRoles().ListByRoleWithMenuAsync();
            if (!String.IsNullOrEmpty(dbResult.exceptionMessage))
            {
                return HttpNotFound();
            }
            var result = new List<RoleViewModel>();
            var roles = dbResult.resultItem.Select(x => x.AspNetRoles).Distinct().ToList();
            foreach (var child in roles)
            {
                result.Add(new RoleViewModel
                {
                    Id = child.Id,
                    Name = child.Name,
                    MenuCount = dbResult.resultItem.Where(x => x.Role_Id == child.Id).ToList().Count
                });
            }
            return View(result);
        }
        [HttpGet]
        public async Task<ActionResult> Point(string id)
        {
            var dbModel = await Bll.DB_Menu().ListByAsync(id);
            if (!String.IsNullOrEmpty(dbModel.exceptionMessage))
            {
                return HttpNotFound();
            }
            var result = new List<MenuViewModel>();
            foreach (var item in dbModel.resultItem)
            {
                result.Add(new MenuViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Controller = item.Controller,
                    Action = item.Action,
                    Icon = item.Icon
                });
            }
            ViewBag.Id = id;
            return View(result);
        }
        [HttpGet]
        public async Task<ActionResult> Delete(string id, string locationId)
        {
            var db = await Bll.DB_Menu().FindByIdAsync(locationId);
            if (!String.IsNullOrEmpty(db.exceptionMessage))
            {
                return HttpNotFound();
            }
            if (db.resultItem != null)
            {
                var result = new MenuViewModel();
                result.Id = db.resultItem.Id;
                result.Name = db.resultItem.Name;
                return View(result);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public async Task<ActionResult> Delete(string id,string locationId,MenuViewModel model)
        {
            var request = await Bll.DB_Menu().DeletePoint(id, locationId);
            if (request.success)
            {
                //this.ClearCache();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("","Erişim Noktası Silinemedi");
            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var roles = await Bll.DB_AspNetRoles().ListAsync();
            if (!String.IsNullOrEmpty(roles.exceptionMessage))
            {
                return HttpNotFound();
            }
            var menus = await Bll.DB_Menu().ListMainMenuAsync();
            if (!String.IsNullOrEmpty(menus.exceptionMessage))
            {
                return HttpNotFound();
            }
            var result = new ProtectionViewModel();
            foreach (var item in roles.resultItem)
            {
                result.roles.Add(new RoleViewModel {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            foreach (var item in menus.resultItem)
            {
                result.menu.Add(new MenuViewModel {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProtectionCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var request = await Bll.DB_Menu().AddPoint(new DataAccessLayer.AdoModel.RoleMenu
                {
                    Id = Guid.NewGuid().ToString(),
                    Menu_Id = model.menu_id,
                    Role_Id = model.role_id
                });
                if (request.success)
                {
                    //this.ClearCache();
                    return RedirectToAction("Index");
                }
               
            }
            var roles = await Bll.DB_AspNetRoles().ListAsync();
            var menus = await Bll.DB_Menu().ListMainMenuAsync();
            var result = new ProtectionViewModel();
            foreach (var item in roles.resultItem)
            {
                result.roles.Add(new RoleViewModel
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            foreach (var item in menus.resultItem)
            {
                result.menu.Add(new MenuViewModel
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            return View(result);
        }
    }
}