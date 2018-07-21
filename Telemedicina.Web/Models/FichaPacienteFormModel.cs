using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Telemedicina.Web.Models
{
    public class FichaPacienteFormModel
    {

        [Required]
        [DisplayName("Illness")]
        public string Enfermedad { get; set; }

        [Required]
        [DisplayName("Description")]
        public string Descripcion { get; set; }
    }
}