using Common.CustomAttributes; 
using System; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 

namespace Entities.Models
{
    //[Table("DonVi")]
    [DropDown(ValueField = "Id", TextField = "Name")]
    public class Menu : Entity
    {
        [Key]
        public long Id { get; set; }
        #region const
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDeleted { get; set; }
        #endregion
        [Display(Name = "Tên")]
        public string Name { get; set; }

        [Display(Name = "Đường dẫn")]
        public string Url { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Display(Name = "Cấp Menu")]
        public int Cap { get; set; }

        [Display(Name = "Đơn vị quản lý trực tiếp")]
        public long? IdCha { get; set; }
        [ForeignKey("IdCha")]
        public virtual Menu Menus { get; set; }
        public Menu()
        {
            CreateDate = DateTime.Now;
            IsDeleted = false;
        }
        public string Describe()
        {
            return "{id: " + Id + "}";

        }
    }
  
}