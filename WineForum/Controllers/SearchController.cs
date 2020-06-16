using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WineForum.Data;
using WineForum.Data.Models;
using WineForum.Models.Forum;
using WineForum.Models.Post;
using WineForum.Models.Search;

namespace WineForum.Controllers
{
    public class SearchController : Controller
    {
        private readonly IPost _postService;
        

        public SearchController(IPost postService)
        {
            _postService = postService;
        }
        public IActionResult Results(string searchQuery)
        {
            var posts = _postService.GetFilteredPosts(searchQuery).ToList();

            var areNoResults = (!string.IsNullOrEmpty(searchQuery) && !posts.Any());

            var postListings = posts.Select(post => new PostListingModel
            {
                Id = post.Id,
                AuthorId = post.User.Id,
                AuthorName = post.User.UserName,
                AuthorRating = post.User.Rating,
                Title = post.Title,
                DatePosted = post.Created.ToString(),
                RepliesCount = post.Replies.Count(),
                Forum = BuildForumListing(post)

            });

            var model = new SearchResultModel
            {
                Posts = postListings,
                SearchQuery = searchQuery,
                EmptySearchResult = areNoResults,
            };
            

            return View(model);
        }

        private ForumListingModel BuildForumListing(Post post)
        {
            var forum = post.Forum;

            return new ForumListingModel
            {
                Id = forum.Id,
                ImageUrl = forum.ImageUrl,
                Name = forum.Title,
                Description = forum.Description
            };
        }

        [HttpPost]
        public IActionResult Search(string searchQuery)
        {
            return RedirectToAction("Results", new { searchQuery });
        }
    }
}