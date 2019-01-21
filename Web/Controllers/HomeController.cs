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
    public class HomeController : BaseController
    {
        [OutputCache(Duration = 120)]
        public async Task<ActionResult> Index()
        {
            var user = await UserManager.FindByEmailAsync(User.Identity.Name);
            var roles = await UserManager.GetRolesAsync(user.Id);
            if(roles.FirstOrDefault()=="Kullanıcı")
            {
                return RedirectToAction("Index","Works");
            }
            var request = await Bll.DB_Statistics().ListAsync();
            var result = new AdminDashModel();
            result.ACTION_COUNT = request.resultItem.ACTION_COUNT;
            result.NOTE_COUNT = request.resultItem.NOTE_COUNT;
            result.PARAMETER_COUNT = request.resultItem.PARAMETER_COUNT;
            result.PROJECT_COUNT = request.resultItem.PROJECT_COUNT;
            result.USER_COUNT = request.resultItem.USER_COUNT;
            result.WORKDEF_COUNT = request.resultItem.WORKDEF_COUNT;
            return View(result);
        }   
        [AllowAnonymous]
        public ActionResult Error()
        {
            return View();
        }
    }
}