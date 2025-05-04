using EasyConnect.Application.Services;
using EasyConnect.Application.Services.Base;
using EasyConnect.Controllers.Base;
using EasyConnect.Models.Dtos;
using EasyConnect.Models.Entities;
using EasyConnect.Models.Utilities.Filtering;
using EasyConnect.Models.Utilities.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace EasyConnect.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BusinessController : BaseController<BusinessProfile, BusinessProfileDto>
    {
        private readonly IBusinessService _businessService;

        public BusinessController(IBusinessService service) : base(service)
        {
            _businessService = service;

        }

        [HttpGet("GetListForServiceAndProvince/{serviceId}/{provinceId}")]
        public async Task<IActionResult> GetListForUser(int serviceId,int provinceId)
        {
            var result = _businessService.GetBusinessForServiceIdAndProvinceCode(serviceId, provinceId);

            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result);
        }

        [HttpPost("AssignServices")]
        public IActionResult AssignServices([FromBody] BusinessProfileServiceAssignDto dto)
        {
            var result = _businessService.AssignServicesToBusiness(dto);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetServicesForBusinessProfile/{businessProfileId}")]
        public IActionResult GetServicesForBusinessProfile(int businessProfileId)
        {
            var result = _businessService.GetServicesForBusinessProfileId(businessProfileId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

    }
}
