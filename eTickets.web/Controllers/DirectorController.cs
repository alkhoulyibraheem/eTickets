using eTickets.core.Dto;
using eTickets.core.Enums;
using eTickets.infrastructure.Services.Actor;
using eTickets.infrastructure.Services.Category;
using eTickets.infrastructure.Services.Director;
using eTickets.infrastructure.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Threading.Tasks;
using static eTickets.core.DataTable.DataTable;

namespace eTickets.web.Controllers
{
	[Authorize(Roles = "Admin")]
	public class DirectorController : BaseController
    {
        private readonly IDirectorServices _DirectorServices;

        public DirectorController(IDirectorServices DirectorServices, IUserServices userService) : base(userService)
		{
            _DirectorServices = DirectorServices;
        }


        public IActionResult Index()
        {
			return View();
        }

		[HttpPost]

		public async Task<JsonResult> GetDataTableData(Request request, string x)
		{
			return Json(await _DirectorServices.GetAllForDataTable(request));
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
		public async Task<IActionResult> Create(CreateDirectorDto dto)
		{
            dto.User.ImageURL = dto.ImageURl;
            dto.User.UserType = UserType.director;
            dto.User.Status = status.Active;
            if (ModelState.IsValid) 
            {
				var users = await _DirectorServices.Create(dto);
				TempData["msg"] = "s:Director Created Succsfuly !";
				return RedirectToAction("Index");
			}
			TempData["msg"] = "e:Director Created Not Succsfuly !";
			return View(dto);
		}

        [HttpGet]
        public async Task<IActionResult> Update(int Id)
        {
            var data = await _DirectorServices.Get(Id);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateDirectorDto dto)
        {
            if (ModelState.IsValid)
            {
                var users = await _DirectorServices.Update(dto);
				TempData["msg"] = "s:Director Updated Succsfuly !";
				return RedirectToAction("Index");
            }
			TempData["msg"] = "e:Director Updated Not Succsfuly !";
			return View(dto);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
			await _DirectorServices.Delete(Id);
			TempData["msg"] = "s:Director Deleted Succsfuly !";
			return RedirectToAction("Index");
        }


    }
}
