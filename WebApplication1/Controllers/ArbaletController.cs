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
    public class ArbaletController : Controller
    {

        UnitOfWork unitOfWork;

        public ArbaletController()
        {
            unitOfWork = new UnitOfWork();
        }



        public FileContentResult GetImage(int Id)
        {
            Arbalet arbalet = unitOfWork.Arbalets.GetAll()
                .FirstOrDefault(g => g.Id == Id);

            if (arbalet != null)
            {
                return File(arbalet.ImageData, arbalet.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

        public ActionResult DetailsModal(int id)
        {
            Arbalet c = unitOfWork.Arbalets.GetAll().FirstOrDefault(com => com.Id == id);
            if (c != null)
                return PartialView(c);
            return HttpNotFound();
        }

        public ActionResult Details(int id)
        {
            Arbalet c = unitOfWork.Arbalets.GetAll().FirstOrDefault(com => com.Id == id);
            if (c != null)
                return PartialView(c);
            return HttpNotFound();
        }

        [HttpPost]
        public PartialViewResult ArbaletModel(int page = 1)
        {

            int pageSize = 24;

            IEnumerable<Arbalet> GunsPerPages = GetFilter().Gun.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = GetFilter().Gun.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Arbalets = GunsPerPages
            };
            ViewBag.IVM = ivm;
            return PartialView(ivm);
        }

        public ActionResult Arbalet(int page = 1)
        {

            int count = unitOfWork.Arbalets.GetAll().Count();
            var items = unitOfWork.Arbalets.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();
            List<Arbalet> Gun;
            if (GetFilter().Gun == null)
            {
                Gun = unitOfWork.Arbalets.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();
            }
            else
            {
                Gun = GetFilter().Gun;
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
            var diametrTyagi = new List<int>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                diametrTyagi.Add(items[i].DiametrTyagi);
            }
            List<int> newdiametrTyagi = new List<int>(diametrTyagi.Distinct());
            newdiametrTyagi.Reverse();
            ViewBag.DiametrTyagi = newdiametrTyagi;
            //------------------------------------------------
            var dopTyagi = new List<string>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                dopTyagi.Add(items[i].DopTyagi);
            }
            List<string> newdopTyagi = new List<string>(dopTyagi.Distinct());
            newdopTyagi.Reverse();
            ViewBag.DopTyagi = newdopTyagi;
            //------------------------------------------------
            var sumTyagi = new List<int>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                sumTyagi.Add(items[i].SumTyagi);
            }
            List<int> newsumTyagi = new List<int>(sumTyagi.Distinct());
            newsumTyagi.Reverse();
            ViewBag.SumTyagi = newsumTyagi;
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
            ViewBag.MinPrice = unitOfWork.Arbalets.GetAll().Min(a => a.Price);
            ViewBag.MaxPrice = unitOfWork.Arbalets.GetAll().Max(a => a.Price);
            ViewBag.CheckedMinPrice = GetFilter().CheckedMinPrice;
            if (GetFilter().CheckedMaxPrice == 0)
            {
                ViewBag.CheckedMaxPrice = unitOfWork.Arbalets.GetAll().Max(a => a.Price);
            }
            else
            {
                ViewBag.CheckedMaxPrice = GetFilter().CheckedMaxPrice;
            }
            ViewBag.CheckedBrand = GetFilter().CheckedBrand;
            ViewBag.CheckedCountry = GetFilter().CheckedCountry;
            ViewBag.CheckedDiametrGarpun = GetFilter().CheckedDiametrGarpun;
            ViewBag.CheckedDiametrTyagi = GetFilter().CheckedDiametrTyagi;
            ViewBag.CheckedDopTyagi = GetFilter().CheckedDopTyagi;
            ViewBag.CheckedSumTyagi = GetFilter().CheckedSumTyagi;
            ViewBag.CheckedLength = GetFilter().CheckedLength;
            ViewBag.CheckedCount = GetFilter().CheckedCount;
            int pageSize = 24; // количество объектов на страницу
            IEnumerable<Arbalet> GunsPerPages = Gun.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = Gun.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Arbalets = GunsPerPages
            };

            ViewBag.IVM = ivm;

            return View(ivm);
        }
        public FilterArbalet GetFilter()

        {
            FilterArbalet filter = (FilterArbalet)Session["FilterArbalet"];
            if (filter == null)
            {
                filter = new FilterArbalet();
                Session["FilterArbalet"] = filter;
            }
            return filter;
        }

        [HttpPost]
        public ActionResult ArbaletFilter(int MinPrice, int MaxPrice, string[] brand, string[] country, decimal[] diametrGarpun, int[] diametrTyagi, string[] dopTyagi, int[] sumTyagi, int[] length)
        {
            int count = unitOfWork.Arbalets.GetAll().Count();
            List<Arbalet> Gun = unitOfWork.Arbalets.GetAll().Where(p => p.Price >= MinPrice && p.Price <= MaxPrice).ToList();
            List<Arbalet> newGun = new List<Arbalet>();

            GetFilter().CheckedMinPrice = MinPrice;
            GetFilter().CheckedMaxPrice = MaxPrice;
            GetFilter().CheckedBrand = 0;
            GetFilter().CheckedCountry = 0;
            GetFilter().CheckedDiametrGarpun = 0;
            GetFilter().CheckedDiametrTyagi = 0;
            GetFilter().CheckedDopTyagi = 0;
            GetFilter().CheckedSumTyagi = 0;
            GetFilter().CheckedLength = 0;
            GetFilter().CheckedCount = 0;


            ViewBag.MinPrice = unitOfWork.Arbalets.GetAll().Min(a => a.Price);
            ViewBag.MaxPrice = unitOfWork.Arbalets.GetAll().Max(a => a.Price);
            ViewBag.CheckedMinPrice = MinPrice;
            ViewBag.CheckedMaxPrice = MaxPrice;
            ViewBag.CheckedBrand = 0;
            ViewBag.CheckedCountry = 0;
            ViewBag.CheckedDiametrGarpun = 0;
            ViewBag.CheckedDiametrTyagi = 0;
            ViewBag.CheckedDopTyagi = 0;
            ViewBag.CheckedSumTyagi = 0;
            ViewBag.CheckedLength = 0;
            ViewBag.CheckedCount = 0;

            int Count = 0;

            if (brand != null)
            {
                string a = "";
                for (int i = 0; i < brand.Count(); i++)
                {

                    a = brand[i];
                    foreach (var c in Gun.Where(p => p.Brand.Name == a))
                    {

                        newGun.Add(c);
                    }
                }
                Gun = newGun;
                GetFilter().CheckedBrand = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedBrand = 1;
                Count++;
            }

            if (country != null)
            {
                newGun = new List<Arbalet>();
                string a = "";
                for (int i = 0; i < country.Count(); i++)
                {

                    a = country[i];
                    foreach (var c in Gun.Where(p => p.Brand.Country == a))
                    {
                        newGun.Add(c);
                    }
                }
                Gun = newGun;
                GetFilter().CheckedCountry = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedCountry = 1;
                Count++;
            }
            if (diametrGarpun != null)
            {
                newGun = new List<Arbalet>();
                decimal a = 0;
                for (int i = 0; i < diametrGarpun.Count(); i++)
                {

                    a = diametrGarpun[i];
                    foreach (var c in Gun.Where(p => p.DiametrGarpun == a))
                    {
                        newGun.Add(c);
                    }
                }
                Gun = newGun;
                GetFilter().CheckedDiametrGarpun = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedDiametrGarpun = 1;
                Count++;
            }
            if (diametrTyagi != null)
            {
                newGun = new List<Arbalet>();
                int a = 0;
                for (int i = 0; i < diametrTyagi.Count(); i++)
                {

                    a = diametrTyagi[i];
                    foreach (var c in Gun.Where(p => p.DiametrTyagi == a))
                    {
                        newGun.Add(c);
                    }
                }
                Gun = newGun;
                GetFilter().CheckedDiametrTyagi = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedDiametrTyagi = 1;
                Count++;
            }
            if (dopTyagi != null)
            {
                newGun = new List<Arbalet>();
                string a = "";
                for (int i = 0; i < dopTyagi.Count(); i++)
                {

                    a = dopTyagi[i];
                    foreach (var c in Gun.Where(p => p.DopTyagi == a))
                    {
                        newGun.Add(c);
                    }
                }
                Gun = newGun;
                GetFilter().CheckedDopTyagi = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedDopTyagi = 1;
                Count++;
            }
            if (sumTyagi != null)
            {
                newGun = new List<Arbalet>();
                int a = 0;
                for (int i = 0; i < sumTyagi.Count(); i++)
                {

                    a = sumTyagi[i];
                    foreach (var c in Gun.Where(p => p.SumTyagi == a))
                    {
                        newGun.Add(c);
                    }
                }
                Gun = newGun;
                GetFilter().CheckedSumTyagi = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedSumTyagi = 1;
                Count++;
            }
            if (length != null)
            {
                newGun = new List<Arbalet>();
                int a = 0;
                for (int i = 0; i < length.Count(); i++)
                {

                    a = length[i];
                    foreach (var c in Gun.Where(p => p.Length >= a && p.Length < a + 30))
                    {
                        newGun.Add(c);
                    }
                }
                Gun = newGun;
                GetFilter().CheckedLength = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedLength = 1;
                Count++;
            }
            ViewBag.CheckedCount = Count;

            var items = unitOfWork.Arbalets.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();

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
            var DiametrTyagi = new List<int>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                DiametrTyagi.Add(items[i].DiametrTyagi);
            }
            List<int> newdiametrTyagi = new List<int>(DiametrTyagi.Distinct());
            newdiametrTyagi.Reverse();
            ViewBag.DiametrTyagi = newdiametrTyagi;
            //------------------------------------------------
            var DopTyagi = new List<string>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                DopTyagi.Add(items[i].DopTyagi);
            }
            List<string> newdopTyagi = new List<string>(DopTyagi.Distinct());
            newdopTyagi.Reverse();
            ViewBag.DopTyagi = newdopTyagi;
            //------------------------------------------------
            var SumTyagi = new List<int>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                SumTyagi.Add(items[i].SumTyagi);
            }
            List<int> newsumTyagi = new List<int>(SumTyagi.Distinct());
            newsumTyagi.Reverse();
            ViewBag.SumTyagi = newsumTyagi;
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

            ViewBag.Count = Gun.Count();
            int page = 1;
            int pageSize = 24; // количество объектов на страницу
            IEnumerable<Arbalet> GunsPerPages = Gun.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = Gun.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Arbalets = Gun
            };

            ViewBag.IVM = ivm;

            GetFilter().Gun = Gun;

            return View(ivm);
        }

        [HttpPost]
        public ActionResult ArbaletCount()
        {
            List<Arbalet> Gun = GetFilter().Gun;

            return Json(Gun.Count());
        }

        [Authorize(Roles = "Admin")]
        public ViewResult Create()
        {
            SelectList brands = new SelectList(unitOfWork.Brands.GetAll(), "Id", "Name");
            ViewBag.Brands = brands;
            return View("Edit", new Arbalet());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int Id)
        {
            Arbalet deletedArbalet = unitOfWork.Arbalets.Delete(Id);
            if (deletedArbalet != null)
            {
                TempData["message"] = string.Format("Товар \"{0}\" был удален",
                    deletedArbalet.Name);
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public ViewResult Index()
        {
            return View(unitOfWork.Arbalets.GetAll().ToList());
        }

        [Authorize(Roles = "Admin")]
        public ViewResult Edit(int Id)
        {
            Arbalet pneumatic = unitOfWork.Arbalets.GetAll().FirstOrDefault(g => g.Id == Id);
            SelectList brands = new SelectList(unitOfWork.Brands.GetAll(), "Id", "Name");
            ViewBag.Brands = brands;
            return View(pneumatic);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Arbalet arbalet, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    arbalet.ImageMimeType = image.ContentType;
                    arbalet.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(arbalet.ImageData, 0, image.ContentLength);
                }
                else
                {
                    arbalet.ImageMimeType = unitOfWork.Arbalets.Get(arbalet.Id).ImageMimeType;
                    arbalet.ImageData = unitOfWork.Arbalets.Get(arbalet.Id).ImageData;
                }
                unitOfWork.Arbalets.SaveItem(arbalet);
                TempData["message"] = string.Format("Изменения в товаре \"{0}\" были сохранены", arbalet.Name);
                return RedirectToAction("Index");
            }
            else
            {
                // Что-то не так со значениями данных
                return View(arbalet);
            }
        }


    }
    public class FilterArbalet
    {
        public List<Arbalet> Gun { get; set; }
        public int CheckedBrand { get; set; }
        public int CheckedCountry { get; set; }
        public int CheckedDiametrGarpun { get; set; }
        public int CheckedDiametrTyagi { get; set; }
        public int CheckedDopTyagi { get; set; }
        public int CheckedSumTyagi { get; set; }
        public int CheckedLength { get; set; }
        public int CheckedCount { get; set; }
        public int CheckedMinPrice { get; set; }
        public int CheckedMaxPrice { get; set; }
    }
}
