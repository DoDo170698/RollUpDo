using Common.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entities.Models.SystemManage
{
    /// <summary>
    /// Tài khoản người dùng
    /// </summary>
    [DropDown(ValueField = "Id", TextField = "Person")]
    public class Person : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        [Display(Name = "Mã nhân viên")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ và tên!")]
        [Display(Name = "Họ và tên")]
        [StringLength(50, ErrorMessage = "Họ và tên không được vượt quá 50 ký tự!")]
        public string FullName { get; set; }


        [RegularExpression(@"^([\w\!\#$\%\&\'*\+\-\/\=\?\^`{\|\}\~]+\.)*[\w\!\#$\%\&\'‌​*\+\-\/\=\?\^`{\|\}\~]+@((((([a-zA-Z0-9]{1}[a-zA-Z0-9\-]{0,62}[a-zA-Z0-9]{1})|[‌​a-zA-Z])\.)+[a-zA-Z]{2,6})|(\d{1,3}\.){3}\d{1,3}(\:\d{1,5})?)$", ErrorMessage = "Địa chỉ E-mail không hợp lệ!")]
        [Display(Name = "Địa chỉ E-mail!")]
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ email!")]
        [Column(TypeName = "nvarchar")]
        [StringLength(50, ErrorMessage = "Địa chỉ E-mail không được vượt quá 50 ký tự!")]
        public string Email { get; set; }

        [StringLength(200, ErrorMessage = "Đường dẫn ảnh đại diện không được vượt quá 200 ký tự!")]
        [Display(Name = "Ảnh đại diện")]
        public string ProfilePicture { get; set; }

        [StringLength(20, ErrorMessage = "Điện thoại không được vượt quá 20 ký tự!")]
        [Column(TypeName = "nvarchar")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Số điện thoại không hợp lệ!")]
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        //Important - always use ICollection, not IEnumerable for Navigation properties and make them virtual - thanks to this ef will be able to track changes

        //Đoạn thêm mới các thông tin
        [Display(Name = "Ngày sinh")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Giới tính")]
        public bool? Sex { get; set; }

        [Display(Name = "Địa chỉ")]
        [StringLength(250, ErrorMessage = "Địa chỉ không được vượt quá 250 ký tự!")]
        public string Address { get; set; }

        public string UnitCode { get; set; }
        [ForeignKey("UnitCode")]
        public virtual DonVi DonVi { get; set; }

        public string CardId { get; set; }
        public string PasspostId { get; set; }
        public string Orther { get; set; }

        public bool? IsDeleted { get; set; }
        public DateTime? CreateDate { get; set; }

        public string Describe()
        {
            return "{ AccountId : \"" + Id + "\", Name : \"" + FullName + "\", { Email : \"" + Email + "\" }";
        }
    }
}
