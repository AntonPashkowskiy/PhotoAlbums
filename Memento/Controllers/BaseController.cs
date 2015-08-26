using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memento.Models;
using ServiceLayer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Memento.Controllers
{
	public class BaseController : Controller
	{
		private IDataService _dataService;
		private ApplicationSignInManager _signInManager;
		private ApplicationUserManager _userManager;
		private ApplicationUser _currentUser;

		public BaseController(IDataService dataService)
		{
			if (dataService == null)
			{
				throw new ArgumentNullException("Data service object can't be null.");
			}
			_dataService = dataService;
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

		protected IDataService DataService 
		{ 
			get
			{
				return _dataService;
			}
			private set
			{
				_dataService = value;
			}
		}

		public ApplicationUser CurrentUser
		{
			get
			{
				if (_currentUser == null)
				{
					_currentUser = UserManager.FindById(User.Identity.GetUserId());
				}
				return _currentUser;
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