using EasyConnect.Application.Services.Base;
using EasyConnect.Controllers.Base;
using EasyConnect.Models.Dtos;
using EasyConnect.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EasyConnect.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class StaffController : BaseController<Staff, StaffDto>
    {
        public StaffController(IService<Staff, StaffDto> manager) : base(manager)
        {
        }
    }
}
