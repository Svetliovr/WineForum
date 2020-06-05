using System;
using System.Collections.Generic;
using System.Text;

namespace WineForum.Data
{
    public class IForum
    {
        Forum GetById(int id);
        IEnumerable<Forum> GetAll();

        Task Create(Forum forum);
        Task Delete(int forumId);
        Task UpdateForumTitle(int forumId, string newTitle);
        Task UpdateForumDescription(int forumId, string newDescription);
        IEnumerable<ApplicationUser> GetActiveUsers(int id);
        bool HasRecentPost(int id);
    }
}
