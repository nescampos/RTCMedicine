using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Telemedicina.Core;
using Telemedicina.Web.Models;

namespace Telemedicina.Web.Controllers
{
    public class MedicoController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [Authorize(Roles = "doctor")]
        public ActionResult Index()
        {
            IndexMedicoViewModel model = new IndexMedicoViewModel();
            Medico medico = db.Medicos.SingleOrDefault(x => x.Email == User.Identity.Name);
            IEnumerable<Atencion> atenciones = db.Atencions.Where(x => x.IdMedico == medico.IdMedico).OrderBy(x => x.FechaInicio);
            model.Atenciones = atenciones.Where(x => x.Realizada);
            model.AtencionPendiente = atenciones.FirstOrDefault(x => !x.Realizada && !x.EsProgramada);
            model.Medico = medico;
            return View(model);
        }

        [Authorize(Roles = "doctor")]
        public ActionResult Habilitar()
        {
            Medico medico = db.Medicos.SingleOrDefault(x => x.Email == User.Identity.Name);
            medico.Disponible = !medico.Disponible;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        public ActionResult Crear()
        {
            CrearMedicoViewModel model = new CrearMedicoViewModel(db);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Crear(CrearMedicoFormModel Form)
        {
            CrearMedicoViewModel model = new CrearMedicoViewModel(db);
            if (!ModelState.IsValid)
            {
                model.Form = Form;
                return View(model);
            }

            var user = new ApplicationUser { UserName = Form.Email, Email = Form.Email };
            var result = await UserManager.CreateAsync(user, Form.Password);
            if (result.Succeeded)
            {
                UserManager.AddToRole(user.Id, "doctor");
                //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                Medico medico = new Medico { Email = Form.Email, FechaCreacion = DateTime.Now, Nombre = Form.Name, IdTipoMedico = Form.IdTipoMedico.Value, IdEspecialidadMedico = Form.IdEspecialidadMedico.Value };
                db.Medicos.Add(medico);
                db.SaveChanges();
                
                return RedirectToAction("Medicos","Home");
            }
            AddErrors(result);
            model.Form = Form;
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("Form.Email", error);
            }
        }

        public ActionResult MisDetalles()
        {
            return View();
        }
    }
}