namespace Social_Media_App.Data.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public List<Message> Messages { get; set; }
        public List<User> Users { get; set; }
    }

}
