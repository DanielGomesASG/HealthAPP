using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    // Criando a estrutura usada para a criação da tabela de consultas médicas no banco de dados 
    [Table("Appointments")]
    public class Appointment : Notifies
    {

        [Column("MSN_ID")]
        public int Id { get; set; }

        [Column("MSN_TITLE")]
        [MaxLength(255)]
        public string Title { get; set; }

        [Column("MSN_MEDIC")]
        [MaxLength(255)]
        public string Medic { get; set; }

        [Column("MSN_ACTIVE")]
        public bool Active { get; set; }

        [Column("MSN_CREATE_DATE")]
        public DateTime CreateDate { get; set; }

        [Column("MSN_UPDATE_DATE")]
        public DateTime UpdateDate { get; set; }

        [FutureDate(ErrorMessage = "A data deve estar no futuro.")]
        [Column("MSN_APPOINTMENT_DATE")]
        public DateTime AppointmentDate { get; set; }

        [ForeignKey("ApplicationUser")]
        [Column(Order = 1)]
        public string UserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public class FutureDate : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value != null)
                {
                    DateTime dataSelecionada = (DateTime)value;

                    if (dataSelecionada < DateTime.Now)
                    {
                        return new ValidationResult("A data deve estar no futuro.");
                    }
                }

                return ValidationResult.Success;
            }
        }

    }
}