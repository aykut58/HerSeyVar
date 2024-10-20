using System.ComponentModel.DataAnnotations;

namespace HerSeyVar.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Bu alan gereklidir.")]
        public string Name { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
