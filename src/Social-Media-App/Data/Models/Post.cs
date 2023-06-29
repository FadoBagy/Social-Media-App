namespace Social_Media_App.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static DataConstants.Post;

    public class Post
    {
        public int Id { get; set; }

        [Required]
        public string ImagePath { get; set; }
        [StringLength(CaptionMaxLength)]
        public string? Caption { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public List<Like> Likes { get; set; }
        public List<Comment> Comments { get; set; }
        //public int Shares { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
