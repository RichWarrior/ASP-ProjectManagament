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
    public class MenuController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var result = new List<MenuViewModel>();
            var dbModel = await Bll.DB_Menu().ListMainMenuAsync();
            if (!String.IsNullOrEmpty(dbModel.exceptionMessage))
            {
                ModelState.AddModelError("", "Bir Hata Oluştu!");
            }
            foreach (var item in dbModel.resultItem)
            {
                result.Add(new MenuViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Controller = item.Controller,
                    Action = item.Action,
                    Icon = item.Icon,
                    Nested = item.Nested,
                    Rank = item.Rank
                });
            }
            return View(result);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MenuViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dbResult = await Bll.DB_Menu().Add(new DataAccessLayer.AdoModel.Menu
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = model.Name,
                    Icon = model.Icon,
                    Controller = model.Controller,
                    Action = model.Action,
                    Nested = "0",
                    Rank = model.Rank
                });
                if (dbResult.success)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError("", "Yeni Menü Oluşturulamadı!");
            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            var dbModel = await Bll.DB_Menu().FindByIdAsync(id);
            if (dbModel.resultItem != null)
            {
                var result = new MenuViewModel();
                result.Id = id;
                result.Name = dbModel.resultItem.Name;
                result.Action = dbModel.resultItem.Action;
                result.Controller = dbModel.resultItem.Controller;
                return View(result);
            }
            else
            {
                return HttpNotFound();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, MenuViewModel model)
        {
            var dbModel = await Bll.DB_Menu().Delete(id);
            if (dbModel.success)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Menü Silinemedi");
            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            var dbResult = await Bll.DB_Menu().FindByIdAsync(id);
            if (dbResult.resultItem != null)
            {
                var result = new MenuViewModel();
                result.Name = dbResult.resultItem.Name;
                result.Controller = dbResult.resultItem.Controller;
                result.Action = dbResult.resultItem.Action;
                result.Icon = dbResult.resultItem.Icon;
                result.Rank = dbResult.resultItem.Rank;
                return View(result);
            }
            else
            {
                return HttpNotFound();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, MenuViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dbModel = await Bll.DB_Menu().Edit(id, new DataAccessLayer.AdoModel.Menu
                {
                    Id = id,
                    Action = model.Action,
                    Controller = model.Controller,
                    Icon = model.Icon,
                    Name = model.Name,
                    Rank = model.Rank
                });
                if (dbModel.success)
                    return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Menü Düzenlenemedi!");
            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> SubMenu(string id)
        {
            var dbResult = await Bll.DB_Menu().ListSubMenuAsync(id);
            if (!String.IsNullOrEmpty(dbResult.exceptionMessage))
            {
                return HttpNotFound();
            }
            else
            {
                var result = new List<MenuViewModel>();
                foreach (var item in dbResult.resultItem)
                {
                    result.Add(new MenuViewModel
                    {
                        Id = item.Id,
                        Action = item.Action,
                        Controller = item.Controller,
                        Icon = item.Icon,
                        Name = item.Name,
                        Nested = item.Nested,
                       Rank = item.Rank 
                    });
                }
                ViewBag.Id = id;
                return View(result);
            }
        }
        [HttpGet]
        public async Task<ActionResult> CreateSubMenu(string id)
        {
            var dbResult = await Bll.DB_Menu().ListMainMenuAsync();
            if (!String.IsNullOrEmpty(dbResult.exceptionMessage))
            {
                return HttpNotFound();
            }
            else
            {
                var result = new SubMenuCreateModel();
                foreach (var item in dbResult.resultItem.Where(x => x.Nested == "0").ToList())
                {
                    result.mainMenu.Add(new MenuViewModel
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
        public async Task<ActionResult> CreateSubMenu(string id, MenuViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dbModel = await Bll.DB_Menu().Add(new DataAccessLayer.AdoModel.Menu
                {
                    Id = Guid.NewGuid().ToString(),
                    Action = model.Action,
                    Controller = model.Controller,
                    Icon = model.Icon,
                    Name = model.Name,
                    Nested = model.Nested,
                    Rank = model.Rank
                });
                if(dbModel.success)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError("", "Yeni Alt Menü Oluşturulamadı!");
            var dbResult = await Bll.DB_Menu().ListMainMenuAsync();
            var result = new SubMenuCreateModel();
            foreach (var item in dbResult.resultItem.Where(x => x.Nested == "0").ToList())
            {
                result.mainMenu.Add(new MenuViewModel
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            return View(result);
        }
    }
}