using eTickets.core.Dto;
using eTickets.infrastructure.Services.Category;
using eTickets.infrastructure.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Threading.Tasks;
using static eTickets.core.DataTable.DataTable;

namespace eTickets.web.Controllers
{
	[Authorize(Roles = "Admin")]
	public class CategoryController : BaseController
    {
        private readonly ICategoryServices _CategoryServices;

        public CategoryController(ICategoryServices CategoryServices, IUserServices userService) : base(userService)
		{
            _CategoryServices = CategoryServices;
        }


        public IActionResult Index()
        {
			return View();
        }

		[HttpPost]

		public async Task<JsonResult> GetDataTableData(Request request, string x)
		{
			return Json(await _CategoryServices.GetAllForDataTable(request));
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
		public async Task<IActionResult> Create(CreateCategoryDto dto)
		{
            if(ModelState.IsValid) 
            {
				var users = await _CategoryServices.Create(dto);
				TempData["msg"] = "s:Category Created Succsfuly !";
				return RedirectToAction("Index");
			}
			TempData["msg"] = "e:Category Created Not Succsfuly !";
			return View(dto);
		}

        [HttpGet]
        public async Task<IActionResult> Update(int Id)
        {
            var data = await _CategoryServices.Get(Id);
			
			return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCategoryDto dto)
        {
            if (ModelState.IsValid)
            {
                var users = await _CategoryServices.Update(dto);
				TempData["msg"] = "s:Category Updated Succsfuly !";
				return RedirectToAction("Index");
            }
			TempData["msg"] = "e:Category Updated Not  Succsfuly !";

			return View(dto);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
			await _CategoryServices.Delete(Id);
			TempData["msg"] = "s:Category Deleted Succsfuly !";
			return RedirectToAction("Index");
        }


    }
}
