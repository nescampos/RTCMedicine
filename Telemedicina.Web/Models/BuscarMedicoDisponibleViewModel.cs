using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telemedicina.Core;

namespace Telemedicina.Web.Models
{
    public class BuscarMedicoDisponibleViewModel
    {
        public BuscarMedicoDisponibleFormModel Form { get; set; }
        public IEnumerable<Medico> Medicos { get; set; }
        public IEnumerable<SelectListItem> TipoMedico { get; set; }
        public IEnumerable<SelectListItem> Especialidades { get; set; }

        public BuscarMedicoDisponibleViewModel(TelemedicinaEntities db)
        {
            TipoMedico = db.TipoMedicoes.OrderBy(x => x.Nombre).ToList()
                .Select(x => new SelectListItem { Text = x.Nombre, Value = x.IdTipoMedico.ToString() });
            Especialidades = db.EspecialidadMedicoes.Where(x => x.Habilitado).OrderBy(x => x.Nombre).ToList()
                .Select(x => new SelectListItem { Text = x.Nombre, Value = x.IdEspecialidadMedico.ToString() });
        }
    }
}