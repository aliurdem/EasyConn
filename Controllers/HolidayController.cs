using EasyConnect.Application.Services;
using EasyConnect.Application.Services.Base;
using EasyConnect.Controllers.Base;
using EasyConnect.Models.Dtos;
using EasyConnect.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EasyConnect.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HolidayController : BaseController<Holiday, HolidayDto>
    {
        private readonly IHolidayService _holidayService;

        public HolidayController(IHolidayService holidayService) : base(holidayService)
        {
            _holidayService = holidayService;
        }

        [HttpGet("GetByBusinessProfileId/{businessProfileId}")]
        public IActionResult GetByBusinessProfileId(int businessProfileId)
        {
            var result = _holidayService.GetByBusinessProfileId(businessProfileId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
