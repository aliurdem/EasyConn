using EasyConnect.Application.Services.Base;
using EasyConnect.Core.Exceptions;
using EasyConnect.Models.Dtos;
using EasyConnect.Models.Entities;
using EasyConnect.Models.Repositories;
using EasyConnect.Models.Utilities.Results;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace EasyConnect.Application.Services
{
    public class BusinessService : Service<BusinessProfile, BusinessProfileDto>, IBusinessService
    {

        private readonly IRepository<WorkingHour> _workingHourRepository;
        private readonly IRepository<Service> _serviceRepository;

        public BusinessService(IRepository<BusinessProfile> repository, IRepository<WorkingHour> workingHourRepository, IRepository<Service> serviceRepository) : base(repository)
        {
            _workingHourRepository = workingHourRepository;
            _serviceRepository = serviceRepository;

        }
        public IDataResult<List<ServiceDto>> GetServicesForBusinessProfileId(int businessProfileId)
        {
            var business = _repository
                .Select(x => x.Id == businessProfileId)
                .Include(x => x.Services)
                .FirstOrDefault();

            if (business == null)
                return new ErrorDataResult<List<ServiceDto>>("İşletme bulunamadı.");

            var serviceDtos = business.Services.Adapt<List<ServiceDto>>();
            return new SuccessDataResult<List<ServiceDto>>(serviceDtos);
        }

        public IDataResult<List<BusinessProfileDto>> GetBusinessForServiceIdAndProvinceCode(int serviceId, int provinceCode)
        {
            var business = _repository
        .Select(x => x.ProvinceCode == provinceCode && x.Services.Any(s => s.Id == serviceId))
        .Include(x => x.Services).ToList();

            var dtoList = business.Select(b => new BusinessProfileDto
            {
                Id = b.Id,
                BusinessName = b.BusinessName,
                Phone = b.Phone,
                Address = b.Address,
                ProvinceCode = b.ProvinceCode
            }).ToList();

            return new SuccessDataResult<List<BusinessProfileDto>>(dtoList);

        }

        public override IDataResult<BusinessProfileDto> Insert(BusinessProfileDto dto)
        {
            // 1. Önce BusinessProfile'ı oluştur
            var result = base.Insert(dto);
            if (!result.Success) return result;

            var createdBusiness = result.Data;

            // 2. Haftanın 7 günü için 09:00 – 18:00 arası çalışma saati hazırla
            var workingHours = Enum.GetValues(typeof(DayOfWeek))
                .Cast<DayOfWeek>()
                .Select(day => new WorkingHour
                {
                    BusinessProfileId = createdBusiness.Id,
                    DayOfWeek = day,
                    StartTime = new TimeSpan(9, 0, 0),  // 09:00:00
                    EndTime = new TimeSpan(18, 0, 0)    // 18:00:00
                })
                .ToList();

            // 3. Veritabanına ekle
            _workingHourRepository.InsertRange(workingHours);
            _workingHourRepository.SaveChanges();

            return result;
        }
        public IDataResult<string> AssignServicesToBusiness(BusinessProfileServiceAssignDto dto)
        {
            var business = _repository
                .Select(x => x.Id == dto.BusinessProfileId, asTracking: true)
                .Include(x => x.Services)
                .FirstOrDefault();

            if (business == null)
                return new ErrorDataResult<string>("İşletme bulunamadı.");

            // 1. Tüm eski ilişkileri temizle
            business.Services.Clear();

            _repository.SaveChanges();

            // 2. Yeni gelen id'leri EF'ye bağla ve ilişkilendir
            foreach (var serviceId in dto.ServiceIds.Distinct())
            {
                var serviceStub = new Service { Id = serviceId };
                _serviceRepository.Attach(serviceStub); // EF çakışmasın diye
                business.Services.Add(serviceStub);
            }

            // 3. Değişiklikleri kaydet
            _repository.SaveChanges();

            return new SuccessDataResult<string>("Servis listesi güncellendi.");
        }



    }
}
