using System.ComponentModel.DataAnnotations;

namespace Asp.NetCore_Identity.Models
{
    public class UserCreateModel
    {
        [Required(ErrorMessage ="Kullanıcı adı gereklidir.")]
        public string UserName { get; set; }

        [EmailAddress(ErrorMessage ="Lütfen bir mail formatı giriniz.")]
        [Required(ErrorMessage ="Email gereklidir.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre gereklidir.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Cinsiyet gereklidir.")]
        public string Gender { get; set; }
    }
}
