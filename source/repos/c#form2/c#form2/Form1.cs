using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace c_form2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Samsun");
            comboBox1.Items.Add("Ankara");
            comboBox1.Items.Add("Diyarbakır");
            comboBox1.Items.Add("Mersin");
            comboBox1.Items.Add("İstanbul");
        }

        private void btnsec_Click(object sender, EventArgs e)
        {
            string secilenSehir = comboBox1.SelectedItem.ToString();
            lblsonuc.Text = $"{secilenSehir}Şehrini seçtiniz.";

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)//butona gerek kalmadan seçildiğini yazdırdı.
        {
            string secilenSehir = comboBox1.SelectedItem.ToString();
            lblsonuc.Text = $"{secilenSehir}Şehrini seçtiniz.";
        }
    }
}

