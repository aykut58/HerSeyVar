using HerSeyVar.Models;
using HerSeyVar.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HerSeyVar.Services
{
    public class CategoryService
    {
        private readonly RepositoryContext _context; // Veri tabanı bağlamı

        public CategoryService(RepositoryContext context)
        {
            _context = context; // Bağlamı başlatma
        }

        // Tüm kategorileri alma
        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync(); // Kategorileri liste olarak döndürme
        }

        // Belirli bir kategori ID'sine göre kategoriyi alma
        public async Task<Category?> GetCategoryById(int? id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id); // Kategoriyi bulma
        }

        // Yeni kategori oluşturma
        public async Task CreateCategory(Category category)
        {
            _context.Categories.Add(category); // Kategoriyi ekleme
            await _context.SaveChangesAsync(); // Değişiklikleri kaydetme
        }

        // Kategoriyi güncelleme
        public async Task UpdateCategory(Category category)
        {
            _context.Categories.Update(category); // Kategoriyi güncelleme
            await _context.SaveChangesAsync(); // Değişiklikleri kaydetme
        }

        // Kategoriyi silme
        public async Task DeleteCategory(int id)
        {
            var category = await GetCategoryById(id); // Kategoriyi ID ile alma
            if (category != null)
            {
                _context.Categories.Remove(category); // Kategoriyi kaldırma
                await _context.SaveChangesAsync(); // Değişiklikleri kaydetme
            }
        }
    }
}
