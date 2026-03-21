using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8
{
    public class Otomobil
    {
        public string Marka {  get; set; }
        public string Model { get; set; }
        public int ÜretimYılı {  get; set; }
        public int Km {  get; set; }
        public int MotorNumarası { get; set; }
        public int ŞasiNumarası { get; set; }
        public Otomobil(string _Marka)
        {
            this.Marka = _Marka;
        }


    }
}
