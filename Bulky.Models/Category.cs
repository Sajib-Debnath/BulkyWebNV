using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [MaxLength(30)]
        [DisplayName("Category Name")]
        [Required]
        public string CategoryName { get; set; }

        [Range(0, 30)]
        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }
    }
}
