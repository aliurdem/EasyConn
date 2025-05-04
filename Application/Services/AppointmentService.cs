using EasyConnect.Application.Services.Base;
using EasyConnect.Models.Dtos;
using EasyConnect.Models.Entities;
using EasyConnect.Models.Enums;
using EasyConnect.Models.Repositories;
using EasyConnect.Models.Utilities.Results;
using Mapster;

namespace EasyConnect.Application.Services
{
    public class AppointmentService : Service<Appointment, AppointmentDto>, IAppointmentService
    {
        public AppointmentService(IRepository<Appointment> repository) : base(repository)
        {
        }

        public override IDataResult<AppointmentDto> Insert(AppointmentDto dto)
        {
            if (IsOverlappingAppointment(dto))
            {
                return new ErrorDataResult<AppointmentDto>("Bu zaman aralığında seçilen personel için zaten bir randevu var.");
            }

            return base.Insert(dto);
        }
        public override void Delete(int id)
        {
            var appointment = _repository.GetById(id);

            if (appointment == null)
                return;

            if (appointment.Status == AppointmentStatus.Cancelled)
                return;

            appointment.Status = AppointmentStatus.Cancelled;

            _repository.Update(appointment);
            _repository.SaveChanges();
        }
        public IDataResult<List<AppointmentDto>> GetByUserId(string userId)
        {
            var list = _repository
                .Select(a => a.UserId == userId && a.Status != AppointmentStatus.Cancelled)
                .OrderByDescending(a => a.AppointmentDate)
                .ToList();

            return new SuccessDataResult<List<AppointmentDto>>(list.Adapt<List<AppointmentDto>>());
        }

        public IDataResult<List<AppointmentDto>> GetByFilter(int? businessProfileId = null, int? staffId = null)
        {
            var query = _repository.Select(a => a.Status != AppointmentStatus.Cancelled);

            if (businessProfileId.HasValue)
                query = query.Where(a => a.BusinessProfileId == businessProfileId.Value);

            if (staffId.HasValue)
                query = query.Where(a => a.StaffId == staffId.Value);

            var list = query.OrderByDescending(a => a.AppointmentDate).ToList();

            return new SuccessDataResult<List<AppointmentDto>>(list.Adapt<List<AppointmentDto>>());
        }

        public IDataResult<AppointmentDto> ApproveAppointment(int appointmentId)
        {
            var appointment = _repository.GetById(appointmentId);
            if (appointment == null)
                return new ErrorDataResult<AppointmentDto>("Randevu bulunamadı.");

            if (appointment.Status == AppointmentStatus.Cancelled)
                return new ErrorDataResult<AppointmentDto>("İptal edilmiş bir randevu onaylanamaz.");

            if (appointment.Status == AppointmentStatus.Approved)
                return new SuccessDataResult<AppointmentDto>("Randevu zaten onaylanmış.");

            appointment.Status = AppointmentStatus.Approved;
            _repository.Update(appointment);
            _repository.SaveChanges();

            return new SuccessDataResult<AppointmentDto>(appointment.Adapt<AppointmentDto>(), "Randevu onaylandı.");
        }
        private bool IsOverlappingAppointment(AppointmentDto dto)
        {
            var newStart = dto.AppointmentDate;
            var newEnd = newStart + dto.Duration;

            var appointments = _repository
                .Select(a =>
                    a.Status != AppointmentStatus.Cancelled &&
                    a.BusinessProfileId == dto.BusinessProfileId &&
                    a.StaffId == dto.StaffId)
                .ToList(); // LINQ to Objects'a geçtik

            return appointments.Any(a =>
            {
                var existingStart = a.AppointmentDate;
                var existingEnd = a.AppointmentDate + a.Duration;

                return newStart < existingEnd && newEnd > existingStart;
            });
        }

    }
}
