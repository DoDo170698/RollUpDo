using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.SystemManage
{
	public class Weight : Entity
	{
		[Key]
		public long id { get; set; }

		[Required]
		public int code { get; set; }

		public DateTime CreateDate { get; set; }

		public DateTime? UpdateDate { get; set; }

		public bool? IsDeleted { get; set; }

		public Weight()
		{
			CreateDate = DateTime.Now;
		}

					public string Describe()
					{
							 return "{ Weight: \"" + id;
					}
		 }
}
