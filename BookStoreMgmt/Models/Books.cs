using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreMgmt.Models
{
    public class Books
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="BookName is required")]
        public string BookName { get; set; }
        [Required(ErrorMessage = "Author is required")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal Price { get; set; }

    }
}
