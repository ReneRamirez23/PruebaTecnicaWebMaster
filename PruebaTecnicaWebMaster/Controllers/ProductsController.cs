using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaWebMaster.Models;
using PruebaTecnicaWebMaster.Repositories;

namespace PruebaTecnicaWebMaster.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [Authorize(policy: "Admin")]
        public async Task<IActionResult> Index()
        {
            var productList = _productRepository.GetAll();
            return View(productList);
        }

        // Add Product
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepository.Add(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // Update Product
        public IActionResult Edit(int id)
        {
            var products = _productRepository.GetById(id);
            if (products == null)
            {
                return NotFound();
            }
            return View(products);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product product)
        {
            if (id != product.IdProducts)
            {
                return BadRequest();
            }
            if (ModelState.IsValid) {
                try
                {
                    _productRepository.Update(product);
                } 
                catch (DbUpdateConcurrencyException)
                {
                    if (_productRepository.GetById(product.IdProducts) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
        

        // Delete User
        public IActionResult Delete(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _productRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
