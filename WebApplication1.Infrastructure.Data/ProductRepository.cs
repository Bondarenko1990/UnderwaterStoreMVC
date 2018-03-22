using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Domain.Core;
using WebApplication1.Domain.Interfaces;
using System.Data.Entity;
using System.Web;

namespace WebApplication1.Infrastructure.Data
{

    public class ProductRepository : IProductRepository<Product>
    {
        private OrderContext db;

        public ProductRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return db.Products;
        }

        public Product Get(int id)
        {
            return db.Products.Find(id);
        }
        public void SaveItem(Product item)
        {
            if (item.Id == 0)
                db.Products.Add(item);
            else
            {
                Product dbEntry = db.Products.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Category = item.Category;
                    dbEntry.Price = item.Price;
                    dbEntry.Description = item.Description;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                }
            }
            db.SaveChanges();
        }

        public Product Delete(int Id)
        {
            Product dbEntry = db.Products.Find(Id);
            if (dbEntry != null)
            {
                db.Products.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Product item)
        {
            db.Products.Add(item);
        }

        public void Update(Product item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }
    public class PneumaticRepository : IProductRepository<Pneumatic>
    {
        private OrderContext db;

        public PneumaticRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Pneumatic> GetAll()
        {
            return db.Pneumatics.Include(o => o.Brand);
        }

        public Pneumatic Get(int id)
        {
            return db.Pneumatics.Find(id);
        }
        public void SaveItem(Pneumatic item)
        {
            if (item.Id == 0)
                db.Pneumatics.Add(item);
            else
            {
                Pneumatic dbEntry = db.Pneumatics.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Garpun = item.Garpun;
                    dbEntry.RegPower = item.RegPower;
                    dbEntry.Length = item.Length;
                    dbEntry.Price = item.Price;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                }
            }
            db.SaveChanges();
        }

        public Pneumatic Delete(int Id)
        {
            Pneumatic dbEntry = db.Pneumatics.Find(Id);
            if (dbEntry != null)
            {
                db.Pneumatics.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Pneumatic item)
        {
            db.Pneumatics.Add(item);
        }

        public void Update(Pneumatic item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }

    public class MaskiRepository : IProductRepository<Maski>
    {
        private OrderContext db;

        public MaskiRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Maski> GetAll()
        {
            return db.Maskis.Include(o => o.Brand);
        }

        public Maski Get(int id)
        {
            return db.Maskis.Find(id);
        }
        public void SaveItem(Maski item)
        {
            if (item.Id == 0)
                db.Maskis.Add(item);
            else
            {
                Maski dbEntry = db.Maskis.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Price = item.Price;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                    dbEntry.Primenenie = item.Primenenie;
                    dbEntry.Glass = item.Glass;

                }
            }
            db.SaveChanges();
        }

        public Maski Delete(int Id)
        {
            Maski dbEntry = db.Maskis.Find(Id);
            if (dbEntry != null)
            {
                db.Maskis.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Maski item)
        {
            db.Maskis.Add(item);
        }

        public void Update(Maski item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }

    public class Lasty_dlya_okhotyRepository : IProductRepository<Lasty_dlya_okhoty>
    {
        private OrderContext db;

        public Lasty_dlya_okhotyRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Lasty_dlya_okhoty> GetAll()
        {
            return db.Lasty_dlya_okhotys.Include(o => o.Brand);
        }

        public Lasty_dlya_okhoty Get(int id)
        {
            return db.Lasty_dlya_okhotys.Find(id);
        }
        public void SaveItem(Lasty_dlya_okhoty item)
        {
            if (item.Id == 0)
                db.Lasty_dlya_okhotys.Add(item);
            else
            {
                Lasty_dlya_okhoty dbEntry = db.Lasty_dlya_okhotys.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Price = item.Price;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                    dbEntry.Pyatka = item.Pyatka;
                    dbEntry.Lopast = item.Lopast;
                    dbEntry.Material = item.Material;
                    dbEntry.Length = item.Length;
                }
            }
            db.SaveChanges();
        }

        public Lasty_dlya_okhoty Delete(int Id)
        {
            Lasty_dlya_okhoty dbEntry = db.Lasty_dlya_okhotys.Find(Id);
            if (dbEntry != null)
            {
                db.Lasty_dlya_okhotys.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Lasty_dlya_okhoty item)
        {
            db.Lasty_dlya_okhotys.Add(item);
        }

        public void Update(Lasty_dlya_okhoty item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }

    public class Lasty_dlya_dayvingaRepository : IProductRepository<Lasty_dlya_dayvinga>
    {
        private OrderContext db;

        public Lasty_dlya_dayvingaRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Lasty_dlya_dayvinga> GetAll()
        {
            return db.Lasty_dlya_dayvingas.Include(o => o.Brand);
        }

        public Lasty_dlya_dayvinga Get(int id)
        {
            return db.Lasty_dlya_dayvingas.Find(id);
        }
        public void SaveItem(Lasty_dlya_dayvinga item)
        {
            if (item.Id == 0)
                db.Lasty_dlya_dayvingas.Add(item);
            else
            {
                Lasty_dlya_dayvinga dbEntry = db.Lasty_dlya_dayvingas.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Price = item.Price;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                    dbEntry.Pyatka = item.Pyatka;
                    dbEntry.Lopast = item.Lopast;
                    dbEntry.Length = item.Length;
                }
            }
            db.SaveChanges();
        }

        public Lasty_dlya_dayvinga Delete(int Id)
        {
            Lasty_dlya_dayvinga dbEntry = db.Lasty_dlya_dayvingas.Find(Id);
            if (dbEntry != null)
            {
                db.Lasty_dlya_dayvingas.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Lasty_dlya_dayvinga item)
        {
            db.Lasty_dlya_dayvingas.Add(item);
        }

        public void Update(Lasty_dlya_dayvinga item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }

    public class Aksessuary_k_maskamRepository : IProductRepository<Aksessuary_k_maskam>
    {
        private OrderContext db;

        public Aksessuary_k_maskamRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Aksessuary_k_maskam> GetAll()
        {
            return db.Aksessuary_k_maskams.Include(o => o.Brand);
        }

        public Aksessuary_k_maskam Get(int id)
        {
            return db.Aksessuary_k_maskams.Find(id);
        }
        public void SaveItem(Aksessuary_k_maskam item)
        {
            if (item.Id == 0)
                db.Aksessuary_k_maskams.Add(item);
            else
            {
                Aksessuary_k_maskam dbEntry = db.Aksessuary_k_maskams.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Price = item.Price;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                }
            }
            db.SaveChanges();
        }

        public Aksessuary_k_maskam Delete(int Id)
        {
            Aksessuary_k_maskam dbEntry = db.Aksessuary_k_maskams.Find(Id);
            if (dbEntry != null)
            {
                db.Aksessuary_k_maskams.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Aksessuary_k_maskam item)
        {
            db.Aksessuary_k_maskams.Add(item);
        }

        public void Update(Aksessuary_k_maskam item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }

    public class Aksessuary_k_lastamRepository : IProductRepository<Aksessuary_k_lastam>
    {
        private OrderContext db;

        public Aksessuary_k_lastamRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Aksessuary_k_lastam> GetAll()
        {
            return db.Aksessuary_k_lastams.Include(o => o.Brand);
        }

        public Aksessuary_k_lastam Get(int id)
        {
            return db.Aksessuary_k_lastams.Find(id);
        }
        public void SaveItem(Aksessuary_k_lastam item)
        {
            if (item.Id == 0)
                db.Aksessuary_k_lastams.Add(item);
            else
            {
                Aksessuary_k_lastam dbEntry = db.Aksessuary_k_lastams.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Price = item.Price;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                }
            }
            db.SaveChanges();
        }

        public Aksessuary_k_lastam Delete(int Id)
        {
            Aksessuary_k_lastam dbEntry = db.Aksessuary_k_lastams.Find(Id);
            if (dbEntry != null)
            {
                db.Aksessuary_k_lastams.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Aksessuary_k_lastam item)
        {
            db.Aksessuary_k_lastams.Add(item);
        }

        public void Update(Aksessuary_k_lastam item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }

    public class Aksessuary_k_trubkamRepository : IProductRepository<Aksessuary_k_trubkam>
    {
        private OrderContext db;

        public Aksessuary_k_trubkamRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Aksessuary_k_trubkam> GetAll()
        {
            return db.Aksessuary_k_trubkams.Include(o => o.Brand);
        }

        public Aksessuary_k_trubkam Get(int id)
        {
            return db.Aksessuary_k_trubkams.Find(id);
        }
        public void SaveItem(Aksessuary_k_trubkam item)
        {
            if (item.Id == 0)
                db.Aksessuary_k_trubkams.Add(item);
            else
            {
                Aksessuary_k_trubkam dbEntry = db.Aksessuary_k_trubkams.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Price = item.Price;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                }
            }
            db.SaveChanges();
        }

        public Aksessuary_k_trubkam Delete(int Id)
        {
            Aksessuary_k_trubkam dbEntry = db.Aksessuary_k_trubkams.Find(Id);
            if (dbEntry != null)
            {
                db.Aksessuary_k_trubkams.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Aksessuary_k_trubkam item)
        {
            db.Aksessuary_k_trubkams.Add(item);
        }

        public void Update(Aksessuary_k_trubkam item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }

    public class FonariRepository : IProductRepository<Fonari>
    {
        private OrderContext db;

        public FonariRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Fonari> GetAll()
        {
            return db.Fonaris.Include(o => o.Brand);
        }

        public Fonari Get(int id)
        {
            return db.Fonaris.Find(id);
        }
        public void SaveItem(Fonari item)
        {
            if (item.Id == 0)
                db.Fonaris.Add(item);
            else
            {
                Fonari dbEntry = db.Fonaris.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Price = item.Price;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                    dbEntry.TypeLamp = item.TypeLamp;
                    dbEntry.TypeBattery = item.TypeBattery;
                    dbEntry.Svet = item.Svet;
                    dbEntry.TimeWork = item.TimeWork;
                    dbEntry.QuantityBattery = item.QuantityBattery;
                    dbEntry.MaxGlubina = item.MaxGlubina;
                }
            }
            db.SaveChanges();
        }

        public Fonari Delete(int Id)
        {
            Fonari dbEntry = db.Fonaris.Find(Id);
            if (dbEntry != null)
            {
                db.Fonaris.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Fonari item)
        {
            db.Fonaris.Add(item);
        }

        public void Update(Fonari item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }

    public class PerchatkiRepository : IProductRepository<Perchatki>
    {
        private OrderContext db;

        public PerchatkiRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Perchatki> GetAll()
        {
            return db.Perchatkis.Include(o => o.Brand);
        }

        public Perchatki Get(int id)
        {
            return db.Perchatkis.Find(id);
        }
        public void SaveItem(Perchatki item)
        {
            if (item.Id == 0)
                db.Perchatkis.Add(item);
            else
            {
                Perchatki dbEntry = db.Perchatkis.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Price = item.Price;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                    dbEntry.Pokritie = item.Pokritie;
                    dbEntry.Mangeti = item.Mangeti;
                    dbEntry.Obturaciya = item.Obturaciya;
                    dbEntry.Tolshina = item.Tolshina;
                }
            }
            db.SaveChanges();
        }

        public Perchatki Delete(int Id)
        {
            Perchatki dbEntry = db.Perchatkis.Find(Id);
            if (dbEntry != null)
            {
                db.Perchatkis.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Perchatki item)
        {
            db.Perchatkis.Add(item);
        }

        public void Update(Perchatki item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }

    public class RukavitsyRepository : IProductRepository<Rukavitsy>
    {
        private OrderContext db;

        public RukavitsyRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Rukavitsy> GetAll()
        {
            return db.Rukavitsys.Include(o => o.Brand);
        }

        public Rukavitsy Get(int id)
        {
            return db.Rukavitsys.Find(id);
        }
        public void SaveItem(Rukavitsy item)
        {
            if (item.Id == 0)
                db.Rukavitsys.Add(item);
            else
            {
                Rukavitsy dbEntry = db.Rukavitsys.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Price = item.Price;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                    dbEntry.Pokritie = item.Pokritie;
                    dbEntry.Mangeti = item.Mangeti;
                    dbEntry.Obturaciya = item.Obturaciya;
                    dbEntry.Tolshina = item.Tolshina;
                }
            }
            db.SaveChanges();
        }

        public Rukavitsy Delete(int Id)
        {
            Rukavitsy dbEntry = db.Rukavitsys.Find(Id);
            if (dbEntry != null)
            {
                db.Rukavitsys.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Rukavitsy item)
        {
            db.Rukavitsys.Add(item);
        }

        public void Update(Rukavitsy item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }

    public class TapochkiRepository : IProductRepository<Tapochki>
    {
        private OrderContext db;

        public TapochkiRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Tapochki> GetAll()
        {
            return db.Tapochkis.Include(o => o.Brand);
        }

        public Tapochki Get(int id)
        {
            return db.Tapochkis.Find(id);
        }
        public void SaveItem(Tapochki item)
        {
            if (item.Id == 0)
                db.Tapochkis.Add(item);
            else
            {
                Tapochki dbEntry = db.Tapochkis.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Price = item.Price;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                    dbEntry.Podoshva = item.Podoshva;
                    dbEntry.Material = item.Material;
                    dbEntry.Age = item.Age;
                }
            }
            db.SaveChanges();
        }

        public Tapochki Delete(int Id)
        {
            Tapochki dbEntry = db.Tapochkis.Find(Id);
            if (dbEntry != null)
            {
                db.Tapochkis.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Tapochki item)
        {
            db.Tapochkis.Add(item);
        }

        public void Update(Tapochki item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }

    public class KukanyRepository : IProductRepository<Kukany>
    {
        private OrderContext db;

        public KukanyRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Kukany> GetAll()
        {
            return db.Kukanys.Include(o => o.Brand);
        }

        public Kukany Get(int id)
        {
            return db.Kukanys.Find(id);
        }
        public void SaveItem(Kukany item)
        {
            if (item.Id == 0)
                db.Kukanys.Add(item);
            else
            {
                Kukany dbEntry = db.Kukanys.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Price = item.Price;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                }
            }
            db.SaveChanges();
        }

        public Kukany Delete(int Id)
        {
            Kukany dbEntry = db.Kukanys.Find(Id);
            if (dbEntry != null)
            {
                db.Kukanys.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Kukany item)
        {
            db.Kukanys.Add(item);
        }

        public void Update(Kukany item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }

    public class Bui_i_aksessuaryRepository : IProductRepository<Bui_i_aksessuary>
    {
        private OrderContext db;

        public Bui_i_aksessuaryRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Bui_i_aksessuary> GetAll()
        {
            return db.Bui_i_aksessuarys.Include(o => o.Brand);
        }

        public Bui_i_aksessuary Get(int id)
        {
            return db.Bui_i_aksessuarys.Find(id);
        }
        public void SaveItem(Bui_i_aksessuary item)
        {
            if (item.Id == 0)
                db.Bui_i_aksessuarys.Add(item);
            else
            {
                Bui_i_aksessuary dbEntry = db.Bui_i_aksessuarys.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Price = item.Price;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                }
            }
            db.SaveChanges();
        }

        public Bui_i_aksessuary Delete(int Id)
        {
            Bui_i_aksessuary dbEntry = db.Bui_i_aksessuarys.Find(Id);
            if (dbEntry != null)
            {
                db.Bui_i_aksessuarys.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Bui_i_aksessuary item)
        {
            db.Bui_i_aksessuarys.Add(item);
        }

        public void Update(Bui_i_aksessuary item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }

    public class Khimicheskie_sredstva_i_maslaRepository : IProductRepository<Khimicheskie_sredstva_i_masla>
    {
        private OrderContext db;

        public Khimicheskie_sredstva_i_maslaRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Khimicheskie_sredstva_i_masla> GetAll()
        {
            return db.Khimicheskie_sredstva_i_maslas.Include(o => o.Brand);
        }

        public Khimicheskie_sredstva_i_masla Get(int id)
        {
            return db.Khimicheskie_sredstva_i_maslas.Find(id);
        }
        public void SaveItem(Khimicheskie_sredstva_i_masla item)
        {
            if (item.Id == 0)
                db.Khimicheskie_sredstva_i_maslas.Add(item);
            else
            {
                Khimicheskie_sredstva_i_masla dbEntry = db.Khimicheskie_sredstva_i_maslas.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Price = item.Price;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                }
            }
            db.SaveChanges();
        }

        public Khimicheskie_sredstva_i_masla Delete(int Id)
        {
            Khimicheskie_sredstva_i_masla dbEntry = db.Khimicheskie_sredstva_i_maslas.Find(Id);
            if (dbEntry != null)
            {
                db.Khimicheskie_sredstva_i_maslas.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Khimicheskie_sredstva_i_masla item)
        {
            db.Khimicheskie_sredstva_i_maslas.Add(item);
        }

        public void Update(Khimicheskie_sredstva_i_masla item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }

    public class Katushki_khodovyeRepository : IProductRepository<Katushki_khodovye>
    {
        private OrderContext db;

        public Katushki_khodovyeRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Katushki_khodovye> GetAll()
        {
            return db.Katushki_khodovyes.Include(o => o.Brand);
        }

        public Katushki_khodovye Get(int id)
        {
            return db.Katushki_khodovyes.Find(id);
        }
        public void SaveItem(Katushki_khodovye item)
        {
            if (item.Id == 0)
                db.Katushki_khodovyes.Add(item);
            else
            {
                Katushki_khodovye dbEntry = db.Katushki_khodovyes.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Price = item.Price;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                }
            }
            db.SaveChanges();
        }

        public Katushki_khodovye Delete(int Id)
        {
            Katushki_khodovye dbEntry = db.Katushki_khodovyes.Find(Id);
            if (dbEntry != null)
            {
                db.Katushki_khodovyes.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Katushki_khodovye item)
        {
            db.Katushki_khodovyes.Add(item);
        }

        public void Update(Katushki_khodovye item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }

    public class Chekhly_setkiRepository : IProductRepository<Chekhly_setki>
    {
        private OrderContext db;

        public Chekhly_setkiRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Chekhly_setki> GetAll()
        {
            return db.Chekhly_setkis.Include(o => o.Brand);
        }

        public Chekhly_setki Get(int id)
        {
            return db.Chekhly_setkis.Find(id);
        }
        public void SaveItem(Chekhly_setki item)
        {
            if (item.Id == 0)
                db.Chekhly_setkis.Add(item);
            else
            {
                Chekhly_setki dbEntry = db.Chekhly_setkis.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Price = item.Price;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                }
            }
            db.SaveChanges();
        }

        public Chekhly_setki Delete(int Id)
        {
            Chekhly_setki dbEntry = db.Chekhly_setkis.Find(Id);
            if (dbEntry != null)
            {
                db.Chekhly_setkis.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Chekhly_setki item)
        {
            db.Chekhly_setkis.Add(item);
        }

        public void Update(Chekhly_setki item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }

    public class MulyazhiRepository : IProductRepository<Mulyazhi>
    {
        private OrderContext db;

        public MulyazhiRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Mulyazhi> GetAll()
        {
            return db.Mulyazhis.Include(o => o.Brand);
        }

        public Mulyazhi Get(int id)
        {
            return db.Mulyazhis.Find(id);
        }
        public void SaveItem(Mulyazhi item)
        {
            if (item.Id == 0)
                db.Mulyazhis.Add(item);
            else
            {
                Mulyazhi dbEntry = db.Mulyazhis.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Price = item.Price;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                }
            }
            db.SaveChanges();
        }

        public Mulyazhi Delete(int Id)
        {
            Mulyazhi dbEntry = db.Mulyazhis.Find(Id);
            if (dbEntry != null)
            {
                db.Mulyazhis.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Mulyazhi item)
        {
            db.Mulyazhis.Add(item);
        }

        public void Update(Mulyazhi item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }

    public class Poleznye_aksessuaryRepository : IProductRepository<Poleznye_aksessuary>
    {
        private OrderContext db;

        public Poleznye_aksessuaryRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Poleznye_aksessuary> GetAll()
        {
            return db.Poleznye_aksessuarys.Include(o => o.Brand);
        }

        public Poleznye_aksessuary Get(int id)
        {
            return db.Poleznye_aksessuarys.Find(id);
        }
        public void SaveItem(Poleznye_aksessuary item)
        {
            if (item.Id == 0)
                db.Poleznye_aksessuarys.Add(item);
            else
            {
                Poleznye_aksessuary dbEntry = db.Poleznye_aksessuarys.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Price = item.Price;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                }
            }
            db.SaveChanges();
        }

        public Poleznye_aksessuary Delete(int Id)
        {
            Poleznye_aksessuary dbEntry = db.Poleznye_aksessuarys.Find(Id);
            if (dbEntry != null)
            {
                db.Poleznye_aksessuarys.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Poleznye_aksessuary item)
        {
            db.Poleznye_aksessuarys.Add(item);
        }

        public void Update(Poleznye_aksessuary item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }
    public class BotyRepository : IProductRepository<Boty>
    {
        private OrderContext db;

        public BotyRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Boty> GetAll()
        {
            return db.Botys.Include(o => o.Brand);
        }

        public Boty Get(int id)
        {
            return db.Botys.Find(id);
        }
        public void SaveItem(Boty item)
        {
            if (item.Id == 0)
                db.Botys.Add(item);
            else
            {
                Boty dbEntry = db.Botys.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Price = item.Price;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                    dbEntry.Podoshva = item.Podoshva;
                    dbEntry.Mangeti = item.Mangeti;
                    dbEntry.Obturaciya = item.Obturaciya;
                    dbEntry.Tolshina = item.Tolshina;
                }
            }
            db.SaveChanges();
        }

        public Boty Delete(int Id)
        {
            Boty dbEntry = db.Botys.Find(Id);
            if (dbEntry != null)
            {
                db.Botys.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Boty item)
        {
            db.Botys.Add(item);
        }

        public void Update(Boty item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }

    public class NoskiRepository : IProductRepository<Noski>
    {
        private OrderContext db;

        public NoskiRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Noski> GetAll()
        {
            return db.Noskis.Include(o => o.Brand);
        }

        public Noski Get(int id)
        {
            return db.Noskis.Find(id);
        }
        public void SaveItem(Noski item)
        {
            if (item.Id == 0)
                db.Noskis.Add(item);
            else
            {
                Noski dbEntry = db.Noskis.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Price = item.Price;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                    dbEntry.Podoshva = item.Podoshva;
                    dbEntry.PokritieVneshnee = item.PokritieVneshnee;
                    dbEntry.PokritieVnutrenee = item.PokritieVnutrenee;
                    dbEntry.Mangeti = item.Mangeti;
                    dbEntry.Tolshina = item.Tolshina;
                }
            }
            db.SaveChanges();
        }

        public Noski Delete(int Id)
        {
            Noski dbEntry = db.Noskis.Find(Id);
            if (dbEntry != null)
            {
                db.Noskis.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Noski item)
        {
            db.Noskis.Add(item);
        }

        public void Update(Noski item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }

    public class Aksessuary_k_gruzamRepository : IProductRepository<Aksessuary_k_gruzam>
    {
        private OrderContext db;

        public Aksessuary_k_gruzamRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Aksessuary_k_gruzam> GetAll()
        {
            return db.Aksessuary_k_gruzams.Include(o => o.Brand);
        }

        public Aksessuary_k_gruzam Get(int id)
        {
            return db.Aksessuary_k_gruzams.Find(id);
        }
        public void SaveItem(Aksessuary_k_gruzam item)
        {
            if (item.Id == 0)
                db.Aksessuary_k_gruzams.Add(item);
            else
            {
                Aksessuary_k_gruzam dbEntry = db.Aksessuary_k_gruzams.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Price = item.Price;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                }
            }
            db.SaveChanges();
        }

        public Aksessuary_k_gruzam Delete(int Id)
        {
            Aksessuary_k_gruzam dbEntry = db.Aksessuary_k_gruzams.Find(Id);
            if (dbEntry != null)
            {
                db.Aksessuary_k_gruzams.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Aksessuary_k_gruzam item)
        {
            db.Aksessuary_k_gruzams.Add(item);
        }

        public void Update(Aksessuary_k_gruzam item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }

    public class Aksessuary_k_nozhamRepository : IProductRepository<Aksessuary_k_nozham>
    {
        private OrderContext db;

        public Aksessuary_k_nozhamRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Aksessuary_k_nozham> GetAll()
        {
            return db.Aksessuary_k_nozhams.Include(o => o.Brand);
        }

        public Aksessuary_k_nozham Get(int id)
        {
            return db.Aksessuary_k_nozhams.Find(id);
        }
        public void SaveItem(Aksessuary_k_nozham item)
        {
            if (item.Id == 0)
                db.Aksessuary_k_nozhams.Add(item);
            else
            {
                Aksessuary_k_nozham dbEntry = db.Aksessuary_k_nozhams.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Price = item.Price;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                }
            }
            db.SaveChanges();
        }

        public Aksessuary_k_nozham Delete(int Id)
        {
            Aksessuary_k_nozham dbEntry = db.Aksessuary_k_nozhams.Find(Id);
            if (dbEntry != null)
            {
                db.Aksessuary_k_nozhams.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Aksessuary_k_nozham item)
        {
            db.Aksessuary_k_nozhams.Add(item);
        }

        public void Update(Aksessuary_k_nozham item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }

    public class Aksessuary_k_fonaryamRepository : IProductRepository<Aksessuary_k_fonaryam>
    {
        private OrderContext db;

        public Aksessuary_k_fonaryamRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Aksessuary_k_fonaryam> GetAll()
        {
            return db.Aksessuary_k_fonaryams.Include(o => o.Brand);
        }

        public Aksessuary_k_fonaryam Get(int id)
        {
            return db.Aksessuary_k_fonaryams.Find(id);
        }
        public void SaveItem(Aksessuary_k_fonaryam item)
        {
            if (item.Id == 0)
                db.Aksessuary_k_fonaryams.Add(item);
            else
            {
                Aksessuary_k_fonaryam dbEntry = db.Aksessuary_k_fonaryams.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Price = item.Price;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                }
            }
            db.SaveChanges();
        }

        public Aksessuary_k_fonaryam Delete(int Id)
        {
            Aksessuary_k_fonaryam dbEntry = db.Aksessuary_k_fonaryams.Find(Id);
            if (dbEntry != null)
            {
                db.Aksessuary_k_fonaryams.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Aksessuary_k_fonaryam item)
        {
            db.Aksessuary_k_fonaryams.Add(item);
        }

        public void Update(Aksessuary_k_fonaryam item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }

    public class Gruza_i_gruzovye_sistemyRepository : IProductRepository<Gruza_i_gruzovye_sistemy>
    {
        private OrderContext db;

        public Gruza_i_gruzovye_sistemyRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Gruza_i_gruzovye_sistemy> GetAll()
        {
            return db.Gruza_i_gruzovye_sistemys.Include(o => o.Brand);
        }

        public Gruza_i_gruzovye_sistemy Get(int id)
        {
            return db.Gruza_i_gruzovye_sistemys.Find(id);
        }
        public void SaveItem(Gruza_i_gruzovye_sistemy item)
        {
            if (item.Id == 0)
                db.Gruza_i_gruzovye_sistemys.Add(item);
            else
            {
                Gruza_i_gruzovye_sistemy dbEntry = db.Gruza_i_gruzovye_sistemys.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Price = item.Price;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                    dbEntry.Type = item.Type;
                    dbEntry.Kg = item.Kg;
                }
            }
            db.SaveChanges();
        }

        public Gruza_i_gruzovye_sistemy Delete(int Id)
        {
            Gruza_i_gruzovye_sistemy dbEntry = db.Gruza_i_gruzovye_sistemys.Find(Id);
            if (dbEntry != null)
            {
                db.Gruza_i_gruzovye_sistemys.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Gruza_i_gruzovye_sistemy item)
        {
            db.Gruza_i_gruzovye_sistemys.Add(item);
        }

        public void Update(Gruza_i_gruzovye_sistemy item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }

    public class NozhiRepository : IProductRepository<Nozhi>
    {
        private OrderContext db;

        public NozhiRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Nozhi> GetAll()
        {
            return db.Nozhis.Include(o => o.Brand);
        }

        public Nozhi Get(int id)
        {
            return db.Nozhis.Find(id);
        }
        public void SaveItem(Nozhi item)
        {
            if (item.Id == 0)
                db.Nozhis.Add(item);
            else
            {
                Nozhi dbEntry = db.Nozhis.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Price = item.Price;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                    dbEntry.Stroporez = item.Stroporez;
                    dbEntry.ZubKrai = item.ZubKrai;
                    dbEntry.Material = item.Material;
                    dbEntry.Length = item.Length;
                }
            }
            db.SaveChanges();
        }

        public Nozhi Delete(int Id)
        {
            Nozhi dbEntry = db.Nozhis.Find(Id);
            if (dbEntry != null)
            {
                db.Nozhis.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Nozhi item)
        {
            db.Nozhis.Add(item);
        }

        public void Update(Nozhi item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }


    public class KomplektyRepository : IProductRepository<Komplekty>
    {
        private OrderContext db;

        public KomplektyRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Komplekty> GetAll()
        {
            return db.Komplektys.Include(o => o.Brand);
        }

        public Komplekty Get(int id)
        {
            return db.Komplektys.Find(id);
        }
        public void SaveItem(Komplekty item)
        {
            if (item.Id == 0)
                db.Komplektys.Add(item);
            else
            {
                Komplekty dbEntry = db.Komplektys.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Price = item.Price;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                }
            }
            db.SaveChanges();
        }

        public Komplekty Delete(int Id)
        {
            Komplekty dbEntry = db.Komplektys.Find(Id);
            if (dbEntry != null)
            {
                db.Komplektys.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Komplekty item)
        {
            db.Komplektys.Add(item);
        }

        public void Update(Komplekty item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }
    public class TrubkiRepository : IProductRepository<Trubki>
    {
        private OrderContext db;

        public TrubkiRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Trubki> GetAll()
        {
            return db.Trubkis.Include(o => o.Brand);
        }

        public Trubki Get(int id)
        {
            return db.Trubkis.Find(id);
        }
        public void SaveItem(Trubki item)
        {
            if (item.Id == 0)
                db.Trubkis.Add(item);
            else
            {
                Trubki dbEntry = db.Trubkis.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Price = item.Price;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                    dbEntry.Klapan = item.Klapan;
                    dbEntry.Material = item.Material;
                    dbEntry.GofrVstavka = item.GofrVstavka;
                    dbEntry.Freetop = item.Freetop;
                    dbEntry.Primenenie = item.Primenenie;
                }
            }
            db.SaveChanges();
        }

        public Trubki Delete(int Id)
        {
            Trubki dbEntry = db.Trubkis.Find(Id);
            if (dbEntry != null)
            {
                db.Trubkis.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Trubki item)
        {
            db.Trubkis.Add(item);
        }

        public void Update(Trubki item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }

    public class ArbaletRepository : IProductRepository<Arbalet>
    {
        private OrderContext db;

        public ArbaletRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Arbalet> GetAll()
        {
            return db.Arbalets.Include(o => o.Brand); 
        }

        public Arbalet Get(int id)
        {
            return db.Arbalets.Find(id);
        }
        public void SaveItem(Arbalet item)
        {
            if (item.Id == 0)
                db.Arbalets.Add(item);
            else
            {
                Arbalet dbEntry = db.Arbalets.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.DiametrGarpun = item.DiametrGarpun;
                    dbEntry.DiametrTyagi = item.DiametrTyagi;
                    dbEntry.SumTyagi = item.SumTyagi;
                    dbEntry.DopTyagi = item.DopTyagi;
                    dbEntry.Length = item.Length;
                    dbEntry.Price = item.Price;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                }
            }
            db.SaveChanges();
        }

        public Arbalet Delete(int Id)
        {
            Arbalet dbEntry = db.Arbalets.Find(Id);
            if (dbEntry != null)
            {
                db.Arbalets.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Arbalet item)
        {
            db.Arbalets.Add(item);
        }

        public void Update(Arbalet item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }

    public class GarpunRepository : IProductRepository<Garpun>
    {
        private OrderContext db;

        public GarpunRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Garpun> GetAll()
        {
            return db.Garpuns.Include(o => o.Brand);
        }

        public Garpun Get(int id)
        {
            return db.Garpuns.Find(id);
        }
        public void SaveItem(Garpun item)
        {
            if (item.Id == 0)
                db.Garpuns.Add(item);
            else
            {
                Garpun dbEntry = db.Garpuns.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.DiametrGarpun = item.DiametrGarpun;
                    dbEntry.DiametrRezbi = item.DiametrRezbi;
                    dbEntry.Type = item.Type;
                    dbEntry.TypeOfGun = item.TypeOfGun;
                    dbEntry.Length = item.Length;
                    dbEntry.Price = item.Price;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                }
            }
            db.SaveChanges();
        }

        public Garpun Delete(int Id)
        {
            Garpun dbEntry = db.Garpuns.Find(Id);
            if (dbEntry != null)
            {
                db.Garpuns.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Garpun item)
        {
            db.Garpuns.Add(item);
        }

        public void Update(Garpun item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }

    public class LinRepository : IProductRepository<Lin>
    {
        private OrderContext db;

        public LinRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Lin> GetAll()
        {
            return db.Lins.Include(o => o.Brand);
        }

        public Lin Get(int id)
        {
            return db.Lins.Find(id);
        }
        public void SaveItem(Lin item)
        {
            if (item.Id == 0)
                db.Lins.Add(item);
            else
            {
                Lin dbEntry = db.Lins.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Price = item.Price;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                }
            }
            db.SaveChanges();
        }

        public Lin Delete(int Id)
        {
            Lin dbEntry = db.Lins.Find(Id);
            if (dbEntry != null)
            {
                db.Lins.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Lin item)
        {
            db.Lins.Add(item);
        }

        public void Update(Lin item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }

    public class KatushkaRepository : IProductRepository<Katushka>
    {
        private OrderContext db;

        public KatushkaRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Katushka> GetAll()
        {
            return db.Katushkas.Include(o => o.Brand);
        }

        public Katushka Get(int id)
        {
            return db.Katushkas.Find(id);
        }
        public void SaveItem(Katushka item)
        {
            if (item.Id == 0)
                db.Katushkas.Add(item);
            else
            {
                Katushka dbEntry = db.Katushkas.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Price = item.Price;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                }
            }
            db.SaveChanges();
        }

        public Katushka Delete(int Id)
        {
            Katushka dbEntry = db.Katushkas.Find(Id);
            if (dbEntry != null)
            {
                db.Katushkas.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Katushka item)
        {
            db.Katushkas.Add(item);
        }

        public void Update(Katushka item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }

    public class Arbaletnaya_tyagaRepository : IProductRepository<Arbaletnaya_tyaga>
    {
        private OrderContext db;

        public Arbaletnaya_tyagaRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Arbaletnaya_tyaga> GetAll()
        {
            return db.Arbaletnaya_tyagas.Include(o => o.Brand);
        }

        public Arbaletnaya_tyaga Get(int id)
        {
            return db.Arbaletnaya_tyagas.Find(id);
        }
        public void SaveItem(Arbaletnaya_tyaga item)
        {
            if (item.Id == 0)
                db.Arbaletnaya_tyagas.Add(item);
            else
            {
                Arbaletnaya_tyaga dbEntry = db.Arbaletnaya_tyagas.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Diametr = item.Diametr;
                    dbEntry.Type = item.Type;
                    dbEntry.Price = item.Price;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                }
            }
            db.SaveChanges();
        }

        public Arbaletnaya_tyaga Delete(int Id)
        {
            Arbaletnaya_tyaga dbEntry = db.Arbaletnaya_tyagas.Find(Id);
            if (dbEntry != null)
            {
                db.Arbaletnaya_tyagas.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Arbaletnaya_tyaga item)
        {
            db.Arbaletnaya_tyagas.Add(item);
        }

        public void Update(Arbaletnaya_tyaga item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }

    public class Arbaletniy_zacepRepository : IProductRepository<Arbaletniy_zacep>
    {
        private OrderContext db;

        public Arbaletniy_zacepRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Arbaletniy_zacep> GetAll()
        {
            return db.Arbaletniy_zaceps.Include(o => o.Brand);
        }

        public Arbaletniy_zacep Get(int id)
        {
            return db.Arbaletniy_zaceps.Find(id);
        }
        public void SaveItem(Arbaletniy_zacep item)
        {
            if (item.Id == 0)
                db.Arbaletniy_zaceps.Add(item);
            else
            {
                Arbaletniy_zacep dbEntry = db.Arbaletniy_zaceps.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Price = item.Price;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                }
            }
            db.SaveChanges();
        }

        public Arbaletniy_zacep Delete(int Id)
        {
            Arbaletniy_zacep dbEntry = db.Arbaletniy_zaceps.Find(Id);
            if (dbEntry != null)
            {
                db.Arbaletniy_zaceps.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Arbaletniy_zacep item)
        {
            db.Arbaletniy_zaceps.Add(item);
        }

        public void Update(Arbaletniy_zacep item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }

    public class Shtany_i_kurtkiRepository : IProductRepository<Shtany_i_kurtki>
    {
        private OrderContext db;

        public Shtany_i_kurtkiRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Shtany_i_kurtki> GetAll()
        {
            return db.Shtany_i_kurtkis.Include(o => o.Brand);
        }

        public Shtany_i_kurtki Get(int id)
        {
            return db.Shtany_i_kurtkis.Find(id);
        }
        public void SaveItem(Shtany_i_kurtki item)
        {
            if (item.Id == 0)
                db.Shtany_i_kurtkis.Add(item);
            else
            {
                Shtany_i_kurtki dbEntry = db.Shtany_i_kurtkis.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Price = item.Price;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                    dbEntry.ZashitaKoleni = item.ZashitaKoleni;
                    dbEntry.ZashitaLokti = item.ZashitaLokti;
                    dbEntry.PokritieVneshnee = item.PokritieVneshnee;
                    dbEntry.PokritieVnutrenee = item.PokritieVnutrenee;
                    dbEntry.WaterstopRukava = item.WaterstopRukava;
                    dbEntry.WaterstopShtani = item.WaterstopShtani;
                    dbEntry.Type = item.Type;
                    dbEntry.Tolshina = item.Tolshina;
                }
            }
            db.SaveChanges();
        }

        public Shtany_i_kurtki Delete(int Id)
        {
            Shtany_i_kurtki dbEntry = db.Shtany_i_kurtkis.Find(Id);
            if (dbEntry != null)
            {
                db.Shtany_i_kurtkis.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Shtany_i_kurtki item)
        {
            db.Shtany_i_kurtkis.Add(item);
        }

        public void Update(Shtany_i_kurtki item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }

    public class SukhieRepository : IProductRepository<Sukhie>
    {
        private OrderContext db;

        public SukhieRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Sukhie> GetAll()
        {
            return db.Sukhies.Include(o => o.Brand);
        }

        public Sukhie Get(int id)
        {
            return db.Sukhies.Find(id);
        }
        public void SaveItem(Sukhie item)
        {
            if (item.Id == 0)
                db.Sukhies.Add(item);
            else
            {
                Sukhie dbEntry = db.Sukhies.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Price = item.Price;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                    dbEntry.Material = item.Material;
                    dbEntry.Molniya = item.Molniya;
                }
            }
            db.SaveChanges();
        }

        public Sukhie Delete(int Id)
        {
            Sukhie dbEntry = db.Sukhies.Find(Id);
            if (dbEntry != null)
            {
                db.Sukhies.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Sukhie item)
        {
            db.Sukhies.Add(item);
        }

        public void Update(Sukhie item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }

    public class Utepliteli_dlya_gidrokostyumovRepository : IProductRepository<Utepliteli_dlya_gidrokostyumov>
    {
        private OrderContext db;

        public Utepliteli_dlya_gidrokostyumovRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Utepliteli_dlya_gidrokostyumov> GetAll()
        {
            return db.Utepliteli_dlya_gidrokostyumovs.Include(o => o.Brand);
        }

        public Utepliteli_dlya_gidrokostyumov Get(int id)
        {
            return db.Utepliteli_dlya_gidrokostyumovs.Find(id);
        }
        public void SaveItem(Utepliteli_dlya_gidrokostyumov item)
        {
            if (item.Id == 0)
                db.Utepliteli_dlya_gidrokostyumovs.Add(item);
            else
            {
                Utepliteli_dlya_gidrokostyumov dbEntry = db.Utepliteli_dlya_gidrokostyumovs.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Price = item.Price;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                    dbEntry.Tolshina = item.Tolshina;
                }
            }
            db.SaveChanges();
        }

        public Utepliteli_dlya_gidrokostyumov Delete(int Id)
        {
            Utepliteli_dlya_gidrokostyumov dbEntry = db.Utepliteli_dlya_gidrokostyumovs.Find(Id);
            if (dbEntry != null)
            {
                db.Utepliteli_dlya_gidrokostyumovs.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Utepliteli_dlya_gidrokostyumov item)
        {
            db.Utepliteli_dlya_gidrokostyumovs.Add(item);
        }

        public void Update(Utepliteli_dlya_gidrokostyumov item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }

    public class Aksessuary_k_gidrokostyumamRepository : IProductRepository<Aksessuary_k_gidrokostyumam>
    {
        private OrderContext db;

        public Aksessuary_k_gidrokostyumamRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Aksessuary_k_gidrokostyumam> GetAll()
        {
            return db.Aksessuary_k_gidrokostyumams.Include(o => o.Brand);
        }

        public Aksessuary_k_gidrokostyumam Get(int id)
        {
            return db.Aksessuary_k_gidrokostyumams.Find(id);
        }
        public void SaveItem(Aksessuary_k_gidrokostyumam item)
        {
            if (item.Id == 0)
                db.Aksessuary_k_gidrokostyumams.Add(item);
            else
            {
                Aksessuary_k_gidrokostyumam dbEntry = db.Aksessuary_k_gidrokostyumams.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Price = item.Price;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                }
            }
            db.SaveChanges();
        }

        public Aksessuary_k_gidrokostyumam Delete(int Id)
        {
            Aksessuary_k_gidrokostyumam dbEntry = db.Aksessuary_k_gidrokostyumams.Find(Id);
            if (dbEntry != null)
            {
                db.Aksessuary_k_gidrokostyumams.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Aksessuary_k_gidrokostyumam item)
        {
            db.Aksessuary_k_gidrokostyumams.Add(item);
        }

        public void Update(Aksessuary_k_gidrokostyumam item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }

    public class Aksessuary_k_sukhim_gidrokostyumamRepository : IProductRepository<Aksessuary_k_sukhim_gidrokostyumam>
    {
        private OrderContext db;

        public Aksessuary_k_sukhim_gidrokostyumamRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Aksessuary_k_sukhim_gidrokostyumam> GetAll()
        {
            return db.Aksessuary_k_sukhim_gidrokostyumams.Include(o => o.Brand);
        }

        public Aksessuary_k_sukhim_gidrokostyumam Get(int id)
        {
            return db.Aksessuary_k_sukhim_gidrokostyumams.Find(id);
        }
        public void SaveItem(Aksessuary_k_sukhim_gidrokostyumam item)
        {
            if (item.Id == 0)
                db.Aksessuary_k_sukhim_gidrokostyumams.Add(item);
            else
            {
                Aksessuary_k_sukhim_gidrokostyumam dbEntry = db.Aksessuary_k_sukhim_gidrokostyumams.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Price = item.Price;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                }
            }
            db.SaveChanges();
        }

        public Aksessuary_k_sukhim_gidrokostyumam Delete(int Id)
        {
            Aksessuary_k_sukhim_gidrokostyumam dbEntry = db.Aksessuary_k_sukhim_gidrokostyumams.Find(Id);
            if (dbEntry != null)
            {
                db.Aksessuary_k_sukhim_gidrokostyumams.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Aksessuary_k_sukhim_gidrokostyumam item)
        {
            db.Aksessuary_k_sukhim_gidrokostyumams.Add(item);
        }

        public void Update(Aksessuary_k_sukhim_gidrokostyumam item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }

    public class ShlemyRepository : IProductRepository<Shlemy>
    {
        private OrderContext db;

        public ShlemyRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Shlemy> GetAll()
        {
            return db.Shlemys.Include(o => o.Brand);
        }

        public Shlemy Get(int id)
        {
            return db.Shlemys.Find(id);
        }
        public void SaveItem(Shlemy item)
        {
            if (item.Id == 0)
                db.Shlemys.Add(item);
            else
            {
                Shlemy dbEntry = db.Shlemys.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Price = item.Price;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                    dbEntry.Tolshina = item.Tolshina;
                }
            }
            db.SaveChanges();
        }

        public Shlemy Delete(int Id)
        {
            Shlemy dbEntry = db.Shlemys.Find(Id);
            if (dbEntry != null)
            {
                db.Shlemys.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Shlemy item)
        {
            db.Shlemys.Add(item);
        }

        public void Update(Shlemy item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }

    public class Utepliteli_dlya_sukhikh_gidrokostyumovRepository : IProductRepository<Utepliteli_dlya_sukhikh_gidrokostyumov>
    {
        private OrderContext db;

        public Utepliteli_dlya_sukhikh_gidrokostyumovRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Utepliteli_dlya_sukhikh_gidrokostyumov> GetAll()
        {
            return db.Utepliteli_dlya_sukhikh_gidrokostyumovs.Include(o => o.Brand);
        }

        public Utepliteli_dlya_sukhikh_gidrokostyumov Get(int id)
        {
            return db.Utepliteli_dlya_sukhikh_gidrokostyumovs.Find(id);
        }
        public void SaveItem(Utepliteli_dlya_sukhikh_gidrokostyumov item)
        {
            if (item.Id == 0)
                db.Utepliteli_dlya_sukhikh_gidrokostyumovs.Add(item);
            else
            {
                Utepliteli_dlya_sukhikh_gidrokostyumov dbEntry = db.Utepliteli_dlya_sukhikh_gidrokostyumovs.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Price = item.Price;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                }
            }
            db.SaveChanges();
        }

        public Utepliteli_dlya_sukhikh_gidrokostyumov Delete(int Id)
        {
            Utepliteli_dlya_sukhikh_gidrokostyumov dbEntry = db.Utepliteli_dlya_sukhikh_gidrokostyumovs.Find(Id);
            if (dbEntry != null)
            {
                db.Utepliteli_dlya_sukhikh_gidrokostyumovs.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Utepliteli_dlya_sukhikh_gidrokostyumov item)
        {
            db.Utepliteli_dlya_sukhikh_gidrokostyumovs.Add(item);
        }

        public void Update(Utepliteli_dlya_sukhikh_gidrokostyumov item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }


    public class Dlya_podvodnoy_okhotyRepository : IProductRepository<Dlya_podvodnoy_okhoty>
    {
        private OrderContext db;

        public Dlya_podvodnoy_okhotyRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Dlya_podvodnoy_okhoty> GetAll()
        {
            return db.Dlya_podvodnoy_okhotys.Include(o => o.Brand);
        }

        public Dlya_podvodnoy_okhoty Get(int id)
        {
            return db.Dlya_podvodnoy_okhotys.Find(id);
        }
        public void SaveItem(Dlya_podvodnoy_okhoty item)
        {
            if (item.Id == 0)
                db.Dlya_podvodnoy_okhotys.Add(item);
            else
            {
                Dlya_podvodnoy_okhoty dbEntry = db.Dlya_podvodnoy_okhotys.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Price = item.Price;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                    dbEntry.ZashitaKoleni = item.ZashitaKoleni;
                    dbEntry.ZashitaLokti = item.ZashitaLokti;
                    dbEntry.PokritieVneshnee = item.PokritieVneshnee;
                    dbEntry.PokritieVnutrenee = item.PokritieVnutrenee;
                    dbEntry.Tolshina = item.Tolshina;
                }
            }
            db.SaveChanges();
        }

        public Dlya_podvodnoy_okhoty Delete(int Id)
        {
            Dlya_podvodnoy_okhoty dbEntry = db.Dlya_podvodnoy_okhotys.Find(Id);
            if (dbEntry != null)
            {
                db.Dlya_podvodnoy_okhotys.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Dlya_podvodnoy_okhoty item)
        {
            db.Dlya_podvodnoy_okhotys.Add(item);
        }

        public void Update(Dlya_podvodnoy_okhoty item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }

    public class Dlya_dayvinga_i_vodnogo_sportaRepository : IProductRepository<Dlya_dayvinga_i_vodnogo_sporta>
    {
        private OrderContext db;

        public Dlya_dayvinga_i_vodnogo_sportaRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Dlya_dayvinga_i_vodnogo_sporta> GetAll()
        {
            return db.Dlya_dayvinga_i_vodnogo_sportas.Include(o => o.Brand);
        }

        public Dlya_dayvinga_i_vodnogo_sporta Get(int id)
        {
            return db.Dlya_dayvinga_i_vodnogo_sportas.Find(id);
        }
        public void SaveItem(Dlya_dayvinga_i_vodnogo_sporta item)
        {
            if (item.Id == 0)
                db.Dlya_dayvinga_i_vodnogo_sportas.Add(item);
            else
            {
                Dlya_dayvinga_i_vodnogo_sporta dbEntry = db.Dlya_dayvinga_i_vodnogo_sportas.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Price = item.Price;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                    dbEntry.ZashitaKoleni = item.ZashitaKoleni;
                    dbEntry.ZashitaLokti = item.ZashitaLokti;
                    dbEntry.WaterstopRukava = item.WaterstopRukava;
                    dbEntry.WaterstopShtani = item.WaterstopShtani;
                    dbEntry.Molniya = item.Molniya;
                    dbEntry.Tolshina = item.Tolshina;
                }
            }
            db.SaveChanges();
        }

        public Dlya_dayvinga_i_vodnogo_sporta Delete(int Id)
        {
            Dlya_dayvinga_i_vodnogo_sporta dbEntry = db.Dlya_dayvinga_i_vodnogo_sportas.Find(Id);
            if (dbEntry != null)
            {
                db.Dlya_dayvinga_i_vodnogo_sportas.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Dlya_dayvinga_i_vodnogo_sporta item)
        {
            db.Dlya_dayvinga_i_vodnogo_sportas.Add(item);
        }

        public void Update(Dlya_dayvinga_i_vodnogo_sporta item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }

    public class Futbolki_i_shorty_dlya_vodnogo_sportaRepository : IProductRepository<Futbolki_i_shorty_dlya_vodnogo_sporta>
    {
        private OrderContext db;

        public Futbolki_i_shorty_dlya_vodnogo_sportaRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Futbolki_i_shorty_dlya_vodnogo_sporta> GetAll()
        {
            return db.Futbolki_i_shorty_dlya_vodnogo_sportas.Include(o => o.Brand);
        }

        public Futbolki_i_shorty_dlya_vodnogo_sporta Get(int id)
        {
            return db.Futbolki_i_shorty_dlya_vodnogo_sportas.Find(id);
        }
        public void SaveItem(Futbolki_i_shorty_dlya_vodnogo_sporta item)
        {
            if (item.Id == 0)
                db.Futbolki_i_shorty_dlya_vodnogo_sportas.Add(item);
            else
            {
                Futbolki_i_shorty_dlya_vodnogo_sporta dbEntry = db.Futbolki_i_shorty_dlya_vodnogo_sportas.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Price = item.Price;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                    dbEntry.Pol = item.Pol;
                    dbEntry.Type = item.Type;
                }
            }
            db.SaveChanges();
        }

        public Futbolki_i_shorty_dlya_vodnogo_sporta Delete(int Id)
        {
            Futbolki_i_shorty_dlya_vodnogo_sporta dbEntry = db.Futbolki_i_shorty_dlya_vodnogo_sportas.Find(Id);
            if (dbEntry != null)
            {
                db.Futbolki_i_shorty_dlya_vodnogo_sportas.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Futbolki_i_shorty_dlya_vodnogo_sporta item)
        {
            db.Futbolki_i_shorty_dlya_vodnogo_sportas.Add(item);
        }

        public void Update(Futbolki_i_shorty_dlya_vodnogo_sporta item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }

    public class ProcheeRepository : IProductRepository<Prochee>
    {
        private OrderContext db;

        public ProcheeRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Prochee> GetAll()
        {
            return db.Prochees.Include(o => o.Brand);
        }

        public Prochee Get(int id)
        {
            return db.Prochees.Find(id);
        }
        public void SaveItem(Prochee item)
        {
            if (item.Id == 0)
                db.Prochees.Add(item);
            else
            {
                Prochee dbEntry = db.Prochees.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Price = item.Price;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                }
            }
            db.SaveChanges();
        }

        public Prochee Delete(int Id)
        {
            Prochee dbEntry = db.Prochees.Find(Id);
            if (dbEntry != null)
            {
                db.Prochees.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Prochee item)
        {
            db.Prochees.Add(item);
        }

        public void Update(Prochee item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }

    public class NakonechnikRepository : IProductRepository<Nakonechnik>
    {
        private OrderContext db;

        public NakonechnikRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Nakonechnik> GetAll()
        {
            return db.Nakonechniks.Include(o => o.Brand);
        }

        public Nakonechnik Get(int id)
        {
            return db.Nakonechniks.Find(id);
        }
        public void SaveItem(Nakonechnik item)
        {
            if (item.Id == 0)
                db.Nakonechniks.Add(item);
            else
            {
                Nakonechnik dbEntry = db.Nakonechniks.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.DiametrRezbi = item.DiametrRezbi;
                    dbEntry.Type = item.Type;
                    dbEntry.Price = item.Price;
                    dbEntry.BrandId = item.BrandId;
                    dbEntry.Brand = item.Brand;
                    dbEntry.ImageData = item.ImageData;
                    dbEntry.ImageMimeType = item.ImageMimeType;
                    dbEntry.Description = item.Description;
                    dbEntry.Category = item.Category;
                }
            }
            db.SaveChanges();
        }

        public Nakonechnik Delete(int Id)
        {
            Nakonechnik dbEntry = db.Nakonechniks.Find(Id);
            if (dbEntry != null)
            {
                db.Nakonechniks.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Nakonechnik item)
        {
            db.Nakonechniks.Add(item);
        }

        public void Update(Nakonechnik item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }
    public class BrandRepository : IProductRepository<Brand>
    {
        private OrderContext db;

        public BrandRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Brand> GetAll()
        {
            return db.Brands.Include(o => o.Pneumatics);
        }

        public Brand Get(int id)
        {
            return db.Brands.Find(id);
        }

        public void SaveItem(Brand item)
        {
            if (item.Id == 0)
                db.Brands.Add(item);
            else
            {
                Brand dbEntry = db.Brands.Find(item.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Country = item.Country;
                    dbEntry.Pneumatics = item.Pneumatics;
                }
            }
            db.SaveChanges();
        }

        public Brand Delete(int Id)
        {
            Brand dbEntry = db.Brands.Find(Id);
            if (dbEntry != null)
            {
                db.Brands.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void Create(Brand item)
        {
            db.Brands.Add(item);
        }

        public void Update(Brand item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }

    //public class ItemRepository : IProductRepository<Item>
    //{
    //    private OrderContext db;

    //    public ItemRepository(OrderContext context)
    //    {
    //        this.db = context;
    //    }

    //    public IEnumerable<Item> GetAll()
    //    {
    //        return db.Items;
    //    }

    //    public Item Get(int id)
    //    {
    //        return db.Items.Find(id);
    //    }

    //    public void SaveItem(Item item)
    //    {
    //        if (item.Id == 0)
    //            db.Items.Add(item);
    //        else
    //        {
    //            Item dbEntry = db.Items.Find(item.Id);
    //            if (dbEntry != null)
    //            {
    //                dbEntry.Name = item.Name;
    //            }
    //        }
    //        db.SaveChanges();
    //    }

    //    public Item Delete(int Id)
    //    {
    //        Item dbEntry = db.Items.Find(Id);
    //        if (dbEntry != null)
    //        {
    //            db.Items.Remove(dbEntry);
    //            db.SaveChanges();
    //        }
    //        return dbEntry;
    //    }

    //    public void Create(Item item)
    //    {
    //        db.Items.Add(item);
    //    }

    //    public void Update(Item item)
    //    {
    //        db.Entry(item).State = EntityState.Modified;
    //    }

    //}
}
