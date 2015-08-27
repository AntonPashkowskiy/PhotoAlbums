using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Memento.Models;
using ServiceLayer;
using System.IO;
using Memento.Environment.DataManagement;

namespace Memento.Controllers
{
    [Authorize]
    public class ManageController : BaseController
    {
        public ManageController(IDataService dataService) : base(dataService) {}

        public ManageController(
			ApplicationUserManager userManager, 
			ApplicationSignInManager signInManager, 
			IDataService dataService
		) : base(userManager, signInManager, dataService)
        {
        }

		// POST: /Manage/UploadAvatar
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> UploadAvatar(HttpPostedFileBase file)
		{
			if (file == null)
			{
				return Json("Upload failed.");
			}
			string localPath = await SaveFileInUserDirectory(file);
			return Json(localPath);
		}

        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";

            var userId = User.Identity.GetUserId();
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };
            return View(model);
        }

        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
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

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

		private async Task<string> SaveFileInUserDirectory(HttpPostedFileBase file)
		{
			Size smallSize = PhotoManager.GetThumbailSize(PhotoSize.Small);
			Size standartSize = PhotoManager.GetThumbailSize(PhotoSize.AvatarStandart);
			
			string extention = Path.GetExtension(file.FileName);
			ServerPath smallPhotoPath = HttpContext.GetFilePath(DirectoryType.AvatarDirectory, CurrentUser.UserName, extention);
			ServerPath standartPhotoPath = HttpContext.GetFilePath(DirectoryType.AvatarDirectory, CurrentUser.UserName, extention);

			using (var stream = new MemoryStream())
			{
				file.InputStream.CopyTo(stream);
				Image originalImage = Image.FromStream(stream);
				Image smallImage = originalImage.GetThumbnailImage(smallSize.Width, smallSize.Height, null, IntPtr.Zero);
				Image standartImage = originalImage.GetThumbnailImage(standartSize.Width, standartSize.Height, null, IntPtr.Zero);

				smallImage.Save(smallPhotoPath.AbsolutePath);
				standartImage.Save(standartPhotoPath.AbsolutePath);
			}
			CurrentUser.SmallPhotoUrl = smallPhotoPath.LocalPath;
			CurrentUser.PhotoUrl = standartPhotoPath.LocalPath;
			await UserManager.UpdateAsync(CurrentUser);

			return standartPhotoPath.LocalPath;
		}

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }
		#endregion
    }
}