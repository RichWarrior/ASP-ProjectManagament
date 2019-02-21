using DataAccessLayer;
using log4net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class BaseController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public RoleManager<IdentityRole> _roleManager;
        public BLL Bll;
        public static readonly ILog Log =
              LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public BaseController()
        {
            _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            Bll = new BLL();
        }

        public BaseController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //public void ClearCache()
        //{
        //    try
        //    {
        //        List<string> keys = new List<string>();

        //        IDictionaryEnumerator enumerator = new Cache().GetEnumerator();

        //        while (enumerator.MoveNext())
        //        {
        //            keys.Add(enumerator.Key.ToString());
        //        }
        //        for (int i = 0; i < keys.Count; i++)
        //        {
        //            var _cache = new Cache();
        //            _cache.Remove(keys[i]);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex);                
        //    }
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
                if (_roleManager != null)
                {
                    _roleManager.Dispose();
                    _roleManager = null;
                }
                if (Bll != null)
                {
                    Bll.Dispose();
                    Bll = null;
                }
            }
            base.Dispose(disposing);
        }
    }
}