using eTickets.core.Dto;
using eTickets.infrastructure.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using static eTickets.core.DataTable.DataTable;

namespace eTickets.web.Controllers
{
	[Authorize(Roles = "Admin")]
	public class UserController : BaseController
    {


		public UserController(IUserServices userService) : base(userService)
		{
		}

		public IActionResult Index()
        {
			return View();
        }

		[HttpPost]

		public async Task<JsonResult> GetDataTableData(Request request, string x)
		{
			return Json(await _UserServices.GetAllForDataTable(request));
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
		public async Task<IActionResult> Create(CreateUserDto dto)
		{
            if(ModelState.IsValid) 
            {
				var users = await _UserServices.Create(dto);
				TempData["msg"] = "s:User Created Succsfuly !";
				return RedirectToAction("Index");
			}
			TempData["msg"] = "e:User Created Not Succsfuly !";
			return View(dto);
		}

        [HttpGet]
        public async Task<IActionResult> Update(string Id)
        {
            var data = await _UserServices.Get(Id);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateUserDto dto)
        {
            if (ModelState.IsValid)
            {
                var users = await _UserServices.Update(dto);
				TempData["msg"] = "s:User Updated Succsfuly !";
				return RedirectToAction("Index");
            }
			TempData["msg"] = "e:User Updated Not Succsfuly !";

			return View(dto);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(string Id)
        {
			await _UserServices.Delete(Id);

			TempData["msg"] = "s:User Deleted Succsfuly !";

			return RedirectToAction("Index");
        }
		

	}
}
