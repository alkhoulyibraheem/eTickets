using eTickets.core.Dto;
using eTickets.data;
using eTickets.infrastructure.Services.Category;
using eTickets.infrastructure.Services.Cinema;
using eTickets.infrastructure.Services.Movie;
using eTickets.infrastructure.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using static eTickets.core.DataTable.DataTable;

namespace eTickets.web.Controllers
{
	[Authorize(Roles = "Admin")]
	public class MovieController : BaseController
    {
        private readonly IMovieServices _MovieServices;
		private readonly ApplicationDbContext _db;

		public MovieController(ApplicationDbContext db , IMovieServices MovieServices, IUserServices userService) : base(userService)
		{
			_MovieServices = MovieServices;
            _db = db;
        }


        public IActionResult Index()
        {
			return View();
        }

		[HttpPost]

		public async Task<JsonResult> GetDataTableData(Request request, string x)
		{
			return Json(await _MovieServices.GetAllForDataTable(request));
		}

		//public async Task<JsonResult> GetUserData(Pagination pagination, Query query)
		//{
		//	var result = await _UserServices.GetAll(pagination, query);
		//	return Json(result);
		//}


		[HttpGet]
		public IActionResult Create()
		{
			ViewData["Actors"] = new SelectList(_db.Actors.Where(x => !x.IsDelete).ToList(), "Id", "Name");
			ViewData["Cinemas"] = new SelectList(_db.Cinemas.Where(x => !x.IsDelete).ToList(), "Id", "Name");
			ViewData["Directors"] = new SelectList(_db.Directors.Where(x => !x.IsDelete).ToList(), "Id", "Name");
			ViewData["Catgerys"] = new SelectList(_db.Catgerys.Where(x => !x.IsDelete).ToList(), "Id", "Name");
			return View();
		}

        [HttpPost]
		public async Task<IActionResult> Create(CreateMovieDto dto)
		{
            if(ModelState.IsValid && dto.StartDate < dto.EndDate) 
            {
				var users = await _MovieServices.Create(dto);
                TempData["msg"] = "s:Movie Created Succsfuly !";
                return RedirectToAction("Index");
			}
            TempData["msg"] = "e:Movie Created Not Succsfuly !";
            if ( dto.StartDate > dto.EndDate)
            {
                TempData["msg"] = "e:enter right date !";
            }
            ViewData["Actors"] = new SelectList(_db.Actors.Where(x => !x.IsDelete).ToList(), "Id", "Name");
            ViewData["Cinemas"] = new SelectList(_db.Cinemas.Where(x => !x.IsDelete).ToList(), "Id", "Name");
            ViewData["Directors"] = new SelectList(_db.Directors.Where(x => !x.IsDelete).ToList(), "Id", "Name");
            ViewData["Catgerys"] = new SelectList(_db.Catgerys.Where(x => !x.IsDelete).ToList(), "Id", "Name");
            return View(dto);
		}

        [HttpGet]
        public async Task<IActionResult> Update(int Id)
        {
			ViewData["Actors"] = new SelectList(_db.Actors.Where(x => !x.IsDelete).ToList(), "Id", "Name");
			ViewData["Cinemas"] = new SelectList(_db.Cinemas.Where(x => !x.IsDelete).ToList(), "Id", "Name");
			ViewData["Directors"] = new SelectList(_db.Directors.Where(x => !x.IsDelete).ToList(), "Id", "Name");
			ViewData["Catgerys"] = new SelectList(_db.Catgerys.Where(x => !x.IsDelete).ToList(), "Id", "Name");
			var data = await _MovieServices.Get(Id);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateMovieDto dto)
        {
            if (ModelState.IsValid && dto.StartDate < dto.EndDate)
            {
                var users = await _MovieServices.Update(dto);
                TempData["msg"] = "s:Movie Updated Succsfuly !";
                return RedirectToAction("Index");
            }
            TempData["msg"] = "e:Movie Updated Not Succsfuly !";
            if (dto.StartDate > dto.EndDate)
            {
                TempData["msg"] = "e:enter right date !";
            }
            ViewData["Actors"] = new SelectList(_db.Actors.Where(x => !x.IsDelete).ToList(), "Id", "Name");
            ViewData["Cinemas"] = new SelectList(_db.Cinemas.Where(x => !x.IsDelete).ToList(), "Id", "Name");
            ViewData["Directors"] = new SelectList(_db.Directors.Where(x => !x.IsDelete).ToList(), "Id", "Name");
            ViewData["Catgerys"] = new SelectList(_db.Catgerys.Where(x => !x.IsDelete).ToList(), "Id", "Name");
            return View(dto);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
			await _MovieServices.Delete(Id);
            TempData["msg"] = "s:Movie Deleted Succsfuly !";
            return RedirectToAction("Index");
        }


    }
}
