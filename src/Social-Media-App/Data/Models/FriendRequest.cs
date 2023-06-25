namespace Social_Media_App.Data.Models
{
    using Social_Media_App.Data.Models.Enums;
    using System.ComponentModel.DataAnnotations.Schema;

    public class FriendRequest
    {
        public int Id { get; set; }
        public FriendRequestStatus Status { get; set; }

        public string SenderUserId { get; set; }
        [ForeignKey("SenderUserId")]
        public User SenderUser { get; set; }

        public string ReceiverUserId { get; set; }
        [ForeignKey("ReceiverUserId")]
        public User ReceiverUser { get; set; }
    }

}
