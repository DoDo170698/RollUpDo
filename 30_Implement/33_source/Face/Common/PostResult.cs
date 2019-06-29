using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
    public class PostResult
    {
        public long IdLop { get; set; }
        public long IdTruong { get; set; }
        public long IdPersion { get; set; }
        public string FileName { get; set; }
    }
    public class PostFileResult
    {
        public PostResult PostResult { get; set; }
        public HttpPostedFileBase Image { get; set; }
    }
}
