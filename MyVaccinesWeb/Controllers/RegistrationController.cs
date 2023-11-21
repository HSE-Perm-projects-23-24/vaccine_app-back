using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyVaccinesWeb.Services.CountriesService;
using MyVaccinesWeb.Services.RegistrationService;

namespace MyVaccinesWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddUserAsync(User user)
        {
            bool result = await _registrationService.AddUserAsync(user);
            if (result is false)
                return BadRequest();
            return Ok(result);
        }
    }
}
