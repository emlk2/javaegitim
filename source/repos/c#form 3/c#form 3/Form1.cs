using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace c_form_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.Items.Add("elma");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(tboxisim.Text.ToString());
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)//seçili olan index değiştiğinde:,

        {
            lblindex.Text =listBox1.SelectedIndex.ToString();
            lblisim.Text = listBox1.SelectedItem.ToString();


        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)//işaretleme değiştiğinde:
        {
            if (checkBox1.Checked==true)
            {
                string isim = checkBox1.Text.ToString();
                listBox1.Items.Add(isim);

            }
            else
            {
                string isim = checkBox1.Text.ToString();
                listBox1.Items.Remove(isim);
            }
            //radio buttonın checkboxtan farkı birdwn fazla radıobutton olsa bile sadece biri ,işaretlenebilir,
            //ve işaretlendiğinde kaldırılamaz(şık gibi).
        }
    }
}
