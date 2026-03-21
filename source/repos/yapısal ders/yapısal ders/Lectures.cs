using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yapısal_ders
{
    internal class Lectures
    {
        public string DersAdı { get; set; }
        public string Fakülte { get; set; }
        public string Bölüm {  get; set; }
        public AkademikPersonel ÖğretimÜyesi { get; set; }
        public Lectures[] AldıgıDersler {  get; set; }

    }
}
