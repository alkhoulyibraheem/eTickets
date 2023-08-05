using eTickets.core.Enums;
using eTickets.data.Models;
using eTickets.infrastructure.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace eTickets.web.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IUserServices _UserServices;
        protected string userType;
        protected string userId;

        public BaseController(IUserServices userService)
        {
			_UserServices = userService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var user = _UserServices.GetUserByUsername(userName);
                userId = user.Id;
                userType = user.UserType;
				ViewBag.Id = user.Id;
				ViewData["fullName"] = user.FullName;
                ViewData["image"] = user.ImageURL;
				ViewData["UserType"] = user.UserType.ToString();
            }
        }
    }
}
