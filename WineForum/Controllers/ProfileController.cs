using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using WineForum.Data;
using WineForum.Data.Models;
using WineForum.Models.ApplicationUser;

namespace WineForum.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUser _userService;
        private readonly IUpload _uploadService;
        private readonly IConfiguration _configuration;

        public ProfileController(UserManager<ApplicationUser> userManager,
            IApplicationUser userService,
            IUpload uploadService,
            IConfiguration configuration
            )
        {
            _userManager = userManager;
            _userService = userService;
            _uploadService = uploadService;
            _configuration = configuration;
        }

        public IActionResult Detail(string id)
        {
            var user = _userService.GetById(id);
            var userRoles = _userManager.GetRolesAsync(user).Result;

            var model = new ProfileModel()
            {
                UserId = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                UserRating = user.Rating.ToString(),
                ProfileImageUrl = user.ProfileImageUrl,
                MemberSince = user.MemberSince,
                IsAdmin = userRoles.Contains("Admin")
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UploadProfileImage(IFormFile file)
        {
            var userId = _userManager.GetUserId(User);

            //connect to an azure storage
            var connectionString = _configuration.GetConnectionString("AzureStorageAccount");
            //get blob container
            var container = _uploadService.GetBlobContainer(connectionString, "profile-images");
            // parse the content response header
            var contentDisposition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
            //grab filename
            var filename = contentDisposition.FileName.Trim('"');
            // get reference to a block blob
            var blockBlod = container.GetBlockBlobReference(filename);
            // upload file
            await blockBlod.UploadFromStreamAsync(file.OpenReadStream());
            //set profile image
            await _userService.SetProfileImage(userId, blockBlod.Uri);
            //Redirect to profile page
            return RedirectToAction("Detail", "Profile", new { id = userId });
        }
        public IActionResult Index()
        {
            var profiles = _userService.GetAll()
            .OrderByDescending(user => user.Rating)
            .Select(u => new ProfileModel
            {
                Email = u.Email,
                UserName = u.UserName,
                ProfileImageUrl = u.ProfileImageUrl,
                UserRating = u.Rating.ToString(),
                MemberSince = u.MemberSince,

            });
            var model = new ProfileListModel
            {
                Profiles = profiles
            };
            return View(model);
        }
    }
}