using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SimpleBlog.Models.Models
{
    public class BlogPost
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        
        [DisplayName("Date Created")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MMMM d, yyyy}")]
        public DateTime DateCreated { get; set; }

        // Foreign key to ApplicationUser
        [ValidateNever]
        [Required]
        public string AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        [ValidateNever]
        public ApplicationUser Author { get; set; }


        // Navigation property for comments
        [ValidateNever]
        public ICollection<Comment> Comments { get; set; }

    }
}
