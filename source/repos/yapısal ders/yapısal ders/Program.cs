using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yapısal_ders
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region ÖĞRENCİ NESNESİ
            #endregion SINIF NESNE

            Öğrenci student1 = new Öğrenci();
            student1.Fakülte = "mühwndislik";
            student1.Bölüm = "bilgisayar";
            student1.ÖgrenciNo = 123;
            student1.TcNo = 345;
            student1.isim = "emel";
            student1.soyisim = "kılıç";
            student1.Danısman = akademik1;

            #region AKADEMİK PERSONEL NESNESİ
            #endregion 
            AkademikPersonel akademik1 = new AkademikPersonel();
            akademik1.Faculty = "Engineer";
            akademik1.Department = "Computer";
            akademik1.Name = "Deneme1";
            akademik1.Surname = "Deneme2";
            akademik1.AkademikUnvan = Unvan.Prof;

            #region DERS NESNESİ
            #endregion
            Lectures ders1 = new Lectures();
            ders1.DersAdı = "PTG";
            ders1.Fakülte = "Mühendislik";
            ders1.ÖğretimÜyesi = akademik1;



        }
    }
}
