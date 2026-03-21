using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace murat_yücedağ_yapısal
{
    internal class ucak
    {
        private string marka;
        private string kalkis;
        private string varis;

        public string MARKA
        {
            get { return marka; }
            set { marka = value.ToUpper(); }
        }
        public string KALKİS
        {
            get { return kalkis; }
            set { kalkis = value.ToLower(); }


        }

        public string VARİS
        {
            get { return varis; }
            set { varis=value .ToUpper(); }
        }
    }  
}
