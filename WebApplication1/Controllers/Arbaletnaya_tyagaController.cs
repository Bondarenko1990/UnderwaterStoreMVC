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
    public class Arbaletnaya_tyagaController : Controller
    {
        UnitOfWork unitOfWork;

        public Arbaletnaya_tyagaController()
        {
            unitOfWork = new UnitOfWork();
        }

        public FilterArbaletnaya_tyaga GetFilter()
        {
            FilterArbaletnaya_tyaga filter = (FilterArbaletnaya_tyaga)Session["FilterArbaletnaya_tyaga"];
            if (filter == null)
            {
                filter = new FilterArbaletnaya_tyaga();
                Session["FilterArbaletnaya_tyaga"] = filter;
            }
            return filter;
        }

        public FileContentResult GetImage(int Id)
        {
            Arbaletnaya_tyaga item = unitOfWork.Arbaletnaya_tyagas.GetAll()
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
            return View("Edit", new Arbaletnaya_tyaga());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int Id)
        {
            Arbaletnaya_tyaga deletedItem = unitOfWork.Arbaletnaya_tyagas.Delete(Id);
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
           return View(unitOfWork.Arbaletnaya_tyagas.GetAll().ToList());
        }

        [Authorize(Roles = "Admin")]
        public ViewResult Edit(int Id)
        {
            Arbaletnaya_tyaga item = unitOfWork.Arbaletnaya_tyagas.GetAll().FirstOrDefault(g => g.Id == Id);
            SelectList brands = new SelectList(unitOfWork.Brands.GetAll(), "Id", "Name");
            ViewBag.Brands = brands;
            return View(item);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Arbaletnaya_tyaga item, HttpPostedFileBase image = null)
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
                    item.ImageMimeType = unitOfWork.Arbaletnaya_tyagas.Get(item.Id).ImageMimeType;
                    item.ImageData = unitOfWork.Arbaletnaya_tyagas.Get(item.Id).ImageData;
                }
                unitOfWork.Arbaletnaya_tyagas.SaveItem(item);
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
            Arbaletnaya_tyaga c = unitOfWork.Arbaletnaya_tyagas.GetAll().FirstOrDefault(com => com.Id == id);
            if (c != null)
                return PartialView(c);
            return HttpNotFound();
        }

        public ActionResult Details(int id)
        {
            Arbaletnaya_tyaga c = unitOfWork.Arbaletnaya_tyagas.GetAll().FirstOrDefault(com => com.Id == id);
            if (c != null)
                return PartialView(c);
            return HttpNotFound();
        }

        [HttpPost]
        public PartialViewResult Arbaletnaya_tyagaModel(int page = 1)
        {

            int pageSize = 24;

            IEnumerable<Arbaletnaya_tyaga> GunsPerPages = GetFilter().Item.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = GetFilter().Item.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Arbaletnaya_tyagas = GunsPerPages
            };
            ViewBag.IVM = ivm;
            return PartialView(ivm);
        }

        public ActionResult Arbaletnaya_tyaga(int page = 1)
        {

            int count = unitOfWork.Arbaletnaya_tyagas.GetAll().Count();
            var items = unitOfWork.Arbaletnaya_tyagas.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();
            List<Arbaletnaya_tyaga> Item;
            if (GetFilter().Item == null)
            {
                Item = unitOfWork.Arbaletnaya_tyagas.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();
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
            var diametr = new List<decimal>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                diametr.Add(items[i].Diametr);
            }
            var newdiametr = new List<decimal>(diametr.Distinct());
            newdiametr.Sort();
            ViewBag.Diametr = newdiametr;
            //------------------------------------------------
            var type = new List<string>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                type.Add(items[i].Type);
            }
            List<string> newtype = new List<string>(type.Distinct());
            newtype.Reverse();
            ViewBag.Type = newtype;
            //------------------------------------------------
            ViewBag.MinPrice = unitOfWork.Arbaletnaya_tyagas.GetAll().Min(a => a.Price);
            ViewBag.MaxPrice = unitOfWork.Arbaletnaya_tyagas.GetAll().Max(a => a.Price);
            ViewBag.CheckedMinPrice = GetFilter().CheckedMinPrice;
            if (GetFilter().CheckedMaxPrice == 0)
            {
                ViewBag.CheckedMaxPrice = unitOfWork.Arbaletnaya_tyagas.GetAll().Max(a => a.Price);
            }
            else
            {
                ViewBag.CheckedMaxPrice = GetFilter().CheckedMaxPrice;
            }
            ViewBag.CheckedBrand = GetFilter().CheckedBrand;
            ViewBag.CheckedCountry = GetFilter().CheckedCountry;
            ViewBag.CheckedDiametr = GetFilter().CheckedDiametr;
            ViewBag.CheckedType = GetFilter().CheckedType;
            ViewBag.CheckedCount = GetFilter().CheckedCount;
            int pageSize = 24; // количество объектов на страницу
            IEnumerable<Arbaletnaya_tyaga> GunsPerPages = Item.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = Item.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Arbaletnaya_tyagas = GunsPerPages
            };

            ViewBag.IVM = ivm;

            return View(ivm);
        }  

        [HttpPost]
        public ActionResult Arbaletnaya_tyagaFilter(int MinPrice, int MaxPrice, string[] brand, string[] country, decimal[] diametr, string[] type)
        {
            int count = unitOfWork.Arbaletnaya_tyagas.GetAll().Count();
            List<Arbaletnaya_tyaga> Item = unitOfWork.Arbaletnaya_tyagas.GetAll().Where(p => p.Price >= MinPrice && p.Price <= MaxPrice).ToList();
            List<Arbaletnaya_tyaga> newItem = new List<Arbaletnaya_tyaga>();

            GetFilter().CheckedMinPrice = MinPrice;
            GetFilter().CheckedMaxPrice = MaxPrice;
            GetFilter().CheckedBrand = 0;
            GetFilter().CheckedCountry = 0;
            GetFilter().CheckedDiametr = 0;
            GetFilter().CheckedType = 0;
            GetFilter().CheckedCount = 0;

            ViewBag.MinPrice = unitOfWork.Arbaletnaya_tyagas.GetAll().Min(a => a.Price);
            ViewBag.MaxPrice = unitOfWork.Arbaletnaya_tyagas.GetAll().Max(a => a.Price);
            ViewBag.CheckedMinPrice = MinPrice;
            ViewBag.CheckedMaxPrice = MaxPrice;
            ViewBag.CheckedBrand = 0;
            ViewBag.CheckedCountry = 0;
            ViewBag.CheckedDiametr = 0;
            ViewBag.CheckedType = 0;
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
                newItem = new List<Arbaletnaya_tyaga>();
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
            if (diametr != null)
            {
                newItem = new List<Arbaletnaya_tyaga>();
                decimal a = 0;
                for (int i = 0; i < diametr.Count(); i++)
                {

                    a = diametr[i];
                    foreach (var c in Item.Where(p => p.Diametr == a))
                    {
                        newItem.Add(c);
                    }
                }
                Item = newItem;
                GetFilter().CheckedDiametr = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedDiametr = 1;
                Count++;
            }
            
            if (type != null)
            {
                newItem = new List<Arbaletnaya_tyaga>();
                string a = "";
                for (int i = 0; i < type.Count(); i++)
                {

                    a = type[i];
                    foreach (var c in Item.Where(p => p.Type == a))
                    {
                        newItem.Add(c);
                    }
                }
                Item = newItem;
                GetFilter().CheckedType = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedType = 1;
                Count++;
            }
            
            ViewBag.CheckedCount = Count;

            var items = unitOfWork.Arbaletnaya_tyagas.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();

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
            var Diametr = new List<decimal>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                Diametr.Add(items[i].Diametr);
            }
            List<decimal> newdiametr = new List<decimal>(Diametr.Distinct());
            newdiametr.Sort();
            ViewBag.Diametr = newdiametr;
            //------------------------------------------------
            var Type = new List<string>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                Type.Add(items[i].Type);
            }
            List<string> newtype = new List<string>(Type.Distinct());
            newtype.Reverse();
            ViewBag.Type = newtype;
            //------------------------------------------------
            ViewBag.Count = Item.Count();
            int page = 1;
            int pageSize = 24; // количество объектов на страницу
            IEnumerable<Arbaletnaya_tyaga> GunsPerPages = Item.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = Item.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Arbaletnaya_tyagas = Item
            };

            ViewBag.IVM = ivm;

            GetFilter().Item = Item;

            return View(ivm);
        }

        [HttpPost]
        public ActionResult Arbaletnaya_tyagaCount()
        {
            List<Arbaletnaya_tyaga> Item = GetFilter().Item;

            return Json(Item.Count());
        }

    }
    public class FilterArbaletnaya_tyaga
    {
        public List<Arbaletnaya_tyaga> Item { get; set; }
        public int CheckedMinPrice { get; set; }
        public int CheckedMaxPrice { get; set; }
        public int CheckedBrand { get; set; }
        public int CheckedCountry { get; set; }
        public int CheckedDiametr { get; set; }
        public int CheckedType { get; set; }
        public int CheckedCount { get; set; }
    }
}