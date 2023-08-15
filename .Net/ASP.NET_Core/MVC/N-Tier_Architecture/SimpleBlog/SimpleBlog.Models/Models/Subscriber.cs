using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlog.Models.Models
{
    public class Subscriber
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a valid email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [ValidateNever]
        public bool IsSubscribed { get; set; }
        [ValidateNever]
        public DateTime DateSubscribed { get; set; }
        [ValidateNever]
        public DateTime? DateUnSubscribed { get; set; }
    }
}
