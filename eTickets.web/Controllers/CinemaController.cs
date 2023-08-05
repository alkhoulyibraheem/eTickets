using eTickets.core.Dto;
using eTickets.infrastructure.Services.Category;
using eTickets.infrastructure.Services.Cinema;
using eTickets.infrastructure.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Threading.Tasks;
using static eTickets.core.DataTable.DataTable;

namespace eTickets.web.Controllers
{
	[Authorize(Roles = "Admin")]
	public class CinemaController : BaseController
    {
        private readonly ICinemaServices _CinemaServices;

        public CinemaController(ICinemaServices CinemaServices, IUserServices userService) : base(userService)
		{
            _CinemaServices = CinemaServices;
        }


        public IActionResult Index()
        {
			return View();
        }

		[HttpPost]

		public async Task<JsonResult> GetDataTableData(Request request, string x)
		{
			return Json(await _CinemaServices.GetAllForDataTable(request));
		}

		//public async Task<JsonResult> GetUserData(Pagination pagination, Query query)
		//{
		//	var result = await _UserServices.GetAll(pagination, query);
		//	return Json(result);
		//}


		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

        [HttpPost]
		public async Task<IActionResult> Create(CreateCinemaDto dto)
		{
            if(ModelState.IsValid) 
            {
				var users = await _CinemaServices.Create(dto);
				TempData["msg"] = "s:Cinema Created Succsfuly !";
				return RedirectToAction("Index");
			}
			TempData["msg"] = "e:Cinema Created Not Succsfuly !";
			return View(dto);
		}

        [HttpGet]
        public async Task<IActionResult> Update(int Id)
        {
            var data = await _CinemaServices.Get(Id);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCinemaDto dto)
        {
            if (ModelState.IsValid)
            {
                var users = await _CinemaServices.Update(dto);
				TempData["msg"] = "s:Cinema Updated Succsfuly !";
				return RedirectToAction("Index");
            }
			TempData["msg"] = "e:Cinema Updated Not Succsfuly !";
			return View(dto);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
			await _CinemaServices.Delete(Id);
			TempData["msg"] = "s:Cinema Deleted Succsfuly !";
			return RedirectToAction("Index");
        }


    }
}
