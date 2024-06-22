using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaWebMaster.Models;
using PruebaTecnicaWebMaster.Repositories;

namespace PruebaTecnicaWebMaster.Controllers
{
    public class SalesController : Controller
    {
        private readonly BD_ControlVentasContext _dbContext;

        public SalesController(BD_ControlVentasContext context)
        {

            _dbContext = context;

        }

        [Authorize(policy: "Sell")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(policy: "Accounting")]
        public async Task<IActionResult> Accounting()
        {
            List<Sale> salelist = await _dbContext.Sales.ToListAsync();
            return View(salelist);
        }

        // GET: Produc list
        [HttpGet]
        public async Task<IActionResult> InfoData()
        {

            List<Product> data = await _dbContext.Products.ToListAsync();

            return Json(data);
        }


        // GET: Sale/Create

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Sale request)
        {
            Sale newSale = new Sale();

            if (request != null)
            {
                return Json(new { error = "error" });
            }

            var idSale = newSale.IdSale;

            if (request.SalesProducts.Count != 0)
            {
                foreach (var product in request.SalesProducts)
                {
                    SalesProduct saleDetail = new SalesProduct();

                    saleDetail.SalesId = idSale;
                    saleDetail.ProductsId = product.ProductsId;
                    saleDetail.Quantity = product.Quantity;

                    _dbContext.SalesProducts.Add(saleDetail);
                    await _dbContext.SaveChangesAsync();
                }

                foreach (var product in request.SalesProducts)
                {
                    var selectProduct = _dbContext.Products.Where(x => x.IdProducts == product.ProductsId).FirstOrDefault();

                    selectProduct.Quantity -= product.Quantity ?? 0;

                    _dbContext.Products.Update(selectProduct);
                    await _dbContext.SaveChangesAsync();
                }
            }
            return Json(true);
        }

        // GET: Produc/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _dbContext.Sales.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }
            return View(sale);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id )
        {
            if (id == 0)
            {
                return NotFound();
            }

            var sale = _dbContext.Sales.Where(x => x.IdSale == id).FirstOrDefault();

            return RedirectToAction("Accounting", "Sales");
        }

        // GET: Sale/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = _dbContext.Sales.Where(m => m.IdSale == id).FirstOrDefault();
            var products = await _dbContext.SalesProducts.Where(x => x.SalesId == sale.IdSale).Include(c => c.Products)
                .Select(j => new Product {
                    NameProducts = j.Products.NameProducts,
                    Quantity = j.Quantity ?? 0,
                    UnitPrice = (decimal)(j.Products.UnitPrice * j.Quantity),

                }).ToListAsync();

            SaleVM saleVM = new SaleVM()
            {
                sale = sale,
                products = products
            };

            return View(saleVM);
        }
    }
}
