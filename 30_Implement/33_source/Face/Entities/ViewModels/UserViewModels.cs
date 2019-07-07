using System.ComponentModel.DataAnnotations;

namespace Entities.ViewModels
{

    public class UserModels
    {
        public long Id { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }
    }
    public class LoginModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
    public class AccountMappingRole
    {
        public long AccountId { get; set; }
        public long RoleId { get; set; }
    }
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ E-mail!")]
        [RegularExpression(@"^([\w\!\#$\%\&\'*\+\-\/\=\?\^`{\|\}\~]+\.)*[\w\!\#$\%\&\'‌​*\+\-\/\=\?\^`{\|\}\~]+@((((([a-zA-Z0-9]{1}[a-zA-Z0-9\-]{0,62}[a-zA-Z0-9]{1})|[‌​a-zA-Z])\.)+[a-zA-Z]{2,6})|(\d{1,3}\.){3}\d{1,3}(\:\d{1,5})?)$", ErrorMessage = "Địa chỉ E-mail không hợp lệ!")]
        [Display(Name = "Địa chỉ E-mail!")]
        public string Email { get; set; }
    }
}
