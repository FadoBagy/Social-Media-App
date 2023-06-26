namespace Social_Media_App.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Build.Framework;
    using System;
    public class User : IdentityUser
    {
        [Required]
        public string ProfileImage { get; set; }
        [Required]
        public string BackgroundImage { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsPrivate { get; set; } = false;
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public List<User> Friends { get; set; }
        public List<Chat> Chats { get; set; }
        public List<FriendRequest> IncomingFriendRequests { get; set; }
    }

}
