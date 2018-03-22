using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using System.Data.Entity;
using WebApplication1.Domain.Core;

namespace WebApplication1.Models
{
    public class IndexView
    {
        public IEnumerable<Pneumatic> Pneumatics { get; set; }
        public IEnumerable<Arbalet> Arbalets { get; set; }
        public IEnumerable<Garpun> Garpuns { get; set; }
        public IEnumerable<Lin> Lins { get; set; }
        public IEnumerable<Nakonechnik> Nakonechniks { get; set; }
        public IEnumerable<Katushka> Katushkas { get; set; }
        public IEnumerable<Arbaletnaya_tyaga> Arbaletnaya_tyagas { get; set; }
        public IEnumerable<Arbaletniy_zacep> Arbaletniy_zaceps { get; set; }
        public IEnumerable<Prochee> Prochees { get; set; }
        public IEnumerable<Shtany_i_kurtki> Shtany_i_kurtkis { get; set; }
        public IEnumerable<Dlya_podvodnoy_okhoty> Dlya_podvodnoy_okhotys { get; set; }
        public IEnumerable<Dlya_dayvinga_i_vodnogo_sporta> Dlya_dayvinga_i_vodnogo_sportas { get; set; }
        public IEnumerable<Futbolki_i_shorty_dlya_vodnogo_sporta> Futbolki_i_shorty_dlya_vodnogo_sportas { get; set; }
        public IEnumerable<Sukhie> Sukhies { get; set; }
        public IEnumerable<Utepliteli_dlya_gidrokostyumov> Utepliteli_dlya_gidrokostyumovs { get; set; }
        public IEnumerable<Shlemy> Shlemys { get; set; }
        public IEnumerable<Utepliteli_dlya_sukhikh_gidrokostyumov> Utepliteli_dlya_sukhikh_gidrokostyumovs { get; set; }
        public IEnumerable<Aksessuary_k_gidrokostyumam> Aksessuary_k_gidrokostyumams { get; set; }
        public IEnumerable<Aksessuary_k_sukhim_gidrokostyumam> Aksessuary_k_sukhim_gidrokostyumams { get; set; }
        public IEnumerable<Maski> Maskis { get; set; }
        public IEnumerable<Trubki> Trubkis { get; set; }
        public IEnumerable<Lasty_dlya_okhoty> Lasty_dlya_okhotys { get; set; }
        public IEnumerable<Lasty_dlya_dayvinga> Lasty_dlya_dayvingas { get; set; }
        public IEnumerable<Komplekty> Komplektys { get; set; }
        public IEnumerable<Aksessuary_k_maskam> Aksessuary_k_maskams { get; set; }
        public IEnumerable<Aksessuary_k_lastam> Aksessuary_k_lastams { get; set; }
        public IEnumerable<Aksessuary_k_trubkam> Aksessuary_k_trubkams { get; set; }
        public IEnumerable<Fonari> Fonaris { get; set; }
        public IEnumerable<Nozhi> Nozhis { get; set; }
        public IEnumerable<Gruza_i_gruzovye_sistemy> Gruza_i_gruzovye_sistemys { get; set; }
        public IEnumerable<Aksessuary_k_fonaryam> Aksessuary_k_fonaryams { get; set; }
        public IEnumerable<Aksessuary_k_nozham> Aksessuary_k_nozhams { get; set; }
        public IEnumerable<Aksessuary_k_gruzam> Aksessuary_k_gruzams { get; set; }
        public IEnumerable<Perchatki> Perchatkis { get; set; }
        public IEnumerable<Noski> Noskis { get; set; }
        public IEnumerable<Rukavitsy> Rukavitsys { get; set; }
        public IEnumerable<Boty> Botys { get; set; }
        public IEnumerable<Tapochki> Tapochkis { get; set; }
        public IEnumerable<Kukany> Kukanys { get; set; }
        public IEnumerable<Bui_i_aksessuary> Bui_i_aksessuarys { get; set; }
        public IEnumerable<Khimicheskie_sredstva_i_masla> Khimicheskie_sredstva_i_maslas { get; set; }
        public IEnumerable<Katushki_khodovye> Katushki_khodovyes { get; set; }
        public IEnumerable<Chekhly_setki> Chekhly_setkis { get; set; }
        public IEnumerable<Mulyazhi> Mulyazhis { get; set; }
        public IEnumerable<Poleznye_aksessuary> Poleznye_aksessuarys { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}