using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace c_form
{
    public partial class tboxşifre : Form
    {
        public tboxşifre()
        {
            InitializeComponent();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ka = tboxkullanıcıadı.Text;
            string sifre = tboxsifre.Text;

            if (ka == "Admin")
            {
                if (sifre == "emel123")
                {
                    lblsonuc.Text = "GİRİŞ BAŞARILI.";
                }
                else
                {
                    lblsonuc.Text = "ŞİFRENİZ YANLIŞ";
                }
            }
            else
            {
                lblsonuc.Text = "KULLANICI ADINIZ YANLIŞ.";
            }


        }

        
}
