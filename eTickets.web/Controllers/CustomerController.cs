using eTickets.core.Dto;
using eTickets.core.Enums;
using eTickets.infrastructure.Services.Customers;
using eTickets.infrastructure.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Threading.Tasks;
using static eTickets.core.DataTable.DataTable;

namespace eTickets.web.Controllers
{
	[Authorize(Roles = "Admin")]
	public class CustomerController : BaseController
    {
        private readonly ICustomerServices _CustomerServices;

        public CustomerController(ICustomerServices CustomerServices, IUserServices userService) : base(userService)
		{
            _CustomerServices = CustomerServices;
        }


        public IActionResult Index()
        {
			return View();
        }

		[HttpPost]

		public async Task<JsonResult> GetDataTableData(Request request, string x)
		{
			return Json(await _CustomerServices.GetAllForDataTable(request));
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
		public async Task<IActionResult> Create(CreateCustomerDto dto)
		{
            dto.User.ImageURL = dto.ImageURl;
            dto.User.UserType = UserType.Customer;
            dto.User.Status = status.Active;
            if (ModelState.IsValid) 
            {
				var users = await _CustomerServices.Create(dto);
				if (users == -1)
				{
					TempData["msg"] = "e:Image Size so much !";
					return View(dto);
				}
				TempData["msg"] = "s:Customer Created Succsfuly !";
				return RedirectToAction("Index");
			}
			TempData["msg"] = "e:Customer Created Not Succsfuly !";

			return View(dto);
		}

        [HttpGet]
        public async Task<IActionResult> Update(int Id)
        {
            var data = await _CustomerServices.Get(Id);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCustomerDto dto)
        {
            if (ModelState.IsValid)
            {
                var users = await _CustomerServices.Update(dto);
				if (users == -1)
				{
					TempData["msg"] = "e:Image Size so much !";
					return View(dto);
				}
				TempData["msg"] = "s:Customer Updated Succsfuly !";
				return RedirectToAction("Index");
            }
			TempData["msg"] = "e:Customer Update Not Succsfuly !";
			return View(dto);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
			await _CustomerServices.Delete(Id);
			TempData["msg"] = "s:Customer Deleted Succsfuly !";
			return RedirectToAction("Index");
        }


    }
}
