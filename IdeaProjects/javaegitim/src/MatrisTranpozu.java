public class  MatrisTranspozu {
    public static void main(String[] args) {
        // Örnek 2x3'lük bir matris
        int[][] matrix = {
                {2, 3, 4},
                {5, 6, 4}
        };

        // Matrisin boyutlarını alıyoruz
        int row = matrix.length;
        int column = matrix[0].length;

        // Transpoze matrisin boyutları orijinalin tam tersi olur (3x2)
        int[][] transpose = new int[column][row];

        // Transpoze alma işlemi
        for (int i = 0; i < row; i++) {
            for (int j = 0; j < column; j++) {
                // i. satır j. sütundaki eleman, yeni matrisin j. satır i. sütununa geçer
                transpose[j][i] = matrix[i][j];
            }
        }

        // Sonuçları yazdırma
        System.out.println("Matris :");
        printMatrix(matrix);

        System.out.println("Transpoze :");
        printMatrix(transpose);
    }

    // Matrisleri düzgün bir şekilde ekrana yazdırmak için yardımcı metod
    public static void printMatrix(int[][] matrix) {
        for (int[] rows : matrix) {
            for (int col : rows) {
                System.out.print(col + "    ");
            }
            System.out.println();
        }
    }
}
