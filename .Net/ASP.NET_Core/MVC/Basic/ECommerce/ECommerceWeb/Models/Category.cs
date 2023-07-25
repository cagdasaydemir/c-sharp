using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ECommerceWeb.Models
{
    public class Category
    {

        // if the name is different then Id and used as a PrimaryKey [Key] needed to be added 
        //[Key]
        public int Id { get; set; }
        //[Required]
        [MaxLength(30)]
        [DisplayName("Category Name")]
        public required string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100)]
        public int DisplayOrder { get; set; }

    }
}
