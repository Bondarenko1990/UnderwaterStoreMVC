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
    public class MaskiController : Controller
    {
        UnitOfWork unitOfWork;
        public MaskiController()
        {
            unitOfWork = new UnitOfWork();
        }

        public FilterMaski GetFilter()
        {
            FilterMaski filter = (FilterMaski)Session["FilterMaski"];
            if (filter == null)
            {
                filter = new FilterMaski();
                Session["FilterMaski"] = filter;
            }
            return filter;
        }

        public FileContentResult GetImage(int Id)
        {
            Maski item = unitOfWork.Maskis.GetAll().FirstOrDefault(g => g.Id == Id);
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
            return View("Edit", new Maski());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int Id)
        {
            Maski deletedItem = unitOfWork.Maskis.Delete(Id);
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
            return View(unitOfWork.Maskis.GetAll().ToList());
        }

        [Authorize(Roles = "Admin")]
        public ViewResult Edit(int Id)
        {
            Maski item = unitOfWork.Maskis.GetAll().FirstOrDefault(g => g.Id == Id);
            SelectList brands = new SelectList(unitOfWork.Brands.GetAll(), "Id", "Name");
            ViewBag.Brands = brands;
            return View(item);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Maski item, HttpPostedFileBase image = null)
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
                    item.ImageMimeType = unitOfWork.Maskis.Get(item.Id).ImageMimeType;
                    item.ImageData = unitOfWork.Maskis.Get(item.Id).ImageData;
                }
                unitOfWork.Maskis.SaveItem(item);
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
            Maski c = unitOfWork.Maskis.GetAll().FirstOrDefault(com => com.Id == id);
            if (c != null)
                return PartialView(c);
            return HttpNotFound();
        }

        public ActionResult Details(int id)
        {
            Maski c = unitOfWork.Maskis.GetAll().FirstOrDefault(com => com.Id == id);
            if (c != null)
                return PartialView(c);
            return HttpNotFound();
        }

        [HttpPost]
        public PartialViewResult Models(int page = 1)
        {

            int pageSize = 24;

            IEnumerable<Maski> GunsPerPages = GetFilter().Item.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = GetFilter().Item.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Maskis = GunsPerPages
            };
            ViewBag.IVM = ivm;
            return PartialView(ivm);
        }

        public ActionResult List(int page = 1)
        {

            int count = unitOfWork.Maskis.GetAll().Count();
            var items = unitOfWork.Maskis.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();
            List<Maski> Item;
            if (GetFilter().Item == null)
            {
                Item = unitOfWork.Maskis.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();
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
            var Primenenie = new List<string>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                Primenenie.Add(items[i].Primenenie);
            }
            List<string> newPrimenenie = new List<string>(Primenenie.Distinct());
            newPrimenenie.Sort();
            ViewBag.Primenenie = newPrimenenie;
            //------------------------------------------------
            var Glass = new List<string>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                Glass.Add(items[i].Glass);
            }
            List<string> newGlass = new List<string>(Glass.Distinct());
            newGlass.Sort();
            ViewBag.Glass = newGlass;
            //------------------------------------------------
            ViewBag.MinPrice = unitOfWork.Maskis.GetAll().Min(a => a.Price);
            ViewBag.MaxPrice = unitOfWork.Maskis.GetAll().Max(a => a.Price);
            ViewBag.CheckedMinPrice = GetFilter().CheckedMinPrice;
            if (GetFilter().CheckedMaxPrice == 0)
            {
                ViewBag.CheckedMaxPrice = unitOfWork.Maskis.GetAll().Max(a => a.Price);
            }
            else
            {
                ViewBag.CheckedMaxPrice = GetFilter().CheckedMaxPrice;
            }
            ViewBag.CheckedBrand = GetFilter().CheckedBrand;
            ViewBag.CheckedCountry = GetFilter().CheckedCountry;
            ViewBag.CheckedPrimenenie = GetFilter().CheckedPrimenenie;
            ViewBag.CheckedGlass = GetFilter().CheckedGlass;
            ViewBag.CheckedCount = GetFilter().CheckedCount;
            int pageSize = 24; // количество объектов на страницу
            IEnumerable<Maski> GunsPerPages = Item.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = Item.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Maskis = GunsPerPages
            };

            ViewBag.IVM = ivm;

            return View(ivm);
        }

        [HttpPost]
        public ActionResult Filter(int MinPrice, int MaxPrice, string[] brand, string[] country, string[] primenenie, string[] glass)
        {
            int count = unitOfWork.Maskis.GetAll().Count();
            List<Maski> Item = unitOfWork.Maskis.GetAll().Where(p => p.Price >= MinPrice && p.Price <= MaxPrice).ToList();
            List<Maski> newItem = new List<Maski>();

            GetFilter().CheckedMinPrice = MinPrice;
            GetFilter().CheckedMaxPrice = MaxPrice;
            GetFilter().CheckedBrand = 0;
            GetFilter().CheckedCountry = 0;
            GetFilter().CheckedPrimenenie = 0;
            GetFilter().CheckedGlass = 0;
            GetFilter().CheckedCount = 0;


            ViewBag.MinPrice = unitOfWork.Maskis.GetAll().Min(a => a.Price);
            ViewBag.MaxPrice = unitOfWork.Maskis.GetAll().Max(a => a.Price);
            ViewBag.CheckedMinPrice = MinPrice;
            ViewBag.CheckedMaxPrice = MaxPrice;
            ViewBag.CheckedBrand = 0;
            ViewBag.CheckedCountry = 0;
            ViewBag.CheckedPrimenenie = 0;
            ViewBag.CheckedGlass = 0;
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
                newItem = new List<Maski>();
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

            if (primenenie != null)
            {
                newItem = new List<Maski>();
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
            if (glass != null)
            {
                newItem = new List<Maski>();
                string a = "";
                for (int i = 0; i < glass.Count(); i++)
                {

                    a = glass[i];
                    foreach (var c in Item.Where(p => p.Glass == a))
                    {
                        newItem.Add(c);
                    }
                }
                Item = newItem;
                GetFilter().CheckedGlass = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedGlass = 1;
                Count++;
            }

            ViewBag.CheckedCount = Count;

            var items = unitOfWork.Maskis.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();

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
            var Primenenie = new List<string>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                Primenenie.Add(items[i].Primenenie);
            }
            List<string> newPrimenenie = new List<string>(Primenenie.Distinct());
            newPrimenenie.Sort();
            ViewBag.Primenenie = newPrimenenie;
            //------------------------------------------------
            var Glass = new List<string>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                Glass.Add(items[i].Glass);
            }
            List<string> newGlass = new List<string>(Glass.Distinct());
            newGlass.Sort();
            ViewBag.Glass = newGlass;
            //------------------------------------------------

            ViewBag.Count = Item.Count();
            int page = 1;
            int pageSize = 24; // количество объектов на страницу
            IEnumerable<Maski> GunsPerPages = Item.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = Item.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Maskis = Item
            };

            ViewBag.IVM = ivm;

            GetFilter().Item = Item;

            return View(ivm);
        }

        [HttpPost]
        public ActionResult Count()
        {
            List<Maski> Item = GetFilter().Item;

            return Json(Item.Count());
        }
    }
    public class FilterMaski
    {
        public List<Maski> Item { get; set; }
        public int CheckedBrand { get; set; }
        public int CheckedCountry { get; set; }
        public int CheckedPrimenenie { get; set; }
        public int CheckedGlass { get; set; }
        public int CheckedCount { get; set; }
        public int CheckedMinPrice { get; set; }
        public int CheckedMaxPrice { get; set; }

    }
}