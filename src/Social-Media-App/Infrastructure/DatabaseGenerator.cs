namespace Social_Media_App.Infrastructure
{
    using Data.Models;
    using Data.Models.Enums;
    using Social_Media_App.Data;

    public static class DatabaseGenerator
    {
        public static async Task GenerateAsync(ApplicationDbContext data)
        {
            var users = await Task.Run(() => GenerateUsers());
            await Task.Run(() => AddFriends(ref users)); // Only First Two Users Become Friends
            var friendRequest = GenerateFriendRequest(users[2], users[0]);
            var chat = GenerateChat(users[0], users[1]);

            await data.Users.AddRangeAsync(users);
            await data.FriendRequests.AddAsync(friendRequest);
            await data.Chats.AddAsync(chat);
            await data.SaveChangesAsync();
        }

        private static List<User> GenerateUsers()
        {
            var userAlexander = new User
            {
                Id = "1",
                FirstName = "Alexander",
                LastName = "Nedelchev",
                Friends = new List<User>()
            };

            var userMladen = new User
            {
                Id = "2",
                FirstName = "Mladen",
                LastName = "Nedev",
                Friends = new List<User>()
            };

            var userTosho = new User
            {
                Id = "3",
                FirstName = "Tosho",
                LastName = "Toshev",
                Friends = new List<User>()
            };

            var users = new List<User> { userAlexander, userMladen, userTosho };

            return users;
        }
        private static void AddFriends(ref List<User> users)
        {
            users[0].Friends.Add(users[1]);
            users[1].Friends.Add(users[0]);
        }
        private static FriendRequest GenerateFriendRequest(User requestingUser, User requestedUser)
        {
            var friendRequest = new FriendRequest
            {
                Status = FriendRequestStatus.Unresolved,
                SenderUserId = requestingUser.Id,
                ReceiverUserId = requestedUser.Id
            };

            return friendRequest;
        }
        private static Chat GenerateChat(User firstUser, User secondUser)
        {
            var chat = new Chat
            {
                Title = "Test",
                Users = new List<User> { firstUser, secondUser },
                Messages = GenerateMessages()
            };

            return chat;
        }

        private static List<Message> GenerateMessages()
        {
            var message = new Message
            {
                Content = "Test1",
                CreatedDate = DateTime.Parse("2023-01-01"),
                UserId = "1"
            };

            var message2 = new Message
            {
                Content = "Test2",
                CreatedDate = DateTime.Parse("2023-01-01"),
                UserId = "2"
            };
            var messages = new List<Message> { message, message2 };

            return messages;
        }
    }
}
