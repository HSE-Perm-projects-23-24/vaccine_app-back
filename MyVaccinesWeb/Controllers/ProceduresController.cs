using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyVaccinesWeb.Models;
using MyVaccinesWeb.My_Classes;
using MyVaccinesWeb.Services.ProceduresService;
using System.Collections;

namespace MyVaccinesWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProceduresController : ControllerBase
    {
        private readonly IProceduresService _proceduresService;

        public ProceduresController(IProceduresService proceduresService)
        {
            _proceduresService = proceduresService;
        }

        [HttpGet("{usrId}")]
        public async Task<List<MyProcedure>?> GetAllProceduresAsync(int usrId)
        {
            return await _proceduresService.GetAllProceduresAsync(usrId);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteProcedureAsync(int id)
        {
            bool result = await _proceduresService.DeleteProcedureAsync(id);
            if (result is false)
                return BadRequest();
            return Ok(result);
        }

        [HttpPost("{userId}")]
        public async Task<ActionResult<bool>> AddProcedureAsync(int userId, MyProceduresClass myProcedure)
        {
            bool result = await _proceduresService.AddProcedureAsync(userId, myProcedure);
            if (result is false)
                return BadRequest();
            return Ok(result);
        }

        [HttpGet("{id}/getSingle")]
        public async Task<MyProcedure?> GetSingleProcedureAsync(int id)
        {
            return await _proceduresService.GetSingleProcedureAsync(id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> GetSingleProcedureAsync(int id, MyProceduresClass myProcedure)
        {
            bool result = await _proceduresService.UpdateProcedureAsync(id, myProcedure);
            if (result is false)
                return BadRequest();
            return Ok(result);
        }
    }
}
