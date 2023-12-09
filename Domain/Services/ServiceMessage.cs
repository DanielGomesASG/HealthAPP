using Domain.Interfaces;
using Domain.Interfaces.InterfaceServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    // Criando a classe de mensagem de serviço
    public class ServiceMessage : IServiceMessage
    {
        private readonly IMessage _IMessage;

        public ServiceMessage(IMessage IMessage) 
        { 
            _IMessage = IMessage;
        }
    }
}
