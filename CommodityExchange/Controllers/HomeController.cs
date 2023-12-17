using CommodityExchange.Context;
using CommodityExchange.Models;
using CommodityExchange.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CommodityExchange.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _applicationContext;

        public HomeController(
            ILogger<HomeController> logger,
            ApplicationContext applicationContext)
        {
            _logger = logger;
            _applicationContext = applicationContext;
        }

        public async Task<IActionResult> Index()
        {
            var barters = await _applicationContext.Barters.ToListAsync();
            var list = new List<BarterViewModel>();

            foreach (var barter in barters)
            {
                var model = new BarterViewModel
                {
                    Id = barter.Id,
                    Name = barter.Name,
                    Photo = barter.Photo,
                    Status = barter.Status,
                    Owner = barter.Owner,
                    Description = barter.Description,
                    StartPrice = barter.StartPrice,
                    CreationTime = barter.CreationTime,
                    ClosedTime = barter.ClosedTime
                };
                list.Add(model);
            }
            return View(list);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}