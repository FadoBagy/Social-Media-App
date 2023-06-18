namespace Social_Media_App.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    public class User : IdentityUser
    {
        public string ProfileImage { get; set; }
        public DateTime CreatedDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<User> Friends { get; set; }
        public List<Chat> Chats { get; set; }
        public List<FriendRequest> IncomingFriendRequests { get; set; }
    }

}
