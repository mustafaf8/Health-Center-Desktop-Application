using System;
using System.Windows.Forms;

namespace Hasta_takip
{
    public partial class yonetici_ekrani : Form
    {
        public yonetici_ekrani()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        
            string yoneticiAdi = textBox1.Text; 
            string sifre = textBox2.Text;    

            
            if (yoneticiAdi == "nahsan" && sifre == "123")
            {
                db_ekrani lgn3 = new db_ekrani();

                lgn3.Show();
                this.Hide();
            }
            else
            {
                // Yanlışsa hata mesajı gösterilir
                MessageBox.Show("Yönetici adı veya şifre hatalı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
