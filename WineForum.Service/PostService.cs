﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WineForum.Data;
using WineForum.Data.Models;

namespace WineForum.Service
{
    public class PostService : IPost
    {
        private readonly ApplicationDbContext _context;



        public PostService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Post post)
        {
            _context.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task AddReply(PostReply reply)
        {
            _context.PostReplies.Add(reply);
            await _context.SaveChangesAsync();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditPostContent(int id, string newContent)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetAll()
        {
            return _context.Posts
                .Include(post => post.User)
                .Include(post => post.Replies)
                    .ThenInclude(reply => reply.User)
                .Include(post => post.Forum);

        }

        public Post GetById(int id)
        {
            return _context.Posts.Where(post => post.Id == id)
               .Include(post => post.User)
               .Include(post => post.Replies)
                   .ThenInclude(reply => reply.User)
               .Include(post => post.Forum)
               .SingleOrDefault();
        }

        public IEnumerable<Post> GetFilteredPosts(string searchQuery)
        {
            var normalized = searchQuery.ToLower();
            return GetAll().Where(post
                => post.Title.ToLower().Contains(normalized)
                || post.Content.ToLower().Contains(normalized));
        }

        public IEnumerable<Post> GetFilteredPosts(Forum forum, string searchQuery)
        {
            return string.IsNullOrEmpty(searchQuery)
           ? forum.Posts
           : forum.Posts
           .Where(post => post.Title.ToLower().Contains(searchQuery)
               || post.Content.ToLower().Contains(searchQuery));
        }

        public IEnumerable<Post> GetLatestPosts(int n)
        {
            return GetAll().OrderByDescending(post => post.Created).Take(n);
        }

        public IEnumerable<Post> GetPostsByForum(int id)
        {
            return _context.Forums
                .Where(forum => forum.Id == id).First()
                .Posts;
        }
    }
}
