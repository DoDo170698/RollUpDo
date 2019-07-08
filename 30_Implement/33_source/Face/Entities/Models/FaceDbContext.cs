using Entities.Models.SystemManage;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class FaceDbContext : DbContext
    {
        public FaceDbContext() : base("name=FaceDbContext") { }
        
        public DbSet<Role> roles { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<UserRole> userRoles { get; set; }
        public DbSet<Person> people { get; set; }
    }
}
