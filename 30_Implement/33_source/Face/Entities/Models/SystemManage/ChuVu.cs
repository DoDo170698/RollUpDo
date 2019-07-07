using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models.SystemManage
{
    /// <summary>
    /// Chức vụ
    /// </summary>
    public class ChuVu : Entity
    {
        [Key]
       
        public long Id { get; set; }
         
        [Display(Name = "Tên ")] 
        public string Name { get; set; }

        [Display(Name = "Mô tả!")]
        [StringLength(200, ErrorMessage = "Mô tả nhóm quyền không được vượt quá 200 ký tự!")]
        public string Description { get; set; }
       

        public string Describe()
        {
            return "{ RoleId : \"" + Id + "\", Name : \"" + Name + "\" }";
        }
    }
}
