using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Entities;
using Memento.Models;
using ServiceLayer;

namespace Memento.Controllers
{
	public class HomeController : BaseController
	{
		public HomeController(IDataService dataService) : base(dataService) {}

		// GET: /Home/Index
		public ActionResult Index()
		{
			ViewBag.CurrentUser = CurrentUser;

			if (Request.IsAuthenticated)
			{
				return View("HomePage");
			}
			return View("StartPage");
		}
		
		// GET: /Home/PersonalRoom
		[HttpGet]
		public ActionResult PersonalRoom()
		{
			ViewBag.CurrentUser = CurrentUser;
			UserStatistic statistic = DataService.GetUserStatistic(CurrentUser.Id);
			
			var model = new PersonalRoomViewModel()
			{
				FirstName = CurrentUser.FirstName,
				LastName = CurrentUser.LastName,
				Login = CurrentUser.Login,
				NumberOfAlbums = statistic.NumberOfAlbums,
				NumberOfPhotos = statistic.NumberOfPhotos,
				OverallAlbumsRating = statistic.OverallRatingOfAlbums,
				OverallPhotosRating = statistic.OverallRatingOfPhotos,
				Job = CurrentUser.Job
			};
			return View(model);
		}

		// POST: /Home/PersonalRoom
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> PersonalRoom(PersonalRoomViewModel model)
		{
			ViewBag.CurrentUser = CurrentUser;

			if (model.Job != null)
			{
				CurrentUser.Job = model.Job;
				await UserManager.UpdateAsync(CurrentUser);
			}
			else
			{
				model.Job = string.Empty;
			}
			return View(model);
		}

		// GET: /Home/Albums
		public ActionResult Albums()
		{
			ViewBag.CurrentUser = CurrentUser;
			return View();
		}

		// GET: /Home/Search
		public ActionResult Search()
		{
			return View();
		}

		public ActionResult About()
		{
			return View();
		}

		public ActionResult Contact()
		{
			return View();
		}
		
		#region Helpers
		#endregion
	}
}