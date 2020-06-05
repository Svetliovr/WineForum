using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WineForum.Models.Post;

namespace WineForum.Models.Forum
{
    public class ForumTopicModel
    {
        public ForumListingModel Forum { get; set; }
        public IEnumerable<PostListingModel> Posts { get; set; }
        
    }
}
