using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WineForum.Data.Models;

namespace WineForum.Data
{
    public interface IForum
    {
        Forum GetById(int id);
        IEnumerable<Forum> GetAll();

        Task Create(Forum forum);
        Task Delete(int forumId);
        Task UpdateForumTitle(int forumId, string newTitle);
        Task UpdateForumDescription(int forumId, string newDescription);
        IEnumerable<ApplicationUser> GetAllActiveUsers(int id);
        bool HasRecentPost(int id);
        IEnumerable<ApplicationUser> GetActiveUsers(int id);
    }
}
