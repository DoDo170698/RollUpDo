using Common.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entities.Models.SystemManage
{
    /// <summary>
    /// Bảng điểm danh học sinh
    /// </summary>
    [DropDown(ValueField = "Id", TextField = "Name")]
    public class DiemDanh : Entity
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [Display(Name = "Người")]
        public long PersonId { get; set; }
        public int TinhTrang { get; set; }
        public DateTime CreateDate { get; set; }
        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        public int? IDBot { get; set; }

        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }

        public string Describe()
        {
            return "{ HocSinhId : \"" + Id + "\", TinhTrang : \"" + TinhTrang + "\", { CreateDate : \"" + CreateDate + "\" }";
        }

        public DiemDanh()
        {
            CreateDate = DateTime.Now;
        }
    }
}
