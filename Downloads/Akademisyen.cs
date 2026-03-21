using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Akademisyen
    {
        public string İsim { get; set; }

        public string Soyisim { get; set; }

        public string Fakülte { get; set; }

        public override string ToString()
        {
            return this.İsim + " " + this.Soyisim;

        }
    }
}
