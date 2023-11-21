using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyVaccinesWeb.Services.DoneProceduresService;

namespace MyVaccinesWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoneProceduresController : ControllerBase
    {
        private readonly IDoneProceduresService _doneProceduresService;

        public DoneProceduresController(IDoneProceduresService doneProceduresService)
        {
            _doneProceduresService = doneProceduresService;
        }

        [HttpGet("{userId}")]
        public async Task<List<ProceduresDone>?> GetAllDoneProceduresAsync(int userId)
        {
            return await _doneProceduresService.GetAllDoneProceduresAsync(userId);
        }

        [HttpPost("{procedureId}/{fullOrNot}/{actualDate}")]
        public async Task<ActionResult<bool>> AddDoneProcedureAsync(string procedureId, bool fullOrNot, DateTime actualDate)
        {
            bool result = await _doneProceduresService.AddDoneProcedureAsync(procedureId, fullOrNot, actualDate);
            if (result is false)
                return BadRequest();
            return Ok(result);
        }
    }
}
