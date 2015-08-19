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
				OverallPhotosRating = 5,
				PhoneNumber = ""
			};
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult PersonalRoom(PersonalRoomViewModel model)
		{
			model.PhoneNumber = "";
			return View(model);
		}

		public ActionResult Albums()
		{
			return View();
		}
		
		[HttpGet]
		public ActionResult CreateAlbum(string returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CreateAlbum(CreateAlbumViewModel model)
		{
			if (ModelState.IsValid)
			{

			}

			return View(model);
		}

		#region Helpers
		#endregion
	}
}