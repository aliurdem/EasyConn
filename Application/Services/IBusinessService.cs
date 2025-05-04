using EasyConnect.Application.Services.Base;
using EasyConnect.Models.Dtos;
using EasyConnect.Models.Entities;
using EasyConnect.Models.Utilities.Results;

namespace EasyConnect.Application.Services
{
    public interface IBusinessService : IService<BusinessProfile, BusinessProfileDto>
    {
        IDataResult<List<ServiceDto>> GetServicesForBusinessProfileId(int businessProfileId);
        IDataResult<string> AssignServicesToBusiness(BusinessProfileServiceAssignDto dto);
        IDataResult<List<BusinessProfileDto>> GetBusinessForServiceIdAndProvinceCode(int serviceId, int provinceCode);
    }
}
