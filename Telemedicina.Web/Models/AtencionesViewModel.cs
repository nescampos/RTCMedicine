using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telemedicina.Core;

namespace Telemedicina.Web.Models
{
    public class AtencionesViewModel
    {
        public IEnumerable<Atencion> Atenciones { get; set; }

        public AtencionesViewModel(TelemedicinaEntities db, string name)
        {
            Atenciones = db.Atencions.Where(x => x.Persona.Email == name)
                .OrderByDescending(x => x.FechaInicio);
        }
    }
}