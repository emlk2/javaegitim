import java.util.Scanner;

public class Fibonacci {
    public static void main(String[] args) {
        Scanner input = new Scanner(System.in);
        int n=input.nextInt();
        int n1=0;
        int n2=0;
        int n3;
        for(int i =1;i<=n;i++) {

            System.out.print(n1+ " ");
            n3=n1+n2;
            n1=n2;
            n2=n3;

        }
        }

    }
}