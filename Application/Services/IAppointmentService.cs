using EasyConnect.Application.Services.Base;
using EasyConnect.Models.Dtos;
using EasyConnect.Models.Entities;
using EasyConnect.Models.Utilities.Results;

namespace EasyConnect.Application.Services
{
    public interface IAppointmentService : IService<Appointment, AppointmentDto>
    {
        IDataResult<List<AppointmentDto>> GetByUserId(string userId);
        IDataResult<List<AppointmentDto>> GetByFilter(int? businessProfileId = null, int? staffId = null);
        IDataResult<AppointmentDto> ApproveAppointment(int appointmentId);

    }
}
