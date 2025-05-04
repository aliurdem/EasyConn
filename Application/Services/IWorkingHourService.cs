using EasyConnect.Application.Services.Base;
using EasyConnect.Models.Dtos;
using EasyConnect.Models.Entities;
using EasyConnect.Models.Utilities.Results;

namespace EasyConnect.Application.Services
{
    public interface IWorkingHourService : IService<WorkingHour, WorkingHourDto>
    {
        IDataResult<List<WorkingHourDto>> GetByBusinessProfileId(int businessProfileId);

    }
}
