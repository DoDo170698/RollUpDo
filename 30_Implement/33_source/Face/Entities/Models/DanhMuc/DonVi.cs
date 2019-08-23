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
    [DropDown(ValueField = "Code", TextField = "Name")]
    public class DonVi : Entity
    {
        [Key]
        [Display(Name = "Mã đơn vị")]
        [StringLength(20)]
        public string Code { get; set; }

        #region const
        [Column(TypeName = "datetime2")]
        public DateTime CreateDate { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? UpdateDate { get; set; }
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


        [Display(Name = "Cấp đơn vị")]
        public int Level { get; set; }

        [Display(Name = "Đơn vị cha")]
        public string CodeParent { get; set; }
        [ForeignKey("CodeParent")]
        public virtual DonVi DonVis { get; set; }

        public DonVi()
        {
            CreateDate = DateTime.Now;
            UpdateDate = DateTime.Now;
            IsDeleted = false;
        }
     
        public string Describe()
        {
            return "{UnitCode: " + Code + "Name:"+Name+"}";

        }
    }
  
}