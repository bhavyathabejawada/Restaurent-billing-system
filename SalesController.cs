using BabasWells.Services;
using Microsoft.AspNetCore.Mvc;

namespace BabasWells.Controllers
{
    public class SalesController : Controller
    {
        private readonly IService Service;

        public SalesController(IService service)
        {
            this.Service = service;
        }
        public async  Task<IActionResult> Index()
        {
            var res = await Service.GetOrdersAsync();
            return View(res);
        }

        public async Task<IActionResult> Details(int id)
        {
            var res = await Service.GetSalesAsync(id);
            return View(res);
        }

        public async Task<IActionResult> Menu()
        {
            var res = await Service.GetItemsAsync();
            return View(res);
        }
    }
}
