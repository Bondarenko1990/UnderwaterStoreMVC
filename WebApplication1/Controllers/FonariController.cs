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
    public class FonariController : Controller
    {
        UnitOfWork unitOfWork;
        public FonariController()
        {
            unitOfWork = new UnitOfWork();
        }

        public FilterFonari GetFilter()
        {
            FilterFonari filter = (FilterFonari)Session["FilterFonari"];
            if (filter == null)
            {
                filter = new FilterFonari();
                Session["FilterFonari"] = filter;
            }
            return filter;
        }

        public FileContentResult GetImage(int Id)
        {
            Fonari item = unitOfWork.Fonaris.GetAll().FirstOrDefault(g => g.Id == Id);

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
            return View("Edit", new Fonari());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int Id)
        {
            Fonari deletedItem = unitOfWork.Fonaris.Delete(Id);
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
            return View(unitOfWork.Fonaris.GetAll().ToList());
        }

        [Authorize(Roles = "Admin")]
        public ViewResult Edit(int Id)
        {
            Fonari item = unitOfWork.Fonaris.GetAll().FirstOrDefault(g => g.Id == Id);
            SelectList brands = new SelectList(unitOfWork.Brands.GetAll(), "Id", "Name");
            ViewBag.Brands = brands;
            return View(item);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Fonari item, HttpPostedFileBase image = null)
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
                    item.ImageMimeType = unitOfWork.Fonaris.Get(item.Id).ImageMimeType;
                    item.ImageData = unitOfWork.Fonaris.Get(item.Id).ImageData;
                }
                unitOfWork.Fonaris.SaveItem(item);
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
            Fonari c = unitOfWork.Fonaris.GetAll().FirstOrDefault(com => com.Id == id);
            if (c != null)
                return PartialView(c);
            return HttpNotFound();
        }

        public ActionResult Details(int id)
        {
            Fonari c = unitOfWork.Fonaris.GetAll().FirstOrDefault(com => com.Id == id);
            if (c != null)
                return PartialView(c);
            return HttpNotFound();
        }

        [HttpPost]
        public PartialViewResult Models(int page = 1)
        {

            int pageSize = 24;

            IEnumerable<Fonari> GunsPerPages = GetFilter().Item.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = GetFilter().Item.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Fonaris = GunsPerPages
            };
            ViewBag.IVM = ivm;
            return PartialView(ivm);
        }

        public ActionResult List(int page = 1)
        {

            int count = unitOfWork.Fonaris.GetAll().Count();
            var items = unitOfWork.Fonaris.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();
            List<Fonari> Item;
            if (GetFilter().Item == null)
            {
                Item = unitOfWork.Fonaris.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();
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
            var TypeLamp = new List<string>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                TypeLamp.Add(items[i].TypeLamp);
            }
            List<string> newTypeLamp = new List<string>(TypeLamp.Distinct());
            newTypeLamp.Sort();
            ViewBag.TypeLamp = newTypeLamp;
            //------------------------------------------------
            var TypeBattery = new List<string>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                TypeBattery.Add(items[i].TypeBattery);
            }
            List<string> newTypeBattery = new List<string>(TypeBattery.Distinct());
            newTypeBattery.Sort();
            ViewBag.TypeBattery = newTypeBattery;
            //------------------------------------------------
            var Svet = new List<int[]>();
            int[][] SvetEdit = new int[6][];
            SvetEdit[0] = new int[] { 0, 100 };
            SvetEdit[1] = new int[] { 100, 200 };
            SvetEdit[2] = new int[] { 200, 500 };
            SvetEdit[3] = new int[] { 500, 1000 };
            SvetEdit[4] = new int[] { 1000, 2000 };
            SvetEdit[5] = new int[] { 2000, 10000 };
            for (int i = 0; i < SvetEdit.Length; i++)
            {
                if (items.Exists(p => p.Svet >= SvetEdit[i][0] && p.Svet < SvetEdit[i][1] && p.Svet != 0))
                {
                    Svet.Add(SvetEdit[i]);
                }
            }
            if (items.Exists(p => p.Svet == 0))
            {
                Svet.Add(new int[] { 0, 0 });
            }
            List<int[]> newSvet = new List<int[]>(Svet.Distinct());
            ViewBag.Svet = newSvet.ToList();
            //------------------------------------------------
            var TimeWork = new List<int[]>();
            int[][] TimeWorkEdit = new int[7][];
            TimeWorkEdit[0] = new int[] { 0, 5 };
            TimeWorkEdit[1] = new int[] { 5, 10 };
            TimeWorkEdit[2] = new int[] { 10, 15 };
            TimeWorkEdit[3] = new int[] { 15, 20 };
            TimeWorkEdit[4] = new int[] { 20, 50 };
            TimeWorkEdit[5] = new int[] { 200, 500 };
            TimeWorkEdit[6] = new int[] { 500, 1000 };
            for (int i = 0; i < TimeWorkEdit.Length; i++)
            {
                if (items.Exists(p => p.TimeWork >= TimeWorkEdit[i][0] && p.TimeWork < TimeWorkEdit[i][1] && p.TimeWork != 0))
                {
                    TimeWork.Add(TimeWorkEdit[i]);
                }
            }
            if (items.Exists(p => p.TimeWork == 0))
            {
                TimeWork.Add(new int[] { 0, 0 });
            }
            List<int[]> newTimeWork = new List<int[]>(TimeWork.Distinct());
            ViewBag.TimeWork = newTimeWork.ToList();

            //------------------------------------------------
            var QuantityBattery = new List<int[]>();
            int[][] QuantityEdit = new int[3][];
            QuantityEdit[0] = new int[] { 0, 3 };
            QuantityEdit[1] = new int[] { 3, 6 };
            QuantityEdit[2] = new int[] { 6, 9 };
            for (int i = 0; i < QuantityEdit.Length; i++)
            {
                if (items.Exists(p => p.QuantityBattery >= QuantityEdit[i][0] && p.QuantityBattery < QuantityEdit[i][1] && p.QuantityBattery != 0))
                {
                    QuantityBattery.Add(QuantityEdit[i]);
                }
            }
            if (items.Exists(p => p.QuantityBattery == 0))
            {
                QuantityBattery.Add(new int[] { 0, 0 });
            }
            List<int[]> newQuantityBattery = new List<int[]>(QuantityBattery.Distinct());

            ViewBag.QuantityBattery = newQuantityBattery.ToList();
            //------------------------------------------------
            var MaxGlubina = new List<int[]>();
            int[][] MaxGlubinaEdit = new int[6][];
            MaxGlubinaEdit[0] = new int[] { 0, 30 };
            MaxGlubinaEdit[1] = new int[] { 30, 60 };
            MaxGlubinaEdit[2] = new int[] { 60, 90 };
            MaxGlubinaEdit[3] = new int[] { 90, 120 };
            MaxGlubinaEdit[4] = new int[] { 120, 220 };
            MaxGlubinaEdit[5] = new int[] { 220, 600 };
            for (int i = 0; i < MaxGlubinaEdit.Length; i++)
            {
                if (items.Exists(p => p.MaxGlubina >= MaxGlubinaEdit[i][0] && p.MaxGlubina < MaxGlubinaEdit[i][1] && p.MaxGlubina != 0))
                {
                    MaxGlubina.Add(MaxGlubinaEdit[i]);
                }
            }
            if (items.Exists(p => p.MaxGlubina == 0))
            {
                MaxGlubina.Add(new int[] { 0, 0 });
            }
            List<int[]> newMaxGlubina = new List<int[]>(MaxGlubina.Distinct());

            ViewBag.MaxGlubina = newMaxGlubina.ToList();
            //------------------------------------------------           
            ViewBag.MinPrice = unitOfWork.Fonaris.GetAll().Min(a => a.Price);
            ViewBag.MaxPrice = unitOfWork.Fonaris.GetAll().Max(a => a.Price);
            ViewBag.CheckedMinPrice = GetFilter().CheckedMinPrice;
            if (GetFilter().CheckedMaxPrice == 0)
            {
                ViewBag.CheckedMaxPrice = unitOfWork.Fonaris.GetAll().Max(a => a.Price);
            }
            else
            {
                ViewBag.CheckedMaxPrice = GetFilter().CheckedMaxPrice;
            }
            ViewBag.CheckedBrand = GetFilter().CheckedBrand;
            ViewBag.CheckedCountry = GetFilter().CheckedCountry;
            ViewBag.CheckedTypeLamp = GetFilter().CheckedTypeLamp;
            ViewBag.CheckedTypeBattery = GetFilter().CheckedTypeBattery;
            ViewBag.CheckedSvet = GetFilter().CheckedSvet;
            ViewBag.CheckedTimeWork = GetFilter().CheckedTimeWork;
            ViewBag.CheckedQuantityBattery = GetFilter().CheckedQuantityBattery;
            ViewBag.CheckedMaxGlubina = GetFilter().CheckedMaxGlubina;
            ViewBag.CheckedCount = GetFilter().CheckedCount;
            int pageSize = 24; // количество объектов на страницу
            IEnumerable<Fonari> GunsPerPages = Item.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = Item.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Fonaris= GunsPerPages
            };

            ViewBag.IVM = ivm;

            return View(ivm);
        }

        [HttpPost]
        public ActionResult Filter(int MinPrice, int MaxPrice, string[] brand, string[] country,string[] typelamp, string[] typebattery,
              int[][] svet, int[][] timework, int[][] quantitybattery, int[][] maxglubina)
        {
            int count = unitOfWork.Fonaris.GetAll().Count();
            List<Fonari> Item = unitOfWork.Fonaris.GetAll().Where(p => p.Price >= MinPrice && p.Price <= MaxPrice).ToList();
            List<Fonari> newItem = new List<Fonari>();

            GetFilter().CheckedMinPrice = MinPrice;
            GetFilter().CheckedMaxPrice = MaxPrice;
            GetFilter().CheckedBrand = 0;
            GetFilter().CheckedCountry = 0;
            GetFilter().CheckedTypeLamp = 0;
            GetFilter().CheckedTypeBattery = 0;
            GetFilter().CheckedSvet = 0;
            GetFilter().CheckedTimeWork = 0;
            GetFilter().CheckedQuantityBattery = 0;
            GetFilter().CheckedMaxGlubina = 0;
            GetFilter().CheckedCount = 0;


            ViewBag.MinPrice = unitOfWork.Fonaris.GetAll().Min(a => a.Price);
            ViewBag.MaxPrice = unitOfWork.Fonaris.GetAll().Max(a => a.Price);
            ViewBag.CheckedMinPrice = MinPrice;
            ViewBag.CheckedMaxPrice = MaxPrice;
            ViewBag.CheckedBrand = 0;
            ViewBag.CheckedCountry = 0;
            ViewBag.CheckedTypeLamp = 0;
            ViewBag.CheckedTypeOBattery = 0;
            ViewBag.CheckedSvet = 0;
            ViewBag.CheckedTimeWork = 0;
            ViewBag.CheckedQuantityBattery = 0;
            ViewBag.CheckedMaxGlubina = 0;
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
                newItem = new List<Fonari>();
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

            if (typelamp != null)
            {
                newItem = new List<Fonari>();
                string a = "";
                for (int i = 0; i < typelamp.Count(); i++)
                {

                    a = typelamp[i];
                    foreach (var c in Item.Where(p => p.TypeLamp == a))
                    {
                        newItem.Add(c);
                    }
                }
                Item = newItem;
                GetFilter().CheckedTypeLamp = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedTypeLamp = 1;
                Count++;
            }

            if (typebattery != null)
            {
                newItem = new List<Fonari>();
                string a = "";
                for (int i = 0; i < typebattery.Count(); i++)
                {

                    a = typebattery[i];
                    foreach (var c in Item.Where(p => p.TypeBattery == a))
                    {
                        newItem.Add(c);
                    }
                }
                Item = newItem;
                GetFilter().CheckedTypeBattery = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedTypeBattery = 1;
                Count++;
            }

            if (svet != null)
            {
                newItem = new List<Fonari>();
                for (int i = 0; i < svet.Count(); i++)
                {
                    foreach (var c in Item.Where(p => (p.Svet >= svet[i][0] && p.Svet < svet[i][1]) || (p.Svet == svet[i][0] && p.Svet == svet[i][1])))
                    {
                        newItem.Add(c);
                    }
                }
                Item = newItem;
                GetFilter().CheckedSvet = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedSvet = 1;
                Count++;
            }
            if (timework != null)
            {
                newItem = new List<Fonari>();
                
                for (int i = 0; i < timework.Count(); i++)
                {
                    foreach (var c in Item.Where(p => (p.TimeWork >= timework[i][0] && p.TimeWork < timework[i][1]) || (p.TimeWork == timework[i][0] && p.TimeWork == timework[i][1])))
                    {
                        newItem.Add(c);
                    }
                }
 
                Item = newItem;
                GetFilter().CheckedTimeWork = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedTimeWork = 1;
                Count++;
            }

            if (quantitybattery != null)
            {
                newItem = new List<Fonari>();
                for (int i = 0; i < quantitybattery.Count(); i++)
                {  
                    foreach (var c in Item.Where(p => (p.QuantityBattery >= quantitybattery[i][0] && p.QuantityBattery < quantitybattery[i][1]) || (p.QuantityBattery == quantitybattery[i][0] && p.QuantityBattery == quantitybattery[i][1])))
                    {
                        newItem.Add(c);
                    }
                }
                Item = newItem;
                GetFilter().CheckedQuantityBattery = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedQuantityBattery = 1;
                Count++;
            }

            if (maxglubina != null)
            {
                newItem = new List<Fonari>();
                for (int i = 0; i < maxglubina.Count(); i++)
                {
                    foreach (var c in Item.Where(p => (p.MaxGlubina >= maxglubina[i][0] && p.MaxGlubina < maxglubina[i][1]) || (p.MaxGlubina == maxglubina[i][0] && p.MaxGlubina == maxglubina[i][1])))
                    {
                        newItem.Add(c);
                    }
                }
                Item = newItem;
                GetFilter().CheckedMaxGlubina = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedMaxGlubina = 1;
                Count++;
            }

            ViewBag.CheckedCount = Count;

            var items = unitOfWork.Fonaris.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();

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
            var TypeLamp = new List<string>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                TypeLamp.Add(items[i].TypeLamp);
            }
            List<string> newTypeLamp = new List<string>(TypeLamp.Distinct());
            newTypeLamp.Sort();
            ViewBag.TypeLamp = newTypeLamp;
            //------------------------------------------------
            var TypeBattery = new List<string>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                TypeBattery.Add(items[i].TypeBattery);
            }
            List<string> newTypeBattery = new List<string>(TypeBattery.Distinct());
            newTypeBattery.Sort();
            ViewBag.TypeBattery = newTypeBattery;
            //------------------------------------------------
            var Svet = new List<int[]>();
            int[][] SvetEdit = new int[6][];
            SvetEdit[0] = new int[] { 0, 100 };
            SvetEdit[1] = new int[] { 100, 200 };
            SvetEdit[2] = new int[] { 200, 500 };
            SvetEdit[3] = new int[] { 500, 1000 };
            SvetEdit[4] = new int[] { 1000, 2000 };
            SvetEdit[5] = new int[] { 2000, 10000 };
            for (int i = 0; i < SvetEdit.Length; i++)
            {
                if (items.Exists(p => p.Svet >= SvetEdit[i][0] && p.Svet < SvetEdit[i][1] && p.Svet != 0))
                {
                    Svet.Add(SvetEdit[i]);
                }
            }
            if (items.Exists(p => p.Svet == 0))
            {
                Svet.Add(new int[] { 0, 0 });
            }
            List<int[]> newSvet = new List<int[]>(Svet.Distinct());
            ViewBag.Svet = newSvet.ToList();
            //------------------------------------------------
            var TimeWork = new List<int[]>();
            int[][] TimeWorkEdit = new int[7][];
            TimeWorkEdit[0] = new int[] { 0, 5 };
            TimeWorkEdit[1] = new int[] { 5, 10 };
            TimeWorkEdit[2] = new int[] { 10, 15 };
            TimeWorkEdit[3] = new int[] { 15, 20 };
            TimeWorkEdit[4] = new int[] { 20, 50 };
            TimeWorkEdit[5] = new int[] { 200, 500 };
            TimeWorkEdit[6] = new int[] { 500, 1000 };
            for (int i = 0; i < TimeWorkEdit.Length; i++)
            {
                if (items.Exists(p => p.TimeWork >= TimeWorkEdit[i][0] && p.TimeWork < TimeWorkEdit[i][1] && p.TimeWork != 0))
                {
                    TimeWork.Add(TimeWorkEdit[i]);
                }
            }
            if (items.Exists(p => p.TimeWork == 0))
            {
                TimeWork.Add(new int[]{ 0 , 0 });
            }
            List<int[]> newTimeWork = new List<int[]>(TimeWork.Distinct());
            ViewBag.TimeWork = newTimeWork.ToList();
            
        //------------------------------------------------
            var QuantityBattery = new List<int[]>();
            int[][] QuantityEdit = new int[3][];
            QuantityEdit[0] = new int[] { 0, 3 };
            QuantityEdit[1] = new int[] { 3, 6 };
            QuantityEdit[2] = new int[] { 6, 9 };
            for (int i = 0; i < QuantityEdit.Length; i++)
            {
                if (items.Exists(p => p.QuantityBattery >= QuantityEdit[i][0] && p.QuantityBattery < QuantityEdit[i][1] && p.QuantityBattery != 0))
                {
                    QuantityBattery.Add(QuantityEdit[i]);
                }
            }
            if (items.Exists(p => p.QuantityBattery == 0))
            {
                QuantityBattery.Add(new int[] { 0, 0 });
            }
            List<int[]> newQuantityBattery = new List<int[]>(QuantityBattery.Distinct());

            ViewBag.QuantityBattery = newQuantityBattery.ToList();
            //------------------------------------------------
            var MaxGlubina = new List<int[]>();
            int[][] MaxGlubinaEdit = new int[6][];
            MaxGlubinaEdit[0] = new int[] { 0, 30 };
            MaxGlubinaEdit[1] = new int[] { 30, 60 };
            MaxGlubinaEdit[2] = new int[] { 60, 90 };
            MaxGlubinaEdit[3] = new int[] { 90, 120 };
            MaxGlubinaEdit[4] = new int[] { 120, 220 };
            MaxGlubinaEdit[5] = new int[] { 220, 600 };
            for (int i = 0; i < MaxGlubinaEdit.Length; i++)
            {
                if (items.Exists(p => p.MaxGlubina >= MaxGlubinaEdit[i][0] && p.MaxGlubina < MaxGlubinaEdit[i][1] && p.MaxGlubina != 0))
                {
                    MaxGlubina.Add(MaxGlubinaEdit[i]);
                }
            }
            if (items.Exists(p => p.MaxGlubina == 0))
            {
                MaxGlubina.Add(new int[] { 0, 0 });
            }
            List<int[]> newMaxGlubina = new List<int[]>(MaxGlubina.Distinct());

            ViewBag.MaxGlubina = newMaxGlubina.ToList();
            //------------------------------------------------

            ViewBag.Count = Item.Count();
            int page = 1;
            int pageSize = 24; // количество объектов на страницу
            IEnumerable<Fonari> GunsPerPages = Item.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = Item.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Fonaris = Item
            };

            ViewBag.IVM = ivm;

            GetFilter().Item = Item;

            return View(ivm);
        }

        [HttpPost]
        public ActionResult Count()
        {
            List<Fonari> Item = GetFilter().Item;

            return Json(Item.Count());
        }

    }
    public class FilterFonari
    {
        public List<Fonari> Item { get; set; }
        public int CheckedBrand { get; set; }
        public int CheckedCountry { get; set; }
        public int CheckedTypeLamp { get; set; }
        public int CheckedTypeBattery { get; set; }
        public int CheckedSvet { get; set; }
        public int CheckedTimeWork { get; set; }
        public int CheckedQuantityBattery { get; set; }
        public int CheckedMaxGlubina { get; set; }
        public int CheckedCount { get; set; }
        public int CheckedMinPrice { get; set; }
        public int CheckedMaxPrice { get; set; }

    }
}