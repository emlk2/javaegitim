using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTG_ERDAL_HOCA_ÖRNEKLER
{
    internal class Program
    {
        static string mysubstring(int sayi, int kelime)
        {
            string temp = "";
            for(int i = 0; i < sayi; i++)
            {
                temp += kelime[i];
            }
            return temp;
        }
        static void Main(string[] args)
        {
            //split()
            //içerisine bir string karakter alan karaktere göre stringi parçalayıp parçalanmış stringi geri dönen metot 

            //substring metodu 
            //abc.substring(5)=>ilk 5 karakteri alıp çöpe attık
            //abc.substring(4,4)=>4.indiksten başlayıp dört tane alan
            //bir string bir int alıcaz sıfırdan başlayıp 
            //Equals içeriye aldığı stirngler aynı ise true farklı ise false

            Console.ReadKey();
        }
    }
}
