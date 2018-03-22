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
    public class Futbolki_i_shorty_dlya_vodnogo_sportaController : Controller
    {
        UnitOfWork unitOfWork;

        public Futbolki_i_shorty_dlya_vodnogo_sportaController()
        {
            unitOfWork = new UnitOfWork();
        }

        public FilterFutbolki_i_shorty_dlya_vodnogo_sporta GetFilter()
        {
            FilterFutbolki_i_shorty_dlya_vodnogo_sporta filter = (FilterFutbolki_i_shorty_dlya_vodnogo_sporta)Session["FilterFutbolki_i_shorty_dlya_vodnogo_sporta"];
            if (filter == null)
            {
                filter = new FilterFutbolki_i_shorty_dlya_vodnogo_sporta();
                Session["FilterFutbolki_i_shorty_dlya_vodnogo_sporta"] = filter;
            }
            return filter;
        }

        public FileContentResult GetImage(int Id)
        {
            Futbolki_i_shorty_dlya_vodnogo_sporta item = unitOfWork.Futbolki_i_shorty_dlya_vodnogo_sportas.GetAll()
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
            return View("Edit", new Futbolki_i_shorty_dlya_vodnogo_sporta());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int Id)
        {
            Futbolki_i_shorty_dlya_vodnogo_sporta deletedItem = unitOfWork.Futbolki_i_shorty_dlya_vodnogo_sportas.Delete(Id);
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
            return View(unitOfWork.Futbolki_i_shorty_dlya_vodnogo_sportas.GetAll().ToList());
        }

        [Authorize(Roles = "Admin")]
        public ViewResult Edit(int Id)
        {
            Futbolki_i_shorty_dlya_vodnogo_sporta item = unitOfWork.Futbolki_i_shorty_dlya_vodnogo_sportas.GetAll().FirstOrDefault(g => g.Id == Id);
            SelectList brands = new SelectList(unitOfWork.Brands.GetAll(), "Id", "Name");
            ViewBag.Brands = brands;
            return View(item);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Futbolki_i_shorty_dlya_vodnogo_sporta item, HttpPostedFileBase image = null)
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
                    item.ImageMimeType = unitOfWork.Futbolki_i_shorty_dlya_vodnogo_sportas.Get(item.Id).ImageMimeType;
                    item.ImageData = unitOfWork.Futbolki_i_shorty_dlya_vodnogo_sportas.Get(item.Id).ImageData;
                }
                unitOfWork.Futbolki_i_shorty_dlya_vodnogo_sportas.SaveItem(item);
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
            Futbolki_i_shorty_dlya_vodnogo_sporta c = unitOfWork.Futbolki_i_shorty_dlya_vodnogo_sportas.GetAll().FirstOrDefault(com => com.Id == id);
            if (c != null)
                return PartialView(c);
            return HttpNotFound();
        }

        public ActionResult Details(int id)
        {
            Futbolki_i_shorty_dlya_vodnogo_sporta c = unitOfWork.Futbolki_i_shorty_dlya_vodnogo_sportas.GetAll().FirstOrDefault(com => com.Id == id);
            if (c != null)
                return PartialView(c);
            return HttpNotFound();
        }

        [HttpPost]
        public PartialViewResult Models(int page = 1)
        {

            int pageSize = 24;

            IEnumerable<Futbolki_i_shorty_dlya_vodnogo_sporta> GunsPerPages = GetFilter().Item.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = GetFilter().Item.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Futbolki_i_shorty_dlya_vodnogo_sportas = GunsPerPages
            };
            ViewBag.IVM = ivm;
            return PartialView(ivm);
        }

        public ActionResult List(int page = 1)
        {

            int count = unitOfWork.Futbolki_i_shorty_dlya_vodnogo_sportas.GetAll().Count();
            var items = unitOfWork.Futbolki_i_shorty_dlya_vodnogo_sportas.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();
            List<Futbolki_i_shorty_dlya_vodnogo_sporta> Item;
            if (GetFilter().Item == null)
            {
                Item = unitOfWork.Futbolki_i_shorty_dlya_vodnogo_sportas.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();
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
            var Type = new List<string>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                Type.Add(items[i].Type);
            }
            List<string> newtype = new List<string>(Type.Distinct());
            newtype.Sort();
            ViewBag.Type = newtype;
            //------------------------------------------------
            var Pol = new List<string>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                Pol.Add(items[i].Pol);
            }
            List<string> newpol = new List<string>(Pol.Distinct());
            newpol.Sort();
            ViewBag.Pol = newpol;
            //------------------------------------------------
            ViewBag.MinPrice = unitOfWork.Futbolki_i_shorty_dlya_vodnogo_sportas.GetAll().Min(a => a.Price);
            ViewBag.MaxPrice = unitOfWork.Futbolki_i_shorty_dlya_vodnogo_sportas.GetAll().Max(a => a.Price);
            ViewBag.CheckedMinPrice = GetFilter().CheckedMinPrice;
            if (GetFilter().CheckedMaxPrice == 0)
            {
                ViewBag.CheckedMaxPrice = unitOfWork.Futbolki_i_shorty_dlya_vodnogo_sportas.GetAll().Max(a => a.Price);
            }
            else
            {
                ViewBag.CheckedMaxPrice = GetFilter().CheckedMaxPrice;
            }
            ViewBag.CheckedBrand = GetFilter().CheckedBrand;
            ViewBag.CheckedCountry = GetFilter().CheckedCountry;
            ViewBag.CheckedType = GetFilter().CheckedType;
            ViewBag.CheckedPol = GetFilter().CheckedPol;
            ViewBag.CheckedCount = GetFilter().CheckedCount;
            int pageSize = 24; // количество объектов на страницу
            IEnumerable<Futbolki_i_shorty_dlya_vodnogo_sporta> GunsPerPages = Item.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = Item.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Futbolki_i_shorty_dlya_vodnogo_sportas = GunsPerPages
            };

            ViewBag.IVM = ivm;

            return View(ivm);
        }

        [HttpPost]
        public ActionResult Filter(int MinPrice, int MaxPrice, string[] brand, string[] country,  string[] type, string[] pol)
        {
            int count = unitOfWork.Futbolki_i_shorty_dlya_vodnogo_sportas.GetAll().Count();
            List<Futbolki_i_shorty_dlya_vodnogo_sporta> Item = unitOfWork.Futbolki_i_shorty_dlya_vodnogo_sportas.GetAll().Where(p => p.Price >= MinPrice && p.Price <= MaxPrice).ToList();
            List<Futbolki_i_shorty_dlya_vodnogo_sporta> newItem = new List<Futbolki_i_shorty_dlya_vodnogo_sporta>();

            GetFilter().CheckedMinPrice = MinPrice;
            GetFilter().CheckedMaxPrice = MaxPrice;
            GetFilter().CheckedBrand = 0;
            GetFilter().CheckedCountry = 0;
            GetFilter().CheckedType = 0;
            GetFilter().CheckedPol = 0;
            GetFilter().CheckedCount = 0;


            ViewBag.MinPrice = unitOfWork.Futbolki_i_shorty_dlya_vodnogo_sportas.GetAll().Min(a => a.Price);
            ViewBag.MaxPrice = unitOfWork.Futbolki_i_shorty_dlya_vodnogo_sportas.GetAll().Max(a => a.Price);
            ViewBag.CheckedMinPrice = MinPrice;
            ViewBag.CheckedMaxPrice = MaxPrice;
            ViewBag.CheckedBrand = 0;
            ViewBag.CheckedCountry = 0;
            ViewBag.CheckedType = 0;
            ViewBag.CheckedPol = 0;
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
                newItem = new List<Futbolki_i_shorty_dlya_vodnogo_sporta>();
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
           
            if (type != null)
            {
                newItem = new List<Futbolki_i_shorty_dlya_vodnogo_sporta>();
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
            if (pol != null)
            {
                newItem = new List<Futbolki_i_shorty_dlya_vodnogo_sporta>();
                string a = "";
                for (int i = 0; i < pol.Count(); i++)
                {

                    a = pol[i];
                    foreach (var c in Item.Where(p => p.Pol == a))
                    {
                        newItem.Add(c);
                    }
                }
                Item = newItem;
                GetFilter().CheckedPol = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedPol = 1;
                Count++;
            }

            ViewBag.CheckedCount = Count;

            var items = unitOfWork.Futbolki_i_shorty_dlya_vodnogo_sportas.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();

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
            var Type = new List<string>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                Type.Add(items[i].Type);
            }
            List<string> newtype = new List<string>(Type.Distinct());
            newtype.Sort();
            ViewBag.Type = newtype;
            //------------------------------------------------
            var Pol = new List<string>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                Pol.Add(items[i].Pol);
            }
            List<string> newpol = new List<string>(Pol.Distinct());
            newpol.Sort();
            ViewBag.Pol = newpol;
            //------------------------------------------------

            ViewBag.Count = Item.Count();
            int page = 1;
            int pageSize = 24; // количество объектов на страницу
            IEnumerable<Futbolki_i_shorty_dlya_vodnogo_sporta> GunsPerPages = Item.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = Item.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Futbolki_i_shorty_dlya_vodnogo_sportas = Item
            };

            ViewBag.IVM = ivm;

            GetFilter().Item = Item;

            return View(ivm);
        }

        [HttpPost]
        public ActionResult Count()
        {
            List<Futbolki_i_shorty_dlya_vodnogo_sporta> Item = GetFilter().Item;

            return Json(Item.Count());
        }

    }
    public class FilterFutbolki_i_shorty_dlya_vodnogo_sporta
    {
        public List<Futbolki_i_shorty_dlya_vodnogo_sporta> Item { get; set; }
        public int CheckedBrand { get; set; }
        public int CheckedCountry { get; set; }
        public int CheckedType { get; set; }
        public int CheckedPol { get; set; }
        public int CheckedCount { get; set; }
        public int CheckedMinPrice { get; set; }
        public int CheckedMaxPrice { get; set; }
    }
}