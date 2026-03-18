import java.util.Scanner;

class Main{
    public static  void main (String  [] args){
        Scanner scan = new Scanner(System.in);

        System.out.println("Mayın Tarlası boyutlarını belirleyin (En az 2x2 olmalı).");
        System.out.print("Satır sayısı: ");
        int row= scan.nextInt();
        System.out.print("Sutun sayısı: ");
        int col= scan.nextInt();
        MineSweeper oyun = new MineSweeper(row, col);

        // Ve oyunu başlatıyoruz!
        oyun.run();
    }

}

