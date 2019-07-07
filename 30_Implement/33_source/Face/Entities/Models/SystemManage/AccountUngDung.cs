using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entities.Models.SystemManage
{
    [Table("AccountUngDung")]
    public class AccountUngDung : Entity
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long Id { get; set; }
		//Người dùng
		public long UserId { get; set; }
		[ForeignKey("UserId")]
		public virtual Person Account { get; set; }

        public long AppId { get; set; }
        [ForeignKey("AppId")]
        public virtual Application Apps { get; set; }

        public DateTime CreateDate { get; set; }

    public string Describe()
    {
        return "{ AccountId : \"" + UserId;
    }

		public AccountUngDung()
		{
			CreateDate = DateTime.Now;
		}
  }
}
