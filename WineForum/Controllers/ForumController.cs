using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WineForum.Data;
using WineForum.Data.Models;
using WineForum.Models.Forum;

namespace WineForum.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForum _forumService;
        private readonly IPost _postService;

        public ForumController (IForum forumService)
        {
            _forumService = forumService;
        }
        public IActionResult Index()
        {
            var forums = _forumService.GetAll()
               .Select(forum => new ForumListingModel
               {
                   Id = forum.Id,
                   Name = forum.Title,
                   Description = forum.Description,
               });

            var model = new ForumIndexModel
            {
                ForumList = forums
            };

            return View(model);
        }


        public IActionResult Topic(int id)
        {
            var forum = _forumService.GetById(id);

            var post = _postService.GetFilteredPosts(id);

            var postListings = 
        }
    }
}