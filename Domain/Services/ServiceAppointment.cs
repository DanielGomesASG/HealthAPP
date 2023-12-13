using Domain.Interfaces;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    // Criando a classe de consultas médicas
    public class ServiceAppointment : IServiceAppointment
    {
        private readonly IAppointment _IAppointment;

        public ServiceAppointment(IAppointment IAppointment)
        {
            _IAppointment = IAppointment;
        }

        public async Task Add(Appointment Object)
        {
            var titlevalidate = Object.StringPropValidate(Object.Title, "Title");
            if (titlevalidate)
            {
                Object.CreateDate = DateTime.Now;
                Object.UpdateDate = DateTime.Now;
                Object.Active = true;
                await _IAppointment.Add(Object);
            }
        }

        public async Task Update(Appointment Object)
        {
            var titlevalidate = Object.StringPropValidate(Object.Title, "Title");
            if (titlevalidate)
            {
                Object.UpdateDate = DateTime.Now;
                await _IAppointment.Update(Object);
            }
        }

        public async Task<List<Appointment>> ListActiveAppointments()
        {
            return await _IAppointment.ListAppointment(n => n.Active);
        }
    }
}
