using Domain.Interfaces.Generics;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    // Criando a interface de consultas médicas
    public interface IAppointment : IGeneric<Appointment>
    {
        Task<List<Appointment>> ListAppointment(Expression<Func<Appointment, bool>> exAppointment);
    }
}
