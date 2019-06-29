using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Handler.Ingetface
{
    interface ISegment
    {
        /// <summary>
        /// Cắt theo dòng
        /// </summary>
        /// <param name="outFolder"> thư mục ra của hình</param>
        /// <param name="image"> mảng đường dẫn ảnh hình ảnh vào</param>
        /// <returns></returns>
        int LineSegment(string outFolder, string[] image = null);
    }
}
