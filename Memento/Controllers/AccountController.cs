using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Memento.Models;
using ServiceLayer;
using Entities;
using Memento.Environment.DataManagement;

namespace Memento.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
		public AccountController(IDataService dataService) : base(dataService) {}

        public AccountController(
			ApplicationUserManager userManager, 
			ApplicationSignInManager signInManager, 
			IDataService dataService
		) : base(userManager, signInManager, dataService)
        {
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Account/Login
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			// This doesn't count login failures towards account lockout
			// To enable password failures to trigger account lockout, change to shouldLockout: true
			var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
			switch (result)
			{
				case SignInStatus.Success:
					return RedirectToLocal(returnUrl);

				case SignInStatus.Failure:
				default:
					ModelState.AddModelError("", "Invalid login attempt.");
					return View(model);
			}
		}

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
				string userId = Guid.NewGuid().ToString();
                
				var user = new ApplicationUser {
					Id = userId,
 					FirstName = model.FirstName,
					LastName = model.LastName,
					Login = model.Login,
					UserName = model.Email, 
					Email = model.Email,
					Job = String.Empty,
					PhotoUrl = "~/Content/Images/default-avatar.png",
					SmallPhotoUrl = "~/Content/Images/default-avatar.png",
					CreationDate = DateTime.Now
				};

                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
					DataService.NewUser(new User() 
					{ 
						Id = userId,
						FirstName = model.FirstName,
						LastName = model.LastName,
						Email = model.Email
					});
					HttpContext.CreateUserDataDirectory(user.UserName);
                    await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);
                    
                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

		private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
		}
		#endregion
	}
}