using HerSeyVar.Models;
using HerSeyVar.Services;
using Microsoft.AspNetCore.Mvc;

namespace HerSeyVar.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;

        // Constructor: CategoryService bağımlılığını alır ve içeriği başlatır.
        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // Index: Tüm kategorileri alır ve görüntülemesi için view'a gönderir.
        public async Task<IActionResult> Index()
        {
            return View(await _categoryService.GetAllCategories());
        }

        // Create: Yeni kategori oluşturma formunu görüntüler.
        public IActionResult Create()
        {
            return View();
        }

        // Create (POST): Yeni bir kategori oluşturur ve başarılıysa Index'e yönlendirir.
        [HttpPost]
        public async Task<IActionResult> Create(Category model)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.CreateCategory(model);
                return RedirectToAction("Index");
            }
            // Eğer model geçerli değilse, formu aynı model ile yeniden gösterir.
            return View(model);
        }

        // Edit: Belirli bir kategori ID'sine göre kategoriyi alır ve düzenleme formunu görüntüler.
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
                return NotFound();

            return View(category);
        }

        // Edit (POST): Kategoriyi günceller ve başarılıysa Index'e yönlendirir.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category model)
        {
            if (id != model.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                await _categoryService.UpdateCategory(model);
                return RedirectToAction("Index");
            }
            // Eğer model geçerli değilse, formu aynı model ile yeniden gösterir.
            return View(model);
        }

        // Delete: Belirli bir kategori ID'sine göre silme onay sayfasını görüntüler.
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
                return NotFound();

            return View(category);
        }

        // Delete (POST): Belirli bir kategoriyi siler ve başarılıysa Index'e yönlendirir.
        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            await _categoryService.DeleteCategory(id);
            return RedirectToAction("Index");
        }
    }
}
