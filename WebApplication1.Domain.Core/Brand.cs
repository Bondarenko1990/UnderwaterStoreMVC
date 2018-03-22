using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Domain.Core
{
    public class Brand
    {
        // ID бренда
        public int Id { get; set; }
        // название бренда
        public string Name { get; set; }
        // Производитель бренда
        public string Country { get; set; }
        public ICollection<Prochee> Prochees { get; set; }
        public ICollection<Arbaletnaya_tyaga> Arbaletnaya_tyagas { get; set; }
        public ICollection<Katushka> Katushkas { get; set; }
        public ICollection<Nakonechnik> Nakonechniks { get; set; }
        public ICollection<Lin> Lins { get; set; }
        public ICollection<Pneumatic> Pneumatics { get; set; }
        public ICollection<Arbalet> Arbalets { get; set; }
        public ICollection<Garpun> Garpuns { get; set; }
        public ICollection<Arbaletniy_zacep> Arbaletniy_zaceps { get; set; }
        public ICollection<Shtany_i_kurtki> Shtany_i_kurtkis { get; set; }
        public ICollection<Dlya_podvodnoy_okhoty> Dlya_podvodnoy_okhotys { get; set; }
        public ICollection<Dlya_dayvinga_i_vodnogo_sporta> Dlya_dayvinga_i_vodnogo_sportas { get; set; }
        public ICollection<Futbolki_i_shorty_dlya_vodnogo_sporta> Futbolki_i_shorty_dlya_vodnogo_sportas { get; set; }
        public ICollection<Sukhie> Sukhies { get; set; }
        public ICollection<Utepliteli_dlya_gidrokostyumov> Utepliteli_dlya_gidrokostyumovs { get; set; }
        public ICollection<Shlemy> Shlemys { get; set; }
        public ICollection<Utepliteli_dlya_sukhikh_gidrokostyumov> Utepliteli_dlya_sukhikh_gidrokostyumovs { get; set; }
        public ICollection<Aksessuary_k_gidrokostyumam> Aksessuary_k_gidrokostyumams { get; set; }
        public ICollection<Aksessuary_k_sukhim_gidrokostyumam> Aksessuary_k_sukhim_gidrokostyumams { get; set; }
        public ICollection<Maski> Maskis { get; set; }
        public ICollection<Trubki> Trubkis { get; set; }
        public ICollection<Lasty_dlya_okhoty> Lasty_dlya_okhotys { get; set; }
        public ICollection<Lasty_dlya_dayvinga> Lasty_dlya_dayvingas { get; set; }
        public ICollection<Komplekty> Komplektys { get; set; }
        public ICollection<Aksessuary_k_maskam> Aksessuary_k_maskams { get; set; }
        public ICollection<Aksessuary_k_lastam> Aksessuary_k_lastams { get; set; }
        public ICollection<Aksessuary_k_trubkam> Aksessuary_k_trubkams { get; set; }
        public ICollection<Fonari> Fonaris { get; set; }
        public ICollection<Nozhi> Nozhis { get; set; }
        public ICollection<Gruza_i_gruzovye_sistemy> Gruza_i_gruzovye_sistemys { get; set; }
        public ICollection<Aksessuary_k_fonaryam> Aksessuary_k_fonaryams { get; set; }
        public ICollection<Aksessuary_k_nozham> Aksessuary_k_nozhams { get; set; }
        public ICollection<Aksessuary_k_gruzam> Aksessuary_k_gruzams { get; set; }
        public ICollection<Noski> Noskis { get; set; }
        public ICollection<Rukavitsy> Rukavitsys { get; set; }
        public ICollection<Boty> Botys { get; set; }
        public ICollection<Tapochki> Tapochkis { get; set; }
        public ICollection<Kukany> Kukanys { get; set; }
        public ICollection<Bui_i_aksessuary> Bui_i_aksessuarys { get; set; }
        public ICollection<Khimicheskie_sredstva_i_masla> Khimicheskie_sredstva_i_maslas { get; set; }
        public ICollection<Katushki_khodovye> Katushki_khodovyes { get; set; }
        public ICollection<Chekhly_setki> Chekhly_setkis { get; set; }
        public ICollection<Mulyazhi> Mulyazhis { get; set; }
        public ICollection<Poleznye_aksessuary> Poleznye_aksessuarys { get; set; }

        public Brand()
        {
            Poleznye_aksessuarys = new List<Poleznye_aksessuary>();
            Mulyazhis = new List<Mulyazhi>();
            Chekhly_setkis = new List<Chekhly_setki>();
            Katushki_khodovyes = new List<Katushki_khodovye>();
            Khimicheskie_sredstva_i_maslas = new List<Khimicheskie_sredstva_i_masla>();
            Bui_i_aksessuarys = new List<Bui_i_aksessuary>();
            Kukanys = new List<Kukany>();
            Tapochkis = new List<Tapochki>();
            Botys = new List<Boty>();
            Rukavitsys = new List<Rukavitsy>();
            Noskis = new List<Noski>();
            Aksessuary_k_gruzams = new List<Aksessuary_k_gruzam>();
            Aksessuary_k_nozhams = new List<Aksessuary_k_nozham>();
            Aksessuary_k_fonaryams = new List<Aksessuary_k_fonaryam>();
            Gruza_i_gruzovye_sistemys = new List<Gruza_i_gruzovye_sistemy>();
            Nozhis = new List<Nozhi>();
            Fonaris = new List<Fonari>();
            Aksessuary_k_trubkams = new List<Aksessuary_k_trubkam>();
            Aksessuary_k_lastams = new List<Aksessuary_k_lastam>();
            Aksessuary_k_maskams = new List<Aksessuary_k_maskam>();
            Komplektys = new List<Komplekty>();
            Lasty_dlya_dayvingas = new List<Lasty_dlya_dayvinga>();
            Lasty_dlya_okhotys = new List<Lasty_dlya_okhoty>();
            Trubkis = new List<Trubki>();
            Maskis = new List<Maski>();
            Aksessuary_k_sukhim_gidrokostyumams = new List<Aksessuary_k_sukhim_gidrokostyumam>();
            Aksessuary_k_gidrokostyumams = new List<Aksessuary_k_gidrokostyumam>();
            Utepliteli_dlya_sukhikh_gidrokostyumovs = new List<Utepliteli_dlya_sukhikh_gidrokostyumov>();
            Shlemys = new List<Shlemy>();
            Utepliteli_dlya_gidrokostyumovs = new List<Utepliteli_dlya_gidrokostyumov>();
            Sukhies = new List<Sukhie>();
            Futbolki_i_shorty_dlya_vodnogo_sportas = new List<Futbolki_i_shorty_dlya_vodnogo_sporta>();
            Dlya_dayvinga_i_vodnogo_sportas = new List<Dlya_dayvinga_i_vodnogo_sporta>();
            Dlya_podvodnoy_okhotys = new List<Dlya_podvodnoy_okhoty>();
            Shtany_i_kurtkis = new List<Shtany_i_kurtki>();
            Prochees = new List<Prochee>();
            Arbaletniy_zaceps = new List<Arbaletniy_zacep>();
            Arbaletnaya_tyagas = new List<Arbaletnaya_tyaga>();
            Katushkas = new List<Katushka>();
            Nakonechniks = new List<Nakonechnik>();
            Lins = new List<Lin>();
            Pneumatics = new List<Pneumatic>();
            Arbalets = new List<Arbalet>();
            Garpuns = new List<Garpun>();
        }
    }
}
