using EasyConnect.Application.Services.Base;
using EasyConnect.Models.Dtos;
using EasyConnect.Models.Entities;
using EasyConnect.Models.Utilities.Results;

namespace EasyConnect.Application.Services
{
    public interface IHolidayService : IService<Holiday, HolidayDto>
    {
        IDataResult<List<HolidayDto>> GetByBusinessProfileId(int businessProfileId);

    }
}
