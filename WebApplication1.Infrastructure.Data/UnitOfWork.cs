using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Infrastructure.Data
{
    public class UnitOfWork : IDisposable
    {
        private OrderContext db = new OrderContext();
        private PneumaticRepository pneumaticRepository;
        private ArbaletRepository arbaletRepository;
        private BrandRepository brandRepository;
        private ProductRepository productRepository;
        private GarpunRepository garpunRepository;
        private LinRepository linRepository;
        private NakonechnikRepository nakonechnikRepository;
        private KatushkaRepository katushkaRepository;
        private Arbaletnaya_tyagaRepository arbaletnaya_tyagaRepository;
        private Arbaletniy_zacepRepository arbaletniy_zacepRepository;
        private ProcheeRepository procheeRepository;
        private Shtany_i_kurtkiRepository shtany_i_kurtkiRepository;
        private Dlya_podvodnoy_okhotyRepository dlya_podvodnoy_okhotyRepository;        
        private Dlya_dayvinga_i_vodnogo_sportaRepository dlya_dayvinga_i_vodnogo_sportaRepository;
        private Futbolki_i_shorty_dlya_vodnogo_sportaRepository futbolki_i_shorty_dlya_vodnogo_sportaRepository;
        private SukhieRepository sukhieRepository;
        private Utepliteli_dlya_gidrokostyumovRepository utepliteli_dlya_gidrokostyumovRepository;
        private ShlemyRepository shlemyRepository;
        private Utepliteli_dlya_sukhikh_gidrokostyumovRepository utepliteli_dlya_sukhikh_gidrokostyumovRepository;
        private Aksessuary_k_gidrokostyumamRepository aksessuary_k_gidrokostyumamRepository;
        private Aksessuary_k_sukhim_gidrokostyumamRepository aksessuary_k_sukhim_gidrokostyumamRepository;
        private MaskiRepository maskiRepository;
        private TrubkiRepository trubkiRepository;
        private Lasty_dlya_okhotyRepository lasty_dlya_okhotyRepository;
        private Lasty_dlya_dayvingaRepository lasty_dlya_dayvingaRepository;
        private KomplektyRepository komplektyRepository;
        private Aksessuary_k_maskamRepository aksessuary_k_maskamRepository;
        private Aksessuary_k_lastamRepository aksessuary_k_lastamRepository;
        private Aksessuary_k_trubkamRepository aksessuary_k_trubkamRepository;
        private FonariRepository fonariRepository;
        private NozhiRepository nozhiRepository;
        private Gruza_i_gruzovye_sistemyRepository gruza_i_gruzovye_sistemyRepository;
        private Aksessuary_k_fonaryamRepository aksessuary_k_fonaryamRepository;
        private Aksessuary_k_nozhamRepository aksessuary_k_nozhamRepository;
        private Aksessuary_k_gruzamRepository aksessuary_k_gruzamRepository;
        private PerchatkiRepository perchatkiRepository;
        private NoskiRepository noskiRepository;
        private RukavitsyRepository rukavitsyRepository;
        private BotyRepository botyRepository;
        private TapochkiRepository tapochkiRepository;
        private KukanyRepository kukanyRepository;
        private Bui_i_aksessuaryRepository bui_i_aksessuaryRepository;
        private Khimicheskie_sredstva_i_maslaRepository khimicheskie_sredstva_i_maslaRepository;
        private Katushki_khodovyeRepository katushki_khodovyeRepository;
        private Chekhly_setkiRepository chekhly_setkiRepository;
        private MulyazhiRepository mulyazhiRepository;
        private Poleznye_aksessuaryRepository poleznye_aksessuaryRepository;

        public PneumaticRepository Pneumatics
        {
            get
            {
                if (pneumaticRepository == null)
                    pneumaticRepository = new PneumaticRepository(db);
                return pneumaticRepository;
            }
        }

        public ArbaletRepository Arbalets
        {
            get
            {
                if (arbaletRepository == null)
                    arbaletRepository = new ArbaletRepository(db);
                return arbaletRepository;
            }
        }

        public GarpunRepository Garpuns
        {
            get
            {
                if (garpunRepository == null)
                    garpunRepository = new GarpunRepository(db);
                return garpunRepository;
            }
        }

        public LinRepository Lins
        {
            get
            {
                if (linRepository == null)
                    linRepository = new LinRepository(db);
                return linRepository;
            }
        }

        public NakonechnikRepository Nakonechniks
        {
            get
            {
                if (nakonechnikRepository == null)
                    nakonechnikRepository = new NakonechnikRepository(db);
                return nakonechnikRepository;
            }
        }

        public KatushkaRepository Katushkas
        {
            get
            {
                if (katushkaRepository == null)
                    katushkaRepository = new KatushkaRepository(db);
                return katushkaRepository;
            }
        }

        public Arbaletnaya_tyagaRepository Arbaletnaya_tyagas
        {
            get
            {
                if (arbaletnaya_tyagaRepository == null)
                    arbaletnaya_tyagaRepository = new Arbaletnaya_tyagaRepository(db);
                return arbaletnaya_tyagaRepository;
            }
        }

        public Shtany_i_kurtkiRepository Shtany_i_kurtkis
        {
            get
            {
                if (shtany_i_kurtkiRepository == null)
                    shtany_i_kurtkiRepository = new Shtany_i_kurtkiRepository(db);
                return shtany_i_kurtkiRepository;
            }
        }

        public Dlya_podvodnoy_okhotyRepository Dlya_podvodnoy_okhotys
        {
            get
            {
                if (dlya_podvodnoy_okhotyRepository == null)
                    dlya_podvodnoy_okhotyRepository = new Dlya_podvodnoy_okhotyRepository(db);
                return dlya_podvodnoy_okhotyRepository;
            }
        }

        public Lasty_dlya_dayvingaRepository Lasty_dlya_dayvingas
        {
            get
            {
                if (lasty_dlya_dayvingaRepository == null)
                    lasty_dlya_dayvingaRepository = new Lasty_dlya_dayvingaRepository(db);
                return lasty_dlya_dayvingaRepository;
            }
        }

        public KomplektyRepository Komplektys
        {
            get
            {
                if (komplektyRepository == null)
                    komplektyRepository = new KomplektyRepository(db);
                return komplektyRepository;
            }
        }

        public Aksessuary_k_maskamRepository Aksessuary_k_maskams
        {
            get
            {
                if (aksessuary_k_maskamRepository == null)
                    aksessuary_k_maskamRepository = new Aksessuary_k_maskamRepository(db);
                return aksessuary_k_maskamRepository;
            }
        }

        public Aksessuary_k_lastamRepository Aksessuary_k_lastams
        {
            get
            {
                if (aksessuary_k_lastamRepository == null)
                    aksessuary_k_lastamRepository = new Aksessuary_k_lastamRepository(db);
                return aksessuary_k_lastamRepository;
            }
        }

        public Aksessuary_k_trubkamRepository Aksessuary_k_trubkams
        {
            get
            {
                if (aksessuary_k_trubkamRepository == null)
                    aksessuary_k_trubkamRepository = new Aksessuary_k_trubkamRepository(db);
                return aksessuary_k_trubkamRepository;
            }
        }

        public FonariRepository Fonaris
        {
            get
            {
                if (fonariRepository == null)
                    fonariRepository = new FonariRepository(db);
                return fonariRepository;
            }
        }

        public RukavitsyRepository Rukavitsys
        {
            get
            {
                if (rukavitsyRepository == null)
                    rukavitsyRepository = new RukavitsyRepository(db);
                return rukavitsyRepository;
            }
        }

        public TapochkiRepository Tapochkis
        {
            get
            {
                if (tapochkiRepository == null)
                    tapochkiRepository = new TapochkiRepository(db);
                return tapochkiRepository;
            }
        }

        public KukanyRepository Kukanys
        {
            get
            {
                if (kukanyRepository == null)
                    kukanyRepository = new KukanyRepository(db);
                return kukanyRepository;
            }
        }

        public Bui_i_aksessuaryRepository Bui_i_aksessuarys
        {
            get
            {
                if (bui_i_aksessuaryRepository == null)
                    bui_i_aksessuaryRepository = new Bui_i_aksessuaryRepository(db);
                return bui_i_aksessuaryRepository;
            }
        }

        public Khimicheskie_sredstva_i_maslaRepository Khimicheskie_sredstva_i_maslas
        {
            get
            {
                if (khimicheskie_sredstva_i_maslaRepository == null)
                    khimicheskie_sredstva_i_maslaRepository = new Khimicheskie_sredstva_i_maslaRepository(db);
                return khimicheskie_sredstva_i_maslaRepository;
            }
        }

        public Katushki_khodovyeRepository Katushki_khodovyes
        {
            get
            {
                if (katushki_khodovyeRepository == null)
                    katushki_khodovyeRepository = new Katushki_khodovyeRepository(db);
                return katushki_khodovyeRepository;
            }
        }

        public Chekhly_setkiRepository Chekhly_setkis
        {
            get
            {
                if (chekhly_setkiRepository == null)
                    chekhly_setkiRepository = new Chekhly_setkiRepository(db);
                return chekhly_setkiRepository;
            }
        }

        public MulyazhiRepository Mulyazhis
        {
            get
            {
                if (mulyazhiRepository == null)
                    mulyazhiRepository = new MulyazhiRepository(db);
                return mulyazhiRepository;
            }
        }

        public Poleznye_aksessuaryRepository Poleznye_aksessuarys
        {
            get
            {
                if (poleznye_aksessuaryRepository == null)
                    poleznye_aksessuaryRepository = new Poleznye_aksessuaryRepository(db);
                return poleznye_aksessuaryRepository;
            }
        }

        public BotyRepository Botys
        {
            get
            {
                if (botyRepository == null)
                    botyRepository = new BotyRepository(db);
                return botyRepository;
            }
        }
        public NoskiRepository Noskis
        {
            get
            {
                if (noskiRepository == null)
                    noskiRepository = new NoskiRepository(db);
                return noskiRepository;
            }
        }

        public Aksessuary_k_gruzamRepository Aksessuary_k_gruzams
        {
            get
            {
                if (aksessuary_k_gruzamRepository == null)
                    aksessuary_k_gruzamRepository = new Aksessuary_k_gruzamRepository(db);
                return aksessuary_k_gruzamRepository;
            }
        }

        public Aksessuary_k_nozhamRepository Aksessuary_k_nozhams
        {
            get
            {
                if (aksessuary_k_nozhamRepository == null)
                    aksessuary_k_nozhamRepository = new Aksessuary_k_nozhamRepository(db);
                return aksessuary_k_nozhamRepository;
            }
        }


        public Aksessuary_k_fonaryamRepository Aksessuary_k_fonaryams
        {
            get
            {
                if (aksessuary_k_fonaryamRepository == null)
                    aksessuary_k_fonaryamRepository = new Aksessuary_k_fonaryamRepository(db);
                return aksessuary_k_fonaryamRepository;
            }
        }

        public Gruza_i_gruzovye_sistemyRepository Gruza_i_gruzovye_sistemys
        {
            get
            {
                if (gruza_i_gruzovye_sistemyRepository == null)
                    gruza_i_gruzovye_sistemyRepository = new Gruza_i_gruzovye_sistemyRepository(db);
                return gruza_i_gruzovye_sistemyRepository;
            }
        }

        public NozhiRepository Nozhis
        {
            get
            {
                if (nozhiRepository == null)
                    nozhiRepository = new NozhiRepository(db);
                return nozhiRepository;
            }
        }

        public Dlya_dayvinga_i_vodnogo_sportaRepository Dlya_dayvinga_i_vodnogo_sportas
        {
            get
            {
                if (dlya_dayvinga_i_vodnogo_sportaRepository == null)
                    dlya_dayvinga_i_vodnogo_sportaRepository = new Dlya_dayvinga_i_vodnogo_sportaRepository(db);
                return dlya_dayvinga_i_vodnogo_sportaRepository;
            }
        }

        public SukhieRepository Sukhies
        {
            get
            {
                if (sukhieRepository == null)
                    sukhieRepository = new SukhieRepository(db);
                return sukhieRepository;
            }
        }

        public TrubkiRepository Trubkis
        {
            get
            {
                if (trubkiRepository == null)
                    trubkiRepository = new TrubkiRepository(db);
                return trubkiRepository;
            }
        }

        public Lasty_dlya_okhotyRepository Lasty_dlya_okhotys
        {
            get
            {
                if (lasty_dlya_okhotyRepository == null)
                    lasty_dlya_okhotyRepository = new Lasty_dlya_okhotyRepository(db);
                return lasty_dlya_okhotyRepository;
            }
        }

        public MaskiRepository Maskis
        {
            get
            {
                if (maskiRepository == null)
                    maskiRepository = new MaskiRepository(db);
                return maskiRepository;
            }
        }

        public PerchatkiRepository Perchatkis
        {
            get
            {
                if (perchatkiRepository == null)
                    perchatkiRepository = new PerchatkiRepository(db);
                return perchatkiRepository;
            }
        }

        public Utepliteli_dlya_gidrokostyumovRepository Utepliteli_dlya_gidrokostyumovs
        {
            get
            {
                if (utepliteli_dlya_gidrokostyumovRepository == null)
                    utepliteli_dlya_gidrokostyumovRepository = new Utepliteli_dlya_gidrokostyumovRepository(db);
                return utepliteli_dlya_gidrokostyumovRepository;
            }
        }

        public Utepliteli_dlya_sukhikh_gidrokostyumovRepository Utepliteli_dlya_sukhikh_gidrokostyumovs
        {
            get
            {
                if (utepliteli_dlya_sukhikh_gidrokostyumovRepository == null)
                    utepliteli_dlya_sukhikh_gidrokostyumovRepository = new Utepliteli_dlya_sukhikh_gidrokostyumovRepository(db);
                return utepliteli_dlya_sukhikh_gidrokostyumovRepository;
            }
        }

        public Aksessuary_k_gidrokostyumamRepository Aksessuary_k_gidrokostyumams
        {
            get
            {
                if (aksessuary_k_gidrokostyumamRepository == null)
                    aksessuary_k_gidrokostyumamRepository = new Aksessuary_k_gidrokostyumamRepository(db);
                return aksessuary_k_gidrokostyumamRepository;
            }
        }

        public Aksessuary_k_sukhim_gidrokostyumamRepository Aksessuary_k_sukhim_gidrokostyumams
        {
            get
            {
                if (aksessuary_k_sukhim_gidrokostyumamRepository == null)
                    aksessuary_k_sukhim_gidrokostyumamRepository = new Aksessuary_k_sukhim_gidrokostyumamRepository(db);
                return aksessuary_k_sukhim_gidrokostyumamRepository;
            }
        }

        public ShlemyRepository Shlemys
        {
            get
            {
                if (shlemyRepository == null)
                    shlemyRepository = new ShlemyRepository(db);
                return shlemyRepository;
            }
        }
        public Futbolki_i_shorty_dlya_vodnogo_sportaRepository Futbolki_i_shorty_dlya_vodnogo_sportas
        {
            get
            {
                if (futbolki_i_shorty_dlya_vodnogo_sportaRepository == null)
                    futbolki_i_shorty_dlya_vodnogo_sportaRepository = new Futbolki_i_shorty_dlya_vodnogo_sportaRepository(db);
                return futbolki_i_shorty_dlya_vodnogo_sportaRepository;
            }
        }

        public Arbaletniy_zacepRepository Arbaletniy_zaceps
        {
            get
            {
                if (arbaletniy_zacepRepository == null)
                    arbaletniy_zacepRepository = new Arbaletniy_zacepRepository(db);
                return arbaletniy_zacepRepository;
            }
        }

        public ProcheeRepository Prochees
        {
            get
            {
                if (procheeRepository == null)
                    procheeRepository = new ProcheeRepository(db);
                return procheeRepository;
            }
        }

        public BrandRepository Brands
        {
            get
            {
                if (brandRepository == null)
                    brandRepository = new BrandRepository(db);
                return brandRepository;
            }
        }

        public ProductRepository Products
        {
            get
            {
                if (productRepository == null)
                    productRepository = new ProductRepository(db);
                return productRepository;
            }
        }


        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
