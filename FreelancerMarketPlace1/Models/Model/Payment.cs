namespace FreelancerMarketPlace1.Models.Model
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Recipient { get; set; }
        public string Status { get; set; }
        public int Amount { get; set; } // ödeme miktar
    }
}
