using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Noyan_Task.API.Controllers;
using Noyan_Task.API.Entities;
using Noyan_Task.API.Repositories.Interfaces;

namespace Noyan_Task.Test.Controller
{
    public class BlogControllerTest
    {
        private readonly IBlogRepository _blogRepository;
        public BlogControllerTest()
        {
            _blogRepository = A.Fake<IBlogRepository>();
        }

        //This method return just ok of result
        [Fact]
        public async Task BlogControllerTest_GetAllBlogPosts_ReturnOk()
        {
            //Arrange
            var controller = new BlogController(_blogRepository);

            //Act
            var result = await controller.GetAllBlogPosts();

            //Assert

            var actionResult = Assert.IsType<ActionResult<ICollection<Blog>>>(result);
            var OkResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var ReturnValue = Assert.IsAssignableFrom<ICollection<Blog>>(OkResult.Value);

            Assert.NotNull(ReturnValue);
            Assert.Equal(200, OkResult.StatusCode);
        }

        [Fact]
        public async Task BlogControllerTest_AddNewBlogPost_ReturnCreated()
        {
            //Arrange
            var controller = new BlogController(_blogRepository);
            var BlogPost = A.Fake<Blog>();
            BlogPost.Title = "Test";
            BlogPost.Content = "Test";

            //Act
            A.CallTo(() => _blogRepository.AddNewBlogPost(BlogPost)).Returns(true);
            var Result = await controller.AddNewBlogPost(BlogPost);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Blog>>(Result);
            var CreatedResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var Content = Assert.IsAssignableFrom<Blog>(CreatedResult.Value);

            Assert.Equal(201, CreatedResult.StatusCode);
        }
    }
}
