using CommodityExchange.Context;
using CommodityExchange.Enums;
using CommodityExchange.Models;
using CommodityExchange.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;

namespace CommodityExchange.Controllers
{
    public class OfferController : Controller
    {
        private readonly ApplicationContext _applicationContext;
        private readonly UserManager<User> _userManager;

        public OfferController(
            ApplicationContext applicationContext,
            UserManager<User> userManager) 
        {
            _applicationContext = applicationContext;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> AddOffer(int barterId)
        {
            if(User.Identity.IsAuthenticated)
            {
                var selectedBarter = await _applicationContext.Barters.FirstOrDefaultAsync(barter => barter.Id == barterId);
                var user = await _userManager.GetUserAsync(HttpContext.User);

                if (selectedBarter == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                var model = new OfferAddViewModel
                {
                    BarterId = selectedBarter.Id,
                    BarterName = selectedBarter.Name,
                    StartPrice = selectedBarter.StartPrice,
                    OfferorId = user.Id,
                    Offeror = user.UserName,
                    Status = Enums.OfferStatus.Active
                };


                ViewBag.BarterPhoto = selectedBarter.Photo;

                return View(model);

            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> AddOffer (OfferAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var offer = new Offer
                {
                    OfferorId = model.OfferorId,
                    BarterId = model.BarterId,
                    OfferPrice = model.OfferPrice,
                    Status = model.Status
                };

                await _applicationContext.Offers.AddAsync(offer);
                await _applicationContext.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }

            var selectedBarter = await _applicationContext.Barters.FirstOrDefaultAsync(barter => barter.Id == model.BarterId);
            ViewBag.BarterPhoto = selectedBarter.Photo;

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ShowOffers (int barterId)
        {
            var offers = await _applicationContext.Offers.ToListAsync();
            var list = new List<OfferShowViewModel>();
            var selectedBarter = await _applicationContext.Barters.FirstOrDefaultAsync(barter => barter.Id == barterId);
           

            foreach (var offer in offers)
            {
                var offeror = await _applicationContext.Users.FirstOrDefaultAsync(user => user.Id == offer.OfferorId);

                var model = new OfferShowViewModel
                {
                    OfferId = offer.Id,
                    BarterName = selectedBarter.Name,
                    Photo = selectedBarter.Photo,
                    Offeror = offeror.UserName,
                    StartPrice = selectedBarter.StartPrice,
                    OfferPrice = offer.OfferPrice,
                    Status = offer.Status,
                };

                if (selectedBarter.Id == offer.BarterId) 
                {
                    list.Add(model);
                }
            }
            return View(list);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AcceptOffer(int offerId)
        {
            var offers = await _applicationContext.Offers.ToListAsync();
            var acceptedOffer = offers
                .Where(offer => offer.Id == offerId)
                .FirstOrDefault();

            var closedBarter = await _applicationContext.Barters
                .Where(barter => barter.Id == acceptedOffer.BarterId)
                .FirstOrDefaultAsync();

            var closedBarterOffers = new List<Offer>();

            if (closedBarter != null)
            {
                closedBarter.ClosedTime = DateTime.Now;
                closedBarter.Status = StatusType.Closed;
            }
            

            foreach (var offer in offers)
            {
                var model = new Offer
                {
                    Id = offer.Id,
                    OfferorId = offer.OfferorId,
                    BarterId = offer.BarterId,
                    OfferPrice = offer.OfferPrice,
                    Status = offer.Status
                };

                if (closedBarter.Id == offer.BarterId)
                {
                    closedBarterOffers.Add(model);
                }
            }

            foreach (var offer in closedBarterOffers) 
            {
                var updatedOffer = await _applicationContext.Offers
                    .Where(updOffer => updOffer.Id == offer.Id)
                    .FirstOrDefaultAsync();


                if (updatedOffer.Id == acceptedOffer.Id)
                {
                    updatedOffer.Status = OfferStatus.Accepted;
                }
                else
                {
                    updatedOffer.Status = OfferStatus.Closed;
                }

                await _applicationContext.SaveChangesAsync();
            }

    
            return RedirectToAction("Index", "Home");
        }
    }
}
