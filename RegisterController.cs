using BabasWells.customSession;
using BabasWells.Models;
using BabasWells.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BabasWells.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IService Service;
       
        public RegisterController(IService service)
        {
           this.Service = service;
        }
        public async Task<IActionResult> Index()
        {
            var cart = HttpContext.Session.GetSessionData<CartModel2>("cart");
            if(cart == null) { cart = new CartModel2(); }
            HttpContext.Session.SetSessionData<CartModel2>("cart", cart);
           
            var res = await Service.GetCatsAsync();
            return View(res);
        }
        [HttpGet]
        public async Task<IActionResult> items(int Id)
        {
            var cart = HttpContext.Session.GetSessionData<CartModel2>("cart");
          
           var items = await Service.GetItemsAsync(Id);
            return View(items);

        }

        [HttpGet]
        public async Task<IActionResult> GetToppings(int Id)
        {
           
            var Toppings = await Service.GetToppingsAsync(Id);
            return Json(Toppings);

        }

        [HttpPost]

        public async Task<IActionResult> AddToCart(int itemId,List<string> Toppings)
        {
            var cart = HttpContext.Session.GetSessionData<CartModel2>("cart");
            cart =  await Service.getCartModel(itemId, 1, cart);
            HttpContext.Session.SetSessionData<CartModel2>("cart", cart);
            
           

            return PartialView("_cart", cart);
        }
        [HttpGet]
        public async Task<IActionResult> PaymentPage()
        {
            var cart = HttpContext.Session.GetSessionData<CartModel2>("cart");
            return View(cart);
        }
        [HttpPost]
        public async Task<IActionResult> PaymentPage(string paymentType)
        {
            
            var cart = HttpContext.Session.GetSessionData<CartModel2>("cart");
            var res = await Service.PostOrderAsync(cart);
            HttpContext.Session.SetSessionData<CartModel2>("cart", null);
            return RedirectToAction("Index");
        }
       

    }
}
