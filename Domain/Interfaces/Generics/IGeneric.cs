using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Generics
{
    // Criando a interface genérica de CRUD
    public interface IGeneric<T> where T : class
    {
        Task Add(T Object);
        Task Update(T Object);
        Task Delete(T Object);
        Task<T> GetEntityById(int Id);
        Task<List<T>> List();
    }
}
