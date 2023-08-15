using SimpleBlog.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlog.Models.ViewModels
{
    public class UserBlogPostsViewModel
    {
        public List<BlogPost> BlogPosts { get; set; }
        public ApplicationUser User { get; set; }
    }
}
