using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    /// <summary>
    /// Grant-Type : Admin
    /// Parametre Yönetimi
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class ServerController : BaseController
    {
        /// <summary>
        /// Parametre Listesi
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var result = new ResultItemList<ServerInfoViewModel>();
            var dbModel = await Bll.DB_ServerInfo().ListAsync("10");
            result.exceptionMessage = dbModel.exceptionMessage;
            foreach (var item in dbModel.resultItem)
            {
                result.data.Add(new ServerInfoViewModel
                {
                    Id = item.Id,
                    key_str = item.key_str,
                    value_str = item.value_str
                });
            }
            if (!String.IsNullOrEmpty(result.exceptionMessage))
            {
                ModelState.AddModelError("", "Veriler Sistemden Getirilemedi");
            }
            return View(result);
        }
        /// <summary>
        /// Parametreleri Sayıya Göre Filtreleme
        /// </summary>
        /// <param name="args">Veritabanından İstenilen Değer Sayısı</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult> Filter(string args)
        {
            var result = new ResultItemList<ServerInfoViewModel>();
            var dbModel = await Bll.DB_ServerInfo().ListAsync(args);
            result.exceptionMessage = dbModel.exceptionMessage;
            foreach (var item in dbModel.resultItem)
            {
                result.data.Add(new ServerInfoViewModel
                {
                    Id = item.Id,
                    key_str = item.key_str,
                    value_str = item.value_str
                });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Parametre Silme İşlemi
        /// </summary>
        /// <param name="id"></param>
        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            var dbModel = await Bll.DB_ServerInfo().FindByIdAsync(id);
            if (!String.IsNullOrEmpty(dbModel.exceptionMessage) || dbModel.resultItem == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (dbModel.resultItem != null)
                {
                    var result = new ServerInfoViewModel();
                    result.Id = id;
                    result.key_str = dbModel.resultItem.key_str;
                    result.value_str = dbModel.resultItem.value_str;
                    return View(result);
                }
            }
            return HttpNotFound();
        }
        /// <summary>
        /// Parametre Silme İşlemi
        /// </summary>
        /// <param name="id">Parametre Id</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, ServerInfoViewModel model)
        {
            var dbModel = await Bll.DB_ServerInfo().Delete(id);
            if (dbModel.success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Parametre Silinemedi");
                return View(model);
            }
        }
        /// <summary>
        /// Parametre Ekleme
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// Parametre Ekleme
        /// </summary>
        /// <param name="model">Server Info Model</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ServerInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dbModel = await Bll.DB_ServerInfo().Add(new DataAccessLayer.AdoModel.ServerInfo
                {
                    Id = Guid.NewGuid().ToString(),
                    key_str = model.key_str,
                    value_str = model.value_str
                });
                if (dbModel.success)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError("", "Parametre Eklenemedi!");
            return View(model);
        }
        /// <summary>
        /// Parametre Düzenleme
        /// </summary>
        /// <param name="id">Düzenlenecek Parametre Id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            var dbModel = await Bll.DB_ServerInfo().FindByIdAsync(id);
            if (!String.IsNullOrEmpty(dbModel.exceptionMessage) || dbModel.resultItem == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (dbModel.resultItem != null)
                {
                    var result = new ServerInfoViewModel();
                    result.Id = id;
                    result.key_str = dbModel.resultItem.key_str;
                    result.value_str = dbModel.resultItem.value_str;
                    return View(result);
                }
                return HttpNotFound();
            }
        }
        /// <summary>
        /// Parametre Düzenleme
        /// </summary>
        /// <param name="id">Düzenlenecek Parametre Id</param>
        /// <param name="model">Yeni Değer</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, ServerInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dbResult = await Bll.DB_ServerInfo().Edit(id, new DataAccessLayer.AdoModel.ServerInfo
                {
                    Id = id,
                    key_str = model.key_str,
                    value_str = model.value_str
                });
                if (dbResult.success)
                    return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Parametre Düzenlenemedi!");
            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> UpdateVersion()
        {
            var success = await Bll.DB_ServerInfo().UpdateVersion();
            if (success.success)
            {
                return RedirectToAction("Index");
            }
            return HttpNotFound();
        }
    }
}