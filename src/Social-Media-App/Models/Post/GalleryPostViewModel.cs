namespace Social_Media_App.Models.Post
{
    using Social_Media_App.Data.Models;

    public class GalleryPostViewModel
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public int LikeCount { get; set; }
        public int CommentCount { get; set; }
        //public List<Like> Likes { get; set; }
        //public List<Comment> Comments { get; set; }
    }
}
