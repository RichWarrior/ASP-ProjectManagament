using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class ProjectController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var user = await UserManager.FindByEmailAsync(User.Identity.Name);
            var dbResult = await Bll.DB_Projects().ListAsync(user.Id);
            if (!String.IsNullOrEmpty(dbResult.exceptionMessage))
            {
                return HttpNotFound();
            }
            var result = new List<ProjectViewModel>();
            foreach (var item in dbResult.resultItem)
            {
                result.Add(new ProjectViewModel
                {
                    Id = item.Id,
                    Name = item.Name
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
        public async Task<ActionResult> Create(ProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(User.Identity.Name);
                var result = await Bll.DB_Projects().Add(new DataAccessLayer.AdoModel.Projects
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = model.Name,
                    User_Id = user.Id
                });
                if (result.success)
                    return RedirectToAction("Index","Project");                
            }
            ModelState.AddModelError("", "Proje Oluşturulamadı!");
            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            var user = await UserManager.FindByEmailAsync(User.Identity.Name);
            var dbResult = await Bll.DB_Projects().FindByIdAsync(id,user.Id);
            if (!String.IsNullOrEmpty(dbResult.exceptionMessage) || dbResult.resultItem == null)
                return HttpNotFound();
            var result = new ProjectViewModel();
            result.Name = dbResult.resultItem.Name;
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id,ProjectViewModel model)
        {
            var user = await UserManager.FindByEmailAsync(User.Identity.Name);
            var dbResult = await Bll.DB_Projects().Delete(id, user.Id);
            if (dbResult.success)
                return RedirectToAction("Index");
            ModelState.AddModelError("","Proje Silinemedi!");
            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            var user = await UserManager.FindByEmailAsync(User.Identity.Name);
            var dbResult = await Bll.DB_Projects().FindByIdAsync(id,user.Id);
            if (!String.IsNullOrEmpty(dbResult.exceptionMessage) || dbResult.resultItem == null)
                return HttpNotFound();
            var result = new ProjectViewModel();
            result.Name = dbResult.resultItem.Name;
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult>Edit(string id,ProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(User.Identity.Name);
                var response = await Bll.DB_Projects().Edit(id,user.Id,new DataAccessLayer.AdoModel.Projects {
                    Name = model.Name
                });
                if (response.success)
                    return RedirectToAction("Index");
            }
            ModelState.AddModelError("","Proje Düzenlenemedi!");
            return View(model);
        }
    }
}