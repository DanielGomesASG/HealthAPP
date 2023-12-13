using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    // Criando a classe de notificação
    public class Notifies
    {
        // Construtor da lista
        public Notifies()
        {
            Notifications = new List<Notifies>();
        }

        // Nome da propriedade
        [NotMapped]
        public string PropName { get; set; }

        // Mensagem
        [NotMapped]
        public string Appointment { get; set; }

        // Lista de notificações
        [NotMapped]
        public List<Notifies> Notifications { get; set; }

        // Validando uma String
        public bool StringPropValidate(string value, string propName)
        {
            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(propName))
            {
                Notifications.Add(new Notifies
                {
                    Appointment = "Campo Obrigatório",
                    PropName = propName
                });

                return false;

            }

            return true;
        }

        // Validando um Int
        public bool IntPropValidate(int value, string propName)
        {
            if (value < 1 || string.IsNullOrWhiteSpace(propName))
            {
                Notifications.Add(new Notifies
                {
                    Appointment = "Campo Obrigatório",
                    PropName = propName
                });

                return false;

            }

            return true;
        }

        // Validando o tipo de usuário
        public bool UserTypeValidate(string userType, string propName)
        {
            if (userType != "Admin")
            {
                Notifications.Add(new Notifies
                {
                    Appointment = "Usuário não autorizado",
                    PropName = propName
                });

                return false;
            }

            return true;
        }
    }
}