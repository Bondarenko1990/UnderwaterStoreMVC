using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Infrastructure.Data;

namespace WebApplication1.Controllers
{
    public class AksessuaryController : Controller
    {
 
        public ActionResult Aksessuary_k_ruzhyam()
        {
            return View();
        }

        public ActionResult Aksessuary_k_gidrokostymam()
        {
            return View();
        }

        public ActionResult Aksessuary_k_lasti_maski_trubki()
        {
            return View();
        }

        public ActionResult Aksessuary_k_nozhi_fonari_gruza()
        {
            return View();
        }
    }
}