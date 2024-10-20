using System.ComponentModel.DataAnnotations;

namespace HerSeyVar.Models;

public class Product
{

    public int Id { get; set; }

    [Required(ErrorMessage = "Bu alan gereklidir.")]
    public string? Name { get; set; }


    [MaxLength(100, ErrorMessage = "Açıklama en fazla 100 karakter olmalıdır.")]
    public string? Description { get; set; }

    [Range(0, 100000, ErrorMessage = "Fiyat 1 ile 100000 arasında olmalıdır.")]
    [Required(ErrorMessage = "Bu alan gereklidir.")]
    public decimal Price { get; set; }


    public string? Image { get; set; } = string.Empty;


    [Required(ErrorMessage = "Bu alan gereklidir.")]
    [Range(1, 1000, ErrorMessage = "Stok 1 ile 100 arasında olmalıdır.")]
    public int Stock { get; set; }

    
    [Required(ErrorMessage = "Bu alan gereklidir.")]
    public int CategoryId { get; set; }

    public Category? Category { get; set; }
}

