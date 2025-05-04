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
    public class AppointmentController : BaseController<Appointment, AppointmentDto>
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService) : base(appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet("GetByUserId/{userId}")]
        public IActionResult GetByUserId(string userId)
        {
            var result = _appointmentService.GetByUserId(userId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetByFilter")]
        public IActionResult GetByFilter([FromQuery] int? businessProfileId, [FromQuery] int? staffId)
        {
            var result = _appointmentService.GetByFilter(businessProfileId, staffId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPatch("Approve/{id}")]
        public IActionResult Approve(int id)
        {
            var result = _appointmentService.ApproveAppointment(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
