using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SimpleBlog.Models.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Name")]
        public string CommentAuthor { get; set; }

        [Required]
        public string Text { get; set; }

        [DisplayName("Date Posted")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MMMM d, yyyy}")]
        [ValidateNever]
        public DateTime DatePosted { get; set; }

        // Foreign key to associate with BlogPost
        [ValidateNever]
        public int BlogPostId { get; set; }
        [ForeignKey("BlogPostId")]
        [ValidateNever]
        public BlogPost BlogPost { get; set; }

        
    }
}
