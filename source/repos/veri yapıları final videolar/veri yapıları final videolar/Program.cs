using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace veri_yapıları_final_videolar
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //4 ARALIK
            //Stack <char> st=new Stack<char>();
            //st.Push('S');
            //char chr = ' ';

            //string infix = "a+b*(a-b*c+d)-f/g";//abc*+d-
            //Console.WriteLine("ababc*-d+*+fg/-");
            //string postfix = "";
            //string ch = "$(+-*/)";
            //string pr = "001122";
            //for (int i = 0; i < infix.Length; i++)
            //{
            //    int index = ch.IndexOf(infix[i]);
            //    if (index==-1) { postfix = postfix + infix[i]; continue; }
            //    if (index == 1) { st.Push('('); continue; }
            //    if (index == 6)
            //    {
            //        while (st.Push() !='(') postfix = postfix + st.Pop();
            //        st.Pop();
            //        continue;

            //    }
            //    while (pr[ch.Length.IndexOf(st.Peek())] >= pr[index])
            //    {
            //        postfix = postfix + st.Pop();

            //    }
            //    st.Push(infix[i]);

            //}
            ////stakta linked list kullanacağız.
            //while (st.Peek() != '$')
            //    postfix = postfix + st.Pop();
            //Console.WriteLine(postfix);
            //-----------------------------------------------------------------
            int topla1(int v1, int v2)
            {
                return v1 + v2;
            }
            int fark1(int v1, int v2)
            {
                return v1 - v2;
            }
            int carp1(int  v1, int v2)
            {
                return v1 * v2;
            }
            int divide1(int v1,int v2)
            {
                if (v2 == 0)
                {
                    // Hata yönetimi (örneğin bir istisna atılabilir veya hata kodu döndürülebilir)
                    Console.WriteLine("Hata: Sifira bolme girisimi!\n");
                    return 0;
                }
            }
                    string postfix = "abcd/-*e+";
            //a,b,c,d,e
            //d,h,j,n,v
            string operands = "abcde";
            string[] vars = { "en", "boy", "yükseklik" };
            int[] values = { 1, 2, 3, 3, 4, };
            Stack<int> st = new Stack<int>();
            string var1 = "";
            for(int i = 0; i < postfix.Length; i++)
            {
                int index = operands.IndexOf(postfix[i]);
                if (index != -1)
                {
                    st.Push(values[index]);
                    continue;

                }
                int v2 = st.Pop();
                int v1 = st.Pop();

                int sonuc = 0;
                if (postfix[i] == '+') sonuc = topla1(v1, v2);
                if (postfix[i] == '-') sonuc = fark1(v1, v2);
                if (postfix[i] == '*') sonuc == carp1(v1, v2);
                if (postfix[i] == '/') sonuc = divide1(v1, v2);

                st.Push(sonuc);


            }
            Console.WriteLine(st.Pop());
            

        }
    }
}
