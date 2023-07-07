namespace Social_Media_App.Models.User
{
    using Social_Media_App.Models.Post;

    public class UserProfileViewModel
    {
        public UserViewModel User { get; set; }
        public List<GalleryPostViewModel> Posts { get; set; }
    }
}
