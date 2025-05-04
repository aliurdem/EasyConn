using EasyConnect.Application.Services;
using EasyConnect.Controllers.Base;
using EasyConnect.Models.Dtos;
using EasyConnect.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EasyConnect.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WorkingHourController : BaseController<WorkingHour, WorkingHourDto>
    {
        private readonly IWorkingHourService _workingHourService;

        public WorkingHourController(IWorkingHourService workingHourService) : base(workingHourService)
        {
            _workingHourService = workingHourService;
        }

        [HttpPost("Upsert")]
        public IActionResult Upsert([FromBody] WorkingHourDto dto)
        {
            var result = _workingHourService.Insert(dto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetByBusinessProfileId/{businessProfileId}")]
        public IActionResult GetByBusinessProfileId(int businessProfileId)
        {
            var result = _workingHourService.GetByBusinessProfileId(businessProfileId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
