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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Display(Name = "Mã đơn vị")]
        [Column(TypeName ="nvarchar")]
        [StringLength(20)]
        public string Code { get; set; }
     
        #region const
        //public DateTime CreateDate { get; set; }
        //public DateTime? UpdateDate { get; set; }
        public bool? IsDeleted { get; set; }
        [Display(Name = "Tên")]
        public string Name { get; set; }
        
        #endregion

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }

        [Display(Name = "email")]
        public string Email { get; set; }


        //[Display(Name = "Cấp đơn vị")]
        //public int Level { get; set; }

        //[Display(Name = "Đơn vị quản lý trực tiếp")]
        //public string CodeParent { get; set; }
        //[ForeignKey("CodeParent")]
        //public virtual DonVi DonVis { get; set; }
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public long ParentId { get; set; }
        
        public DonVi()
        {
            //CreateDate = DateTime.Now;
            IsDeleted = false;
        }
     
        public string Describe()
        {
            return "{UnitId: " + Id + "}";

        }
    }
  
}