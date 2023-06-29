namespace Social_Media_App.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static DataConstants.Comment;

    public class Comment
    {
        public int Id { get; set; }
        [Required]
        [StringLength(CommentMaxLength)]
        public string Content { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User User { get; set; }

        [ForeignKey(nameof(Post))]
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
