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
    [DropDown(ValueField = "Id", TextField = "UserName")]
    public class User : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }


        public bool? IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public string Describe()
        {
            return "{ AccountId : \"" + Id + "\", Name : \"" + UserName + " }";
        }

        public User()
        {
            CreateDate = DateTime.Now;
            IsDeleted = false;
        }
    }
}
