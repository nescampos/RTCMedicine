using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telemedicina.Core;
using Telemedicina.Web.Models;

namespace Telemedicina.Web.Controllers
{
    [Authorize(Roles = "person")]
    public class PacienteController : BaseController
    {
        // GET: Paciente
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Atenciones()
        {
            AtencionesViewModel model = new AtencionesViewModel(db, User.Identity.Name);
            return View(model);
        }

        public ActionResult Ficha()
        {
            FichaPacienteViewModel model = new FichaPacienteViewModel(db, User.Identity.Name);
            return View(model);
        }

        [HttpPost]
        public ActionResult Ficha(FichaPacienteFormModel Form)
        {
            if(!ModelState.IsValid)
            {
                FichaPacienteViewModel model = new FichaPacienteViewModel(db, User.Identity.Name);
                model.Form = Form;
                return View(model);
            }
            Persona persona = db.Personas.SingleOrDefault(x => x.Email == User.Identity.Name);
            FichaPersona fichaPersona = new FichaPersona
            { Descripcion = Form.Descripcion, Enfermedad = Form.Enfermedad, EsCronica = false, FechaCreacion = DateTime.UtcNow, IdFichaPersona = Guid.NewGuid()  };

            persona.FichaPersonas.Add(fichaPersona);
            db.SaveChanges();
            
            return RedirectToAction("Ficha");
        }
    }
}