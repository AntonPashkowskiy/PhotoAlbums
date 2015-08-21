using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;

namespace Memento.Models
{
	public class CurrentUser
	{
		public string FirstName 
		{
			get
			{
				if (_firstName != null)
				{
					return _firstName;
				}
				_firstName = GetIdentityClaim("FirstName");

				return _firstName ?? "";
			}
		}

		public string LastName
		{
			get
			{
				if (_lastName != null)
				{
					return _lastName;
				}
				_lastName = GetIdentityClaim("LastName");

				return _lastName ?? "";
			}
		}

		public string Login
		{
			get
			{
				if (_login != null)
				{
					return _login;
				}
				_login = GetIdentityClaim("Login");

				return _login ?? "";
			}
		}
		
		public string PhotoUrl
		{
			get
			{
				if (_photoUrl != null)
				{
					return _photoUrl;
				}
				_photoUrl = GetIdentityClaim("PhotoUrl");

				return _photoUrl ?? "";
			}
		}

		public string SmallPhotoUrl
		{
			get
			{
				if (_smallPhotoUrl != null)
				{
					return _smallPhotoUrl;
				}
				_smallPhotoUrl = GetIdentityClaim("SmallPhotoUrl"); 

				return _smallPhotoUrl ?? "";
			}
		}

		public string Job
		{
			get
			{
				if (_job != null)
				{
					return _job;
				}
				_job = GetIdentityClaim("Job");

				return _job ?? "";
			}
		}

		#region Private 
		private string _firstName = null;
		private string _lastName = null;
		private string _login = null;
		private string _photoUrl = null;
		private string _smallPhotoUrl = null;
		private string _job = null;

		private string GetIdentityClaim(string claimType)
		{
			var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

			return identity.Claims
				.Where(c => c.Type == claimType)
				.Select(c => c.Value)
				.FirstOrDefault();
		}
		#endregion
	}
}