using System.ComponentModel.DataAnnotations;

namespace FreelancerMarketPlace1.Models.Model
{
    public class UserRegisterViewModel
    {
        [Required(ErrorMessage = "Lütfen isminizi giriniz.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Lütfen soyisminizi giriniz.")]
        public string Surname { get; set; }
        
        [Required(ErrorMessage = "Lütfen soyisminizi giriniz.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Lütfen mail adresinizi giriniz.")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Lütfen şifre giriniz.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Lütfen şifre tekrar giriniz.")]
        [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor.")]
        public string ConfirmPassword { get; set; }

        public bool IsFreeLancer { get; set; }
        public bool IsCompany { get; set; }
    }
}
