using HerSeyVar.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using HerSeyVar.Repositories;
using HerSeyVar.Helpers;

namespace HerSeyVar.Services
{
    public class ProductService
    {
        private readonly RepositoryContext _context;

        public ProductService(RepositoryContext context)
        {
            _context = context;
        }

        // Sayfalı ürünleri alma
        public async Task<PaginatedList<Product>> GetPaginatedProducts(int pageIndex, int pageSize)
        {
            var query = _context.Products.Include(p => p.Category).AsNoTracking(); // Category'yi dahil ediyoruz
            return await PaginatedList<Product>.CreateAsync(query, pageIndex, pageSize);
        }

        // Ürün ID'sine göre ürünü alma
        public async Task<Product> GetProductById(int? id)
        {
            return await _context.Products.Include(p => p.Category)
                                          .FirstOrDefaultAsync(p => p.Id == id);
        }

        // Yeni ürün oluşturma
        public async Task CreateProduct(Product product, IFormFile imageFile)
        {
            var allowedExtensions = new[] { ".jpg", ".png", ".jpeg" }; // İzin verilen dosya uzantıları

            if (imageFile != null)
            {
                var extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
                if (allowedExtensions.Contains(extension))
                {
                    var randomFileName = $"{Guid.NewGuid()}{extension}"; // Rastgele dosya adı oluşturma
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomFileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream); // Dosyayı kaydetme
                    }

                    product.Image = randomFileName; // Resim dosya adını product modeline atıyoruz
                }
            }

            _context.Products.Add(product); // Ürünü ekliyoruz
            await _context.SaveChangesAsync(); // Değişiklikleri kaydediyoruz
        }

        // Ürünü güncelleme
        public async Task UpdateProduct(Product product)
        {
            _context.Update(product); // Ürünü güncelliyoruz
            await _context.SaveChangesAsync(); // Değişiklikleri kaydediyoruz
        }

        // Ürünü silme
        public async Task DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product); // Ürünü kaldırıyoruz
                await _context.SaveChangesAsync(); // Değişiklikleri kaydediyoruz
            }
        }

        // Tüm kategorileri alma
        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories.ToList(); // Kategorileri liste olarak döndürme
        }

        // Belirli bir kategoriye ait ürünleri alma
        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {
            return _context.Products.Where(p => p.CategoryId == categoryId).Include(p => p.Category).ToList(); // Kategoriye göre ürünleri döndürme
        }
    }
}
