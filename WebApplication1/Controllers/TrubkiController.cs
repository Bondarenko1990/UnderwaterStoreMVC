using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Domain.Core;
using WebApplication1.Infrastructure.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TrubkiController : Controller
    {
        UnitOfWork unitOfWork;
        public TrubkiController()
        {
            unitOfWork = new UnitOfWork();
        }

        public FilterTrubki GetFilter()
        {
            FilterTrubki filter = (FilterTrubki)Session["FilterTrubki"];
            if (filter == null)
            {
                filter = new FilterTrubki();
                Session["FilterTrubki"] = filter;
            }
            return filter;
        }

        public FileContentResult GetImage(int Id)
        {
            Trubki item = unitOfWork.Trubkis.GetAll().FirstOrDefault(g => g.Id == Id);

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
            return View("Edit", new Trubki());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int Id)
        {
            Trubki deletedItem = unitOfWork.Trubkis.Delete(Id);
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
            return View(unitOfWork.Trubkis.GetAll().ToList());
        }

        [Authorize(Roles = "Admin")]
        public ViewResult Edit(int Id)
        {
            Trubki item = unitOfWork.Trubkis.GetAll().FirstOrDefault(g => g.Id == Id);
            SelectList brands = new SelectList(unitOfWork.Brands.GetAll(), "Id", "Name");
            ViewBag.Brands = brands;
            return View(item);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Trubki item, HttpPostedFileBase image = null)
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
                    item.ImageMimeType = unitOfWork.Trubkis.Get(item.Id).ImageMimeType;
                    item.ImageData = unitOfWork.Trubkis.Get(item.Id).ImageData;
                }
                unitOfWork.Trubkis.SaveItem(item);
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
            Trubki c = unitOfWork.Trubkis.GetAll().FirstOrDefault(com => com.Id == id);
            if (c != null)
                return PartialView(c);
            return HttpNotFound();
        }

        public ActionResult Details(int id)
        {
            Trubki c = unitOfWork.Trubkis.GetAll().FirstOrDefault(com => com.Id == id);
            if (c != null)
                return PartialView(c);
            return HttpNotFound();
        }

        [HttpPost]
        public PartialViewResult Models(int page = 1)
        {

            int pageSize = 24;

            IEnumerable<Trubki> GunsPerPages = GetFilter().Item.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = GetFilter().Item.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Trubkis = GunsPerPages
            };
            ViewBag.IVM = ivm;
            return PartialView(ivm);
        }

        public ActionResult List(int page = 1)
        {

            int count = unitOfWork.Trubkis.GetAll().Count();
            var items = unitOfWork.Trubkis.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();
            List<Trubki> Item;
            if (GetFilter().Item == null)
            {
                Item = unitOfWork.Trubkis.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();
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
            var Klapan = new List<bool>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                Klapan.Add(items[i].Klapan);
            }
            List<bool> newKlapan = new List<bool>(Klapan.Distinct());
            newKlapan.Sort();
            newKlapan.Reverse();
            ViewBag.Klapan = newKlapan;
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
            var GofrVstavka = new List<bool>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                GofrVstavka.Add(items[i].GofrVstavka);
            }
            List<bool> newGofrVstavka = new List<bool>(GofrVstavka.Distinct());
            newGofrVstavka.Sort();
            newGofrVstavka.Reverse();
            ViewBag.GofrVstavka = newGofrVstavka;
            //------------------------------------------------
            var Freetop = new List<bool>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                Freetop.Add(items[i].Freetop);
            }
            List<bool> newFreetop = new List<bool>(Freetop.Distinct());
            newFreetop.Sort();
            newFreetop.Reverse();
            ViewBag.Freetop = newFreetop;
            //------------------------------------------------
            var Primenenie = new List<string>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                Primenenie.Add(items[i].Primenenie);
            }
            List<string> newPrimenenie = new List<string>(Primenenie.Distinct());
            newPrimenenie.Sort();
            ViewBag.Primenenie = newPrimenenie;
            //------------------------------------------------
            ViewBag.MinPrice = unitOfWork.Trubkis.GetAll().Min(a => a.Price);
            ViewBag.MaxPrice = unitOfWork.Trubkis.GetAll().Max(a => a.Price);
            ViewBag.CheckedMinPrice = GetFilter().CheckedMinPrice;
            if (GetFilter().CheckedMaxPrice == 0)
            {
                ViewBag.CheckedMaxPrice = unitOfWork.Trubkis.GetAll().Max(a => a.Price);
            }
            else
            {
                ViewBag.CheckedMaxPrice = GetFilter().CheckedMaxPrice;
            }
            ViewBag.CheckedBrand = GetFilter().CheckedBrand;
            ViewBag.CheckedCountry = GetFilter().CheckedCountry;
            ViewBag.CheckedKlapan = GetFilter().CheckedKlapan;
            ViewBag.CheckedMaterial = GetFilter().CheckedMaterial;
            ViewBag.CheckedGofrVstavka = GetFilter().CheckedGofrVstavka;
            ViewBag.CheckedFreetop = GetFilter().CheckedFreetop;
            ViewBag.CheckedPrimenenie = GetFilter().CheckedPrimenenie;
            ViewBag.CheckedCount = GetFilter().CheckedCount;
            int pageSize = 24; // количество объектов на страницу
            IEnumerable<Trubki> GunsPerPages = Item.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = Item.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Trubkis = GunsPerPages
            };

            ViewBag.IVM = ivm;

            return View(ivm);
        }

        [HttpPost]
        public ActionResult Filter(int MinPrice, int MaxPrice, string[] brand, string[] country, bool[] klapan, string[] material,
            bool[] gofrvstavka, bool[] freetop, string[] primenenie )
        {
            int count = unitOfWork.Trubkis.GetAll().Count();
            List<Trubki> Item = unitOfWork.Trubkis.GetAll().Where(p => p.Price >= MinPrice && p.Price <= MaxPrice).ToList();
            List<Trubki> newItem = new List<Trubki>();

            GetFilter().CheckedMinPrice = MinPrice;
            GetFilter().CheckedMaxPrice = MaxPrice;
            GetFilter().CheckedBrand = 0;
            GetFilter().CheckedCountry = 0;
            GetFilter().CheckedKlapan = 0;
            GetFilter().CheckedMaterial = 0;
            GetFilter().CheckedGofrVstavka = 0;
            GetFilter().CheckedFreetop = 0;
            GetFilter().CheckedPrimenenie = 0;
            GetFilter().CheckedCount = 0;


            ViewBag.MinPrice = unitOfWork.Trubkis.GetAll().Min(a => a.Price);
            ViewBag.MaxPrice = unitOfWork.Trubkis.GetAll().Max(a => a.Price);
            ViewBag.CheckedMinPrice = MinPrice;
            ViewBag.CheckedMaxPrice = MaxPrice;
            ViewBag.CheckedBrand = 0;
            ViewBag.CheckedCountry = 0;
            ViewBag.CheckedKlapan = 0;
            ViewBag.CheckedMaterial = 0;
            ViewBag.CheckedGofrVstavka = 0;
            ViewBag.CheckedFreetop = 0;
            ViewBag.CheckedPrimenenie = 0;
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
                newItem = new List<Trubki>();
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

            if (klapan != null)
            {
                newItem = new List<Trubki>();
                bool a;
                for (int i = 0; i < klapan.Count(); i++)
                {

                    a = klapan[i];
                    foreach (var c in Item.Where(p => p.Klapan == a))
                    {
                        newItem.Add(c);
                    }
                }
                Item = newItem;
                GetFilter().CheckedKlapan = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedKlapan = 1;
                Count++;
            }

            if (material != null)
            {
                newItem = new List<Trubki>();
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


            if (gofrvstavka != null)
            {
                newItem = new List<Trubki>();
                bool a;
                for (int i = 0; i < gofrvstavka.Count(); i++)
                {

                    a = gofrvstavka[i];
                    foreach (var c in Item.Where(p => p.GofrVstavka == a))
                    {
                        newItem.Add(c);
                    }
                }
                Item = newItem;
                GetFilter().CheckedGofrVstavka = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedGofrVstavka = 1;
                Count++;
            }
            if (freetop != null)
            {
                newItem = new List<Trubki>();
                bool a;
                for (int i = 0; i < freetop.Count(); i++)
                {

                    a = freetop[i];
                    foreach (var c in Item.Where(p => p.Freetop == a))
                    {
                        newItem.Add(c);
                    }
                }
                Item = newItem;
                GetFilter().CheckedFreetop = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedFreetop = 1;
                Count++;
            }
            if (primenenie != null)
            {
                newItem = new List<Trubki>();
                string a = "";
                for (int i = 0; i < primenenie.Count(); i++)
                {

                    a = primenenie[i];
                    foreach (var c in Item.Where(p => p.Primenenie == a))
                    {
                        newItem.Add(c);
                    }
                }
                Item = newItem;
                GetFilter().CheckedPrimenenie = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedPrimenenie = 1;
                Count++;
            }

            ViewBag.CheckedCount = Count;

            var items = unitOfWork.Trubkis.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();

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
            var Klapan = new List<bool>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                Klapan.Add(items[i].Klapan);
            }
            List<bool> newKlapan = new List<bool>(Klapan.Distinct());
            newKlapan.Sort();
            newKlapan.Reverse();
            ViewBag.Klapan = newKlapan;
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
            var GofrVstavka = new List<bool>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                GofrVstavka.Add(items[i].GofrVstavka);
            }
            List<bool> newGofrVstavka = new List<bool>(GofrVstavka.Distinct());
            newGofrVstavka.Sort();
            newGofrVstavka.Reverse();
            ViewBag.GofrVstavka = newGofrVstavka;
            //------------------------------------------------
            var Freetop = new List<bool>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                Freetop.Add(items[i].Freetop);
            }
            List<bool> newFreetop = new List<bool>(Freetop.Distinct());
            newFreetop.Sort();
            newFreetop.Reverse();
            ViewBag.Freetop = newFreetop;
            //------------------------------------------------
            var Primenenie = new List<string>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                Primenenie.Add(items[i].Primenenie);
            }
            List<string> newPrimenenie = new List<string>(Primenenie.Distinct());
            newPrimenenie.Sort();
            ViewBag.Primenenie = newPrimenenie;
            //------------------------------------------------

            ViewBag.Count = Item.Count();
            int page = 1;
            int pageSize = 24; // количество объектов на страницу
            IEnumerable<Trubki> GunsPerPages = Item.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = Item.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Trubkis = Item
            };

            ViewBag.IVM = ivm;

            GetFilter().Item = Item;

            return View(ivm);
        }

        [HttpPost]
        public ActionResult Count()
        {
            List<Trubki> Item = GetFilter().Item;

            return Json(Item.Count());
        }

    }
    public class FilterTrubki
    {
        public List<Trubki> Item { get; set; }
        public int CheckedBrand { get; set; }
        public int CheckedCountry { get; set; }
        public int CheckedKlapan { get; set; }
        public int CheckedMaterial { get; set; }
        public int CheckedGofrVstavka { get; set; }
        public int CheckedFreetop { get; set; }
        public int CheckedPrimenenie { get; set; }
        public int CheckedCount { get; set; }
        public int CheckedMinPrice { get; set; }
        public int CheckedMaxPrice { get; set; }

    }
}