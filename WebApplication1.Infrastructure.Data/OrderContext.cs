using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using WebApplication1.Domain.Core;
using System.Data.Entity.Infrastructure;

namespace WebApplication1.Infrastructure.Data
{
    public class OrderContext : DbContext
    {
        public OrderContext() :  base("name=OrderContext") {
            var adapter = (IObjectContextAdapter)this;
            var objectContext = adapter.ObjectContext;
            objectContext.CommandTimeout = 1 * 60; // value in seconds
        }
        public DbSet<Poleznye_aksessuary> Poleznye_aksessuarys { get; set; }
        public DbSet<Mulyazhi> Mulyazhis { get; set; }
        public DbSet<Chekhly_setki> Chekhly_setkis { get; set; }
        public DbSet<Katushki_khodovye> Katushki_khodovyes { get; set; }
        public DbSet<Khimicheskie_sredstva_i_masla> Khimicheskie_sredstva_i_maslas { get; set; }
        public DbSet<Bui_i_aksessuary> Bui_i_aksessuarys { get; set; }
        public DbSet<Kukany> Kukanys { get; set; }
        public DbSet<Tapochki> Tapochkis { get; set; }
        public DbSet<Boty> Botys { get; set; }
        public DbSet<Rukavitsy> Rukavitsys { get; set; }
        public DbSet<Noski> Noskis { get; set; }
        public DbSet<Perchatki> Perchatkis { get; set; }
        public DbSet<Aksessuary_k_gruzam> Aksessuary_k_gruzams { get; set; }
        public DbSet<Aksessuary_k_nozham> Aksessuary_k_nozhams { get; set; }
        public DbSet<Aksessuary_k_fonaryam> Aksessuary_k_fonaryams { get; set; }
        public DbSet<Gruza_i_gruzovye_sistemy> Gruza_i_gruzovye_sistemys { get; set; }
        public DbSet<Nozhi> Nozhis { get; set; }
        public DbSet<Fonari> Fonaris { get; set; }
        public DbSet<Aksessuary_k_lastam> Aksessuary_k_lastams { get; set; }
        public DbSet<Aksessuary_k_maskam> Aksessuary_k_maskams { get; set; }
        public DbSet<Lasty_dlya_dayvinga> Lasty_dlya_dayvingas { get; set; }
        public DbSet<Lasty_dlya_okhoty> Lasty_dlya_okhotys { get; set; }
        public DbSet<Trubki> Trubkis { get; set; }
        public DbSet<Maski> Maskis { get; set; }
        public DbSet<Sukhie> Sukhies { get; set; }
        public DbSet<Shtany_i_kurtki> Shtany_i_kurtkis { get; set; }
        public DbSet<Prochee> Prochees { get; set; }
        public DbSet<Arbaletnaya_tyaga> Arbaletnaya_tyagas { get; set; }
        public DbSet<Katushka> Katushkas { get; set; }
        public DbSet<Nakonechnik> Nakonechniks { get; set; }
        public DbSet<Lin> Lins { get; set; }
        public DbSet<Garpun> Garpuns { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Arbalet> Arbalets { get; set; }
        public DbSet<Pneumatic> Pneumatics { get; set; }
        public DbSet<Arbaletniy_zacep> Arbaletniy_zaceps { get; set; }
        public DbSet<Dlya_podvodnoy_okhoty> Dlya_podvodnoy_okhotys{ get; set; }
        public DbSet<Dlya_dayvinga_i_vodnogo_sporta> Dlya_dayvinga_i_vodnogo_sportas { get; set; }
        public DbSet<Futbolki_i_shorty_dlya_vodnogo_sporta> Futbolki_i_shorty_dlya_vodnogo_sportas { get; set; }
        public DbSet<Utepliteli_dlya_gidrokostyumov> Utepliteli_dlya_gidrokostyumovs { get; set; }
        public DbSet<Shlemy> Shlemys { get; set; }
        public DbSet<Utepliteli_dlya_sukhikh_gidrokostyumov> Utepliteli_dlya_sukhikh_gidrokostyumovs { get; set; }
        public DbSet<Aksessuary_k_gidrokostyumam> Aksessuary_k_gidrokostyumams { get; set; }
        public DbSet<Aksessuary_k_sukhim_gidrokostyumam> Aksessuary_k_sukhim_gidrokostyumams { get; set; }
        public DbSet<Komplekty> Komplektys { get; set; }
        public DbSet<Aksessuary_k_trubkam> Aksessuary_k_trubkams { get; set; }
        public DbSet<Brand> Brands { get; set; }

    }
}
