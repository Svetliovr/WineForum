using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WineForum.Models.Post;

namespace WineForum.Models.Home
{
    public class HomeIndexModel
    {
        public string SearchQuery { get; set; }
        public IEnumerable<PostListingModel> LatestPosts { get; set; }

    }
}
