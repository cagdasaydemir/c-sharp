using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlog.Models.ViewModels
{
    public class UnsubscribeVM
    {

        [Required(ErrorMessage = "Please enter a valid email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }
        
    }
}
