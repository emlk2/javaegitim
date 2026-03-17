import java.util.Arrays;

public class DizideFrekans{
    public static void main(String[] args) {
        int[] list = {10, 20, 20, 10, 10, 20, 5, 20};

        // Daha önce sayılan sayıları işaretlemek için bir dizi oluşturuyoruz
        // Orijinal dizi ile aynı boyutta olacak
        boolean[] visited = new boolean[list.length];
        Arrays.fill(visited, false);

        System.out.println("Dizi : " + Arrays.toString(list));
        System.out.println("Tekrar Sayıları");

        for (int i = 0; i < list.length; i++) {

            // Eğer bu eleman daha önce sayıldıysa (visited[i] true ise) atla
            if (visited[i]) {
                continue;
            }

            int count = 1; // Her sayı kendisinden 1 tane barındırır
            for (int j = i + 1; j < list.length; j++) {
                if (list[i] == list[j]) {
                    visited[j] = true; // Aynı sayı bulunduğunda onu "ziyaret edildi" olarak işaretle
                    count++;
                }
            }

            System.out.println(list[i] + " sayısı " + count + " kere tekrar edildi.");
        }
    }
}
