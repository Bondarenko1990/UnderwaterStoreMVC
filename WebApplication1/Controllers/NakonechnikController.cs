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
    public class NakonechnikController : Controller
    {
        UnitOfWork unitOfWork;

        public NakonechnikController()
        {
            unitOfWork = new UnitOfWork();
        }

        public FilterNakonechnik GetFilter()

        {
            FilterNakonechnik filter = (FilterNakonechnik)Session["FilterNakonechnik"];
            if (filter == null)
            {
                filter = new FilterNakonechnik();
                Session["FilterNakonechnik"] = filter;
            }
            return filter;
        }

        public FileContentResult GetImage(int Id)
        {
            Nakonechnik item = unitOfWork.Nakonechniks.GetAll()
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
            return View("Edit", new Nakonechnik());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int Id)
        {
            Nakonechnik deletedItem = unitOfWork.Nakonechniks.Delete(Id);
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
           return View(unitOfWork.Nakonechniks.GetAll().ToList());
        }

        [Authorize(Roles = "Admin")]
        public ViewResult Edit(int Id)
        {
            Nakonechnik item = unitOfWork.Nakonechniks.GetAll().FirstOrDefault(g => g.Id == Id);
            SelectList brands = new SelectList(unitOfWork.Brands.GetAll(), "Id", "Name");
            ViewBag.Brands = brands;
            return View(item);
        }
    
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Nakonechnik item, HttpPostedFileBase image = null)
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
                    item.ImageMimeType = unitOfWork.Nakonechniks.Get(item.Id).ImageMimeType;
                    item.ImageData = unitOfWork.Nakonechniks.Get(item.Id).ImageData;
                }
                unitOfWork.Nakonechniks.SaveItem(item);
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
            Nakonechnik c = unitOfWork.Nakonechniks.GetAll().FirstOrDefault(com => com.Id == id);
            if (c != null)
                return PartialView(c);
            return HttpNotFound();
        }

        public ActionResult Details(int id)
        {
            Nakonechnik c = unitOfWork.Nakonechniks.GetAll().FirstOrDefault(com => com.Id == id);
            if (c != null)
                return PartialView(c);
            return HttpNotFound();
        }

        [HttpPost]
        public PartialViewResult NakonechnikModel(int page = 1)
        {

            int pageSize = 24;

            IEnumerable<Nakonechnik> GunsPerPages = GetFilter().Item.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = GetFilter().Item.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Nakonechniks = GunsPerPages
            };
            ViewBag.IVM = ivm;
            return PartialView(ivm);
        }

        public ActionResult Nakonechnik(int page = 1)
        {

            int count = unitOfWork.Nakonechniks.GetAll().Count();
            var items = unitOfWork.Nakonechniks.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();
            List<Nakonechnik> Item;
            if (GetFilter().Item == null)
            {
                Item = unitOfWork.Nakonechniks.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();
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
            var diametrRezbi = new List<decimal>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                diametrRezbi.Add(items[i].DiametrRezbi);
            }
            List<decimal> newdiametrRezbi = new List<decimal>(diametrRezbi.Distinct());
            newdiametrRezbi.Reverse();
            ViewBag.DiametrRezbi = newdiametrRezbi;
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
            ViewBag.MinPrice = unitOfWork.Nakonechniks.GetAll().Min(a => a.Price);
            ViewBag.MaxPrice = unitOfWork.Nakonechniks.GetAll().Max(a => a.Price);
            ViewBag.CheckedMinPrice = GetFilter().CheckedMinPrice;
            if (GetFilter().CheckedMaxPrice == 0)
            {
                ViewBag.CheckedMaxPrice = unitOfWork.Nakonechniks.GetAll().Max(a => a.Price);
            }
            else
            {
                ViewBag.CheckedMaxPrice = GetFilter().CheckedMaxPrice;
            }
            ViewBag.CheckedBrand = GetFilter().CheckedBrand;
            ViewBag.CheckedCountry = GetFilter().CheckedCountry;
            ViewBag.CheckedDiametrRezbi = GetFilter().CheckedDiametrRezbi;
            ViewBag.CheckedType = GetFilter().CheckedType;
            ViewBag.CheckedCount = GetFilter().CheckedCount;
            int pageSize = 24; // количество объектов на страницу
            IEnumerable<Nakonechnik> GunsPerPages = Item.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = Item.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Nakonechniks = GunsPerPages
            };

            ViewBag.IVM = ivm;

            return View(ivm);
        }
        
        [HttpPost]
        public ActionResult NakonechnikFilter(int MinPrice, int MaxPrice, string[] brand, string[] country, decimal[] diametrRezbi, string[] type)
        {
            int count = unitOfWork.Nakonechniks.GetAll().Count();
            List<Nakonechnik> Item = unitOfWork.Nakonechniks.GetAll().Where(p => p.Price >= MinPrice && p.Price <= MaxPrice).ToList();
            List<Nakonechnik> newItem = new List<Nakonechnik>();

            GetFilter().CheckedMinPrice = MinPrice;
            GetFilter().CheckedMaxPrice = MaxPrice;
            GetFilter().CheckedBrand = 0;
            GetFilter().CheckedCountry = 0;
            GetFilter().CheckedDiametrRezbi = 0;
            GetFilter().CheckedType = 0;
            GetFilter().CheckedCount = 0;

            ViewBag.MinPrice = unitOfWork.Nakonechniks.GetAll().Min(a => a.Price);
            ViewBag.MaxPrice = unitOfWork.Nakonechniks.GetAll().Max(a => a.Price);
            ViewBag.CheckedMinPrice = MinPrice;
            ViewBag.CheckedMaxPrice = MaxPrice;
            ViewBag.CheckedBrand = 0;
            ViewBag.CheckedCountry = 0;
            ViewBag.CheckedDiametrRezbi = 0;
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
                newItem = new List<Nakonechnik>();
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
            if (diametrRezbi != null)
            {
                newItem = new List<Nakonechnik>();
                decimal a = 0;
                for (int i = 0; i < diametrRezbi.Count(); i++)
                {

                    a = diametrRezbi[i];
                    foreach (var c in Item.Where(p => p.DiametrRezbi == a))
                    {
                        newItem.Add(c);
                    }
                }
                Item = newItem;
                GetFilter().CheckedDiametrRezbi = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedDiametrRezbi = 1;
                Count++;
            }
            if (type != null)
            {
                newItem = new List<Nakonechnik>();
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

            var items = unitOfWork.Nakonechniks.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();

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
            var DiametrRezbi = new List<decimal>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                DiametrRezbi.Add(items[i].DiametrRezbi);
            }
            List<decimal> newdiametrRezbi = new List<decimal>(DiametrRezbi.Distinct());
            newdiametrRezbi.Reverse();
            ViewBag.DiametrRezbi = newdiametrRezbi;
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
            IEnumerable<Nakonechnik> GunsPerPages = Item.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = Item.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Nakonechniks = Item
            };

            ViewBag.IVM = ivm;

            GetFilter().Item = Item;

            return View(ivm);
        }

        [HttpPost]
        public ActionResult NakonechnikCount()
        {
            List<Nakonechnik> Item = GetFilter().Item;

            return Json(Item.Count());
        }
        
    }
    public class FilterNakonechnik
    {
        public List<Nakonechnik> Item { get; set; }
        public int CheckedMinPrice { get; set; }
        public int CheckedMaxPrice { get; set; }
        public int CheckedBrand { get; set; }
        public int CheckedCountry { get; set; }
        public int CheckedDiametrRezbi { get; set; }
        public int CheckedType { get; set; }
        public int CheckedCount { get; set; }
    }
}