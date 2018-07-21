using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Telemedicina.Web.Controllers
{
    public class BaseController : Controller
    {
        protected Core.TelemedicinaEntities db = new Core.TelemedicinaEntities();
    }
}