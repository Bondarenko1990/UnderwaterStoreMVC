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
    public class PerchatkiController : Controller
    {
        UnitOfWork unitOfWork;
        public PerchatkiController()
        {
            unitOfWork = new UnitOfWork();
        }

        public FilterPerchatki GetFilter()
        {
            FilterPerchatki filter = (FilterPerchatki)Session["FilterPerchatki"];
            if (filter == null)
            {
                filter = new FilterPerchatki();
                Session["FilterPerchatki"] = filter;
            }
            return filter;
        }

        public FileContentResult GetImage(int Id)
        {
            Perchatki item = unitOfWork.Perchatkis.GetAll().FirstOrDefault(g => g.Id == Id);

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
            return View("Edit", new Perchatki());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int Id)
        {
            Perchatki deletedItem = unitOfWork.Perchatkis.Delete(Id);
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
            return View(unitOfWork.Perchatkis.GetAll().ToList());
        }

        [Authorize(Roles = "Admin")]
        public ViewResult Edit(int Id)
        {
            Perchatki item = unitOfWork.Perchatkis.GetAll().FirstOrDefault(g => g.Id == Id);
            SelectList brands = new SelectList(unitOfWork.Brands.GetAll(), "Id", "Name");
            ViewBag.Brands = brands;
            return View(item);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Perchatki item, HttpPostedFileBase image = null)
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
                    item.ImageMimeType = unitOfWork.Perchatkis.Get(item.Id).ImageMimeType;
                    item.ImageData = unitOfWork.Perchatkis.Get(item.Id).ImageData;
                }
                unitOfWork.Perchatkis.SaveItem(item);
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
            Perchatki c = unitOfWork.Perchatkis.GetAll().FirstOrDefault(com => com.Id == id);
            if (c != null)
                return PartialView(c);
            return HttpNotFound();
        }

        public ActionResult Details(int id)
        {
            Perchatki c = unitOfWork.Perchatkis.GetAll().FirstOrDefault(com => com.Id == id);
            if (c != null)
                return PartialView(c);
            return HttpNotFound();
        }

        [HttpPost]
        public PartialViewResult Models(int page = 1)
        {

            int pageSize = 24;

            IEnumerable<Perchatki> GunsPerPages = GetFilter().Item.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = GetFilter().Item.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Perchatkis = GunsPerPages
            };
            ViewBag.IVM = ivm;
            return PartialView(ivm);
        }

        public ActionResult List(int page = 1)
        {

            int count = unitOfWork.Perchatkis.GetAll().Count();
            var items = unitOfWork.Perchatkis.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();
            List<Perchatki> Item;
            if (GetFilter().Item == null)
            {
                Item = unitOfWork.Perchatkis.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();
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
            var Pokritie = new List<string>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                Pokritie.Add(items[i].Pokritie);
            }
            List<string> newPokritie = new List<string>(Pokritie.Distinct());
            newPokritie.Sort();
            ViewBag.Pokritie = newPokritie;
            //------------------------------------------------
            var Mangeti = new List<bool>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                Mangeti.Add(items[i].Mangeti);
            }
            List<bool> newMangeti = new List<bool>(Mangeti.Distinct());
            newMangeti.Sort();
            newMangeti.Reverse();
            ViewBag.Mangeti = newMangeti;
            //------------------------------------------------
            var Obturaciya = new List<bool>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                Obturaciya.Add(items[i].Obturaciya);
            }
            List<bool> newObturaciya = new List<bool>(Obturaciya.Distinct());
            newObturaciya.Sort();
            newObturaciya.Reverse();
            ViewBag.Obturaciya = newObturaciya;
            //------------------------------------------------
            var Tolshina = new List<int[]>();
            int[][] TolshinaEdit = new int[4][];
            TolshinaEdit[0] = new int[] { 0, 4 };
            TolshinaEdit[1] = new int[] { 4, 6 };
            TolshinaEdit[2] = new int[] { 6, 8 };
            TolshinaEdit[3] = new int[] { 8, 10 };
            for (int i = 0; i < TolshinaEdit.Length; i++)
            {
                if (items.Exists(p => p.Tolshina >= TolshinaEdit[i][0] && p.Tolshina < TolshinaEdit[i][1] && p.Tolshina != 0))
                {
                    Tolshina.Add(TolshinaEdit[i]);
                }
            }
            if (items.Exists(p => p.Tolshina == 0))
            {
                Tolshina.Add(new int[] { 0, 0 });
            }
            List<int[]> newTolshina = new List<int[]>(Tolshina.Distinct());
            ViewBag.Tolshina = newTolshina;
            //------------------------------------------------         
            ViewBag.MinPrice = unitOfWork.Perchatkis.GetAll().Min(a => a.Price);
            ViewBag.MaxPrice = unitOfWork.Perchatkis.GetAll().Max(a => a.Price);
            ViewBag.CheckedMinPrice = GetFilter().CheckedMinPrice;
            if (GetFilter().CheckedMaxPrice == 0)
            {
                ViewBag.CheckedMaxPrice = unitOfWork.Perchatkis.GetAll().Max(a => a.Price);
            }
            else
            {
                ViewBag.CheckedMaxPrice = GetFilter().CheckedMaxPrice;
            }
            ViewBag.CheckedBrand = GetFilter().CheckedBrand;
            ViewBag.CheckedCountry = GetFilter().CheckedCountry;
            ViewBag.CheckedPokritie = GetFilter().CheckedPokritie;
            ViewBag.CheckedMangeti = GetFilter().CheckedMangeti;
            ViewBag.CheckedObturaciya = GetFilter().CheckedObturaciya;
            ViewBag.CheckedTolshina = GetFilter().CheckedTolshina;
            ViewBag.CheckedCount = GetFilter().CheckedCount;
            int pageSize = 24; // количество объектов на страницу
            IEnumerable<Perchatki> GunsPerPages = Item.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = Item.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Perchatkis = GunsPerPages
            };

            ViewBag.IVM = ivm;

            return View(ivm);
        }

        [HttpPost]
        public ActionResult Filter(int MinPrice, int MaxPrice, string[] brand, string[] country, string[] pokritie, bool[] mangeti, bool[] obturaciya, int[][] tolshina)
        {
            int count = unitOfWork.Perchatkis.GetAll().Count();
            List<Perchatki> Item = unitOfWork.Perchatkis.GetAll().Where(p => p.Price >= MinPrice && p.Price <= MaxPrice).ToList();
            List<Perchatki> newItem = new List<Perchatki>();

            GetFilter().CheckedMinPrice = MinPrice;
            GetFilter().CheckedMaxPrice = MaxPrice;
            GetFilter().CheckedBrand = 0;
            GetFilter().CheckedCountry = 0;
            GetFilter().CheckedPokritie = 0;
            GetFilter().CheckedMangeti = 0;
            GetFilter().CheckedObturaciya = 0;
            GetFilter().CheckedTolshina = 0;
            GetFilter().CheckedCount = 0;


            ViewBag.MinPrice = unitOfWork.Perchatkis.GetAll().Min(a => a.Price);
            ViewBag.MaxPrice = unitOfWork.Perchatkis.GetAll().Max(a => a.Price);
            ViewBag.CheckedMinPrice = MinPrice;
            ViewBag.CheckedMaxPrice = MaxPrice;
            ViewBag.CheckedBrand = 0;
            ViewBag.CheckedCountry = 0;
            ViewBag.CheckedPokritie = 0;
            ViewBag.CheckedMangeti = 0;
            ViewBag.CheckedObturaciya = 0;
            ViewBag.CheckedTolshina = 0;
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
                newItem = new List<Perchatki>();
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

            if (pokritie != null)
            {
                newItem = new List<Perchatki>();
                string a = "";
                for (int i = 0; i < pokritie.Count(); i++)
                {

                    a = pokritie[i];
                    foreach (var c in Item.Where(p => p.Pokritie == a))
                    {
                        newItem.Add(c);
                    }
                }
                Item = newItem;
                GetFilter().CheckedPokritie = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedPokritie = 1;
                Count++;
            }
            if (mangeti != null)
            {
                newItem = new List<Perchatki>();
                bool a;
                for (int i = 0; i < mangeti.Count(); i++)
                {

                    a = mangeti[i];
                    foreach (var c in Item.Where(p => p.Mangeti == a))
                    {
                        newItem.Add(c);
                    }
                }
                Item = newItem;
                GetFilter().CheckedMangeti = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedMangeti = 1;
                Count++;
            }

            if (obturaciya != null)
            {
                newItem = new List<Perchatki>();
                bool a;
                for (int i = 0; i < obturaciya.Count(); i++)
                {

                    a = obturaciya[i];
                    foreach (var c in Item.Where(p => p.Obturaciya == a))
                    {
                        newItem.Add(c);
                    }
                }
                Item = newItem;
                GetFilter().CheckedObturaciya = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedObturaciya = 1;
                Count++;
            }

            if (tolshina != null)
            {
                newItem = new List<Perchatki>();
                for (int i = 0; i < tolshina.Count(); i++)
                {
                    foreach (var c in Item.Where(p => (p.Tolshina >= tolshina[i][0] && p.Tolshina < tolshina[i][1]) || (p.Tolshina == tolshina[i][0] && p.Tolshina == tolshina[i][1])))
                    {
                        newItem.Add(c);
                    }
                }
                Item = newItem;
                GetFilter().CheckedTolshina = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedTolshina = 1;
                Count++;
            }

            ViewBag.CheckedCount = Count;

            var items = unitOfWork.Perchatkis.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();

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
            var Pokritie = new List<string>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                Pokritie.Add(items[i].Pokritie);
            }
            List<string> newPokritie = new List<string>(Pokritie.Distinct());
            newPokritie.Sort();
            ViewBag.Pokritie = newPokritie;
            //------------------------------------------------
            var Mangeti = new List<bool>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                Mangeti.Add(items[i].Mangeti);
            }
            List<bool> newMangeti = new List<bool>(Mangeti.Distinct());
            newMangeti.Sort();
            newMangeti.Reverse();
            ViewBag.Mangeti = newMangeti;
            //------------------------------------------------
            var Obturaciya = new List<bool>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                Obturaciya.Add(items[i].Obturaciya);
            }
            List<bool> newObturaciya = new List<bool>(Obturaciya.Distinct());
            newObturaciya.Sort();
            newObturaciya.Reverse();
            ViewBag.Obturaciya = newObturaciya;
            //------------------------------------------------
            var Tolshina = new List<int[]>();
            int[][] TolshinaEdit = new int[4][];
            TolshinaEdit[0] = new int[] { 0, 4 };
            TolshinaEdit[1] = new int[] { 4, 6 };
            TolshinaEdit[2] = new int[] { 6, 8 };
            TolshinaEdit[3] = new int[] { 8, 10 };
            for (int i = 0; i < TolshinaEdit.Length; i++)
            {
                if (items.Exists(p => p.Tolshina >= TolshinaEdit[i][0] && p.Tolshina < TolshinaEdit[i][1] && p.Tolshina != 0))
                {
                    Tolshina.Add(TolshinaEdit[i]);
                }
            }
            if (items.Exists(p => p.Tolshina == 0))
            {
                Tolshina.Add(new int[] { 0, 0 });
            }
            List<int[]> newTolshina = new List<int[]>(Tolshina.Distinct());
            ViewBag.Tolshina = newTolshina;
            //------------------------------------------------

            ViewBag.Count = Item.Count();
            int page = 1;
            int pageSize = 24; // количество объектов на страницу
            IEnumerable<Perchatki> GunsPerPages = Item.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = Item.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Perchatkis = Item
            };

            ViewBag.IVM = ivm;

            GetFilter().Item = Item;

            return View(ivm);
        }

        [HttpPost]
        public ActionResult Count()
        {
            List<Perchatki> Item = GetFilter().Item;

            return Json(Item.Count());
        }
    }
    public class FilterPerchatki
    {
        public List<Perchatki> Item { get; set; }
        public int CheckedBrand { get; set; }
        public int CheckedCountry { get; set; }
        public int CheckedPokritie { get; set; }
        public int CheckedMangeti { get; set; }
        public int CheckedObturaciya { get; set; }
        public int CheckedTolshina { get; set; }
        public int CheckedCount { get; set; }
        public int CheckedMinPrice { get; set; }
        public int CheckedMaxPrice { get; set; }

    }
}