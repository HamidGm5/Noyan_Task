using Microsoft.AspNetCore.Mvc;
using Noyan_Task.API.Entities;
using Noyan_Task.API.Repositories.Interfaces;
using System.Collections.ObjectModel;

namespace Noyan_Task.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : Controller
    {
        private readonly IBlogRepository _BlogRepository;

        public BlogController(IBlogRepository BlogRepository)
        {
            _BlogRepository = BlogRepository;
        }

        [HttpGet(Name = "GetAllBlogPosts")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<ICollection<Blog>>> GetAllBlogPosts()
        {
            var BlogPosts = await _BlogRepository.GetAllBlogPosts();
            return Ok(BlogPosts);
        }


        [HttpGet("{ID:int}", Name = "GetBlogPostByID")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Blog>> GetBlogPostByID(int ID)
        {
            var FindBlog = await _BlogRepository.GetBlogPostWithID(ID);

            if (FindBlog == null)
            {
                return NotFound();
            }
            return Ok(FindBlog);
        }


        [HttpGet("{PageSize:int}/{PageNumber:int}", Name = "GetBlogByPageAndSize")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<ICollection<Blog>>> GetBlogByPageAndSize(int PageSize, int PageNumber)
        {
            var Blogs = await _BlogRepository.GetBlogPostsWithPageNumber(PageSize, PageNumber);
            return Ok(Blogs);
        }


        [HttpPost(Name = "AddNewBlogPost")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]

        public async Task<ActionResult<Blog>> AddNewBlogPost([FromBody] Blog NewBlog)
        {
            if (string.IsNullOrEmpty(NewBlog.Title) || string.IsNullOrEmpty(NewBlog.Content))
            {
                return BadRequest();
            }
            var InsertBlog = await _BlogRepository.AddNewBlogPost(NewBlog);
            if (InsertBlog)
            {
                return CreatedAtAction("AddNewBlogPost", NewBlog);
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }


        [HttpPut("{ID:int}", Name = "UpdateBlogPost")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public async Task<ActionResult<Blog>> UpdateBlogPost([FromBody] Blog NewBlog, int ID)
        {

            if (NewBlog.ID != ID)
                return BadRequest();

            var FindBlog = await _BlogRepository.BlogPostExists(ID);
            if (!FindBlog)
                return NotFound();

            var UpdateBlog = await _BlogRepository.UpdateBlogPost(NewBlog);

            if (UpdateBlog)
            {
                return Ok(NewBlog);
            }
            return BadRequest();
        }


        [HttpDelete("{ID:int}", Name = "DeleteBlogPost")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public async Task<ActionResult<Blog>> DeleteBlogPost(int ID)
        {
            var FindBlog = await _BlogRepository.BlogPostExists(ID);
            if (!FindBlog)
            {
                return NotFound();
            }

            var Deleted = await _BlogRepository.DeleteBlogPost(ID);
            if (Deleted)
            {
                return Ok("Successfully");
            }
            return BadRequest();
        }
    }
}
