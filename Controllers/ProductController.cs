using HerSeyVar.Models;
using HerSeyVar.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace HerSeyVar.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        // Constructor: ProductService bağımlılığını alır ve içeriği başlatır.
        public ProductController(ProductService service)
        {
            _productService = service;
        }

        // Index: Ürünleri sayfalama ile getirir ve view'a gönderir.
        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 10)
        {
            var paginatedProducts = await _productService.GetPaginatedProducts(pageIndex, pageSize);
            return View(paginatedProducts);
        }

        // Create: Yeni ürün oluşturma formunu görüntüler.
        public IActionResult Create()
        {
            // Kategorileri ViewBag'e ekler, böylece seçim yapabilmek için formda kullanılabilir.
            ViewBag.Categories = _productService.GetCategories();
            return View();
        }

        // Create (POST): Yeni bir ürün oluşturur ve başarılıysa Index'e yönlendirir.
        [HttpPost]
        public async Task<IActionResult> Create(Product model, IFormFile imageFile)
        {
            // Model geçerliyse ürünü oluşturur.
            if (ModelState.IsValid)
            {
                await _productService.CreateProduct(model, imageFile);
                return RedirectToAction("Index");
            }

            // Model geçerli değilse, kategorileri tekrar yükler ve formu aynı model ile yeniden gösterir.
            ViewBag.Categories = _productService.GetCategories();
            return View(model);
        }

        // Edit: Belirli bir ürün ID'sine göre ürünü alır ve düzenleme formunu görüntüler.
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
                return NotFound();

            // Kategorileri ViewBag'e ekler.
            ViewBag.Categories = _productService.GetCategories();
            return View(product);
        }

        // Edit (POST): Ürünü günceller ve başarılıysa Index'e yönlendirir.
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Product model)
        {
            // ID'ler eşleşmiyorsa NotFound döner.
            if (id != model.Id)
                return NotFound();

            // Model geçerliyse ürünü günceller.
            if (ModelState.IsValid)
            {
                await _productService.UpdateProduct(model);
                return RedirectToAction("Index");
            }

            // Model geçerli değilse, kategorileri tekrar yükler ve formu aynı model ile yeniden gösterir.
            ViewBag.Categories = _productService.GetCategories();
            return View(model);
        }

        // Delete (GET): Belirli bir ürün ID'sine göre silme onay sayfasını görüntüler.
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        // Delete (POST): Belirli bir ürünü siler ve başarılıysa Index'e yönlendirir.
        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            await _productService.DeleteProduct(id);
            return RedirectToAction("Index");
        }

        // ByCategory: Belirli bir kategori ID'sine göre ürünleri alır ve görüntüler.
        public IActionResult ByCategory(int id)
        {
            var products = _productService.GetProductsByCategory(id);
            return View(products);
        }
    }
}
