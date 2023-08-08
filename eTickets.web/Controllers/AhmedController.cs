using eTickets.infrastructure.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eTickets.web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AhmedController : BaseController
    {
        private readonly IUserServices _userServices;

        public AhmedController(IUserServices userServices) : base(userServices)
        {
            _userServices = userServices;
        }

        // Implement the necessary methods and actions based on the requirements specified in the issue description
        // ...

        // Add the necessary methods and actions based on the requirements specified in the issue description
        // ...

    }
}