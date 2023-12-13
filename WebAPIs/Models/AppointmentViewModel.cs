using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIs.Models
{
    // Criando o model de consultas médicas
    public class AppointmentViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Medic { get; set; }

        public bool Active { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string UserId { get; set; }
    }
}
