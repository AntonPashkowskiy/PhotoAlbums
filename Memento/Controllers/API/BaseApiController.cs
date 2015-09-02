using System;
using System.Web.Http;
using ServiceLayer;
using Microsoft.AspNet.Identity;

namespace Memento.Controllers.API
{
    public class BaseApiController : ApiController
    {
	    public BaseApiController(IDataService dataService)
		{
			if (dataService == null)
			{
				throw new ArgumentNullException("Data service object can't be null.");
			}
			DataService = dataService;
		}

	    protected IDataService DataService { get; private set; }

	    protected string CurrentUserId 
		{
			get
			{
				return User.Identity.GetUserId();
			}
		}
    }
}
