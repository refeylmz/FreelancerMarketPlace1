using System.ComponentModel.DataAnnotations;

namespace FreelancerMarketPlace1.Models.ViewModels
{
    public class RoleUpdateViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Lütfen rol adı giriniz.")]
        public string Name { get; set; }
    }
}
