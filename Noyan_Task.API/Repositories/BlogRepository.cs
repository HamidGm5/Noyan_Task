using Microsoft.EntityFrameworkCore;
using Noyan_Task.API.Context;
using Noyan_Task.API.Entities;
using Noyan_Task.API.Repositories.Interfaces;

namespace Noyan_Task.API.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly DataContext _context;
        public BlogRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> AddNewBlogPost(Blog NewBlog)
        {
            try
            {
                await _context.Blogs.AddAsync(NewBlog);
                return await Save();
            }
            catch
            {
                //log exception
                throw;
            }
        }

        public async Task<bool> BlogPostExists(int ID)
        {
            try
            {
                return await _context.Blogs.AnyAsync(b => b.ID == ID);
            }
            catch
            {
                //log exception
                throw;
            }
        }

        public async Task<bool> DeleteBlogPost(int ID)
        {
            try
            {
                var FindBlog = await _context.Blogs.FindAsync(ID);
                if (FindBlog == null)
                {
                    return false;
                }
                _context.Blogs.Remove(FindBlog);
                return await Save();
            }
            catch
            {
                //log exception
                throw;
            }
        }

        public async Task<ICollection<Blog>> GetAllBlogPosts()
        {
            try
            {
                var blogs = await _context.Blogs.ToListAsync();
                return blogs;
            }
            catch
            {
                //log exception 
                throw;
            }
        }

        public async Task<Blog> GetBlogPostWithID(int ID)
        {
            try
            {
                var blog = await _context.Blogs.Where(fb => fb.ID == ID).FirstOrDefaultAsync();
                return blog;
            }
            catch
            {
                //log exception
                throw;
            }
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> UpdateBlogPost(Blog NewBlog)
        {
            try
            {
                _context.Blogs.Update(NewBlog);
                return await Save();
            }
            catch
            {
                //log exception
                throw;
            }
        }
    }
}
