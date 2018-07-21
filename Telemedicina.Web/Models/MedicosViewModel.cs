using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telemedicina.Core;

namespace Telemedicina.Web.Models
{
    public class MedicosViewModel
    {
        public IEnumerable<Medico> Medicos { get; set; }

        public MedicosViewModel(TelemedicinaEntities db)
        {
            Medicos = db.Medicos.OrderBy(x => x.Nombre);
        }
    }
}