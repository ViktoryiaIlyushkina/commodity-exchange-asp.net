using CommodityExchange.Context;
using CommodityExchange.Enums;
using CommodityExchange.Models;
using CommodityExchange.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CommodityExchange.Controllers
{
    [Authorize]
    public class BarterController : Controller
    {
        private readonly ApplicationContext _applicationContext;
        private readonly UserManager<User> _userManager;

        public BarterController(
            ApplicationContext applicationContext,
            UserManager<User> userManager) 
        {
            _applicationContext = applicationContext; 
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet]
        public IActionResult AddBarter()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddBarter(BarterAddViewModel model) 
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (ModelState.IsValid)
            {
                var barter = new Barter
                {
                    Name = model.Name,
                    Status = StatusType.Open,
                    Owner = user.UserName,
                    Description = model.Description,
                    StartPrice = model.StartPrice,
                    CreationTime = DateTime.Now,
                    ClosedTime = null
                };


                if (model.Photo != null)
                {
                    byte[] imageData = null;
                    using (var binaryReader = new BinaryReader(model.Photo.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)model.Photo.Length);
                    }
                    barter.Photo = imageData;
                }

                await _applicationContext.Barters.AddAsync(barter);
                await _applicationContext.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ShowMyBarters()
        {
            var barters = await _applicationContext.Barters.ToListAsync();
            var list = new List<BarterViewModel>();
            var user = await _userManager.GetUserAsync(HttpContext.User);

           

            foreach (var barter in barters)
            {
                if (user.UserName == barter.Owner)
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
            }
            return View(list);
        }
    }
}
