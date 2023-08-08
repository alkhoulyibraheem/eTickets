using Microsoft.AspNetCore.Mvc;
using eTickets.data;
using eTickets.data.Models;

namespace eTickets.web.Controllers
{
    public class PriceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PriceController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Price price)
        {
            _context.Prices.Add(price);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var price = _context.Prices.Find(id);
            return View(price);
        }

        [HttpPost]
        public IActionResult Update(Price price)
        {
            _context.Prices.Update(price);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var price = _context.Prices.Find(id);
            return View(price);
        }

        [HttpPost]
        public IActionResult Delete(Price price)
        {
            _context.Prices.Remove(price);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}