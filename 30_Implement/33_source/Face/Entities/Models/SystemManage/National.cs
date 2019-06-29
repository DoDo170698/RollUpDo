using Common.CustomAttributes;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.SystemManage
{
	[DropDown(ValueField = "Id", TextField = "Name")]
	public class National : Entity
	{
		[Key]
		public int Id { get; set; }

		[Display(Name = "Dân tộc")]
		public string Name { get; set; }

		[Display(Name = "Mô tả")]
		public string Description { get; set; }

		[Display(Name = "Ngày tạo")]
		public DateTime CreateDate { get; set; }

		[Display(Name = "Ngày sửa")]	
		public DateTime? UpdateDate { get; set; }
		public bool IsDeleted { get; set; }

		public string Describe()
		{
			throw new NotImplementedException();
		}

		public National() 
		{
				CreateDate = DateTime.Now;

		}
	}
}
