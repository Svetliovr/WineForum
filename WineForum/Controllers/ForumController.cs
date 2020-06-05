using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WineForum.Data;
using WineForum.Data.Models;
using WineForum.Models.Forum;
using WineForum.Models.Post;

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

            var posts = _postService.GetPostsByForum(id);

            var postListings = posts.Select(post => new PostListingModel
            {
                Id = post.Id,

                AuthorRating = post.User.Rating,
                AuthorName = post.User.UserName,
                Title = post.Title,
                DatePosted = post.Created.ToString(),
                RepliesCount = post.Replies.Count(),
                Forum = BuildForumListing(post)

            });
        }

        private ForumListingModel BuildForumListing(object post)
        {
            throw new NotImplementedException();
        }
    }
}