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
    public class Bui_i_aksessuaryController : Controller
    {
        UnitOfWork unitOfWork;
        public Bui_i_aksessuaryController()
        {
            unitOfWork = new UnitOfWork();
        }

        public FilterBui_i_aksessuary GetFilter()
        {
            FilterBui_i_aksessuary filter = (FilterBui_i_aksessuary)Session["FilterBui_i_aksessuary"];
            if (filter == null)
            {
                filter = new FilterBui_i_aksessuary();
                Session["FilterBui_i_aksessuary"] = filter;
            }
            return filter;
        }

        public FileContentResult GetImage(int Id)
        {
            Bui_i_aksessuary item = unitOfWork.Bui_i_aksessuarys.GetAll().FirstOrDefault(g => g.Id == Id);

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
            return View("Edit", new Bui_i_aksessuary());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int Id)
        {
            Bui_i_aksessuary deletedItem = unitOfWork.Bui_i_aksessuarys.Delete(Id);
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
            return View(unitOfWork.Bui_i_aksessuarys.GetAll().ToList());
        }

        [Authorize(Roles = "Admin")]
        public ViewResult Edit(int Id)
        {
            Bui_i_aksessuary item = unitOfWork.Bui_i_aksessuarys.GetAll().FirstOrDefault(g => g.Id == Id);
            SelectList brands = new SelectList(unitOfWork.Brands.GetAll(), "Id", "Name");
            ViewBag.Brands = brands;
            return View(item);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Bui_i_aksessuary item, HttpPostedFileBase image = null)
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
                    item.ImageMimeType = unitOfWork.Bui_i_aksessuarys.Get(item.Id).ImageMimeType;
                    item.ImageData = unitOfWork.Bui_i_aksessuarys.Get(item.Id).ImageData;
                }
                unitOfWork.Bui_i_aksessuarys.SaveItem(item);
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
            Bui_i_aksessuary c = unitOfWork.Bui_i_aksessuarys.GetAll().FirstOrDefault(com => com.Id == id);
            if (c != null)
                return PartialView(c);
            return HttpNotFound();
        }

        public ActionResult Details(int id)
        {
            Bui_i_aksessuary c = unitOfWork.Bui_i_aksessuarys.GetAll().FirstOrDefault(com => com.Id == id);
            if (c != null)
                return PartialView(c);
            return HttpNotFound();
        }

        [HttpPost]
        public PartialViewResult Models(int page = 1)
        {

            int pageSize = 24;

            IEnumerable<Bui_i_aksessuary> GunsPerPages = GetFilter().Item.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = GetFilter().Item.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Bui_i_aksessuarys = GunsPerPages
            };
            ViewBag.IVM = ivm;
            return PartialView(ivm);
        }

        public ActionResult List(int page = 1)
        {

            int count = unitOfWork.Bui_i_aksessuarys.GetAll().Count();
            var items = unitOfWork.Bui_i_aksessuarys.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();
            List<Bui_i_aksessuary> Item;
            if (GetFilter().Item == null)
            {
                Item = unitOfWork.Bui_i_aksessuarys.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();
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
            ViewBag.MinPrice = unitOfWork.Bui_i_aksessuarys.GetAll().Min(a => a.Price);
            ViewBag.MaxPrice = unitOfWork.Bui_i_aksessuarys.GetAll().Max(a => a.Price);
            ViewBag.CheckedMinPrice = GetFilter().CheckedMinPrice;
            if (GetFilter().CheckedMaxPrice == 0)
            {
                ViewBag.CheckedMaxPrice = unitOfWork.Bui_i_aksessuarys.GetAll().Max(a => a.Price);
            }
            else
            {
                ViewBag.CheckedMaxPrice = GetFilter().CheckedMaxPrice;
            }
            ViewBag.CheckedBrand = GetFilter().CheckedBrand;
            ViewBag.CheckedCountry = GetFilter().CheckedCountry;
            ViewBag.CheckedCount = GetFilter().CheckedCount;
            int pageSize = 24; // количество объектов на страницу
            IEnumerable<Bui_i_aksessuary> GunsPerPages = Item.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = Item.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Bui_i_aksessuarys = GunsPerPages
            };

            ViewBag.IVM = ivm;

            return View(ivm);
        }

        [HttpPost]
        public ActionResult Filter(int MinPrice, int MaxPrice, string[] brand, string[] country)
        {
            int count = unitOfWork.Bui_i_aksessuarys.GetAll().Count();
            List<Bui_i_aksessuary> Item = unitOfWork.Bui_i_aksessuarys.GetAll().Where(p => p.Price >= MinPrice && p.Price <= MaxPrice).ToList();
            List<Bui_i_aksessuary> newItem = new List<Bui_i_aksessuary>();

            GetFilter().CheckedMinPrice = MinPrice;
            GetFilter().CheckedMaxPrice = MaxPrice;
            GetFilter().CheckedBrand = 0;
            GetFilter().CheckedCountry = 0;
            GetFilter().CheckedCount = 0;


            ViewBag.MinPrice = unitOfWork.Bui_i_aksessuarys.GetAll().Min(a => a.Price);
            ViewBag.MaxPrice = unitOfWork.Bui_i_aksessuarys.GetAll().Max(a => a.Price);
            ViewBag.CheckedMinPrice = MinPrice;
            ViewBag.CheckedMaxPrice = MaxPrice;
            ViewBag.CheckedBrand = 0;
            ViewBag.CheckedCountry = 0;
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
                newItem = new List<Bui_i_aksessuary>();
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

            ViewBag.CheckedCount = Count;

            var items = unitOfWork.Bui_i_aksessuarys.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();

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
            ViewBag.Count = Item.Count();
            int page = 1;
            int pageSize = 24; // количество объектов на страницу
            IEnumerable<Bui_i_aksessuary> GunsPerPages = Item.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = Item.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Bui_i_aksessuarys = Item
            };

            ViewBag.IVM = ivm;

            GetFilter().Item = Item;

            return View(ivm);
        }

        [HttpPost]
        public ActionResult Count()
        {
            List<Bui_i_aksessuary> Item = GetFilter().Item;

            return Json(Item.Count());
        }
    }
    public class FilterBui_i_aksessuary
    {
        public List<Bui_i_aksessuary> Item { get; set; }
        public int CheckedBrand { get; set; }
        public int CheckedCountry { get; set; }
        public int CheckedCount { get; set; }
        public int CheckedMinPrice { get; set; }
        public int CheckedMaxPrice { get; set; }
    }
}