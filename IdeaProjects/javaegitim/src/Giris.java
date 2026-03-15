import java.util.Scanner;
public class Giris {
    public static void main(String[] args) {
        System.out.println("merhaba dünya");

        int a;
        Scanner input =new Scanner(System.in);
        a=input.nextInt();
        System.out.println(a);


    }
}
// \t tab boşluk ekler
//\b backspace ekler
//\n bir satır aşağı atlar
//\r metne bir satır başı ekler
// \ f eski bir tekniktir ve sayfa sonu belirtmek için kullanılır
// \' tek tırnak eklemek için kullanılır
// \" çift tırnak eklemeke
// \\ ters eğik çizgi eklemek






/* Byte
8 bit uzunluğundadır. Max 127 , Min -128 değerleri arasındadır.
Anahtar sözcük : byte
Short
16 bit uzunluğundadır. Max 32,767 , Min -32,768 değerleri arasındadır.
Anahtar sözcük : short
Integer
32 bit uzunluğundadır. Max 2,147,483,647 , Min -2,147,483,648 değerleri arasındadır.
En çok tercih edilen veri tipidir , sebebi ise optimize uzunluktadır.
Anahtar sözcük : int
Long
64 bit uzunluğundadır. Max 9,223,372,036,854,775,807 , Min -9,223,372,036,854,775,808 değerleri arasındadır.
Int’in yetersiz olduğu yerlerde kullanılır
Anahtar sözcük : long*/
