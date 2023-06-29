namespace Social_Media_App.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class Like
    {
        public int Id { get; set; }
        public DateTime LikedDate { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User User { get; set; }

        [ForeignKey(nameof(Post))]
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
