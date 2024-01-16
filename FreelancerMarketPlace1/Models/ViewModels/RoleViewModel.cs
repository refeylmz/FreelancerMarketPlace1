using System.ComponentModel.DataAnnotations;

namespace FreelancerMarketPlace1.Models.ViewModels
{
    public class RoleViewModel
    {
        [Required(ErrorMessage = "Lütfen rol adı giriniz.")]
        public string Name { get; set; }
    }
}
