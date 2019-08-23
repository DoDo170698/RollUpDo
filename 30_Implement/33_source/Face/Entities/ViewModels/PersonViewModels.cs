using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class PersonViewModels
    {

        public long Id { get; set; }

        [Display(Name = "Mã nhân viên")]
        public string Code { get; set; }

        

        [Required(ErrorMessage = "Vui lòng nhập họ và tên!")]
        [Display(Name = "Họ và tên")]
        [StringLength(50, ErrorMessage = "Họ và tên không được vượt quá 50 ký tự!")]
        public string FullName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ email!")]
        public string Email { get; set; }

        //[StringLength(20)]
        public string UnitCode { get; set; }
        [ForeignKey("UnitCode")]
        public virtual UnitViewModels DonVi { get; set; }

        [Display(Name = "Ảnh đại diện")]
        public string ProfilePicture { get; set; }

        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Ngày sinh")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Giới tính")]
        public bool? Sex { get; set; }

        [Display(Name = "Địa chỉ")]
        [StringLength(250, ErrorMessage = "Địa chỉ không được vượt quá 250 ký tự!")]
        public string Address { get; set; }
        public string CardId { get; set; }
        public string PasspostId { get; set; }
        public string Orther { get; set; }
        [Display(Name = "Đã xóa")]
        public bool? IsDeleted { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreateDate { get; set; }
    }
}
