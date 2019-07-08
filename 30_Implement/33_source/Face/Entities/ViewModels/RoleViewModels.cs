using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class RoleViewModels
    {
        public long Id { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }


        public long UserId { get; set; }
        public string UserName { get; set; }
    }
}
