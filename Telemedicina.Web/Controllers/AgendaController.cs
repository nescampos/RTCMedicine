using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telemedicina.Core;
using Telemedicina.Web.Models;

namespace Telemedicina.Web.Controllers
{
    public class AgendaController : BaseController
    {
        // GET: Agenda
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Crear()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Crear()
        //{
        //    return View();
        //}

        [Authorize]
        public ActionResult Detalle(Guid id)
        {

            Atencion atencion = db.Atencions.SingleOrDefault(x => x.IdAtencion == id);
            if(!atencion.FechaFin.HasValue)
            {
                atencion.FechaFin = DateTime.UtcNow;
                db.SaveChanges();
            }

            return View(atencion);
        }

        public ActionResult Historial()
        {
            return View();
        }

        [Authorize(Roles = "person")]
        public ActionResult Conectar(int id)
        {
            Persona persona = db.Personas.SingleOrDefault(x => x.Email == User.Identity.Name);
            Medico medico = db.Medicos.SingleOrDefault(x => x.IdMedico == id);
            Atencion atencion = new Atencion
            {
                 IdAtencion = Guid.NewGuid(), FechaCreacion = DateTime.UtcNow, FechaInicio = DateTime.UtcNow,
                  EsProgramada = false, IdPersona = persona.IdPersona, IdMedico = medico.IdMedico,
                CodigoAtencion = Guid.NewGuid().GetHashCode().ToString(), Realizada = false
            };
            db.Atencions.Add(atencion);
            medico.Disponible = false;
            db.SaveChanges();
            return View(atencion);
        }

        [Authorize(Roles = "doctor")]
        public ActionResult Cancelar(Guid id)
        {
            Medico medico = db.Medicos.SingleOrDefault(x => x.Email == User.Identity.Name);
            Atencion atencion = db.Atencions.SingleOrDefault(x => x.IdAtencion == id);
            atencion.EsProgramada = true;
            medico.Disponible = true;
            db.SaveChanges();
            return RedirectToAction("Index", "Medico");
        }

        [Authorize(Roles = "doctor")]
        public ActionResult Aceptar(Guid id)
        {
            Medico medico = db.Medicos.SingleOrDefault(x => x.Email == User.Identity.Name);
            Atencion atencion = db.Atencions.SingleOrDefault(x => x.IdAtencion == id);
            atencion.EsProgramada = false;
            medico.Disponible = false;
            atencion.Realizada = true;
            db.SaveChanges();
            return View(atencion);
        }

        [Authorize(Roles = "doctor")]
        [HttpPost]
        public ActionResult Aceptar(AceptarViewModel Form)
        {
            Medico medico = db.Medicos.SingleOrDefault(x => x.Email == User.Identity.Name);
            Atencion atencion = db.Atencions.SingleOrDefault(x => x.IdAtencion == Form.Id);
            atencion.Notas = Form.Notas;
            atencion.FechaFin = DateTime.UtcNow;
            medico.Disponible = true;
            db.SaveChanges();
            return RedirectToAction("Detalle", new { id = Form.Id });
        }


        public ActionResult RegistrarDatosAgenda(Guid id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarDatosAgenda()
        {
            return View();
        }

    }
}