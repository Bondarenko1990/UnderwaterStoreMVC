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
    public class SukhieController : Controller
    {
        UnitOfWork unitOfWork;
        public SukhieController()
        {
            unitOfWork = new UnitOfWork();
        }

        public FilterSukhie GetFilter()
        {
            FilterSukhie filter = (FilterSukhie)Session["FilterSukhie"];
            if (filter == null)
            {
                filter = new FilterSukhie();
                Session["FilterSukhie"] = filter;
            }
            return filter;
        }

        public FileContentResult GetImage(int Id)
        {
            Sukhie item = unitOfWork.Sukhies.GetAll()
                .FirstOrDefault(g => g.Id == Id);
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
            return View("Edit", new Sukhie());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int Id)
        {
            Sukhie deletedItem = unitOfWork.Sukhies.Delete(Id);
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
            return View(unitOfWork.Sukhies.GetAll().ToList());
        }

        [Authorize(Roles = "Admin")]
        public ViewResult Edit(int Id)
        {
            Sukhie item = unitOfWork.Sukhies.GetAll().FirstOrDefault(g => g.Id == Id);
            SelectList brands = new SelectList(unitOfWork.Brands.GetAll(), "Id", "Name");
            ViewBag.Brands = brands;
            return View(item);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Sukhie item, HttpPostedFileBase image = null)
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
                    item.ImageMimeType = unitOfWork.Sukhies.Get(item.Id).ImageMimeType;
                    item.ImageData = unitOfWork.Sukhies.Get(item.Id).ImageData;
                }
                unitOfWork.Sukhies.SaveItem(item);
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
            Sukhie c = unitOfWork.Sukhies.GetAll().FirstOrDefault(com => com.Id == id);
            if (c != null)
                return PartialView(c);
            return HttpNotFound();
        }

        public ActionResult Details(int id)
        {
            Sukhie c = unitOfWork.Sukhies.GetAll().FirstOrDefault(com => com.Id == id);
            if (c != null)
                return PartialView(c);
            return HttpNotFound();
        }

        [HttpPost]
        public PartialViewResult Models(int page = 1)
        {

            int pageSize = 24;

            IEnumerable<Sukhie> GunsPerPages = GetFilter().Item.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = GetFilter().Item.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Sukhies = GunsPerPages
            };
            ViewBag.IVM = ivm;
            return PartialView(ivm);
        }

        public ActionResult List(int page = 1)
        {

            int count = unitOfWork.Sukhies.GetAll().Count();
            var items = unitOfWork.Sukhies.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();
            List<Sukhie> Item;
            if (GetFilter().Item == null)
            {
                Item = unitOfWork.Sukhies.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();
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
            var Material = new List<string>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                Material.Add(items[i].Material);
            }
            List<string> newMaterial = new List<string>(Material.Distinct());
            newMaterial.Sort();
            ViewBag.Material = newMaterial;
            //------------------------------------------------
            var Molniya = new List<string>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                Molniya.Add(items[i].Molniya);
            }
            List<string> newMolniya = new List<string>(Molniya.Distinct());
            newMolniya.Sort();
            ViewBag.Molniya = newMolniya;
            //------------------------------------------------
            ViewBag.MinPrice = unitOfWork.Sukhies.GetAll().Min(a => a.Price);
            ViewBag.MaxPrice = unitOfWork.Sukhies.GetAll().Max(a => a.Price);
            ViewBag.CheckedMinPrice = GetFilter().CheckedMinPrice;
            if (GetFilter().CheckedMaxPrice == 0)
            {
                ViewBag.CheckedMaxPrice = unitOfWork.Sukhies.GetAll().Max(a => a.Price);
            }
            else
            {
                ViewBag.CheckedMaxPrice = GetFilter().CheckedMaxPrice;
            }
            ViewBag.CheckedBrand = GetFilter().CheckedBrand;
            ViewBag.CheckedCountry = GetFilter().CheckedCountry;
            ViewBag.CheckedType = GetFilter().CheckedMaterial;
            ViewBag.CheckedPol = GetFilter().CheckedMolniya;
            ViewBag.CheckedCount = GetFilter().CheckedCount;
            int pageSize = 24; // количество объектов на страницу
            IEnumerable<Sukhie> GunsPerPages = Item.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = Item.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Sukhies = GunsPerPages
            };

            ViewBag.IVM = ivm;

            return View(ivm);
        }

        [HttpPost]
        public ActionResult Filter(int MinPrice, int MaxPrice, string[] brand, string[] country, string[] material, string[] molniya)
        {
            int count = unitOfWork.Sukhies.GetAll().Count();
            List<Sukhie> Item = unitOfWork.Sukhies.GetAll().Where(p => p.Price >= MinPrice && p.Price <= MaxPrice).ToList();
            List<Sukhie> newItem = new List<Sukhie>();

            GetFilter().CheckedMinPrice = MinPrice;
            GetFilter().CheckedMaxPrice = MaxPrice;
            GetFilter().CheckedBrand = 0;
            GetFilter().CheckedCountry = 0;
            GetFilter().CheckedMaterial = 0;
            GetFilter().CheckedMolniya = 0;
            GetFilter().CheckedCount = 0;


            ViewBag.MinPrice = unitOfWork.Sukhies.GetAll().Min(a => a.Price);
            ViewBag.MaxPrice = unitOfWork.Sukhies.GetAll().Max(a => a.Price);
            ViewBag.CheckedMinPrice = MinPrice;
            ViewBag.CheckedMaxPrice = MaxPrice;
            ViewBag.CheckedBrand = 0;
            ViewBag.CheckedCountry = 0;
            ViewBag.CheckedMaterial = 0;
            ViewBag.CheckedMolniya = 0;
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
                newItem = new List<Sukhie>();
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

            if (material != null)
            {
                newItem = new List<Sukhie>();
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
            if (molniya != null)
            {
                newItem = new List<Sukhie>();
                string a = "";
                for (int i = 0; i < molniya.Count(); i++)
                {

                    a = molniya[i];
                    foreach (var c in Item.Where(p => p.Molniya == a))
                    {
                        newItem.Add(c);
                    }
                }
                Item = newItem;
                GetFilter().CheckedMolniya = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedMolniya = 1;
                Count++;
            }

            ViewBag.CheckedCount = Count;

            var items = unitOfWork.Sukhies.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();

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
            var Material = new List<string>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                Material.Add(items[i].Material);
            }
            List<string> newMaterial = new List<string>(Material.Distinct());
            newMaterial.Sort();
            ViewBag.Material = newMaterial;
            //------------------------------------------------
            var Molniya = new List<string>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                Molniya.Add(items[i].Molniya);
            }
            List<string> newMolniya = new List<string>(Molniya.Distinct());
            newMolniya.Sort();
            ViewBag.Molniya = newMolniya;
            //------------------------------------------------

            ViewBag.Count = Item.Count();
            int page = 1;
            int pageSize = 24; // количество объектов на страницу
            IEnumerable<Sukhie> GunsPerPages = Item.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = Item.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Sukhies = Item
            };

            ViewBag.IVM = ivm;

            GetFilter().Item = Item;

            return View(ivm);
        }

        [HttpPost]
        public ActionResult Count()
        {
            List<Sukhie> Item = GetFilter().Item;

            return Json(Item.Count());
        }

    }
    public class FilterSukhie
    {
        public List<Sukhie> Item { get; set; }
        public int CheckedBrand { get; set; }
        public int CheckedCountry { get; set; }
        public int CheckedMaterial { get; set; }
        public int CheckedMolniya { get; set; }
        public int CheckedCount { get; set; }
        public int CheckedMinPrice { get; set; }
        public int CheckedMaxPrice { get; set; }
    }
}