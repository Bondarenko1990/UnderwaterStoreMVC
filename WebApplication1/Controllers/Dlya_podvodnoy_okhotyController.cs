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
    public class Dlya_podvodnoy_okhotyController : Controller
    {
        UnitOfWork unitOfWork;

        public Dlya_podvodnoy_okhotyController()
        {
            unitOfWork = new UnitOfWork();
        }

        public FilterDlya_podvodnoy_okhoty GetFilter()

        {
            FilterDlya_podvodnoy_okhoty filter = (FilterDlya_podvodnoy_okhoty)Session["FilterDlya_podvodnoy_okhoty"];
            if (filter == null)
            {
                filter = new FilterDlya_podvodnoy_okhoty();
                Session["FilterDlya_podvodnoy_okhoty"] = filter;
            }
            return filter;
        }

        public FileContentResult GetImage(int Id)
        {
            Dlya_podvodnoy_okhoty item = unitOfWork.Dlya_podvodnoy_okhotys.GetAll()
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
            return View("Edit", new Dlya_podvodnoy_okhoty());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int Id)
        {
            Dlya_podvodnoy_okhoty deletedItem = unitOfWork.Dlya_podvodnoy_okhotys.Delete(Id);
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
            return View(unitOfWork.Dlya_podvodnoy_okhotys.GetAll().ToList());
        }

        [Authorize(Roles = "Admin")]
        public ViewResult Edit(int Id)
        {
            Dlya_podvodnoy_okhoty item = unitOfWork.Dlya_podvodnoy_okhotys.GetAll().FirstOrDefault(g => g.Id == Id);
            SelectList brands = new SelectList(unitOfWork.Brands.GetAll(), "Id", "Name");
            ViewBag.Brands = brands;
            return View(item);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Dlya_podvodnoy_okhoty item, HttpPostedFileBase image = null)
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
                    item.ImageMimeType = unitOfWork.Dlya_podvodnoy_okhotys.Get(item.Id).ImageMimeType;
                    item.ImageData = unitOfWork.Dlya_podvodnoy_okhotys.Get(item.Id).ImageData;
                }
                unitOfWork.Dlya_podvodnoy_okhotys.SaveItem(item);
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
            Dlya_podvodnoy_okhoty c = unitOfWork.Dlya_podvodnoy_okhotys.GetAll().FirstOrDefault(com => com.Id == id);
            if (c != null)
                return PartialView(c);
            return HttpNotFound();
        }

        public ActionResult Details(int id)
        {
            Dlya_podvodnoy_okhoty c = unitOfWork.Dlya_podvodnoy_okhotys.GetAll().FirstOrDefault(com => com.Id == id);
            if (c != null)
                return PartialView(c);
            return HttpNotFound();
        }

        [HttpPost]
        public PartialViewResult Models(int page = 1)
        {

            int pageSize = 24;

            IEnumerable<Dlya_podvodnoy_okhoty> GunsPerPages = GetFilter().Item.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = GetFilter().Item.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Dlya_podvodnoy_okhotys = GunsPerPages
            };
            ViewBag.IVM = ivm;
            return PartialView(ivm);
        }

        public ActionResult List(int page = 1)
        {

            int count = unitOfWork.Dlya_podvodnoy_okhotys.GetAll().Count();
            var items = unitOfWork.Dlya_podvodnoy_okhotys.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();
            List<Dlya_podvodnoy_okhoty> Item;
            if (GetFilter().Item == null)
            {
                Item = unitOfWork.Dlya_podvodnoy_okhotys.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();
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
            var ZashitaKoleni = new List<bool>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                ZashitaKoleni.Add(items[i].ZashitaKoleni);
            }
            List<bool> newZashitaKoleni = new List<bool>(ZashitaKoleni.Distinct());
            newZashitaKoleni.Sort();
            newZashitaKoleni.Reverse();
            ViewBag.ZashitaKoleni = newZashitaKoleni;
            //------------------------------------------------
            var ZashitaLokti = new List<bool>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                ZashitaLokti.Add(items[i].ZashitaLokti);
            }
            List<bool> newZashitaLokti = new List<bool>(ZashitaLokti.Distinct());
            newZashitaLokti.Sort();
            newZashitaLokti.Reverse();
            ViewBag.ZashitaLokti = newZashitaLokti;
            //------------------------------------------------
            var PokritieVneshnee = new List<string>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                PokritieVneshnee.Add(items[i].PokritieVneshnee);
            }
            List<string> newPokritieVneshnee = new List<string>(PokritieVneshnee.Distinct());
            newPokritieVneshnee.Sort();
            ViewBag.PokritieVneshnee = newPokritieVneshnee;
            //------------------------------------------------
            var PokritieVnutrenee = new List<string>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                PokritieVnutrenee.Add(items[i].PokritieVnutrenee);
            }
            List<string> newPokritieVnutrenee = new List<string>(PokritieVnutrenee.Distinct());
            newPokritieVnutrenee.Sort();
            ViewBag.PokritieVnutrenee = newPokritieVnutrenee;
            //------------------------------------------------
            var Tolshina = new List<decimal>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                Tolshina.Add(items[i].Tolshina);
            }
            List<decimal> newTolshina = new List<decimal>(Tolshina.Distinct());
            newTolshina.Sort();
            ViewBag.Tolshina = newTolshina;
            //------------------------------------------------
            ViewBag.MinPrice = unitOfWork.Dlya_podvodnoy_okhotys.GetAll().Min(a => a.Price);
            ViewBag.MaxPrice = unitOfWork.Dlya_podvodnoy_okhotys.GetAll().Max(a => a.Price);
            ViewBag.CheckedMinPrice = GetFilter().CheckedMinPrice;
            if (GetFilter().CheckedMaxPrice == 0)
            {
                ViewBag.CheckedMaxPrice = unitOfWork.Dlya_podvodnoy_okhotys.GetAll().Max(a => a.Price);
            }
            else
            {
                ViewBag.CheckedMaxPrice = GetFilter().CheckedMaxPrice;
            }
            ViewBag.CheckedBrand = GetFilter().CheckedBrand;
            ViewBag.CheckedCountry = GetFilter().CheckedCountry;
            ViewBag.CheckedZashitaKoleni = GetFilter().CheckedZashitaKoleni;
            ViewBag.CheckedZashitaLokti = GetFilter().CheckedZashitaLokti;
            ViewBag.CheckedPokritieVneshnee = GetFilter().CheckedPokritieVneshnee;
            ViewBag.CheckedPokritieVnutrenee = GetFilter().CheckedPokritieVnutrenee;
            ViewBag.CheckedTolshina = GetFilter().CheckedTolshina;
            ViewBag.CheckedCount = GetFilter().CheckedCount;
            int pageSize = 24; // количество объектов на страницу
            IEnumerable<Dlya_podvodnoy_okhoty> GunsPerPages = Item.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = Item.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Dlya_podvodnoy_okhotys = GunsPerPages
            };

            ViewBag.IVM = ivm;

            return View(ivm);
        }

        [HttpPost]
        public ActionResult Filter(int MinPrice, int MaxPrice, string[] brand, string[] country, bool[] zashitaKoleni, bool[] zashitaLokti, string[] pokritieVneshnee, string[] pokritieVnutrenee, decimal[] tolshina)
        {
            int count = unitOfWork.Dlya_podvodnoy_okhotys.GetAll().Count();
            List<Dlya_podvodnoy_okhoty> Item = unitOfWork.Dlya_podvodnoy_okhotys.GetAll().Where(p => p.Price >= MinPrice && p.Price <= MaxPrice).ToList();
            List<Dlya_podvodnoy_okhoty> newItem = new List<Dlya_podvodnoy_okhoty>();

            GetFilter().CheckedMinPrice = MinPrice;
            GetFilter().CheckedMaxPrice = MaxPrice;
            GetFilter().CheckedBrand = 0;
            GetFilter().CheckedCountry = 0;
            GetFilter().CheckedZashitaKoleni = 0;
            GetFilter().CheckedZashitaLokti = 0;
            GetFilter().CheckedPokritieVneshnee = 0;
            GetFilter().CheckedPokritieVnutrenee = 0;
            GetFilter().CheckedTolshina = 0;
            GetFilter().CheckedCount = 0;


            ViewBag.MinPrice = unitOfWork.Dlya_podvodnoy_okhotys.GetAll().Min(a => a.Price);
            ViewBag.MaxPrice = unitOfWork.Dlya_podvodnoy_okhotys.GetAll().Max(a => a.Price);
            ViewBag.CheckedMinPrice = MinPrice;
            ViewBag.CheckedMaxPrice = MaxPrice;
            ViewBag.CheckedBrand = 0;
            ViewBag.CheckedCountry = 0;
            ViewBag.CheckedZashitaKoleni = 0;
            ViewBag.CheckedZashitaLokti = 0;
            ViewBag.CheckedPokritieVneshnee = 0;
            ViewBag.CheckedPokritieVnutrenee = 0;
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
                newItem = new List<Dlya_podvodnoy_okhoty>();
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

            if (zashitaKoleni != null)
            {
                newItem = new List<Dlya_podvodnoy_okhoty>();
                bool a;
                for (int i = 0; i < zashitaKoleni.Count(); i++)
                {

                    a = zashitaKoleni[i];
                    foreach (var c in Item.Where(p => p.ZashitaKoleni == a))
                    {
                        newItem.Add(c);
                    }
                }
                Item = newItem;
                GetFilter().CheckedZashitaKoleni = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedZashitaKoleni = 1;
                Count++;
            }

            if (zashitaLokti != null)
            {
                newItem = new List<Dlya_podvodnoy_okhoty>();
                bool a;
                for (int i = 0; i < zashitaLokti.Count(); i++)
                {

                    a = zashitaLokti[i];
                    foreach (var c in Item.Where(p => p.ZashitaLokti == a))
                    {
                        newItem.Add(c);
                    }
                }
                Item = newItem;
                GetFilter().CheckedZashitaLokti = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedZashitaLokti = 1;
                Count++;
            }


            if (pokritieVneshnee != null)
            {
                newItem = new List<Dlya_podvodnoy_okhoty>();
                string a = "";
                for (int i = 0; i < pokritieVneshnee.Count(); i++)
                {

                    a = pokritieVneshnee[i];
                    foreach (var c in Item.Where(p => p.PokritieVneshnee == a))
                    {
                        newItem.Add(c);
                    }
                }
                Item = newItem;
                GetFilter().CheckedPokritieVneshnee = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedPokritieVneshnee = 1;
                Count++;
            }
            if (pokritieVneshnee != null)
            {
                newItem = new List<Dlya_podvodnoy_okhoty>();
                string a = "";
                for (int i = 0; i < pokritieVneshnee.Count(); i++)
                {

                    a = pokritieVneshnee[i];
                    foreach (var c in Item.Where(p => p.PokritieVneshnee == a))
                    {
                        newItem.Add(c);
                    }
                }
                Item = newItem;
                GetFilter().CheckedPokritieVneshnee = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedPokritieVneshnee = 1;
                Count++;
            }
            if (pokritieVnutrenee != null)
            {
                newItem = new List<Dlya_podvodnoy_okhoty>();
                string a = "";
                for (int i = 0; i < pokritieVnutrenee.Count(); i++)
                {

                    a = pokritieVnutrenee[i];
                    foreach (var c in Item.Where(p => p.PokritieVnutrenee == a))
                    {
                        newItem.Add(c);
                    }
                }
                Item = newItem;
                GetFilter().CheckedPokritieVnutrenee = 1;
                GetFilter().CheckedCount++;
                ViewBag.CheckedPokritieVnutrenee = 1;
                Count++;
            }

            if (tolshina != null)
            {
                newItem = new List<Dlya_podvodnoy_okhoty>();
                decimal a = 0;
                for (int i = 0; i < tolshina.Count(); i++)
                {

                    a = tolshina[i];
                    foreach (var c in Item.Where(p => p.Tolshina == a))
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

            var items = unitOfWork.Dlya_podvodnoy_okhotys.GetAll().OrderBy(t => Guid.NewGuid()).Take(count).ToList();

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
            var ZashitaKoleni = new List<bool>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                ZashitaKoleni.Add(items[i].ZashitaKoleni);
            }
            List<bool> newZashitaKoleni = new List<bool>(ZashitaKoleni.Distinct());
            newZashitaKoleni.Sort();
            newZashitaKoleni.Reverse();
            ViewBag.ZashitaKoleni = newZashitaKoleni;
            //------------------------------------------------
            var ZashitaLokti = new List<bool>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                ZashitaLokti.Add(items[i].ZashitaLokti);
            }
            List<bool> newZashitaLokti = new List<bool>(ZashitaLokti.Distinct());
            newZashitaLokti.Sort();
            newZashitaLokti.Reverse();
            ViewBag.ZashitaLokti = newZashitaLokti;
            //------------------------------------------------
            var PokritieVneshnee = new List<string>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                PokritieVneshnee.Add(items[i].PokritieVneshnee);
            }
            List<string> newPokritieVneshnee = new List<string>(PokritieVneshnee.Distinct());
            newPokritieVneshnee.Sort();
            ViewBag.PokritieVneshnee = newPokritieVneshnee;
            //------------------------------------------------
            var PokritieVnutrenee = new List<string>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                PokritieVnutrenee.Add(items[i].PokritieVnutrenee);
            }
            List<string> newPokritieVnutrenee = new List<string>(PokritieVnutrenee.Distinct());
            newPokritieVnutrenee.Sort();
            ViewBag.PokritieVnutrenee = newPokritieVnutrenee;
            //------------------------------------------------
            var Tolshina = new List<decimal>();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                Tolshina.Add(items[i].Tolshina);
            }
            List<decimal> newTolshina = new List<decimal>(Tolshina.Distinct());
            newTolshina.Sort();
            ViewBag.Tolshina = newTolshina;
            //------------------------------------------------

            ViewBag.Count = Item.Count();
            int page = 1;
            int pageSize = 24; // количество объектов на страницу
            IEnumerable<Dlya_podvodnoy_okhoty> GunsPerPages = Item.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = Item.Count
            };
            IndexView ivm = new IndexView
            {
                PageInfo = pageInfo,
                Dlya_podvodnoy_okhotys = Item
            };

            ViewBag.IVM = ivm;

            GetFilter().Item = Item;

            return View(ivm);
        }

        [HttpPost]
        public ActionResult Count()
        {
            List<Dlya_podvodnoy_okhoty> Item = GetFilter().Item;

            return Json(Item.Count());
        }

    }
    public class FilterDlya_podvodnoy_okhoty
    {
        public List<Dlya_podvodnoy_okhoty> Item { get; set; }
        public int CheckedBrand { get; set; }
        public int CheckedCountry { get; set; }
        public int CheckedZashitaKoleni { get; set; }
        public int CheckedZashitaLokti { get; set; }
        public int CheckedPokritieVneshnee { get; set; }
        public int CheckedPokritieVnutrenee { get; set; }
        public int CheckedTolshina { get; set; }
        public int CheckedCount { get; set; }
        public int CheckedMinPrice { get; set; }
        public int CheckedMaxPrice { get; set; }
    }
}