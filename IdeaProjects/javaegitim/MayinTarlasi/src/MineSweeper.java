import java.util.Scanner;
import java.util.Random;

public class MineSweeper {
    int rowNumber, colNumber;
    String [][] map;
    String [][] board;
    public MineSweeper (int row,int col){
        this.rowNumber=row;
        this.colNumber=col;
        this.map=new String[row][col];
        this.board=new String [row][col];
        for(int i=0;i<row;i++){
            for(int j=0;j<col;j++){
                this.board [i][j]="-";
                this.map[i][j]="-";

            }
        }


    }
    public void placeMines(){
        Random rnd=new Random();
        int mineCount=(this.rowNumber * this.colNumber)/4;
        int count=0; //sayac
        while(count<mineCount){
           int randomrow =rnd.nextInt(rowNumber);
           int randomcol=rnd.nextInt(colNumber);

           //Java'da metinleri (String) karşılaştırırken == yerine her zaman .equals() metodunu kullanmalısın, bu çok önemli bir kuraldır.)
           if(this.map[randomrow][randomcol].equals('-')){
               this.map[randomrow][randomcol] = "*";
               count++;          }

        }

    }

    public void run(){
        Scanner scan=new Scanner(System.in);
        boolean isGameOver = false;
        System.out.println("===========================");
        System.out.println("Mayın Tarlası Oyununa Hoşgeldiniz !");
        while(!isGameOver) {
            printBoard();
            System.out.print("Satır giriniz: ");
            int r = scan.nextInt();
            System.out.print("Sütun giriniz: ");
            int c = scan.nextInt();


            if (r < 0 || r >= rowNumber || c < 0 || c >= colNumber) {
                System.out.print("Hatalı Koordinat");
                continue;


            }
            if (this.map[r][c].equals("*")) {
                isGameOver = true;
                System.out.println("GAME OVER!");
            }
        }

    }
    public void checkMines(int r, int c){
        int mineCount = 0;


        //Merkezimiz r ve c ise;
        //
        //Satırlarımız r - 1'den başlayıp r + 1'e kadar gitmeli.
        //
        //Sütunlarımız c - 1'den başlayıp c + 1'e kadar gitmeli.
        //Bu sayede (r, c)'nin etrafındaki 3x3'lük o küçük kareyi taramış oluruz.
        for(int i=r-1;i<=r+1;i++){
            for(int j=c-1;j<=c+1;j++){
                if(i>=0&&i<rowNumber &&j>=0&&j<colNumber){
                    if (this.map[i][j].equals("*")) {
                        mineCount++;
                    }

                }
            }
        }
        // Döngüler bittiğinde sepetteki sayıyı String'e çevirip ön yüze (board) yazıyoruz
        this.board[r][c] = String.valueOf(mineCount);
    }
    public void printBoard(){
        for(int i=0;i<this.rowNumber;i++){
            for(int j=0;j<this.colNumber;j++){
                System.out.print(this.board[i][j] + " ");
            }
            System.out.println();
        }
    }



}
