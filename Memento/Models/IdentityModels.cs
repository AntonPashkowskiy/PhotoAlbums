using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Memento.Models
{
    public class ApplicationUser : IdentityUser
    {
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Login { get; set; }
		public string PhotoUrl { get; set; }
		public string SmallPhotoUrl { get; set; }
		public string Job { get; set; }
		public DateTime CreationDate { get; set; }
		public bool IsRemoved { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
			// Custom claims
			userIdentity.AddClaim(new Claim("FirstName", this.FirstName));
			userIdentity.AddClaim(new Claim("LastName", this.LastName));
			userIdentity.AddClaim(new Claim("Login", this.Login));
			userIdentity.AddClaim(new Claim("PhotoUrl", this.PhotoUrl));
			userIdentity.AddClaim(new Claim("SmallPhotoUrl", this.SmallPhotoUrl));
			userIdentity.AddClaim(new Claim("Job", this.Job));
			
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
			: base("PhotoAlbumsDBHome", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}