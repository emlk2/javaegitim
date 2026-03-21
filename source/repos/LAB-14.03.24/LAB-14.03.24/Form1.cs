namespace LAB_14._03._24
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            string ad = txt.Text;

            bool dogruMu = cb.Checked;
            if (dogruMu)
            {
                MessageBox.Show("yapýldý");

            }
            else
            {
                MessageBox.Show("yapýlmadý");
            }

            bool kadýnMý = radioButton1.Checked;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void cb_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txt_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value = 3;
        }
    }
}
