using eTickets.core.Dto;
using eTickets.core.Enums;
using eTickets.infrastructure.Services.Actor;
using Microsoft.AspNetCore.Mvc;
using static eTickets.core.DataTable.DataTable;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using eTickets.infrastructure.Services.Users;

namespace eTickets.web.Controllers
{
	[Authorize(Roles = "Admin")]
	public class ActorController : BaseController
    {
        private readonly IActorServices _ActorServices;

        public ActorController(IActorServices ActorServices ,IUserServices userService) : base(userService)
		{
            _ActorServices = ActorServices;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public async Task<JsonResult> GetDataTableData(Request request, string x)
        {
            return Json(await _ActorServices.GetAllForDataTable(request));
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
        public async Task<IActionResult> Create(CreateActorDto dto)
        {
            dto.User.ImageURL = dto.ImageURl;
            dto.User.UserType = UserType.Actor;
            dto.User.Status = status.Active;
            if (ModelState.IsValid)
            {
                var users = await _ActorServices.Create(dto);
				TempData["msg"] = "s:Actor Created Succsfuly !";
				return RedirectToAction("Index");
            }
			TempData["msg"] = "e:Actor Created Not Succsfuly !";
			return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int Id)
        {
            var data = await _ActorServices.Get(Id);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateActorDto dto)
        {
            if (ModelState.IsValid)
            {
                var users = await _ActorServices.Update(dto);
				TempData["msg"] = "s:Actor Updated Succsfuly !";
				return RedirectToAction("Index");
            }
			TempData["msg"] = "e:Actor Updated Not Succsfuly !";

			return View(dto);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            await _ActorServices.Delete(Id);
			TempData["msg"] = "s:Actor Deleted Succsfuly !";
			return RedirectToAction("Index");
        }
    }
}
