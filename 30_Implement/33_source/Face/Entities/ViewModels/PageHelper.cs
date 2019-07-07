using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities.ViewModels
{
    public class PageHelper
    {
        public int Page { get; set; } 
        public int pageSize { get; set; } 
        public string SearchText { get; set; }
        public PageHelper()
        {
            Page = 1;
            pageSize = 50;
            SearchText = "";
        }
    }
    
}
