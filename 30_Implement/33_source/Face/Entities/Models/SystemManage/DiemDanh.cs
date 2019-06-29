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
        [Display(Name = "Id sinh viên")]
        public long HocSinhId { get; set; }
        public int TinhTrang { get; set; }
        public DateTime CreateDate { get; set; }

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
