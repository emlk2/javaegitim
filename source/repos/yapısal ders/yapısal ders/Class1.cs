using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yapısal_ders
{
    public class Öğrenci

    {
        public int ÖgrenciNo { get; set; }
        public string isim { get; set; }
        public string soyisim { get; set; }
        private long tcno;
        public long TcNo
        {
            get { return tcno; }
            set { tcno = value; }
        }
       public string Fakülte { get; set; }
        public string Bölüm {  get; set; }
        public string AkademikPersonel {  get; set; }
        public Lectures AldüigiDersler { get; set; }
        

                   

        
    


    }
    

}