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
    public class Utepliteli_dlya_sukhikh_gidrokostyumovController : Controller
    {
        UnitOfWork unitOfWork;
        public Utepliteli_dlya_sukhikh_gidrokostyumovController()
        {
            unitOfWork = new UnitOfWork();
        }

        public FilterUtepliteli_dlya_sukhikh_gidrokostyumov GetFilter()
        {
            FilterUtepliteli_dlya_sukhikh_gidrokostyumov filter = (FilterUtepliteli_dlya_sukhikh_gidrokostyumov)Session["FilterUtepliteli_dlya_sukhikh_gidrokostyumov"];
            if (filter == null)
            {
                filter = new FilterUtepliteli_dlya_sukhikh_gidrokostyumov();
                Session["FilterUtepliteli_dlya_sukhikh_gidrokostyumov"] = filter;
            }
            return filter;
        }

        public FileContentResult GetImage(int Id)
        {
            Utepliteli_dlya_sukhikh_gidrokostyumov item = unitOfWork.Utepliteli_dlya_sukhikh_gidrokostyumovs.GetAll()
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
            return View("Edit", new Utepliteli_dlya_sukhikh_gidrokostyumov());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int Id)
        {
            Utepliteli_dlya_sukhikh_gidrokostyumov deletedItem = unitOfWork.Utepliteli_dlya_sukhikh_gidrokostyumovs.Delete(Id);
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
            return View(unitOfWork.Utepliteli_dlya_sukhikh_gidrokostyumovs.GetAll().ToList());
        }

        [Authorize(Roles = "Admin")]
        public ViewResult Edit(int Id)
        {
            Utepliteli_dlya_sukhikh_gidrokostyumov item = unitOfWork.Utepliteli_dlya_sukhikh_gidrokostyumovs.GetAll().FirstOrDefault(g => g.Id == Id);
            SelectList brands = new SelectList(unitOfWork.Brands.GetAll(), "Id", "Name");
            ViewBag.Brands = brands;
            return View(item);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Utepliteli_dlya_sukhikh_gidrokostyumov item, HttpPostedFileBase image = null)
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
                    item.ImageMimeType = unitOfWork.Utepliteli_dlya_sukhikh_gidrokostyumovs.Get(item.Id).ImageMimeType;
                    item.ImageData = unitOfWork.Utepliteli_dlya_sukhikh_gidrokostyumovs.Get(item.Id).ImageData;
                }
                unitOfWork.Utepliteli_dlya_sukhikh_gidrokostyumovs.SaveItem(item);
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
            Utepliteli_dlya_sukhikh_gidrokostyumov c = unitOfWork.Utepliteli_dlya_sukhikh_gidrokostyumovs.GetAll().FirstOrDefault(com => com.Id == id);
            if (c != null)
                return PartialView(c);
            return HttpNotFound();
        }

        public ActionResult Details(int id)
        {
            Utepliteli_dlya_sukhikh_gidrokostyumov c = unitOfWork.Utepliteli_dlya_sukhikh_gidrokostyumovs.GetAll().FirstOrDefault(com => com.Id == id);
            if (c != null)
                return PartialView(c);
            return HttpNotFound();
        }

        [HttpPost]
        public PartialViewResult Models(int page = 1)
        {

            int pageSize = 24;

            IEnumerable<Utepliteli_dlya_sukhikh_gidrokostyumov> GunsPerPages = GetFilter().Item.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = GetFilter().Item.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Utepliteli_dlya_sukhikh_gidrokostyumovs = GunsPerPages
            };
            ViewBag.IVM = ivm;
            return PartialView(ivm);
        }

        public ActionResult List(int page = 1)
        {

            int count = unitOfWork.Utepliteli_dlya_sukhikh_gidrokostyumovs.GetAll().Count();
            var items = unitOfWork.Utepliteli_dlya_sukhikh_gidrokostyumovs.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();
            List<Utepliteli_dlya_sukhikh_gidrokostyumov> Item;
            if (GetFilter().Item == null)
            {
                Item = unitOfWork.Utepliteli_dlya_sukhikh_gidrokostyumovs.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();
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
            ViewBag.MinPrice = unitOfWork.Utepliteli_dlya_sukhikh_gidrokostyumovs.GetAll().Min(a => a.Price);
            ViewBag.MaxPrice = unitOfWork.Utepliteli_dlya_sukhikh_gidrokostyumovs.GetAll().Max(a => a.Price);
            ViewBag.CheckedMinPrice = GetFilter().CheckedMinPrice;
            if (GetFilter().CheckedMaxPrice == 0)
            {
                ViewBag.CheckedMaxPrice = unitOfWork.Utepliteli_dlya_sukhikh_gidrokostyumovs.GetAll().Max(a => a.Price);
            }
            else
            {
                ViewBag.CheckedMaxPrice = GetFilter().CheckedMaxPrice;
            }
            ViewBag.CheckedBrand = GetFilter().CheckedBrand;
            ViewBag.CheckedCountry = GetFilter().CheckedCountry;
            ViewBag.CheckedCount = GetFilter().CheckedCount;
            int pageSize = 24; // количество объектов на страницу
            IEnumerable<Utepliteli_dlya_sukhikh_gidrokostyumov> GunsPerPages = Item.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = Item.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Utepliteli_dlya_sukhikh_gidrokostyumovs = GunsPerPages
            };

            ViewBag.IVM = ivm;

            return View(ivm);
        }

        [HttpPost]
        public ActionResult Filter(int MinPrice, int MaxPrice, string[] brand, string[] country)
        {
            int count = unitOfWork.Utepliteli_dlya_sukhikh_gidrokostyumovs.GetAll().Count();
            List<Utepliteli_dlya_sukhikh_gidrokostyumov> Item = unitOfWork.Utepliteli_dlya_sukhikh_gidrokostyumovs.GetAll().Where(p => p.Price >= MinPrice && p.Price <= MaxPrice).ToList();
            List<Utepliteli_dlya_sukhikh_gidrokostyumov> newItem = new List<Utepliteli_dlya_sukhikh_gidrokostyumov>();

            GetFilter().CheckedMinPrice = MinPrice;
            GetFilter().CheckedMaxPrice = MaxPrice;
            GetFilter().CheckedBrand = 0;
            GetFilter().CheckedCountry = 0;
            GetFilter().CheckedCount = 0;


            ViewBag.MinPrice = unitOfWork.Utepliteli_dlya_sukhikh_gidrokostyumovs.GetAll().Min(a => a.Price);
            ViewBag.MaxPrice = unitOfWork.Utepliteli_dlya_sukhikh_gidrokostyumovs.GetAll().Max(a => a.Price);
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
                newItem = new List<Utepliteli_dlya_sukhikh_gidrokostyumov>();
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

            var items = unitOfWork.Utepliteli_dlya_sukhikh_gidrokostyumovs.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();

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
            IEnumerable<Utepliteli_dlya_sukhikh_gidrokostyumov> GunsPerPages = Item.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = Item.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Utepliteli_dlya_sukhikh_gidrokostyumovs = Item
            };

            ViewBag.IVM = ivm;

            GetFilter().Item = Item;

            return View(ivm);
        }

        [HttpPost]
        public ActionResult Count()
        {
            List<Utepliteli_dlya_sukhikh_gidrokostyumov> Item = GetFilter().Item;

            return Json(Item.Count());
        }
    }
    public class FilterUtepliteli_dlya_sukhikh_gidrokostyumov
    {
        public List<Utepliteli_dlya_sukhikh_gidrokostyumov> Item { get; set; }
        public int CheckedBrand { get; set; }
        public int CheckedCountry { get; set; }
        public int CheckedCount { get; set; }
        public int CheckedMinPrice { get; set; }
        public int CheckedMaxPrice { get; set; }
    }
}