using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class UnitViewModels
    {
        [Key]
        [Display(Name = "Mã đơn vị")]
        [StringLength(20)]
        public string Code { get; set; }
        
        public bool? IsDeleted { get; set; }
        [Display(Name = "Tên")]
        public string Name { get; set; }
        

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

        public virtual UnitViewModels DonVis { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreateDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdateDate { get; set; }
        public UnitViewModels()
        {
            IsDeleted = false;
        }
    }
}
