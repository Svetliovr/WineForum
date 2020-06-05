using System;
using WineForum.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using WineForum.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WineForum.Service
{
    public class ForumService : IForum
    {
        private readonly ApplicationDbContext _context;

        public ForumService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task Create(Forum forum)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int forumId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Forum> GetAll()
        {
            return _context.Forums.Include(forum=>forum.Posts);
        }

        public IEnumerable<ApplicationUser> GetAllActiveUsers(int id)
        {
            throw new NotImplementedException();
        }

        public Forum GetById(int id)
        {
            var forum = _context.Forums.Where(f => f.Id == id)
                 .Include(f => f.Posts)
                 .ThenInclude(p => p.User)
                 .Include(f => f.Posts)
                 .ThenInclude(p => p.Replies)
                 .ThenInclude(r => r.User)
                 .FirstOrDefault();

            return forum;
        }

        public bool HasRecentPost(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateForumDescription(int forumId, string newDescription)
        {
            throw new NotImplementedException();
        }

        public Task UpdateForumTitle(int forumId, string newTitle)
        {
            throw new NotImplementedException();
        }
    }
}
