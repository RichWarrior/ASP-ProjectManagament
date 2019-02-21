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
    /// <summary>
    /// Arkaplanda Ortak Js İşlemlerini Yönetir.
    /// </summary>
    public class RequestController : BaseController
    {
        /// <summary>
        /// Kullanıcı Yetkilerine Göre Menüleri Json Formatında Geriye Döndürür.
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration =15)]
        public async Task<JsonResult> GetMenuByRoleId()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await UserManager.FindByEmailAsync(User.Identity.Name);
                var userRoles = await UserManager.GetRolesAsync(user.Id);
                var roleId = await _roleManager.FindByNameAsync(userRoles.FirstOrDefault().ToString());
                var dbModel = await Bll.DB_Menu().ListByAsync(roleId.Id);
                var result = new ResultItemList<MenuViewModel>();
                result.exceptionMessage = dbModel.exceptionMessage;
                foreach (var item in dbModel.resultItem)
                {
                    result.data.Add(new MenuViewModel {
                        Id = item.Id,
                        Name = item.Name,
                        Icon = item.Icon,
                        Action = item.Action,
                        Controller = item.Controller,
                        Nested = item.Nested,
                        Rank = item.Rank
                    });
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }        
    }
}