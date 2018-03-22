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
    public class PneumaticController : Controller
    {
        UnitOfWork unitOfWork;

        public PneumaticController()
        {
            unitOfWork = new UnitOfWork();
        }

        public FilterPneumatic GetFilter()
        {
            FilterPneumatic filter = (FilterPneumatic)Session["FilterPneumatic"];
            if (filter == null)
            {
                filter = new FilterPneumatic();
                Session["FilterPneumatic"] = filter;
            }
            return filter;
        }

        public FileContentResult GetImage(int Id)
        {
            Pneumatic item = unitOfWork.Pneumatics.GetAll()
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
            return View("Edit", new Pneumatic());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int Id)
        {
            Pneumatic deletedItem = unitOfWork.Pneumatics.Delete(Id);
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
            return View(unitOfWork.Pneumatics.GetAll().ToList());
        }

        [Authorize(Roles = "Admin")]
        public ViewResult Edit(int Id)
        {
            Pneumatic item = unitOfWork.Pneumatics.GetAll().FirstOrDefault(g => g.Id == Id);
            SelectList brands = new SelectList(unitOfWork.Brands.GetAll(), "Id", "Name");
            ViewBag.Brands = brands;
            return View(item);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Pneumatic item, HttpPostedFileBase image = null)
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
                    item.ImageMimeType = unitOfWork.Pneumatics.Get(item.Id).ImageMimeType;
                    item.ImageData = unitOfWork.Pneumatics.Get(item.Id).ImageData;
                }
                unitOfWork.Pneumatics.SaveItem(item);
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
            Pneumatic c = unitOfWork.Pneumatics.GetAll().FirstOrDefault(com => com.Id == id);
            if (c != null)
                return PartialView(c);
            return HttpNotFound();
        }

        public ActionResult Details(int id)
        {
            Pneumatic c = unitOfWork.Pneumatics.GetAll().FirstOrDefault(com => com.Id == id);
            if (c != null)
                return PartialView(c);
            return HttpNotFound();
        }

        [HttpPost]
        public PartialViewResult Models(int page = 1)
        {

            int pageSize = 24;

            IEnumerable<Pneumatic> GunsPerPages = GetFilter().Item.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = GetFilter().Item.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Pneumatics = GunsPerPages
            };
            ViewBag.IVM = ivm;
            return PartialView(ivm);
        }

        public ActionResult List(int page = 1)
        {

            int count = unitOfWork.Pneumatics.GetAll().Count();
            var items = unitOfWork.Pneumatics.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();
            List<Pneumatic> Item;
            if (GetFilter().Item == null)
            {
                Item = unitOfWork.Pneumatics.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();
            }
            else
            {
                Item = GetFilter().Item;
            }

            ViewBag.MinPrice = unitOfWork.Pneumatics.GetAll().Min(a => a.Price);
            ViewBag.MaxPrice = unitOfWork.Pneumatics.GetAll().Max(a => a.Price);

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
            var garpun = new List<decimal>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                garpun.Add(items[i].Garpun);
            }
            var newGarpun = new List<decimal>(garpun.Distinct());
            newGarpun.Sort();
            ViewBag.Garpun = newGarpun;
            //------------------------------------------------
            var RegPower = new List<bool>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                RegPower.Add(items[i].RegPower);
            }
            List<bool> newRegPower = new List<bool>(RegPower.Distinct());
            newRegPower.Sort();
            newRegPower.Reverse();
            ViewBag.RegPower = newRegPower;
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
            ViewBag.CheckedMinPrice = GetFilter().CheckedMinPrice;
            if (GetFilter().CheckedMaxPrice == 0)
            {
                ViewBag.CheckedMaxPrice = unitOfWork.Pneumatics.GetAll().Max(a => a.Price);
            }
            else
            {
                ViewBag.CheckedMaxPrice = GetFilter().CheckedMaxPrice;
            }

            ViewBag.CheckedBrand = GetFilter().CheckedBrand;
            ViewBag.CheckedCountry = GetFilter().CheckedCountry;
            ViewBag.CheckedGarpun = GetFilter().CheckedGarpun;
            ViewBag.CheckedRegpower = GetFilter().CheckedRegpower;
            ViewBag.CheckedLength = GetFilter().CheckedLength;
            ViewBag.CheckedCount = GetFilter().CheckedCount;
            int pageSize = 24; // количество объектов на страницу
            IEnumerable<Pneumatic> GunsPerPages = Item.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = Item.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Pneumatics = GunsPerPages
            };

            ViewBag.IVM = ivm;

            return View(ivm);
        }

        [HttpPost]
        public ActionResult Filter(int MinPrice, int MaxPrice, string[] brand, string[] country, int[] garpun, bool[] regpower, int[] length)
        {
            int count = unitOfWork.Pneumatics.GetAll().Count();
            List<Pneumatic> Item = unitOfWork.Pneumatics.GetAll().Where(p => p.Price >= MinPrice && p.Price <= MaxPrice).ToList();
            List<Pneumatic> newItem = new List<Pneumatic>();

            GetFilter().CheckedMinPrice = MinPrice;
            GetFilter().CheckedMaxPrice = MaxPrice;
            GetFilter().CheckedBrand = 0;
            GetFilter().CheckedCountry = 0;
            GetFilter().CheckedGarpun = 0;
            GetFilter().CheckedRegpower = 0;
            GetFilter().CheckedLength = 0;
            GetFilter().CheckedCount = 0;

            ViewBag.MinPrice = unitOfWork.Pneumatics.GetAll().Min(a => a.Price);
            ViewBag.MaxPrice = unitOfWork.Pneumatics.GetAll().Max(a => a.Price);
            ViewBag.CheckedMinPrice = MinPrice;
            ViewBag.CheckedMaxPrice = MaxPrice;
            ViewBag.CheckedBrand = 0;
            ViewBag.CheckedCountry = 0;
            ViewBag.CheckedGarpun = 0;
            ViewBag.CheckedRegpower = 0;
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
                newItem = new List<Pneumatic>();
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
            if (garpun != null)
            {
                newItem = new List<Pneumatic>();
                int a = 0;
                for (int i = 0; i < garpun.Count(); i++)
                {

                    a = garpun[i];
                    foreach (var c in Item.Where(p => p.Garpun == a))
                    {
                        newItem.Add(c);
                    }
                }
                Item = newItem;
                GetFilter().CheckedGarpun = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedGarpun = 1;
                Count++;
            }
            if (regpower != null)
            {
                newItem = new List<Pneumatic>();
                bool a;
                for (int i = 0; i < regpower.Count(); i++)
                {

                    a = regpower[i];
                    foreach (var c in Item.Where(p => p.RegPower == a))
                    {
                        newItem.Add(c);
                    }
                }
                Item = newItem;
                GetFilter().CheckedRegpower = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedRegpower = 1;
                Count++;
            }
            if (length != null)
            {
                newItem = new List<Pneumatic>();
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

            var items = unitOfWork.Pneumatics.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();

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
            var Garpun = new List<decimal>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                Garpun.Add(items[i].Garpun);
            }
            var newGarpun = new List<decimal>(Garpun.Distinct());
            newGarpun.Sort();
            ViewBag.Garpun = newGarpun.ToList();
            //------------------------------------------------
            var RegPower = new List<bool>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                RegPower.Add(items[i].RegPower);
            }
            List<bool> newRegPower = new List<bool>(RegPower.Distinct());
            newRegPower.Sort();
            newRegPower.Reverse();
            ViewBag.RegPower = newRegPower.ToList();
            //------------------------------------------------
            //------------------------------------------------
            var Length = new List<int>();
            var EditLength = 0;
            for (int i = 1; i <= 6; i++)
            {
                EditLength = i * 30;
                if(items.Exists(p => p.Length >= EditLength && p.Length < EditLength + 30))
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
            IEnumerable<Pneumatic> GunsPerPages = Item.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = Item.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Pneumatics = Item
            };

            ViewBag.IVM = ivm;

            GetFilter().Item = Item;

            return View(ivm);
        }

        [HttpPost]
        public ActionResult Count()
        {
            List<Pneumatic> Item = GetFilter().Item;

            return Json(Item.Count());
        }

    }
    public class FilterPneumatic
    {
        public List<Pneumatic> Item { get; set; }
        public int CheckedBrand { get; set; }
        public int CheckedCountry { get; set; }
        public int CheckedGarpun { get; set; }
        public int CheckedRegpower { get; set; }
        public int CheckedLength { get; set; }
        public int CheckedCount { get; set; }
        public int CheckedMinPrice { get; set; }
        public int CheckedMaxPrice { get; set; }
    }
}