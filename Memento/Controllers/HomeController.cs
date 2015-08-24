using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memento.Models;

namespace Memento.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			Session["CurrentUser"] = new CurrentUser();

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
		
		// GET: /Home/PersonalRoom
		[HttpGet]
		public ActionResult PersonalRoom()
		{
			// get statistic from database
			var model = new PersonalRoomViewModel()
			{
				FirstName = "Anton",
				LastName = "Pashkouski",
				Login = "Wolendrang",
				NumberOfAlbums = 4,
				NumberOfPhotos = 5,
				OverallAlbumsRating = 4,
				OverallPhotosRating = 5
			};
			return View(model);
		}

		// POST: /Home/PersonalRoom
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult PersonalRoom(PersonalRoomViewModel model)
		{
			return View(model);
		}

		// GET: /Home/Albums
		public ActionResult Albums()
		{
			return View();
		}

		// GET: /Home/Album/Id
		public ActionResult Album(int? id)
		{
			AlbumViewModel model = new AlbumViewModel() { AlbumName = "My favorite album." };
			return View(model);
		}

		// GET: /Home/Search
		public ActionResult Search()
		{
			return View();
		}
		
		#region Helpers
		#endregion
	}
}