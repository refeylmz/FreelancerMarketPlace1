using System.ComponentModel.DataAnnotations;

namespace FreelancerMarketPlace1.Models.Model
{
    public class Chat
    {
        [Key]
        public int ChatID { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Subject { get; set; }
        public string MessageDetails { get; set; }
        public DateTime MessageDate { get; set; }
        public bool MessageStatus { get; set; }

    }
}
