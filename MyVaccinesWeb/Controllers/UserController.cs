using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyVaccinesWeb.Services.CountriesService;
using MyVaccinesWeb.Services.UserService;

namespace MyVaccinesWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public ActionResult<bool> CheckUser(User user)
        {
            bool result = _userService.CheckUser(user);
            if (result is false)
                return BadRequest();
            return Ok(result);
        }

        [HttpGet("{userName}")]
        public async Task<List<User>?> GetUserAsync(string userName)
        {
            return await _userService.GetUserAsync(userName);
        }
    }
}
