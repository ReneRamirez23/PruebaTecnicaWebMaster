using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PruebaTecnicaWebMaster.Models;
using PruebaTecnicaWebMaster.Repositories;
using System.Diagnostics;

namespace PruebaTecnicaWebMaster.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISaleRepository _saleRepository;

        public HomeController(ISaleRepository saleRepository, ILogger<HomeController> logger)
        {
            _saleRepository = saleRepository;
            _logger = logger;
        }

        [Authorize(policy: "General")]
        public async Task<IActionResult> Index()
        {
            var salesByDay = await _saleRepository.GetSalesByDayAsync();

            var chartData = salesByDay.Select(s => new { Date = s.CreationDate.ToShortDateString(), TotalPrice = s.TotalPrice });

            ViewBag.ChartData = JsonConvert.SerializeObject(chartData);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
