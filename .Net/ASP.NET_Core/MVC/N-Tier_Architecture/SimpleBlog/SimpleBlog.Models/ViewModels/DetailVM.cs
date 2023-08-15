using SimpleBlog.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlog.Models.ViewModels
{
    public class DetailVM
    {
        public BlogPost BlogPost { get; set; }
        public Comment NewComment { get; set; }
    }
}
