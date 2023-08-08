using eTickets.core.Dto;
using eTickets.core.Enums;
using eTickets.infrastructure.Services.Price;
using Microsoft.AspNetCore.Mvc;
using static eTickets.core.DataTable.DataTable;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using eTickets.infrastructure.Services.Users;

namespace eTickets.web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PriceController : BaseController
    {
        private readonly IPriceServices _PriceServices;

        public PriceController(IPriceServices PriceServices ,IUserServices userService) : base(userService)
        {
            _PriceServices = PriceServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetDataTableData(Request request, string x)
        {
            return Json(await _PriceServices.GetAllForDataTable(request));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePriceDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            await _PriceServices.Create(dto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _PriceServices.GetById(id);
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditPriceDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            await _PriceServices.Update(dto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _PriceServices.Delete(id);
            return RedirectToAction("Index");
        }
    }
}