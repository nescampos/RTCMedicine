using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telemedicina.Core;

namespace Telemedicina.Web.Models
{
    public class FichaPacienteViewModel
    {
        public IEnumerable<FichaPersona> Fichas { get; set; }
        public FichaPacienteFormModel Form { get; set; }

        public FichaPacienteViewModel(TelemedicinaEntities db, string name)
        {
            Fichas = db.FichaPersonas.Where(x => x.Persona.Email == name).OrderBy(x => x.Enfermedad);
        }
    }
}