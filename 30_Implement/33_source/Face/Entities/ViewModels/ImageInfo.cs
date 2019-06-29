using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class ImageInfo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Uri { get; set; }
        public DateTime CreateTime { get; set; }
        public int Width { get; set; }
    }
}
