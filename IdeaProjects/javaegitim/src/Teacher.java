class Teacher {
    String name;
    String mpno;
    String branch;

    public Teacher(String name, String mpno, String branch) {
        this.name = name;
        this.mpno = mpno;
        this.branch = branch;
    }
}

class Course {
    Teacher courseTeacher;
    String name;
    String code;
    String prefix;
    int note;
    int oralNote;
    double oralWeight;

    public Course(String name, String code, String prefix, double oralWeight) {
        this.name = name;
        this.code = code;
        this.prefix = prefix;
        this.note = 0;
        this.oralNote = 0;
        this.oralWeight = oralWeight;
    }

    public void addTeacher(Teacher t) {
        if (this.prefix.equals(t.branch)) {
            this.courseTeacher = t;
            System.out.println("İşlem başarılı");
        } else {
            System.out.println(t.name + " Akademisyeni bu dersi veremez.");
        }
    }

    public void printTeacher() {
        if (courseTeacher != null) {
            System.out.println(this.name + " dersinin Akademisyeni : " + courseTeacher.name);
        } else {
            System.out.println(this.name + " dersine Akademisyen atanmamıştır.");
        }
    }
}

class Student {
    String name, stuNo;
    int classes;
    Course mat;
    Course fizik;
    Course kimya;
    double avarage;
    boolean isPass;

    public Student(String name, int classes, String stuNo, Course mat, Course fizik, Course kimya) {
        this.name = name;
        this.classes = classes;
        this.stuNo = stuNo;
        this.mat = mat;
        this.fizik = fizik;
        this.kimya = kimya;
        this.isPass = false;
    }

    public void addBulkExamNote(int mat, int fizik, int kimya) {
        if (mat >= 0 && mat <= 100) this.mat.note = mat;
        if (fizik >= 0 && fizik <= 100) this.fizik.note = fizik;
        if (kimya >= 0 && kimya <= 100) this.kimya.note = kimya;
    }

    public void addBulkOralNote(int matOral, int fizikOral, int kimyaOral) {
        if (matOral >= 0 && matOral <= 100) this.mat.oralNote = matOral;
        if (fizikOral >= 0 && fizikOral <= 100) this.fizik.oralNote = fizikOral;
        if (kimyaOral >= 0 && kimyaOral <= 100) this.kimya.oralNote = kimyaOral;
    }

    public void isPass() {
        if (this.mat.note == 0 || this.fizik.note == 0 || this.kimya.note == 0) {
            System.out.println("Notlar tam olarak girilmemiş");
        } else {
            this.isPass = isCheckPass();
            printNote();
            System.out.println("Genel Ortalama : " + this.avarage);
            if (this.isPass) {
                System.out.println("Sınıfı Geçti.");
            } else {
                System.out.println("Sınıfta Kaldı.");
            }
        }
    }

    public void calcAvarage() {
        double matAvg = (this.mat.note * (1.0 - this.mat.oralWeight)) + (this.mat.oralNote * this.mat.oralWeight);
        double fizikAvg = (this.fizik.note * (1.0 - this.fizik.oralWeight)) + (this.fizik.oralNote * this.fizik.oralWeight);
        double kimyaAvg = (this.kimya.note * (1.0 - this.kimya.oralWeight)) + (this.kimya.oralNote * this.kimya.oralWeight);

        this.avarage = (matAvg + fizikAvg + kimyaAvg) / 3.0;
    }

    public boolean isCheckPass() {
        calcAvarage();
        return this.avarage > 55;
    }

    public void printNote() {
        System.out.println("=========================");
        System.out.println("Öğrenci : " + this.name);
        System.out.println("Matematik Sınav Notu : " + this.mat.note + " | Sözlü Notu : " + this.mat.oralNote);
        System.out.println("Fizik Sınav Notu : " + this.fizik.note + " | Sözlü Notu : " + this.fizik.oralNote);
        System.out.println("Kimya Sınav Notu : " + this.kimya.note + " | Sözlü Notu : " + this.kimya.oralNote);
    }
}

public class Main {
    public static void main(String[] args) {

        Course mat = new Course("Matematik", "MAT101", "MAT", 0.20);
        Course fizik = new Course("Fizik", "FZK101", "FZK", 0.20);
        Course kimya = new Course("Kimya", "KMY101", "KMY", 0.30);

        Teacher t1 = new Teacher("Mahmut Hoca", "90550000000", "MAT");
        Teacher t2 = new Teacher("Fatma Ayşe", "90550000001", "FZK");
        Teacher t3 = new Teacher("Ali Veli", "90550000002", "KMY");

        mat.addTeacher(t1);
        fizik.addTeacher(t2);
        kimya.addTeacher(t3);

        Student s1 = new Student("İnek Şaban", 4, "140144015", mat, fizik, kimya);
        s1.addBulkExamNote(50, 20, 40);
        s1.addBulkOralNote(80, 90, 85);
        s1.isPass();

        Student s2 = new Student("Güdük Necmi", 4, "2211133", mat, fizik, kimya);
        s2.addBulkExamNote(100, 50, 40);
        s2.addBulkOralNote(90, 80, 70);
        s2.isPass();

        Student s3 = new Student("Hayta İsmail", 4, "221121312", mat, fizik, kimya);
        s3.addBulkExamNote(50, 20, 40);
        s3.addBulkOralNote(50, 60, 55);
        s3.isPass();
    }
}