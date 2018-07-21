using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telemedicina.Core;
using Telemedicina.Web.Models;

namespace Telemedicina.Web.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            if(User.IsInRole("admin"))
            {
                return RedirectToAction("Medicos");
            }
            else
            {
                if (User.IsInRole("doctor"))
                {
                    return RedirectToAction("Index", "Medico");
                }
                else
                {
                    return View();
                }
            }
            
        }

        [Authorize(Roles = "person")]
        public ActionResult BuscarMedicoDisponible(BuscarMedicoDisponibleFormModel Form)
        {
            BuscarMedicoDisponibleViewModel model = new BuscarMedicoDisponibleViewModel(db);
            IEnumerable<Medico> medicos = db.Medicos.OrderByDescending(x => x.Disponible).OrderBy(x => x.Nombre);
            if(Form.IdEspecialidadMedico.HasValue)
            {
                medicos = medicos.Where(x => x.IdEspecialidadMedico == Form.IdEspecialidadMedico.Value);
            }
            if (Form.IdTipoMedico.HasValue)
            {
                medicos = medicos.Where(x => x.IdTipoMedico == Form.IdTipoMedico.Value);
            }
            model.Medicos = medicos;
            model.Form = Form;
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Medicos()
        {
            MedicosViewModel model = new MedicosViewModel(db);
            return View(model);
        }
    }
}