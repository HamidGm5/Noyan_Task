using Noyan_Task.API.Entities;

namespace Noyan_Task.API.Repositories.Interfaces
{
    public interface IBlogRepository
    {
        Task<ICollection<Blog>> GetAllBlogPosts();
        Task<ICollection<Blog>> GetBlogPostsWithPageNumber(int PageSize, int PageNumber);
        Task<Blog> GetBlogPostWithID(int ID);
        Task<bool> BlogPostExists(int ID);
        Task<bool> AddNewBlogPost(Blog NewBlog);
        Task<bool> UpdateBlogPost(Blog NewBlog);
        Task<bool> DeleteBlogPost(int ID);
        Task<bool> Save();
    }
}
