using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.SystemManage
{
    /// <summary>
    /// bảng đơn vị tính chi KPI
    /// </summary>
	public class Unit : Entity
	{
		[Key]
		public int Id { get; set; }

		[Display(Name = "Đơn vị tính")]
		public string Name { get; set; }

		[Display(Name = "Mô tả")]
		public string Description { get; set; }

		[Display(Name = "Ngày tạo")]
		public DateTime CreateDate { get; set; }

		[Display(Name = "Ngày sửa")]
		public DateTime UpdateDate { get; set; }
		public bool IsDeleted { get; set; }

		public string Describe()
		{
			throw new NotImplementedException();
		}

		public Unit() 
		{
			CreateDate = DateTime.Now;
		}
	}
}
