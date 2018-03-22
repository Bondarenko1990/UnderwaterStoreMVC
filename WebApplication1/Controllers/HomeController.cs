using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Data.Entity;
using WebApplication1.Domain.Core;
using WebApplication1.Infrastructure.Data;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {

        UnitOfWork unitOfWork;

        public HomeController()
        {
            unitOfWork = new UnitOfWork();
        }

        public ActionResult Show()
        {
            return View(unitOfWork.Brands.GetAll());
        }

        public ActionResult Index()
        {
            IndexView ivm = new IndexView
            {
                Chekhly_setkis = unitOfWork.Chekhly_setkis.GetAll().ToList(),
                Fonaris = unitOfWork.Fonaris.GetAll().ToList(),
                Trubkis = unitOfWork.Trubkis.GetAll().ToList(),
                Rukavitsys = unitOfWork.Rukavitsys.GetAll().ToList(),
                Komplektys = unitOfWork.Komplektys.GetAll().ToList(),
                Dlya_podvodnoy_okhotys = unitOfWork.Dlya_podvodnoy_okhotys.GetAll().ToList(),
                Lasty_dlya_okhotys = unitOfWork.Lasty_dlya_okhotys.GetAll().ToList(),
                Pneumatics = unitOfWork.Pneumatics.GetAll().ToList(),
                Maskis = unitOfWork.Maskis.GetAll().ToList(),
                Nozhis = unitOfWork.Nozhis.GetAll().ToList(),
                Garpuns = unitOfWork.Garpuns.GetAll().ToList(),
                Aksessuary_k_lastams = unitOfWork.Aksessuary_k_lastams.GetAll().ToList(),
                Dlya_dayvinga_i_vodnogo_sportas = unitOfWork.Dlya_dayvinga_i_vodnogo_sportas.GetAll().ToList(),
                Perchatkis = unitOfWork.Perchatkis.GetAll().ToList(),
                Prochees = unitOfWork.Prochees.GetAll().ToList()

            };
            return View(ivm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Promo()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Akciya1()
        {
            IndexView ivm = new IndexView
            {
                Chekhly_setkis = unitOfWork.Chekhly_setkis.GetAll().Where(g => g.Name == "MARES Сумка для длинных ласт Attack"),
                Lasty_dlya_okhotys = unitOfWork.Lasty_dlya_okhotys.GetAll().Where( g => g.Name.StartsWith("MARES Razor")),
            };
            return View(ivm);
        }
        public ActionResult Akciya2()
        {
            IndexView ivm = new IndexView
            {
                Chekhly_setkis = unitOfWork.Chekhly_setkis.GetAll().Where(g => g.Name == "PELENGAS Чехол для пневматических ружей 55 см"),
                Pneumatics = unitOfWork.Pneumatics.GetAll().Where(g => g.Name.StartsWith("PELENGAS")),
            };
            return View(ivm);
        }
        public ActionResult Akciya3()
        {
            IndexView ivm = new IndexView
            {
                Trubkis = unitOfWork.Trubkis.GetAll().Where(g => g.Name == "MARES Samurai Extrem"),
                Maskis = unitOfWork.Maskis.GetAll().Where(g => g.Name.StartsWith("MARES")),
            };
            return View(ivm);
        }
        public ActionResult Delivery()
        {
            return View();
        }

        public ActionResult Rassrochka()
        {
            return View();
        }

        public ActionResult New()
        {
            IndexView ivm = new IndexView
            {  
                Dlya_podvodnoy_okhotys = unitOfWork.Dlya_podvodnoy_okhotys.GetAll().ToList(),
                Pneumatics = unitOfWork.Pneumatics.GetAll().ToList(),
                Maskis = unitOfWork.Maskis.GetAll().ToList(),
                Nozhis = unitOfWork.Nozhis.GetAll().ToList(),
            };
            return View(ivm);
        }

        public ActionResult Popular()
        {
            IndexView ivm = new IndexView
            {
                Chekhly_setkis = unitOfWork.Chekhly_setkis.GetAll().ToList(),
                Fonaris = unitOfWork.Fonaris.GetAll().ToList(),
                Trubkis = unitOfWork.Trubkis.GetAll().ToList(),
                Rukavitsys = unitOfWork.Rukavitsys.GetAll().ToList(),
                Komplektys = unitOfWork.Komplektys.GetAll().ToList(),
                Dlya_podvodnoy_okhotys = unitOfWork.Dlya_podvodnoy_okhotys.GetAll().ToList(),
                Lasty_dlya_okhotys = unitOfWork.Lasty_dlya_okhotys.GetAll().ToList(),
            };
            return View(ivm);
        }

        public ActionResult Discount()
        {
            IndexView ivm = new IndexView
            {
                Pneumatics = unitOfWork.Pneumatics.GetAll().ToList(),
                Garpuns = unitOfWork.Garpuns.GetAll().ToList(),
                Aksessuary_k_lastams = unitOfWork.Aksessuary_k_lastams.GetAll().ToList(),
                Dlya_dayvinga_i_vodnogo_sportas = unitOfWork.Dlya_dayvinga_i_vodnogo_sportas.GetAll().ToList()
            };
            return View(ivm);
        }

        public ActionResult Akcii()
        {
            IndexView ivm = new IndexView
            {
                Chekhly_setkis = unitOfWork.Chekhly_setkis.GetAll().ToList(),
                Dlya_podvodnoy_okhotys = unitOfWork.Dlya_podvodnoy_okhotys.GetAll().ToList(),
                Perchatkis = unitOfWork.Perchatkis.GetAll().ToList(),
                Prochees = unitOfWork.Prochees.GetAll().ToList()
            };
            return View(ivm);
        }

        public ActionResult Guaranty()
        {
            return View();
        }

        public ActionResult CatalogInfo()
        {
            return View();
        }
        public ActionResult Ruzhya()
        {
            return View();
        }

        public ActionResult Noski_boty_perchatki()
        {
            return View();
        }

        public ActionResult Lasty_maski_trubki()
        {
            return View();
        }

        public ActionResult Gidrokostyumy()
        {
            return View();
        }

        public ActionResult Nozhi_fonari_gruza()
        {
            return View();
        }

        public ActionResult Dopolnitelno()
        {
            return View();
        }

        public ActionResult Catalog()
        {
            return View();
        }
    }
}