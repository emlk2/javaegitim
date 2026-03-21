using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace car
{
    public class araba2

    {
        public string Marka { get; set; }
        public string Model { get; set; }
        public int ModelYılı { get; set; }
        public string Plaka { get; set; }
        public string Otomatik { get; set; }
        public string Renk {  get; set; }
        public Driver CarDriver { get; set; }
        public void UpdateColor(string color)
        {
            this.Renk = color;
        }
        public void UpdateMarkaModel(string marka,string model)
        {
            this.Marka = marka;
            this.Model = model;
        }
        public bool UpdateDriver(Driver newDriver)
        {
            this.CarDriver = newDriver;
            return true;
        }
    }
}
