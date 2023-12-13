using AutoMapper;
using Domain.Interfaces;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using WebAPIs.Models;

namespace WebAPIs.Controllers
{
    // Criando a controladora de consultas médicas
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        // Mapeando as consultas médicas
        private readonly IMapper _IMapper;
        private readonly IAppointment _IAppointment;
        private readonly IServiceAppointment _IServiceAppointment;
        private readonly Notifies _Notifies;

        public AppointmentController(IMapper IMapper, IAppointment IAppointment, IServiceAppointment IServiceAppointment)
        {
            _IMapper = IMapper;
            _IAppointment = IAppointment;
            _IServiceAppointment = IServiceAppointment;
            _Notifies = new Notifies();
        }

        // Create
        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/Add")]
        public async Task<List<Notifies>> Add(AppointmentViewModel Appointment)
        {
            Appointment.UserId = await LoggedUserId();
            var AppointmentMap = _IMapper.Map<Appointment>(Appointment);
            await _IServiceAppointment.Add(AppointmentMap);
            return AppointmentMap.Notifications;

        }

        // Update
        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/UpdateAppointment")]
        public async Task<List<Notifies>> Update(AppointmentViewModel Appointment)
        {
            var type = await LoggedUserType();

            // Retornando a lista caso o usuário tenha permissão
            if (_Notifies.UserTypeValidate(type, "UpdateAppointment"))
            {
                var AppointmentMap = _IMapper.Map<Appointment>(Appointment);
                await _IServiceAppointment.Update(AppointmentMap);
                return AppointmentMap.Notifications;
            }

            // Lista vazia para usuários sem permissão
            return new List<Notifies>();
        }

        // Delete
        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/DeleteAppointment")]
        public async Task<List<Notifies>> Delete(AppointmentViewModel Appointment)
        {
            var type = await LoggedUserType();

            // Retornando a lista caso o usuário tenha permissão
            if (_Notifies.UserTypeValidate(type, "DeleteAppointment"))
            {
                var AppointmentMap = _IMapper.Map<Appointment>(Appointment);
                await _IAppointment.Delete(AppointmentMap);
                return AppointmentMap.Notifications;
            }

            // Lista vazia para usuários sem permissão
            return new List<Notifies>();
        }

        // Read
        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/GetEntityById")]
        public async Task<AppointmentViewModel> GetEntityById(Appointment Appointment)
        {
            var type = await LoggedUserType();

            // Retornando a lista caso o usuário tenha permissão
            if (_Notifies.UserTypeValidate(type, "GetById"))
            {
                Appointment = await _IAppointment.GetEntityById(Appointment.Id);
                var AppointmentMap = _IMapper.Map<AppointmentViewModel>(Appointment);
                return AppointmentMap;
            }

            // Lista vazia para usuários sem permissão
            return new AppointmentViewModel();
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/List")]
        public async Task<List<AppointmentViewModel>> List()
        {
            var type = await LoggedUserType();

            // Retornando a lista caso o usuário tenha permissão
            if (_Notifies.UserTypeValidate(type, "List"))
            {
                var Appointments = await _IAppointment.List();
                var AppointmentMap = _IMapper.Map<List<AppointmentViewModel>>(Appointments);
                return AppointmentMap;
            }

            // Lista vazia para usuários sem permissão
            return new List<AppointmentViewModel>();
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/ListActiveAppointments")]
        public async Task<List<AppointmentViewModel>> ListActiveAppointments()
        {
            var type = await LoggedUserType();

            // Retornando a lista caso o usuário tenha permissão
            if (_Notifies.UserTypeValidate(type, "ListActives"))
            {
                var Appointments = await _IServiceAppointment.ListActiveAppointments();
                var AppointmentMap = _IMapper.Map<List<AppointmentViewModel>>(Appointments);
                return AppointmentMap;
            }
            // Lista vazia para usuários sem permissão
            return new List<AppointmentViewModel>();
        }

        // Criando a função que mapeia e retorna o ID do usuário
        private async Task<string> LoggedUserId()
        {
            if (User != null)
            {
                var userId = User.FindFirst("userId");
                return userId.Value;
            }

            return null;

        }

        // Criando a função que mapeia e retorna o Tipo do usuário
        private async Task<string> LoggedUserType()
        {
            if (User != null)
            {
                var userType = User.FindFirst("userType");
                return userType.Value;
            }

            return null;

        }
    }
}
