public class BHarfi {
    public static void main(String[] args) {
        // 7 satır, 4 sütunluk bir matris oluşturuyoruz
        String[][] letter = new String[7][4];

        for (int i = 0; i < letter.length; i++) {
            for (int j = 0; j < letter[i].length; j++) {
                // i == 0 (Üst), i == 3 (Orta), i == 6 (Alt) yatay çizgiler
                if (i == 0 || i == 3 || i == 6) {
                    letter[i][j] = " * ";
                }
                // j == 0 (Sol sütun) ve j == 3 (Sağ sütun)
                else if (j == 0 || j == 3) {
                    letter[i][j] = " * ";
                }
                // İç kısımdaki boşluklar
                else {
                    letter[i][j] = "   ";
                }
            }
        }

        // Matrisi ekrana yazdırma
        for (String[] row : letter) {
            for (String col : row) {
                System.out.print(col);
            }
            System.out.println();
        }
    }
}