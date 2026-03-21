using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AkademikPersonel
    {
        public partial class Form1
        {
        private void Bt(object sender, EventArgs e)
        {
            Fakülteler müh = new Fakülteler();
            müh.id = 1;
            müh.fakülteadı = "mühendislik";

            Fakülteler tıp = new Fakülteler();
            tıp.id = 2;
            tıp.fakülteadı = "tıp fakültesi";

            bölümler bilg = new bölümler();
            bilg.id = 1;
            bilg.bölümadı = "bilgisayar mühendsiliği";
            bilg.fakültesi = müh;

            bölümler endüstri = new bölümler();
            endüstri.id = 2;
            endüstri.bölümadı = "endüstri mühendisliği";
            endüstri.fakültesi = müh;

            bölümler tıpbölüm = new bölümler();
            tıpbölüm.id = 3;
            tıpbölüm.bölümadı = "tıp";
            tıpbölüm.fakültesi= tıp;

            akademik_personel akademik1 = new akademik_personel();
            akademik1.ad = "erdal";
            akademik1.soyad = "kare";
            akademik1.fakültesi = müh;
            akademik1.unvan = Unvan.DrÖğretimÜyesi;
            akademik1.dogumtarihi = DateTime.Parse("01.11.1986");
            akademik1.bölüm = bilg;

            dersler ders1 = new dersler();
            ders1.dersinhocası = akademik1;
            ders1.dersadı = "ptg";
            ders1.id = 1;

            dersler[] öğrencidersleri = new dersler[2];
            öğrencidersleri[0] = ders1;
            öğrencidersleri[1] = ders2;

            student öğrenci1 = new student();
            öğrenci1.isimsoyisim = "mehmet test";
            öğrenci1.bölümü= bilg;
            öğrenci1.fakültesi = müh;
            öğrenci1.id = 1;
            öğrenci1.aldıgıdersler = öğrencidersleri;





        }

    }
        }
    }
    }
}
