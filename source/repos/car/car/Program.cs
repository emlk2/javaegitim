using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace car
{
    public class Taxi
    {
        public string DurakAdı { get; set; }
        public string DurakAdres { get; set; }
        public Driver[] DriverList { get; set; }
        public araba2[] CarList { get; set; }


    }
    public class Driver
    {
        public int yas {  get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string LicenseNumber { get; set; }
        public int AraçKullanımSaati { get; set; }
        public void DriverInfo()
        {
            string text = "";
            text =text+ "Name:" + this.Name + ";";
            text += "Surname:" + this.Surname + ";";
            text += "Lisans No:" + this.LicenseNumber + ";";
            Console.WriteLine(text);
        }
        public int yaşıdöndür()
        {
            int yası = this.yas;
            return yası;
        }

    }
    public  class Program
    {
        static void Main(string[] args)
        {
            
            #region Drivers
            Driver driver1 = new Driver();
            driver1.Name = "Halit";
            driver1.Surname = "Dağdelen";
            driver1.LicenseNumber = "1231231";
            driver1.DriverInfo();

            Driver driver2 = new Driver();
            driver2.Name = "Deneme";
            driver2.Surname = "Deneme2";
            driver2.LicenseNumber = "123231";
            //Console.WriteLine(driver2.DriverInfo());
            #endregion

            #region Cars
           araba2 car1 = new araba2();
            car1.Marka = "Hyundai";
            car1.Model = "Accent";
            car1.ModelYılı = 2010;
            car1.Plaka = "55 EA 2024";
            car1.CarDriver =driver2 ;
            car1.Renk = "Sarı";
            car1.UpdateColor("Yeni Sarı");

            araba2 car2 = new araba2();
            car2.Marka = "Mercedes";
            car2.Model = "E180";
            car2.Plaka = "55 EA 2024 ";
            car2.Renk = "Siyah";
            car2.ModelYılı = 2025;
            car2.CarDriver = driver1;
            car2.UpdateColor("Sarı");
            car2.UpdateMarkaModel("Ford", "Focus");
            bool result = car2.UpdateDriver(driver2);
            if (result)
            {
                //Console.WriteLine("Şoför güncellendi");
            }
            #endregion 

            Console.ReadLine();



        }
    }
}
