using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaWebMaster.Models;
using PruebaTecnicaWebMaster.Models.ViewModel;
using PruebaTecnicaWebMaster.Repositories;

namespace PruebaTecnicaWebMaster.Controllers
{
    public class SalesController : Controller
    {
        public readonly ISaleRepository _saleRepository;
        private readonly ISalesProductRepository _salesProductRepository;
        private readonly IProductRepository2 _productRepository2;
        public SalesController(ISaleRepository saleRepository, ISalesProductRepository salesProductRepository, IProductRepository2 productRepository2)
        {
            _saleRepository = saleRepository;
            _salesProductRepository = salesProductRepository;
            _productRepository2 = productRepository2;
        }

        [Authorize(policy: "Sell")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(policy: "Accounting")]
        public async Task<IActionResult> Accounting()
        {
            var salelist = _saleRepository.GetAll();
            return View(salelist);
        }

        // produc list
        [HttpGet]
        public async Task<IActionResult> InfoData()
        {

            var data = _saleRepository.GetAllProducts();

            return Json(data);
        }


        // Add sale

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Sale request)
        {
            if (request == null)
            {
                return Json(new { error = "error" });
            }

            Sale newSale = new Sale
            {
                Client = request.Client,
                Descripcion = request.Descripcion,
                MailClient = request.MailClient,
                TotalPrice = request.TotalPrice,
                CreationDate = DateTime.Now,
                PaidDate = DateTime.Now,
                IsPaid = false
            };

            await _saleRepository.AddAsync(newSale);
            await _saleRepository.SaveChangesAsync();

            var idSale = newSale.IdSale;

            if (request.SalesProducts.Count != 0)
            {
                foreach (var product in request.SalesProducts)
                {
                    SalesProduct saleDetail = new SalesProduct
                    {
                        SalesId = idSale,
                        ProductsId = product.ProductsId,
                        Quantity = product.Quantity
                    };

                    await _salesProductRepository.AddAsync(saleDetail);
                }
                await _salesProductRepository.SaveChangesAsync();

                foreach (var product in request.SalesProducts)
                {
                    var selectProduct = await _productRepository2.GetByIdAsync(product.ProductsId);
                    if (selectProduct != null)
                    {
                        selectProduct.Quantity -= product.Quantity ?? 0;
                        await _productRepository2.UpdateAsync(selectProduct);
                    }
                }
                await _productRepository2.SaveChangesAsync();
            }
            return Json(true);
        }

        // update sale
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _saleRepository.GetByIdAsync(id.Value);
            if (sale == null)
            {
                return NotFound();
            }
            return View(sale);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            await _saleRepository.UpdateAsync(id);

            return RedirectToAction("Accounting", "Sales");
        }

        // details sale
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _saleRepository.GetByIdAsync(id.Value);
            if (sale == null)
            {
                return NotFound();
            }

            var products = await _salesProductRepository.GetProductsBySaleIdAsync(sale.IdSale);

            SaleVM saleVM = new SaleVM()
            {
                sale = sale,
                products = products
            };

            return View(saleVM);
        }
    }
}
