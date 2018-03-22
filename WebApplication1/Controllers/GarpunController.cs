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
    public class GarpunController : Controller
    {
        UnitOfWork unitOfWork;

        public GarpunController()
        {
            unitOfWork = new UnitOfWork();
        }



        public FileContentResult GetImage(int Id)
        {
            Garpun garpun = unitOfWork.Garpuns.GetAll()
                .FirstOrDefault(g => g.Id == Id);

            if (garpun != null)
            {
                return File(garpun.ImageData, garpun.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

        public ActionResult DetailsModal(int id)
        {
            Garpun c = unitOfWork.Garpuns.GetAll().FirstOrDefault(com => com.Id == id);
            if (c != null)
                return PartialView(c);
            return HttpNotFound();
        }

        public ActionResult Details(int id)
        {
            Garpun c = unitOfWork.Garpuns.GetAll().FirstOrDefault(com => com.Id == id);
            if (c != null)
                return PartialView(c);
            return HttpNotFound();
        }

        [HttpPost]
        public PartialViewResult GarpunModel(int page = 1)
        {

            int pageSize = 24;

            IEnumerable<Garpun> GunsPerPages = GetFilter().Garpun.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = GetFilter().Garpun.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Garpuns = GunsPerPages
            };
            ViewBag.IVM = ivm;
            return PartialView(ivm);
        }

        public ActionResult Garpun(int page = 1)
        {

            int count = unitOfWork.Garpuns.GetAll().Count();
            var items = unitOfWork.Garpuns.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();
            List<Garpun> Garpun;
            if (GetFilter().Garpun == null)
            {
                Garpun = unitOfWork.Garpuns.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();
            }
            else
            {
                Garpun = GetFilter().Garpun;
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
            var diametrGarpun = new List<decimal>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                diametrGarpun.Add(items[i].DiametrGarpun);
            }
            var newdiametrGarpun = new List<decimal>(diametrGarpun.Distinct());
            newdiametrGarpun.Sort();
            ViewBag.DiametrGarpun = newdiametrGarpun;
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
            var typeOfGun = new List<string>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                typeOfGun.Add(items[i].TypeOfGun);
            }
            List<string> newtypeOfGun = new List<string>(typeOfGun.Distinct());
            newtypeOfGun.Reverse();
            ViewBag.TypeOfGun = newtypeOfGun;
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
            ViewBag.MinPrice = unitOfWork.Garpuns.GetAll().Min(a => a.Price);
            ViewBag.MaxPrice = unitOfWork.Garpuns.GetAll().Max(a => a.Price);
            ViewBag.CheckedMinPrice = GetFilter().CheckedMinPrice;
            if (GetFilter().CheckedMaxPrice == 0)
            {
                ViewBag.CheckedMaxPrice = unitOfWork.Garpuns.GetAll().Max(a => a.Price);
            }
            else
            {
                ViewBag.CheckedMaxPrice = GetFilter().CheckedMaxPrice;
            }
            ViewBag.CheckedBrand = GetFilter().CheckedBrand;
            ViewBag.CheckedCountry = GetFilter().CheckedCountry;
            ViewBag.CheckedDiametrGarpun = GetFilter().CheckedDiametrGarpun;
            ViewBag.CheckedDiametrRezbi = GetFilter().CheckedDiametrRezbi;
            ViewBag.CheckedType = GetFilter().CheckedType;
            ViewBag.CheckedTypeOfGun = GetFilter().CheckedTypeOfGun;
            ViewBag.CheckedLength = GetFilter().CheckedLength;
            ViewBag.CheckedCount = GetFilter().CheckedCount;
            int pageSize = 24; // количество объектов на страницу
            IEnumerable<Garpun> GunsPerPages = Garpun.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = Garpun.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Garpuns = GunsPerPages
            };

            ViewBag.IVM = ivm;

            return View(ivm);
        }
        public FilterGarpun GetFilter()

        {
            FilterGarpun filter = (FilterGarpun)Session["FilterGarpun"];
            if (filter == null)
            {
                filter = new FilterGarpun();
                Session["FilterGarpun"] = filter;
            }
            return filter;
        }

        [HttpPost]
        public ActionResult GarpunFilter(int MinPrice, int MaxPrice, string[] brand, string[] country, decimal[] diametrGarpun, decimal[] diametrRezbi, string[] type, string[] typeOfGun, int[] length)
        {
            int count = unitOfWork.Garpuns.GetAll().Count();
            List<Garpun> Garpun = unitOfWork.Garpuns.GetAll().Where(p => p.Price >= MinPrice && p.Price <= MaxPrice).ToList();
            List<Garpun> newGarpun = new List<Garpun>();

            GetFilter().CheckedMinPrice = MinPrice;
            GetFilter().CheckedMaxPrice = MaxPrice;
            GetFilter().CheckedBrand = 0;
            GetFilter().CheckedCountry = 0;
            GetFilter().CheckedDiametrGarpun = 0;
            GetFilter().CheckedDiametrRezbi = 0;
            GetFilter().CheckedType = 0;
            GetFilter().CheckedTypeOfGun = 0;
            GetFilter().CheckedLength = 0;
            GetFilter().CheckedCount = 0;

            ViewBag.MinPrice = unitOfWork.Garpuns.GetAll().Min(a => a.Price);
            ViewBag.MaxPrice = unitOfWork.Garpuns.GetAll().Max(a => a.Price);
            ViewBag.CheckedMinPrice = MinPrice;
            ViewBag.CheckedMaxPrice = MaxPrice;
            ViewBag.CheckedBrand = 0;
            ViewBag.CheckedCountry = 0;
            ViewBag.CheckedDiametrGarpun = 0;
            ViewBag.CheckedDiametrRezbi = 0;
            ViewBag.CheckedType = 0;
            ViewBag.CheckedTypeOfGun = 0;
            ViewBag.CheckedLength = 0;
            ViewBag.CheckedCount = 0;

            int Count = 0;
            if (brand != null)
            {
                string a = "";
                for (int i = 0; i < brand.Count(); i++)
                {

                    a = brand[i];
                    foreach (var c in Garpun.Where(p => p.Brand.Name == a))
                    {

                        newGarpun.Add(c);
                    }
                }
                Garpun = newGarpun;
                GetFilter().CheckedBrand = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedBrand = 1;
                Count++;
            }

            if (country != null)
            {
                newGarpun = new List<Garpun>();
                string a = "";
                for (int i = 0; i < country.Count(); i++)
                {

                    a = country[i];
                    foreach (var c in Garpun.Where(p => p.Brand.Country == a))
                    {
                        newGarpun.Add(c);
                    }
                }
                Garpun = newGarpun;
                GetFilter().CheckedCountry = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedCountry = 1;
                Count++;
            }
            if (diametrGarpun != null)
            {
                newGarpun = new List<Garpun>();
                decimal a = 0;
                for (int i = 0; i < diametrGarpun.Count(); i++)
                {

                    a = diametrGarpun[i];
                    foreach (var c in Garpun.Where(p => p.DiametrGarpun == a))
                    {
                        newGarpun.Add(c);
                    }
                }
                Garpun = newGarpun;
                GetFilter().CheckedDiametrGarpun = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedDiametrGarpun = 1;
                Count++;
            }
            if (diametrRezbi != null)
            {
                newGarpun = new List<Garpun>();
                decimal a = 0;
                for (int i = 0; i < diametrRezbi.Count(); i++)
                {

                    a = diametrRezbi[i];
                    foreach (var c in Garpun.Where(p => p.DiametrRezbi == a))
                    {
                        newGarpun.Add(c);
                    }
                }
                Garpun = newGarpun;
                GetFilter().CheckedDiametrRezbi = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedDiametrRezbi = 1;
                Count++;
            }
            if (type != null)
            {
                newGarpun = new List<Garpun>();
                string a = "";
                for (int i = 0; i < type.Count(); i++)
                {

                    a = type[i];
                    foreach (var c in Garpun.Where(p => p.Type == a))
                    {
                        newGarpun.Add(c);
                    }
                }
                Garpun = newGarpun;
                GetFilter().CheckedType = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedType = 1;
                Count++;
            }
            if (typeOfGun != null)
            {
                newGarpun = new List<Garpun>();
                string a = "";
                for (int i = 0; i < typeOfGun.Count(); i++)
                {

                    a = typeOfGun[i];
                    foreach (var c in Garpun.Where(p => p.TypeOfGun == a))
                    {
                        newGarpun.Add(c);
                    }
                }
                Garpun = newGarpun;
                GetFilter().CheckedTypeOfGun = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedTypeOfGun = 1;
                Count++;
            }
            if (length != null)
            {
                newGarpun = new List<Garpun>();
                int a = 0;
                for (int i = 0; i < length.Count(); i++)
                {

                    a = length[i];
                    foreach (var c in Garpun.Where(p => p.Length >= a && p.Length < a + 30))
                    {
                        newGarpun.Add(c);
                    }
                }
                Garpun = newGarpun;
                GetFilter().CheckedLength = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedLength = 1;
                Count++;
            }
            ViewBag.CheckedCount = Count;

            var items = unitOfWork.Garpuns.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();

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
            var DiametrGarpun = new List<decimal>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                DiametrGarpun.Add(items[i].DiametrGarpun);
            }
            List<decimal> newdiametrGarpun = new List<decimal>(DiametrGarpun.Distinct());
            newdiametrGarpun.Sort();
            ViewBag.DiametrGarpun = newdiametrGarpun;
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
            var TypeOfGun = new List<string>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                TypeOfGun.Add(items[i].TypeOfGun);
            }
            List<string> newtypeOfGun = new List<string>(TypeOfGun.Distinct());
            newtypeOfGun.Reverse();
            ViewBag.SumTyagi = newtypeOfGun;
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

            ViewBag.Count = Garpun.Count();
            int page = 1;
            int pageSize = 24; // количество объектов на страницу
            IEnumerable<Garpun> GunsPerPages = Garpun.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = Garpun.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Garpuns = Garpun
            };

            ViewBag.IVM = ivm;

            GetFilter().Garpun = Garpun;

            return View(ivm);
        }

        [HttpPost]
        public ActionResult GarpunCount()
        {
            List<Garpun> Garpun = GetFilter().Garpun;

            return Json(Garpun.Count());
        }

        [Authorize(Roles = "Admin")]
        public ViewResult Create()
        {
            SelectList brands = new SelectList(unitOfWork.Brands.GetAll(), "Id", "Name");
            ViewBag.Brands = brands;
            return View("Edit", new Garpun());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int Id)
        {
            Garpun deletedGarpun = unitOfWork.Garpuns.Delete(Id);
            if (deletedGarpun != null)
            {
                TempData["message"] = string.Format("Товар \"{0}\" был удален",
                    deletedGarpun.Name);
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public ViewResult Index()
        {
          if(unitOfWork.Garpuns.GetAll().Count() != 0)
            {     
              return View(unitOfWork.Garpuns.GetAll().ToList());
            }
            else return View();
        }

        [Authorize(Roles = "Admin")]
        public ViewResult Edit(int Id)
        {
            Garpun garpun = unitOfWork.Garpuns.GetAll().FirstOrDefault(g => g.Id == Id);
            SelectList brands = new SelectList(unitOfWork.Brands.GetAll(), "Id", "Name");
            ViewBag.Brands = brands;
            return View(garpun);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Garpun garpun, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    garpun.ImageMimeType = image.ContentType;
                    garpun.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(garpun.ImageData, 0, image.ContentLength);
                }
                else
                {
                    garpun.ImageMimeType = unitOfWork.Garpuns.Get(garpun.Id).ImageMimeType;
                    garpun.ImageData = unitOfWork.Garpuns.Get(garpun.Id).ImageData;
                }
                unitOfWork.Garpuns.SaveItem(garpun);
                TempData["message"] = string.Format("Изменения в товаре \"{0}\" были сохранены", garpun.Name);
                return RedirectToAction("Index");
            }
            else
            {
                // Что-то не так со значениями данных
                return View(garpun);
            }
        }
    }
    public class FilterGarpun
    {
        public List<Garpun> Garpun { get; set; }
        public int CheckedMinPrice { get; set; }
        public int CheckedMaxPrice { get; set; }
        public int CheckedBrand { get; set; }
        public int CheckedCountry { get; set; }
        public int CheckedDiametrGarpun { get; set; }
        public int CheckedDiametrRezbi { get; set; }
        public int CheckedType { get; set; }
        public int CheckedTypeOfGun { get; set; }
        public int CheckedLength { get; set; }
        public int CheckedCount { get; set; }
    }
}