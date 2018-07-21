using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telemedicina.Core;

namespace Telemedicina.Web.Models
{
    public class IndexMedicoViewModel
    {
        public IEnumerable<Atencion> Atenciones { get; internal set; }
        public Medico Medico { get; set; }
        public Atencion AtencionPendiente { get; set; }
    }
}