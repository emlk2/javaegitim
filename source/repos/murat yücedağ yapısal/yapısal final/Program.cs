using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yapısal_final_youtube
{
    //-------------------------OVERLOAD  AŞIRI YÜKLEME--------------------------------------------------------------------------

    //Bir metodun aynı isimle baska bir versiyonu olabilir

    public class Student

    {

        public int Idd { get; set; }
        public string Name { get; set; }

        public string StudentInfo(string today)
        {
            return $"{Name}" + "isimli öğrenci " + today + "tarihinde ve saatinde okula giriş yapmıştır. ";
        }
        public string StudentInfo(int student_no)
        {
            return $"{Name}" + "isimli öğrencinin öğrenci numarası: " + student_no;

        }
        public string StudentInfo()
        {
            return "öğrencinin adı" + Name;
        }
        //$ iç içe geçmiş metin,metin içi değişken yerleştirme.



    }
    internal class yapısal
    {
        static void Main(string[] args)
        {
            Student student1 = new Student();
            student1.Idd = 123;

            student1.Name = "emel";
         
            Console.WriteLine( student1.StudentInfo());
            Console.Read();
        }
    }
}
