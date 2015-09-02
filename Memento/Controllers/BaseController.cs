using System;
using System.Web;
using System.Web.Mvc;
using Memento.App_Start;
using Memento.Models;
using ServiceLayer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Memento.Controllers
{
	public class BaseController : Controller
	{
		private ApplicationSignInManager _signInManager;
		private ApplicationUserManager _userManager;
		private ApplicationUser _currentUser;

		public BaseController(IDataService dataService)
		{
			if (dataService == null)
			{
				throw new ArgumentNullException("Data service object can't be null.");
			}
			DataService = dataService;
		}

		public BaseController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IDataService dataService)
		{
			if (dataService == null)
			{
				throw new ArgumentNullException("Data service object can't be null.");
			}
			UserManager = userManager;
			SignInManager = signInManager;
			DataService = dataService;
		}

		protected IDataService DataService { get; private set; }

		public ApplicationUser CurrentUser
		{
			get
			{
				return _currentUser ?? (_currentUser = UserManager.FindById(User.Identity.GetUserId()));
			}
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
			}
			base.Dispose(disposing);
		}
	}
}