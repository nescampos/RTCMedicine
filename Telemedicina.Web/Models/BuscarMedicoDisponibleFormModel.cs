using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Telemedicina.Web.Models
{
    public class BuscarMedicoDisponibleFormModel
    {
        [DisplayName("Type")]
        public int? IdTipoMedico { get; set; }

        [DisplayName("Specialty")]
        public int? IdEspecialidadMedico { get; set; }
    }
}