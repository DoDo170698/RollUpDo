using Common.CustomAttributes;
using Entities.Models.SystemManage;
using System; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 

namespace Entities.Models
{
    //[Table("DonVi")]
    [DropDown(ValueField = "Id", TextField = "Name")]
    public class Application : Entity
    {
        [Key]
        public long Id { get; set; }
       
        [Display(Name = "Tên")]
        public string Name { get; set; }

        [Display(Name = "Đường dẫn")]
        public string Url { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }
        [Display(Name = "Has")]
        public string Has256 { get; set; }
        
        [Display(Name = "NguoiTao")]
        public long? IdUser { get; set; }
        [ForeignKey("IdUser")]
        public virtual User Users { get; set; }
        #region const
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDeleted { get; set; }

        #endregion

        public Application()
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