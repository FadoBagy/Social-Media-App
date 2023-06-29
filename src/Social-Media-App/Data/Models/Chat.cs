namespace Social_Media_App.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.Chat;

    public class Chat
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(TitleLength)]
        public string Title { get; set; }

        public List<Message> Messages { get; set; }
        public List<User> Users { get; set; }
    }

}
