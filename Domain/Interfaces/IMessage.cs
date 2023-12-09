using Domain.Interfaces.Generics;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    // Criando a interface de mensagem
    public interface IMessage : IGeneric<Message>
    {
    }
}
