using eTickets.core.Dto;
using eTickets.core.Enums;
using eTickets.data;
using eTickets.data.Models;
using eTickets.infrastructure.Services.Customers;
using eTickets.infrastructure.Services.Home;
using eTickets.infrastructure.Services.Movie;
using eTickets.infrastructure.Services.Users;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using static eTickets.core.DataTable.DataTable;
using static Humanizer.On;

namespace eTickets.web.Controllers
{
	public class HomeController : BaseController
    {


		

		private readonly IHomeServices _HomeServices;
		


		public HomeController(IHomeServices HomeServices, IUserServices userService) : base(userService)
		{
			_HomeServices = HomeServices;
		}

        public async Task<IActionResult> Index()
        {
            var data = await _HomeServices.MoviesList();
            return View(data);
        }

		public async Task<IActionResult> profile(int Id)
        {
			var data = await _HomeServices.MovieProFile(Id); 
			return View(data);
        }


		
		[Authorize]
		public async Task<IActionResult> buying(int Id)
		{
			var UserType = ViewData["UserType"] as string;

			if (UserType == "Admin")
			{
				TempData["msg"] = "e: not allowed you are admin !";
				return RedirectToAction("Index");
			}
			var UserId = ViewBag.Id as string;
			await _HomeServices.buying(Id, UserId);
            TempData["msg"] = "s:Movie buying is Done !";
            return RedirectToAction("Index");

		}

		public async Task<IActionResult> OrderList()
		{
			var userId = ViewBag.Id as string;

			var data = await _HomeServices.OrderList(userId);

			return View(data);
		}

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
				var users = await _HomeServices.CreateCustomer(dto);
                TempData["msg"] = "s:Sign up Succsfuly !";
                return RedirectToAction("Index");
			}
            TempData["msg"] = "e:Sign up Not Succsfuly !";
            return View(dto);
		}

		public async Task<IActionResult> InitRoles()
		{
			await _HomeServices.rolos();
			return RedirectToAction("Index");

		}
		[Authorize(Roles = "Customer")]
		[HttpGet]
		public IActionResult Rating(int Id)
		{
			var rat = new RatingDto();
			rat.Id = Id;
			return View(rat);

		}
		[Authorize]
		[HttpPost]
		public async Task<IActionResult> Rating(RatingDto dto)
		{
			var UserType = ViewData["UserType"] as string;

			if (UserType == "Admin")
			{
				TempData["msg"] = "e: not allowed you are admin !";
				return RedirectToAction("Index");
			}
			await _HomeServices.Rating(dto);
            TempData["msg"] = "s:Rating Succsfuly !";
            return RedirectToAction("Index");

		}
        [Authorize(Roles = "Admin")]
        public IActionResult AllOrders()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetDataTableData(Request request, string x)
        {
            return Json(await _HomeServices.GetAllForDataTable(request));
        }


		
		


	}
}
