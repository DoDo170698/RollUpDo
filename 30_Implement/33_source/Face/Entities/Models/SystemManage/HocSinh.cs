using Common.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entities.Models.SystemManage
{
    /// <summary>
    /// Bảng sinh viên
    /// </summary>
    [DropDown(ValueField = "Id", TextField = "Name")]
    public class HocSinh : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        [Display(Name = "Mã sinh viên")]
        public int Code { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ và tên!")]
        [Display(Name = "Họ và tên")]
        [StringLength(50, ErrorMessage = "Họ và tên không được vượt quá 50 ký tự!")]
        public string FullName { get; set; }


        [Display(Name = "Là chuyên gia")]
        public bool? IsExpertsAccount { get; set; }

        //Ngày tạo
        public DateTime CreateDate { get; set; }

        [Display(Name = "Ảnh đại diện")]
        public string ProfilePicture { get; set; }



        [Display(Name = "Địa chỉ")]
        [StringLength(250, ErrorMessage = "Địa chỉ không được vượt quá 250 ký tự!")]
        public string Address { get; set; }


        public long? IdDonVi { get; set; }
        [ForeignKey("IdDonVi")]
        public virtual DonVi DonVi { get; set; }

        public bool? IsDeleted { get; set; }

        public string Describe()
        {
            return "{ AccountId : \"" + Id + "\", Name : \"" + FullName + "\"}" ;
        }

        public HocSinh()
        {
            CreateDate = DateTime.Now;
            IsDeleted = false;
        }
    }
}
