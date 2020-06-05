using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WineForum.Models.Forum
{
    public class ForumIndexModel
    {
        public IEnumerable<ForumListingModel>ForumList { get; set; }
    }
}
