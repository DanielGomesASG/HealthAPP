using Domain.Interfaces;
using Entities.Entities;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Repositories
{
    // Criando a classe do repositório de consultas médicas
    public class RepositoryAppointment : RepositoryGenerics<Appointment>, IAppointment
    {
        private readonly DbContextOptions<ContextBase> _OptionsBuilder;
        public RepositoryAppointment()
        {
            _OptionsBuilder = new DbContextOptions<ContextBase>();
        }

        public async Task<List<Appointment>> ListAppointment(Expression<Func<Appointment, bool>> exAppointment)
        {
            using (var db = new ContextBase(_OptionsBuilder))
            {
                return await db.Appointment.Where(exAppointment).AsNoTracking().ToListAsync();
            }
        }
    }
}
