using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using Memento.Models;
using ServiceLayer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Memento.Controllers.API
{
    public class BaseApiController : ApiController
    {
		private IDataService _dataService;

		public BaseApiController(IDataService dataService)
		{
			if (dataService == null)
			{
				throw new ArgumentNullException("Data service object can't be null.");
			}
			_dataService = dataService;
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

		protected string CurrentUserId 
		{
			get
			{
				return User.Identity.GetUserId();
			}
		}
    }
}
