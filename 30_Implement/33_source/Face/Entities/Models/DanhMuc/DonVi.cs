using Common.CustomAttributes;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    //[Table("DonVi")]
    [DropDown(ValueField = "Id", TextField = "Name")]
    public class DonVi : Entity
    {
        [Key]
        public long Id { get; set; }
        #region const
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDeleted { get; set; }
        [Display(Name = "Tên")]
        public string Name { get; set; }
        [Display(Name = "Mã")]
        public string Code { get; set; }
        #endregion
        [StringLength(20, ErrorMessage = "Điện thoại không được vượt quá 20 ký tự!")]
        public string DienThoai { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }

        [Display(Name = "email")]
        public string Email { get; set; }


        [Display(Name = "Cấp đơn vị")]
        public int CapDV { get; set; }

        [Display(Name = "Đơn vị quản lý trực tiếp")]
        public long? IdCha { get; set; }
        [ForeignKey("IdCha")]
        public virtual DonVi DonVis { get; set; }
        public DonVi()
        {
            CreateDate = DateTime.Now;
        }
     
        public string Describe()
        {
            return "{id: " + Id + "}";

        }
    }
  
}