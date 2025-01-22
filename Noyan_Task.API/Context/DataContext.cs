using Microsoft.EntityFrameworkCore;
using Noyan_Task.API.Entities;

namespace Noyan_Task.API.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        DbSet<Blog> Blogs { get; set; }
    }
}
