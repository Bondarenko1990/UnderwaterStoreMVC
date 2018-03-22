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
    public class Gruza_i_gruzovye_sistemyController : Controller
    {
        UnitOfWork unitOfWork;
        public Gruza_i_gruzovye_sistemyController()
        {
            unitOfWork = new UnitOfWork();
        }

        public FilterGruza_i_gruzovye_sistemy GetFilter()
        {
            FilterGruza_i_gruzovye_sistemy filter = (FilterGruza_i_gruzovye_sistemy)Session["FilterGruza_i_gruzovye_sistemy"];
            if (filter == null)
            {
                filter = new FilterGruza_i_gruzovye_sistemy();
                Session["FilterGruza_i_gruzovye_sistemy"] = filter;
            }
            return filter;
        }

        public FileContentResult GetImage(int Id)
        {
            Gruza_i_gruzovye_sistemy item = unitOfWork.Gruza_i_gruzovye_sistemys.GetAll().FirstOrDefault(g => g.Id == Id);

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
            return View("Edit", new Gruza_i_gruzovye_sistemy());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int Id)
        {
            Gruza_i_gruzovye_sistemy deletedItem = unitOfWork.Gruza_i_gruzovye_sistemys.Delete(Id);
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
            return View(unitOfWork.Gruza_i_gruzovye_sistemys.GetAll().ToList());
        }

        [Authorize(Roles = "Admin")]
        public ViewResult Edit(int Id)
        {
            Gruza_i_gruzovye_sistemy item = unitOfWork.Gruza_i_gruzovye_sistemys.GetAll().FirstOrDefault(g => g.Id == Id);
            SelectList brands = new SelectList(unitOfWork.Brands.GetAll(), "Id", "Name");
            ViewBag.Brands = brands;
            return View(item);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Gruza_i_gruzovye_sistemy item, HttpPostedFileBase image = null)
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
                    item.ImageMimeType = unitOfWork.Gruza_i_gruzovye_sistemys.Get(item.Id).ImageMimeType;
                    item.ImageData = unitOfWork.Gruza_i_gruzovye_sistemys.Get(item.Id).ImageData;
                }
                unitOfWork.Gruza_i_gruzovye_sistemys.SaveItem(item);
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
            Gruza_i_gruzovye_sistemy c = unitOfWork.Gruza_i_gruzovye_sistemys.GetAll().FirstOrDefault(com => com.Id == id);
            if (c != null)
                return PartialView(c);
            return HttpNotFound();
        }

        public ActionResult Details(int id)
        {
            Gruza_i_gruzovye_sistemy c = unitOfWork.Gruza_i_gruzovye_sistemys.GetAll().FirstOrDefault(com => com.Id == id);
            if (c != null)
                return PartialView(c);
            return HttpNotFound();
        }

        [HttpPost]
        public PartialViewResult Models(int page = 1)
        {

            int pageSize = 24;

            IEnumerable<Gruza_i_gruzovye_sistemy> GunsPerPages = GetFilter().Item.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = GetFilter().Item.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Gruza_i_gruzovye_sistemys = GunsPerPages
            };
            ViewBag.IVM = ivm;
            return PartialView(ivm);
        }

        public ActionResult List(int page = 1)
        {

            int count = unitOfWork.Gruza_i_gruzovye_sistemys.GetAll().Count();
            var items = unitOfWork.Gruza_i_gruzovye_sistemys.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();
            List<Gruza_i_gruzovye_sistemy> Item;
            if (GetFilter().Item == null)
            {
                Item = unitOfWork.Gruza_i_gruzovye_sistemys.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();
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
            List<string> newType = new List<string>(Type.Distinct());
            newType.Sort();
            ViewBag.Type = newType;
            //------------------------------------------------
            var Kg = new List<int[]>();
            int[][] KgEdit = new int[4][];
            KgEdit[0] = new int[] { 0, 1 };
            KgEdit[1] = new int[] { 1, 5 };
            KgEdit[2] = new int[] { 5, 10 };
            KgEdit[3] = new int[] { 10, 15 };

            for (int i = 0; i < KgEdit.Length; i++)
            {
                if (items.Exists(p => p.Kg >= KgEdit[i][0] && p.Kg < KgEdit[i][1] && p.Kg != 0))
                {
                    Kg.Add(KgEdit[i]);
                }
            }
            if (items.Exists(p => p.Kg == 0))
            {
                Kg.Add(new int[] { 0, 0 });
            }
            List<int[]> newKg = new List<int[]>(Kg.Distinct());
            ViewBag.Kg = newKg;
            //------------------------------------------------         
            ViewBag.MinPrice = unitOfWork.Gruza_i_gruzovye_sistemys.GetAll().Min(a => a.Price);
            ViewBag.MaxPrice = unitOfWork.Gruza_i_gruzovye_sistemys.GetAll().Max(a => a.Price);
            ViewBag.CheckedMinPrice = GetFilter().CheckedMinPrice;
            if (GetFilter().CheckedMaxPrice == 0)
            {
                ViewBag.CheckedMaxPrice = unitOfWork.Gruza_i_gruzovye_sistemys.GetAll().Max(a => a.Price);
            }
            else
            {
                ViewBag.CheckedMaxPrice = GetFilter().CheckedMaxPrice;
            }
            ViewBag.CheckedBrand = GetFilter().CheckedBrand;
            ViewBag.CheckedCountry = GetFilter().CheckedCountry;
            ViewBag.CheckedType = GetFilter().CheckedType;
            ViewBag.CheckedKg = GetFilter().CheckedKg;
            ViewBag.CheckedCount = GetFilter().CheckedCount;
            int pageSize = 24; // количество объектов на страницу
            IEnumerable<Gruza_i_gruzovye_sistemy> GunsPerPages = Item.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = Item.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Gruza_i_gruzovye_sistemys = GunsPerPages
            };

            ViewBag.IVM = ivm;

            return View(ivm);
        }

        [HttpPost]
        public ActionResult Filter(int MinPrice, int MaxPrice, string[] brand, string[] country, string[] type, int[][] kg)
        {
            int count = unitOfWork.Gruza_i_gruzovye_sistemys.GetAll().Count();
            List<Gruza_i_gruzovye_sistemy> Item = unitOfWork.Gruza_i_gruzovye_sistemys.GetAll().Where(p => p.Price >= MinPrice && p.Price <= MaxPrice).ToList();
            List<Gruza_i_gruzovye_sistemy> newItem = new List<Gruza_i_gruzovye_sistemy>();

            GetFilter().CheckedMinPrice = MinPrice;
            GetFilter().CheckedMaxPrice = MaxPrice;
            GetFilter().CheckedBrand = 0;
            GetFilter().CheckedCountry = 0;
            GetFilter().CheckedType = 0;
            GetFilter().CheckedKg = 0;
            GetFilter().CheckedCount = 0;


            ViewBag.MinPrice = unitOfWork.Gruza_i_gruzovye_sistemys.GetAll().Min(a => a.Price);
            ViewBag.MaxPrice = unitOfWork.Gruza_i_gruzovye_sistemys.GetAll().Max(a => a.Price);
            ViewBag.CheckedMinPrice = MinPrice;
            ViewBag.CheckedMaxPrice = MaxPrice;
            ViewBag.CheckedBrand = 0;
            ViewBag.CheckedCountry = 0;
            ViewBag.CheckedType = 0;
            ViewBag.CheckedKg = 0;
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
                newItem = new List<Gruza_i_gruzovye_sistemy>();
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
                newItem = new List<Gruza_i_gruzovye_sistemy>();
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

            if (kg != null)
            {
                newItem = new List<Gruza_i_gruzovye_sistemy>();
                for (int i = 0; i < kg.Count(); i++)
                {
                    foreach (var c in Item.Where(p => (p.Kg >= kg[i][0] && p.Kg < kg[i][1]) || (p.Kg == kg[i][0] && p.Kg == kg[i][1])))
                    {
                        newItem.Add(c);
                    }
                }
                Item = newItem;
                GetFilter().CheckedKg = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedKg = 1;
                Count++;
            }

            ViewBag.CheckedCount = Count;

            var items = unitOfWork.Gruza_i_gruzovye_sistemys.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();

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
            List<string> newType = new List<string>(Type.Distinct());
            newType.Sort();
            ViewBag.Type = newType;
            //------------------------------------------------
            var Kg = new List<int[]>();
            int[][] KgEdit = new int[4][];
            KgEdit[0] = new int[] { 0, 1 };
            KgEdit[1] = new int[] { 1, 5 };
            KgEdit[2] = new int[] { 5, 10 };
            KgEdit[3] = new int[] { 10, 15 };

            for (int i = 0; i < KgEdit.Length; i++)
            {
                if (items.Exists(p => p.Kg >= KgEdit[i][0] && p.Kg < KgEdit[i][1] && p.Kg != 0))
                {
                    Kg.Add(KgEdit[i]);
                }
            }
            if (items.Exists(p => p.Kg == 0))
            {
                Kg.Add(new int[] { 0, 0 });
            }
            List<int[]> newKg = new List<int[]>(Kg.Distinct());
            ViewBag.Kg = newKg;
            //------------------------------------------------

            ViewBag.Count = Item.Count();
            int page = 1;
            int pageSize = 24; // количество объектов на страницу
            IEnumerable<Gruza_i_gruzovye_sistemy> GunsPerPages = Item.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = Item.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Gruza_i_gruzovye_sistemys = Item
            };

            ViewBag.IVM = ivm;

            GetFilter().Item = Item;

            return View(ivm);
        }

        [HttpPost]
        public ActionResult Count()
        {
            List<Gruza_i_gruzovye_sistemy> Item = GetFilter().Item;

            return Json(Item.Count());
        }
    }
    public class FilterGruza_i_gruzovye_sistemy
    {
        public List<Gruza_i_gruzovye_sistemy> Item { get; set; }
        public int CheckedBrand { get; set; }
        public int CheckedCountry { get; set; }
        public int CheckedType { get; set; }
        public int CheckedKg { get; set; }
        public int CheckedCount { get; set; }
        public int CheckedMinPrice { get; set; }
        public int CheckedMaxPrice { get; set; }

    }
}