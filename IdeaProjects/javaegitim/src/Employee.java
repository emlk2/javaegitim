class Employee {
    String name;
    double salary;
    int workHours;
    int hireYear;

    public Employee(String name, double salary, int workHours, int hireYear) {
        this.name = name;
        this.salary = salary;
        this.workHours = workHours;
        this.hireYear = hireYear;
    }

    public double tax() {
        if (this.salary < 1000) {
            return 0.0;
        } else {
            return this.salary * 0.03;
        }
    }

    public double bonus() {
        if (this.workHours > 40) {
            return (this.workHours - 40) * 30.0;
        }
        return 0.0;
    }

    public double raiseSalary() {
        int currentYear = 2021;
        int workedYears = currentYear - this.hireYear;

        if (workedYears < 10) {
            return this.salary * 0.05;
        } else if (workedYears > 9 && workedYears < 20) {
            return this.salary * 0.10;
        } else {
            return this.salary * 0.15;
        }
    }

    @Override
    public String toString() {
        double currentTax = tax();
        double currentBonus = bonus();
        double currentRaise = raiseSalary();
        double salaryWithTaxAndBonus = this.salary - currentTax + currentBonus;
        double totalSalary = this.salary + currentRaise + currentBonus - currentTax;

        return "Adı : " + this.name + "\n" +
                "Maaşı : " + this.salary + "\n" +
                "Çalışma Saati : " + this.workHours + "\n" +
                "Başlangıç Yılı : " + this.hireYear + "\n" +
                "Vergi : " + currentTax + "\n" +
                "Bonus : " + currentBonus + "\n" +
                "Maaş Artışı : " + currentRaise + "\n" +
                "Vergi ve Bonuslar ile birlikte maaş : " + salaryWithTaxAndBonus + "\n" +
                "Toplam Maaş : " + totalSalary;
    }
}

public class Main {
    public static void main(String[] args) {
        Employee emp = new Employee("kemal", 2000.0, 45, 1985);
        System.out.println(emp.toString());
    }
}
