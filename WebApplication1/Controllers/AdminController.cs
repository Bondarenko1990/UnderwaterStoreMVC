using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Domain.Interfaces;
using WebApplication1.Domain.Core;
using WebApplication1.Infrastructure.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {
        UnitOfWork unitOfWork;

        public AdminController()
        {
            unitOfWork = new UnitOfWork();
        }

        public ViewResult List()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Menu()
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

        public ViewResult Create()
        {
            SelectList brands = new SelectList(unitOfWork.Brands.GetAll(), "Id", "Name");
            //List<Brand> brands = repository.Brands.ToList();
            ViewBag.Brands = brands;
            return View("Edit", new Pneumatic());
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            Pneumatic deletedPneumatic = unitOfWork.Pneumatics.Delete(Id);
            if (deletedPneumatic != null)
            {
                TempData["message"] = string.Format("Товар \"{0}\" был удален",
                    deletedPneumatic.Name);
            }
            return RedirectToAction("Index");
        }


        //public List<object> GetFilter()

        //{

        //    return filter;
        //}
        //public ViewResult Index()
        //{
        //    return View(unitOfWork.Pneumatics.GetAll().ToList());
        //}

        //public ViewResult Index(string item)
        //{
        //    var arbalets = unitOfWork.Arbalets;
        //    var pneumatics = unitOfWork.Pneumatics;

        //    Object items = null;

        //    switch (item)
        //    {
        //        case "arbalets":
        //            items = arbalets;
        //            break;
        //        case "pneumatics":
        //            items = pneumatics; 
        //            break;
        //    }
        //    //int f = 0;
        //    //unitOfWork.Items
        //    //var fm = unitOfWork.Equals.;
        //    //var s = unitOfWork.GetType().wh.ToString();
        //    //if (s == name)
        //    //    { 
        //    //    f = 10;
        //    //    }


        //    //List<Item> ty = itemModels.
        //    return View(items);
        //}
        //public ViewResult View(int Id)
        //{
        //    Pneumatic pneumatic = unitOfWork.Pneumatics.GetAll().FirstOrDefault(g => g.Id == Id);
        //    SelectList brands = new SelectList(unitOfWork.Brands.GetAll(), "Id", "Name");
        //    ViewBag.Brands = brands;
        //    return View(pneumatic);
        //}
        public ViewResult Edit(int Id)
        {
            Pneumatic pneumatic = unitOfWork.Pneumatics.GetAll().FirstOrDefault(g => g.Id == Id);
            SelectList brands = new SelectList(unitOfWork.Brands.GetAll(), "Id" , "Name");
            ViewBag.Brands = brands;
            return View(pneumatic);
        }
        //[HttpPost]
        //public ActionResult Edit(Pneumatic pneumatic)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        unitOfWork.Pneumatics.SaveItem(pneumatic);
        //        TempData["message"] = string.Format("Изменения в товаре \"{0}\" были сохранены", pneumatic.Name);
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        // Что-то не так со значениями данных
        //        return View(pneumatic);
        //    }
        //}


        [HttpPost]
        public ActionResult Edit(Pneumatic pneumatic, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    pneumatic.ImageMimeType = image.ContentType;
                    pneumatic.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(pneumatic.ImageData, 0, image.ContentLength);
                }
                else
                {
                    pneumatic.ImageMimeType = unitOfWork.Pneumatics.Get(pneumatic.Id).ImageMimeType;
                    pneumatic.ImageData = unitOfWork.Pneumatics.Get(pneumatic.Id).ImageData;
                }
                unitOfWork.Pneumatics.SaveItem(pneumatic);
                TempData["message"] = string.Format("Изменения в товаре \"{0}\" были сохранены", pneumatic.Name);
                return RedirectToAction("Index");
            }
            else
            {
                // Что-то не так со значениями данных
                return View(pneumatic);
            }
        }
    }
}
