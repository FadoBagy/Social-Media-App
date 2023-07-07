namespace Social_Media_App.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.User;
    public class User : IdentityUser
    {
        [Required]
        public string ProfileImage { get; set; } = "/img/user/defaultUserIcon.jpg";
        [Required]
        public string BackgroundImage { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsPrivate { get; set; } = false;
        [Required]
        [MaxLength(FirstNameLength)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(LastNameLength)]
        public string LastName { get; set; }
        public string? ChatHubConnectionId { get; set; }
        public string? NotificationHubConnectionId { get; set; }

        public List<User> Friends { get; set; }
        public List<Chat> Chats { get; set; }
        public List<FriendRequest> IncomingFriendRequests { get; set; }
        public List<Post> UploadedPosts { get; set; }
        public List<Like> Likes { get; set; }
        public List<Comment> Comments { get; set; }
    }

}
