namespace Social_Media_App.Models.Post
{
    using Social_Media_App.Data.Models;

    public class PostViewModel
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string? Caption { get; set; }
        public DateTime CreationDate { get; set; }
        public List<Like> Likes { get; set; }
        public List<Comment> Comments { get; set; }
        public User User { get; set; }
        public bool IsSinglePost { get; set; } = false;
    }
}
