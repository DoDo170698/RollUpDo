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
        public long Id { get; set; }

        [Required]
        [Display(Name = "Mã đơn vị")]
        [Column(TypeName = "nvarchar")]
        [StringLength(20)]
        public string Code { get; set; }

        #region const
        //public DateTime CreateDate { get; set; }
        //public DateTime? UpdateDate { get; set; }
        public bool? IsDeleted { get; set; }
        [Display(Name = "Tên đơn vị")]
        public string Name { get; set; }
        #endregion

        //public string CodeParent { get; set; }

        //public long ParentId { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
