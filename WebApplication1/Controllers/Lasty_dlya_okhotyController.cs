﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Domain.Core;
using WebApplication1.Infrastructure.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class Lasty_dlya_okhotyController : Controller
    {
        UnitOfWork unitOfWork;
        public Lasty_dlya_okhotyController()
        {
            unitOfWork = new UnitOfWork();
        }

        public FilterLasty_dlya_okhoty GetFilter()
        {
            FilterLasty_dlya_okhoty filter = (FilterLasty_dlya_okhoty)Session["FilterLasty_dlya_okhoty"];
            if (filter == null)
            {
                filter = new FilterLasty_dlya_okhoty();
                Session["FilterLasty_dlya_okhoty"] = filter;
            }
            return filter;
        }

        public FileContentResult GetImage(int Id)
        {
            Lasty_dlya_okhoty item = unitOfWork.Lasty_dlya_okhotys.GetAll().FirstOrDefault(g => g.Id == Id);

            if (item != null)
            {
                return File(item.ImageData, item.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

        [Authorize(Roles = "Admin")]
        public ViewResult Create()
        {
            SelectList brands = new SelectList(unitOfWork.Brands.GetAll(), "Id", "Name");
            ViewBag.Brands = brands;
            return View("Edit", new Lasty_dlya_okhoty());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int Id)
        {
            Lasty_dlya_okhoty deletedItem = unitOfWork.Lasty_dlya_okhotys.Delete(Id);
            if (deletedItem != null)
            {
                TempData["message"] = string.Format("Товар \"{0}\" был удален",
                    deletedItem.Name);
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public ViewResult Index()
        {
            return View(unitOfWork.Lasty_dlya_okhotys.GetAll().ToList());
        }

        [Authorize(Roles = "Admin")]
        public ViewResult Edit(int Id)
        {
            Lasty_dlya_okhoty item = unitOfWork.Lasty_dlya_okhotys.GetAll().FirstOrDefault(g => g.Id == Id);
            SelectList brands = new SelectList(unitOfWork.Brands.GetAll(), "Id", "Name");
            ViewBag.Brands = brands;
            return View(item);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Lasty_dlya_okhoty item, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    item.ImageMimeType = image.ContentType;
                    item.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(item.ImageData, 0, image.ContentLength);
                }
                else
                {
                    item.ImageMimeType = unitOfWork.Lasty_dlya_okhotys.Get(item.Id).ImageMimeType;
                    item.ImageData = unitOfWork.Lasty_dlya_okhotys.Get(item.Id).ImageData;
                }
                unitOfWork.Lasty_dlya_okhotys.SaveItem(item);
                TempData["message"] = string.Format("Изменения в товаре \"{0}\" были сохранены", item.Name);
                return RedirectToAction("Index");
            }
            else
            {
                // Что-то не так со значениями данных
                return View(item);
            }
        }

        public ActionResult DetailsModal(int id)
        {
            Lasty_dlya_okhoty c = unitOfWork.Lasty_dlya_okhotys.GetAll().FirstOrDefault(com => com.Id == id);
            if (c != null)
                return PartialView(c);
            return HttpNotFound();
        }

        public ActionResult Details(int id)
        {
            Lasty_dlya_okhoty c = unitOfWork.Lasty_dlya_okhotys.GetAll().FirstOrDefault(com => com.Id == id);
            if (c != null)
                return PartialView(c);
            return HttpNotFound();
        }

        [HttpPost]
        public PartialViewResult Models(int page = 1)
        {

            int pageSize = 24;

            IEnumerable<Lasty_dlya_okhoty> GunsPerPages = GetFilter().Item.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = GetFilter().Item.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Lasty_dlya_okhotys = GunsPerPages
            };
            ViewBag.IVM = ivm;
            return PartialView(ivm);
        }

        public ActionResult List(int page = 1)
        {

            int count = unitOfWork.Lasty_dlya_okhotys.GetAll().Count();
            var items = unitOfWork.Lasty_dlya_okhotys.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();
            List<Lasty_dlya_okhoty> Item;
            if (GetFilter().Item == null)
            {
                Item = unitOfWork.Lasty_dlya_okhotys.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();
            }
            else
            {
                Item = GetFilter().Item;
            }

            var Brands = new List<string>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                Brands.Add(items[i].Brand.Name);
            }
            List<string> newBrands = new List<string>(Brands.Distinct());
            newBrands.Sort();
            ViewBag.Brands = newBrands;

            //------------------------------------------------
            var Countries = new List<string>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                Countries.Add(items[i].Brand.Country);
            }
            List<string> newCountries = new List<string>(Countries.Distinct());
            newCountries.Sort();
            ViewBag.Countries = newCountries;
            //------------------------------------------------
            var Pyatka = new List<string>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                Pyatka.Add(items[i].Pyatka);
            }
            List<string> newPyatka = new List<string>(Pyatka.Distinct());
            newPyatka.Sort();
            ViewBag.Pyatka = newPyatka;
            //------------------------------------------------
            var Lopast = new List<string>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                Lopast.Add(items[i].Lopast);
            }
            List<string> newLopast = new List<string>(Lopast.Distinct());
            newLopast.Sort();
            ViewBag.Lopast = newLopast;
            //------------------------------------------------
            var Material = new List<string>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                Material.Add(items[i].Material);
            }
            List<string> newMaterial = new List<string>(Material.Distinct());
            newMaterial.Sort();
            ViewBag.Material = newMaterial;
            //------------------------------------------------
            var Length = new List<int>();
            var EditLength = 0;
            for (int i = 1; i <= 6; i++)
            {
                EditLength = i * 30;
                if (items.Exists(p => p.Length >= EditLength && p.Length < EditLength + 30))
                {
                    Length.Add(EditLength);
                }
            }
            var newLength = new List<int>(Length.Distinct());
            ViewBag.Length = newLength.ToList();
            //------------------------------------------------
            ViewBag.MinPrice = unitOfWork.Lasty_dlya_okhotys.GetAll().Min(a => a.Price);
            ViewBag.MaxPrice = unitOfWork.Lasty_dlya_okhotys.GetAll().Max(a => a.Price);
            ViewBag.CheckedMinPrice = GetFilter().CheckedMinPrice;
            if (GetFilter().CheckedMaxPrice == 0)
            {
                ViewBag.CheckedMaxPrice = unitOfWork.Lasty_dlya_okhotys.GetAll().Max(a => a.Price);
            }
            else
            {
                ViewBag.CheckedMaxPrice = GetFilter().CheckedMaxPrice;
            }
            ViewBag.CheckedBrand = GetFilter().CheckedBrand;
            ViewBag.CheckedCountry = GetFilter().CheckedCountry;
            ViewBag.CheckedPyatka = GetFilter().CheckedPyatka;
            ViewBag.CheckedLopast = GetFilter().CheckedLopast;
            ViewBag.CheckedMaterial = GetFilter().CheckedMaterial;
            ViewBag.CheckedLength = GetFilter().CheckedLength;
            ViewBag.CheckedCount = GetFilter().CheckedCount;
            int pageSize = 24; // количество объектов на страницу
            IEnumerable<Lasty_dlya_okhoty> GunsPerPages = Item.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = Item.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Lasty_dlya_okhotys = GunsPerPages
            };

            ViewBag.IVM = ivm;

            return View(ivm);
        }

        [HttpPost]
        public ActionResult Filter(int MinPrice, int MaxPrice, string[] brand, string[] country, string[] pyatka, string[] lopast,
            string[] material, int[] length )
        {
            int count = unitOfWork.Lasty_dlya_okhotys.GetAll().Count();
            List<Lasty_dlya_okhoty> Item = unitOfWork.Lasty_dlya_okhotys.GetAll().Where(p => p.Price >= MinPrice && p.Price <= MaxPrice).ToList();
            List<Lasty_dlya_okhoty> newItem = new List<Lasty_dlya_okhoty>();

            GetFilter().CheckedMinPrice = MinPrice;
            GetFilter().CheckedMaxPrice = MaxPrice;
            GetFilter().CheckedBrand = 0;
            GetFilter().CheckedCountry = 0;
            GetFilter().CheckedPyatka = 0;
            GetFilter().CheckedLopast = 0;
            GetFilter().CheckedMaterial= 0;
            GetFilter().CheckedLength = 0;
            GetFilter().CheckedCount = 0;


            ViewBag.MinPrice = unitOfWork.Lasty_dlya_okhotys.GetAll().Min(a => a.Price);
            ViewBag.MaxPrice = unitOfWork.Lasty_dlya_okhotys.GetAll().Max(a => a.Price);
            ViewBag.CheckedMinPrice = MinPrice;
            ViewBag.CheckedMaxPrice = MaxPrice;
            ViewBag.CheckedBrand = 0;
            ViewBag.CheckedCountry = 0;
            ViewBag.CheckedPyatka = 0;
            ViewBag.CheckedLopast = 0;
            ViewBag.CheckedMaterial = 0;
            ViewBag.CheckedLength = 0;
            ViewBag.CheckedCount = 0;

            int Count = 0;

            if (brand != null)
            {
                string a = "";
                for (int i = 0; i < brand.Count(); i++)
                {

                    a = brand[i];
                    foreach (var c in Item.Where(p => p.Brand.Name == a))
                    {

                        newItem.Add(c);
                    }
                }
                Item = newItem;
                GetFilter().CheckedBrand = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedBrand = 1;
                Count++;
            }

            if (country != null)
            {
                newItem = new List<Lasty_dlya_okhoty>();
                string a = "";
                for (int i = 0; i < country.Count(); i++)
                {

                    a = country[i];
                    foreach (var c in Item.Where(p => p.Brand.Country == a))
                    {
                        newItem.Add(c);
                    }
                }
                Item = newItem;
                GetFilter().CheckedCountry = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedCountry = 1;
                Count++;
            }

            if (pyatka != null)
            {
                newItem = new List<Lasty_dlya_okhoty>();
                string a = "";
                for (int i = 0; i < pyatka.Count(); i++)
                {

                    a = pyatka[i];
                    foreach (var c in Item.Where(p => p.Pyatka == a))
                    {
                        newItem.Add(c);
                    }
                }
                Item = newItem;
                GetFilter().CheckedPyatka = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedPyatka = 1;
                Count++;
            }

            if (lopast != null)
            {
                newItem = new List<Lasty_dlya_okhoty>();
                string a = "";
                for (int i = 0; i < lopast.Count(); i++)
                {

                    a = lopast[i];
                    foreach (var c in Item.Where(p => p.Lopast == a))
                    {
                        newItem.Add(c);
                    }
                }
                Item = newItem;
                GetFilter().CheckedLopast = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedLopast = 1;
                Count++;
            }


            if (material != null)
            {
                newItem = new List<Lasty_dlya_okhoty>();
                string a = "";
                for (int i = 0; i < material.Count(); i++)
                {

                    a = material[i];
                    foreach (var c in Item.Where(p => p.Material == a))
                    {
                        newItem.Add(c);
                    }
                }
                Item = newItem;
                GetFilter().CheckedMaterial = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedMaterial = 1;
                Count++;
            }
            if (length != null)
            {
                newItem = new List<Lasty_dlya_okhoty>();
                int a = 0;
                for (int i = 0; i < length.Count(); i++)
                {

                    a = length[i];
                    foreach (var c in Item.Where(p => p.Length >= a && p.Length < a + 30))
                    {
                        newItem.Add(c);
                    }
                }
                Item = newItem;
                GetFilter().CheckedLength = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedLength = 1;
                Count++;
            }          

            ViewBag.CheckedCount = Count;

            var items = unitOfWork.Lasty_dlya_okhotys.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();

            var Brands = new List<string>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                Brands.Add(items[i].Brand.Name);
            }
            List<string> newBrands = new List<string>(Brands.Distinct());
            newBrands.Sort();
            ViewBag.Brands = newBrands;

            //------------------------------------------------
            var Countries = new List<string>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                Countries.Add(items[i].Brand.Country);
            }
            List<string> newCountries = new List<string>(Countries.Distinct());
            newCountries.Sort();
            ViewBag.Countries = newCountries;
            //------------------------------------------------
            var Pyatka = new List<string>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                Pyatka.Add(items[i].Pyatka);
            }
            List<string> newPyatka = new List<string>(Pyatka.Distinct());
            newPyatka.Sort();
            ViewBag.Pyatka = newPyatka;
            //------------------------------------------------
            var Lopast = new List<string>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                Lopast.Add(items[i].Lopast);
            }
            List<string> newLopast = new List<string>(Lopast.Distinct());
            newLopast.Sort();
            ViewBag.Lopast = newLopast;
            //------------------------------------------------
            var Material = new List<string>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                Material.Add(items[i].Material);
            }
            List<string> newMaterial = new List<string>(Material.Distinct());
            newMaterial.Sort();
            ViewBag.Material = newMaterial;
            //------------------------------------------------
            var Length = new List<int>();
            var EditLength = 0;
            for (int i = 1; i <= 6; i++)
            {
                EditLength = i * 30;
                if (items.Exists(p => p.Length >= EditLength && p.Length < EditLength + 30))
                {
                    Length.Add(EditLength);
                }
            }
            var newLength = new List<int>(Length.Distinct());
            ViewBag.Length = newLength.ToList();
            //------------------------------------------------

            ViewBag.Count = Item.Count();
            int page = 1;
            int pageSize = 24; // количество объектов на страницу
            IEnumerable<Lasty_dlya_okhoty> GunsPerPages = Item.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = Item.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Lasty_dlya_okhotys = Item
            };

            ViewBag.IVM = ivm;

            GetFilter().Item = Item;

            return View(ivm);
        }

        [HttpPost]
        public ActionResult Count()
        {
            List<Lasty_dlya_okhoty> Item = GetFilter().Item;

            return Json(Item.Count());
        }
    }
    public class FilterLasty_dlya_okhoty
    {
        public List<Lasty_dlya_okhoty> Item { get; set; }
        public int CheckedBrand { get; set; }
        public int CheckedCountry { get; set; }
        public int CheckedPyatka { get; set; }
        public int CheckedLopast { get; set; }
        public int CheckedMaterial { get; set; }
        public int CheckedLength { get; set; }
        public int CheckedCount { get; set; }
        public int CheckedMinPrice { get; set; }
        public int CheckedMaxPrice { get; set; }

    }
}