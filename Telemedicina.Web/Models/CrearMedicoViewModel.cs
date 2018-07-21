using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telemedicina.Core;

namespace Telemedicina.Web.Models
{
    public class CrearMedicoViewModel
    {
        public IEnumerable<SelectListItem> TipoMedico { get; set; }
        public IEnumerable<SelectListItem> Especialidades { get; set; }
        public CrearMedicoFormModel Form { get; set; }

        public CrearMedicoViewModel(TelemedicinaEntities db)
        {
            TipoMedico = db.TipoMedicoes.OrderBy(x => x.Nombre).ToList()
                .Select(x => new SelectListItem { Text = x.Nombre, Value = x.IdTipoMedico.ToString() });
            Especialidades = db.EspecialidadMedicoes.Where(x => x.Habilitado).OrderBy(x => x.Nombre).ToList()
                .Select(x => new SelectListItem { Text = x.Nombre, Value = x.IdEspecialidadMedico.ToString() });
        }
    }
}