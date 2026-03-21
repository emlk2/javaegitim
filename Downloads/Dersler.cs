using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Dersler
    {
        public string DersAdı {  get; set; }

        public Akademisyen DersÖğretimElemanı { get; set; }

        public string DersKodu { get; set; }

        public int Uygulama { get; set; }

        public int Teori { get; set; }

        public Dersler(string _DersKodu)
        {
            this.DersKodu = _DersKodu;
        }

        public string DersKoduSorgula(string _dersKodu)
        {
            //_dersKodu ile DB'den sorgulama
            return DersAdı;
        }

        public int DersHaftalıkSaatGetir()
        {
            return this.Uygulama + this.Teori;
        }

        public override string ToString()
        {
            string bilgi = this.DersKodu + ": " + this.DersAdı;
            return bilgi;
        }
    }
}
