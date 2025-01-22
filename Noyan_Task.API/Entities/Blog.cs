using System.ComponentModel.DataAnnotations;

namespace Noyan_Task.API.Entities
{
    public class Blog
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public string? Author { get; set; }
    }
}
