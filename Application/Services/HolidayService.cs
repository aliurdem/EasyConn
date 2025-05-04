using EasyConnect.Application.Services.Base;
using EasyConnect.Models.Dtos;
using EasyConnect.Models.Entities;
using EasyConnect.Models.Repositories;
using EasyConnect.Models.Utilities.Results;
using Mapster;

namespace EasyConnect.Application.Services
{
    public class HolidayService : Service<Holiday, HolidayDto>, IHolidayService
    {
        private readonly IRepository<BusinessProfile> _businessProfileRepository;

        public HolidayService(IRepository<Holiday> repository, IRepository<BusinessProfile> businessProfileRepository) : base(repository)
        {
            _businessProfileRepository = businessProfileRepository;
        }

        public IDataResult<List<HolidayDto>> GetByBusinessProfileId(int businessProfileId)
        {
            var holidays = _repository
                .Select(h => h.BusinessProfileId == businessProfileId)
                .OrderBy(h => h.Date)
                .ToList();

            return new SuccessDataResult<List<HolidayDto>>(holidays.Adapt<List<HolidayDto>>());
        }

        public override IDataResult<HolidayDto> Insert(HolidayDto dto)
        {
            var targetDate = dto.Date.Date;
            var today = DateTime.Today;

            var businessExists = _businessProfileRepository
    .Select(x => x.Id == dto.BusinessProfileId)
    .Any();

            if (!businessExists)
            {
                return new ErrorDataResult<HolidayDto>("İlgili işletme bulunamadı.");
            }
            // Geçmiş ya da bugünkü tarih kontrolü
            if (targetDate <= today)
            {
                return new ErrorDataResult<HolidayDto>("Geçmiş veya bugünkü tarihe tatil günü tanımlanamaz.");
            }

            // Aynı tarih daha önce tanımlanmış mı?
            var exists = _repository
                .Select(h => h.BusinessProfileId == dto.BusinessProfileId && h.Date.Date == targetDate)
                .Any();

            if (exists)
            {
                return new ErrorDataResult<HolidayDto>("Bu tarih için zaten bir tatil günü tanımlanmış.");
            }

            return base.Insert(dto);
        }
    }
}
