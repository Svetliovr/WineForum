using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WineForum.Data;
using WineForum.Data.Models;
using WineForum.Models.Reply;

namespace WineForum.Controllers
{
    public class ReplyController : Controller
    {
        private readonly IPost _postService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IForum _forumService;
        private readonly IApplicationUser _userService;


        public ReplyController(IForum forumService,
            IPost postService,
            IApplicationUser userService,
            UserManager<ApplicationUser> userManager
            )
        {
            _postService = postService;
            _userManager = userManager;
            _userService = userService;
            _forumService = forumService;
        }

        public async Task<IActionResult> Create(int id)
        {
            var post = _postService.GetById(id);
            var forum = _forumService.GetById(post.Forum.Id);
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var model = new PostReplyModel
            {
                PostContent = post.Content,
                PostTitle = post.Title,
                PostId = post.Id,


                AuthorName = User.Identity.Name,
                AuthorImageUrl = user.ProfileImageUrl,
                AuthorRating = user.Rating,
                AuthorId = user.Id,
                IsAuthorAdmin = User.IsInRole("Admin"),

                ForumId = post.Forum.Id,
                ForumName = post.Forum.Title,
                ForumImageUrl = post.Forum.ImageUrl,

                Created = DateTime.Now,

            };


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddReply(PostReplyModel model)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);

            var reply = BuildReply(model, user);
            await _postService.AddReply(reply);
            await _userService.UpdateUserRating(userId, typeof(PostReply));

            return RedirectToAction("Index", "Post", new { id = model.PostId });
        }

        private PostReply BuildReply(PostReplyModel model, ApplicationUser user)
        {
            var post = _postService.GetById(model.PostId);

            return new PostReply
            {
                Post = post,
                Content = model.ReplyContent,
                Created = DateTime.Now,
                User = user
            };
        }
    }
}