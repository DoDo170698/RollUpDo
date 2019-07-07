using Common.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entities.Models.SystemManage
{
    /// <summary>
    /// Tài khoản người dùng
    /// </summary>
    //[DropDown(ValueField = "Id", TextField = "UserName")]
    public class UserRole : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User Users { get; set; }
        public long RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Roles { get; set; }

        public bool? IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public string Describe()
        {
            return "{ AccountId : \"" + Id + "\", Name : \""  + " }";
        }

        public UserRole()
        {
            CreateDate = DateTime.Now;
            IsDeleted = false;
        }
    }
}
