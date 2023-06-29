namespace Social_Media_App.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.Message;

    public class Message
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(ContentLength)]
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }

        public int ChatId { get; set; }
        public Chat Chat { get; set; }

        [Required]
        public string UserId { get; set; }
        public User User { get; set; }
    }

}
