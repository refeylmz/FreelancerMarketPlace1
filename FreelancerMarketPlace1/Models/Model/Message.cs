

namespace FreelancerMarketPlace1.Models.Model
{
    public class Message
    {
        public int MessageID { get; set; }
        public int? SenderID { get; set; }
        public int? ReceiverID { get; set; }
        public string? Subject { get; set; }
        public string? MessageDetails { get; set; }
        public DateTime? MessageDate { get; set; }
        public bool? MessageStatus { get; set; }
        public AppUser SenderUser { get; set; }
        public AppUser ReceiverUser { get; set; }
    }
}
