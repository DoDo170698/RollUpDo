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
    public class Face : Entity
    {
        [Key]
        [Display(Name = "Mã")]
        public string Code { get; set; }
        #region const
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDeleted { get; set; }
        [Display(Name = "Tên")]
        public string Name { get; set; }
        
        #endregion

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Display(Name = "Địa chỉ")]
        public string URI { get; set; }
 
        public Face()
        {
            CreateDate = DateTime.Now;
        }
     
        public string Describe()
        {
            return "{mã: " + Code + "}";

        }
    }
  
}