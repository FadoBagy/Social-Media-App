namespace Social_Media_App.Data.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }

        public int ChatId { get; set; }
        public Chat Chat { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }

}
