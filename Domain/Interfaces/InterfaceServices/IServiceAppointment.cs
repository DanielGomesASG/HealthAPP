using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceServices
{
    // Criando a interface de consultas médicas
    public interface IServiceAppointment
    {
        Task Add(Appointment Object);

        Task Update(Appointment Object);

        Task<List<Appointment>> ListActiveAppointments();
    }
}
