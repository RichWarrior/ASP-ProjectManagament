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
    public class NotebookController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var user = await UserManager.FindByEmailAsync(User.Identity.Name);
            var dbResult = await Bll.Db_Notes().ListAsync(user.Id, "Hepsi");
            if (!String.IsNullOrEmpty(dbResult.exceptionMessage))
                return HttpNotFound();
            var result = new List<NotesViewModel>();
            foreach (var item in dbResult.resultItem)
            {
                result.Add(new NotesViewModel
                {
                    Id = item.Id,
                    User_Id = item.User_Id,
                    Title = item.Title,
                    Description = item.Description,
                    CreatedDate = item.CreatedDate
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
        public async Task<ActionResult> Create(NoteCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(User.Identity.Name);
                var request = await Bll.Db_Notes().Add(new DataAccessLayer.AdoModel.Notes
                {
                    Id = Guid.NewGuid().ToString(),
                    User_Id = user.Id,
                    Title = model.Title,
                    Description = model.Description,
                    CreatedDate = DateTime.Now
                });
                if (request.success)
                    return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Yeni Notunuz Kaydedilemedi!");
            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            var user = await UserManager.FindByEmailAsync(User.Identity.Name);
            var dbResult = await Bll.Db_Notes().FindByAsync(user.Id, id);
            if (!String.IsNullOrEmpty(dbResult.exceptionMessage) || dbResult.resultItem == null)
                return HttpNotFound();
            var result = new NoteCreateModel();
            result.Title = dbResult.resultItem.Title;
            result.Description = dbResult.resultItem.Description;
            return View(result);
        }
        [HttpPost]
        public async Task<ActionResult> Delete(string id, NoteCreateModel model)
        {
            var user = await UserManager.FindByEmailAsync(User.Identity.Name);
            var dbResult = await Bll.Db_Notes().Delete(user.Id, id);
            if (dbResult.success)
                return RedirectToAction("Index");
            ModelState.AddModelError("", "Notunuz Silinemedi!");
            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            var user = await UserManager.FindByEmailAsync(User.Identity.Name);
            var dbResult = await Bll.Db_Notes().FindByAsync(user.Id, id);
            if (!String.IsNullOrEmpty(dbResult.exceptionMessage) || dbResult.resultItem == null)
                return HttpNotFound();
            var result = new NoteCreateModel();
            result.Title = dbResult.resultItem.Title;
            result.Description = dbResult.resultItem.Description;
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, NoteCreateModel model)
        {
            var user = await UserManager.FindByEmailAsync(User.Identity.Name);
            var dbResult = await Bll.Db_Notes().Edit(user.Id, id, new DataAccessLayer.AdoModel.Notes
            {
                Title = model.Title,
                Description = model.Description
            });
            if (dbResult.success)
                return RedirectToAction("Index");
            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> Detail(string id)
        {
            var user = await UserManager.FindByEmailAsync(User.Identity.Name);
            var dbResult = await Bll.Db_Notes().FindByAsync(user.Id, id);
            if (!String.IsNullOrEmpty(dbResult.exceptionMessage) || dbResult.resultItem == null)
                return HttpNotFound();
            var result = new NoteCreateModel();
            result.Title = dbResult.resultItem.Title;
            result.Description = dbResult.resultItem.Description;
            return View(result);
        }
        [HttpGet]
        public async Task<JsonResult> Filter(string args)
        {
            var user = await UserManager.FindByEmailAsync(User.Identity.Name);
            var request = await Bll.Db_Notes().ListAsync(user.Id, args);
            var result = new List<NotesViewModel>();
            foreach (var item in request.resultItem)
            {
                result.Add(new NotesViewModel
                {
                    Id = item.Id,
                    Title = item.Title,
                    User_Id = item.User_Id,
                    Description = item.Description,
                    CreatedDateView = item.CreatedDate.Value.ToShortDateString()
                });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}