using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyVaccinesWeb.Services.AdminsService;
using MyVaccinesWeb.Services.UserService;

namespace MyVaccinesWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly IAdminsService _adminsService;

        public AdminsController(IAdminsService adminsService)
        {
            _adminsService = adminsService;
        }

        [HttpPost]
        public ActionResult<bool> CheckAdmin(Admin admin)
        {
            bool result = _adminsService.CheckAdmin(admin);
            if (result is false)
                return BadRequest();
            return Ok(result);
        }
    }
}
