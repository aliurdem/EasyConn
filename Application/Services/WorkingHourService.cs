using EasyConnect.Application.Services.Base;
using EasyConnect.Models.Dtos;
using EasyConnect.Models.Entities;
using EasyConnect.Models.Repositories;
using EasyConnect.Models.Utilities.Results;
using Mapster;

namespace EasyConnect.Application.Services
{
    public class WorkingHourService : Service<WorkingHour, WorkingHourDto>, IWorkingHourService
    {
        public WorkingHourService(IRepository<WorkingHour> repository) : base(repository)
        {
        }

        public override IDataResult<WorkingHourDto> Insert(WorkingHourDto dto)
        {
            var existing = _repository
                .Select(w => w.BusinessProfileId == dto.BusinessProfileId && w.DayOfWeek == dto.DayOfWeek)
                .FirstOrDefault();

            if (existing != null)
            {
                existing.StartTime = dto.StartTime;
                existing.EndTime = dto.EndTime;

                _repository.Update(existing);
                _repository.SaveChanges();

                return new SuccessDataResult<WorkingHourDto>(existing.Adapt<WorkingHourDto>(), "Güncellendi.");
            }
            else
            {
                return base.Insert(dto);
            }
        }

        public IDataResult<List<WorkingHourDto>> GetByBusinessProfileId(int businessProfileId)
        {
            var workingHours = _repository
                .Select(w => w.BusinessProfileId == businessProfileId)
                .OrderBy(w => w.DayOfWeek)
                .ToList();

            return new SuccessDataResult<List<WorkingHourDto>>(workingHours.Adapt<List<WorkingHourDto>>());
        }

    }

}
