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
    public class WorksController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var user = await UserManager.FindByEmailAsync(User.Identity.Name);
            var dbResult = await Bll.DB_WorkDefinition().ListAsync(user.Id, "10");
            if (!String.IsNullOrEmpty(dbResult.exceptionMessage))
                return HttpNotFound();
            var dbProjects = await Bll.DB_Projects().ListAsync(user.Id);
            if (!String.IsNullOrEmpty(dbProjects.exceptionMessage))
                return HttpNotFound();
            var result = new WorkDefinitionViewModel();
            foreach (var item in dbResult.resultItem)
            {
                result.works.Add(new WorkDefinitionModel
                {
                    Id = item.Id,
                    Title = item.Title,
                    ProjectName = item.Projects.Name,
                    ExpireDate = item.ExpireDate,
                    completed = item.IsCompleted,
                    rank = item.Rank
                });
            }
            foreach (var item in dbProjects.resultItem)
            {
                result.projects.Add(new ProjectViewModel
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            return View(result);
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var user = await UserManager.FindByEmailAsync(User.Identity.Name);
            var dbResult = await Bll.DB_Projects().ListAsync(user.Id);
            if (!String.IsNullOrEmpty(dbResult.exceptionMessage))
                return HttpNotFound();
            var result = new WorkDefinitionCreateModel();
            foreach (var item in dbResult.resultItem)
            {
                result.projects.Add(new ProjectViewModel
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(WorkDefinitionCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var _user = await UserManager.FindByEmailAsync(User.Identity.Name);
                //Tarih Formatı Gün/Ay/Yıl Olmalı Gelen Format İse Ay/Gün/Yıl
                //Tarih Formatlamayı Unutma
                var piece = model.ExpireDate.Split('/');
                var date = (piece[1].ToString() + "." + piece[0].ToString() + "." + piece[2].ToString()).Replace("/", "");
                var _result = await Bll.DB_WorkDefinition().Add(new DataAccessLayer.AdoModel.WorkDefinition
                {
                    Id = Guid.NewGuid().ToString(),
                    User_Id = _user.Id,
                    Title = model.Title,
                    CreatedDate = DateTime.Now,
                    Description = model.Description,
                    IsCompleted = false,
                    Project_Id = model.ProjectId,
                    ExpireDate = Convert.ToDateTime(date),
                    Rank = model.rank
                });
                if (_result.success)
                    return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "İş Tanımı Oluşturulamadı!");
            var user = await UserManager.FindByEmailAsync(User.Identity.Name);
            var dbResult = await Bll.DB_Projects().ListAsync(user.Id);
            if (!String.IsNullOrEmpty(dbResult.exceptionMessage))
                return HttpNotFound();
            var result = new WorkDefinitionCreateModel();
            foreach (var item in dbResult.resultItem)
            {
                result.projects.Add(new ProjectViewModel
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            return View(result);
        }
        [HttpGet]
        public async Task<ActionResult> Detail(string id)
        {
            var user = await UserManager.FindByEmailAsync(User.Identity.Name);
            var db = await Bll.DB_WorkDefinition().FindByIdAsync(user.Id, id);
            if (!String.IsNullOrEmpty(db.exceptionMessage) || db.resultItem == null)
                return HttpNotFound();
            var result = new WorkDefinitionCreateModel();
            result.ProjectName = db.resultItem.Projects.Name;
            result.Title = db.resultItem.Title;
            result.Description = db.resultItem.Description;
            result.ExpireDate = db.resultItem.ExpireDate.ToShortDateString();
            return View(result);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            var user = await UserManager.FindByEmailAsync(User.Identity.Name);
            var db = await Bll.DB_WorkDefinition().FindByIdAsync(user.Id, id);
            if (!String.IsNullOrEmpty(db.exceptionMessage) || db.resultItem == null)
                return HttpNotFound();
            var projects = await Bll.DB_Projects().ListAsync(user.Id);
            if (!String.IsNullOrEmpty(projects.exceptionMessage))
                return HttpNotFound();
            var result = new WorkDefinitionCreateModel();
            foreach (var item in projects.resultItem)
            {
                result.projects.Add(new ProjectViewModel
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            result.Title = db.resultItem.Title;
            result.Description = db.resultItem.Description;
            result.ProjectId = db.resultItem.Project_Id;
            result.ExpireDate = db.resultItem.ExpireDate.ToShortDateString();
            result.completed = db.resultItem.IsCompleted;
            result.rank = db.resultItem.Rank;
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, WorkDefinitionCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var _user = await UserManager.FindByEmailAsync(User.Identity.Name);
                string _date = "";
                if (model.ExpireDate.Contains("/"))
                {
                    var _piece = model.ExpireDate.Split('/');
                    _date = (_piece[1].ToString() + "." + _piece[0].ToString() + "." + _piece[2].ToString()).Replace("/", "");
                }
                else
                {
                    _date = model.ExpireDate;
                }
                var request = await Bll.DB_WorkDefinition().Edit(id, _user.Id, new DataAccessLayer.AdoModel.WorkDefinition
                {
                    Title = model.Title,
                    Description = model.Description,
                    ExpireDate = Convert.ToDateTime(_date),
                    Project_Id = model.ProjectId,
                    IsCompleted = model.completed == null ? false : true,
                    Rank = model.rank
                });
                if (request.success)
                    return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "İş Tanımı Güncellenemedi!");
            var user = await UserManager.FindByEmailAsync(User.Identity.Name);
            var db = await Bll.DB_WorkDefinition().FindByIdAsync(user.Id, id);
            if (!String.IsNullOrEmpty(db.exceptionMessage) || db.resultItem == null)
                return HttpNotFound();
            var projects = await Bll.DB_Projects().ListAsync(user.Id);
            if (!String.IsNullOrEmpty(projects.exceptionMessage))
                return HttpNotFound();
            var result = new WorkDefinitionCreateModel();
            foreach (var item in projects.resultItem)
            {
                result.projects.Add(new ProjectViewModel
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            result.Title = db.resultItem.Title;
            result.Description = db.resultItem.Description;
            result.ProjectId = db.resultItem.Project_Id;
            result.ExpireDate = db.resultItem.ExpireDate.ToShortDateString();
            result.completed = db.resultItem.IsCompleted;
            return View(result);
        }
        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            var user = await UserManager.FindByEmailAsync(User.Identity.Name);
            var db = await Bll.DB_WorkDefinition().FindByIdAsync(user.Id, id);
            if (!String.IsNullOrEmpty(db.exceptionMessage) || db.resultItem == null)
                return HttpNotFound();
            var result = new WorkDefinitionCreateModel();
            result.Title = db.resultItem.Title;
            result.Description = db.resultItem.Description;
            return View(result);
        }
        [HttpPost]
        public async Task<ActionResult> Delete(string id, WorkDefinitionViewModel model)
        {
            var user = await UserManager.FindByEmailAsync(User.Identity.Name);
            var request = await Bll.DB_WorkDefinition().Delete(id, user.Id);
            if (request.success)
                return RedirectToAction("Index");
            ModelState.AddModelError("", "İş Tanımı Silinemedi!");
            return View(model);
        }
        [HttpGet]
        public async Task<JsonResult> Filter(string project_id, string count)
        {
            var user = await UserManager.FindByEmailAsync(User.Identity.Name);
            var request = await Bll.DB_WorkDefinition().ListAsync(user.Id, project_id, count);
            var result = new List<WorkDefinitionModel>();
            foreach (var item in request.resultItem)
            {
                result.Add(new WorkDefinitionModel
                {
                    Id = item.Id,
                    Title = item.Title,
                    completed = item.IsCompleted,
                    ExpireDateView = item.ExpireDate.ToShortDateString(),
                    ProjectName = item.Projects.Name,
                    rank = item.Rank
                });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}