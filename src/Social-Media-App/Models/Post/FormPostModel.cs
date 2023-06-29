namespace Social_Media_App.Models.Post
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants.Post;

    public class FormPostModel
    {
        [Required(ErrorMessage = "Please select an image.")]
        public IFormFile Image { get; set; }
        [StringLength(CaptionMaxLength,
            ErrorMessage = "Up to {1} characters")]
        public string? Caption { get; set; }
    }
}
