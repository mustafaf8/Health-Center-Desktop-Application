using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Hasta_takip
{
    public partial class hasta_ekrani : Form

    {
        
        private string connectionString = "Server=localhost;Database=HastaTakipDB;Uid=root;Pwd=;";

        private MySqlDataAdapter dataAdapter;
        private DataTable dataTable;

        public hasta_ekrani()
        {
            InitializeComponent();
        }

        private void login_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void LoadData()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                
                string sqlQuery = "SELECT * FROM HASTALAR";

                // MySqlDataAdapter ve DataTable kullanarak verileri çekme
                dataAdapter = new MySqlDataAdapter(sqlQuery, connection);
                dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                // DataGridView'e verileri yükleme
                dataGridView1.DataSource = dataTable;
            }
        }

        private void Temizle()
        {
            textBox1.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            pictureBox1.ImageLocation = "";
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string insertQuery = "INSERT INTO HASTALAR (TC, AD, SOYAD, ADRES, TELEFON, G_TARIHI, RESIM) " +
                                         "VALUES (@param1, @param2, @param3, @param4, @param5, @param8, @param10)";

                    using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@param1", textBox6.Text);
                        command.Parameters.AddWithValue("@param2", textBox1.Text);
                        command.Parameters.AddWithValue("@param3", textBox8.Text);
                        command.Parameters.AddWithValue("@param4", textBox5.Text);
                        command.Parameters.AddWithValue("@param5", textBox4.Text);
                        command.Parameters.AddWithValue("@param8", dateTimePicker1.Value);
                        command.Parameters.AddWithValue("@param10", textBox9.Text);

                        if (textBox5.Text == "" || textBox1.Text == "" || textBox8.Text == "")
                        {
                            MessageBox.Show("Lütfen Doldurulması Zorunlu Alanları Doldurunuz");
                        }
                        else
                        {
                            command.ExecuteNonQuery();
                            MessageBox.Show("Yeni Kayıt Başarıyla Eklendi.");
                        }
                    }

                    LoadData();
                    Temizle();
                }
            }
            catch
            {
                MessageBox.Show("Hatalı İşlem!!!");
            }
        }

        private void btnYukle_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosya = new OpenFileDialog();
            dosya.Filter = "C://Users//musta//source//repos//Hasta_takip_Otomasyonu//Resimler |*.jpg; *.png";
            dosya.ShowDialog();
            string dosyayolu = dosya.FileName;
            textBox9.Text = dosyayolu;
            pictureBox1.ImageLocation = dosyayolu;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //text boxlar 
            textBox6.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox8.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            pictureBox1.ImageLocation = dataGridView1.CurrentRow.Cells[6].Value.ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string deleteQuery = "DELETE FROM HASTALAR WHERE TC = @param1";

                    using (MySqlCommand command = new MySqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@param1", textBox6.Text);

                        if (textBox6.Text == "")
                        {
                            MessageBox.Show("Lütfen TC Kısmını Doldurunuz.");
                        }
                        else
                        {
                            int sonuc = command.ExecuteNonQuery();
                            if (sonuc == 1)
                            {
                                MessageBox.Show("Mevcut Kayıt Başarıyla Silindi");
                            }
                            else
                            {
                                MessageBox.Show("Lütfen Silmek İstediğiniz Kişinin TC Kimlik Numarasını Doğru Giriniz.");
                            }
                            LoadData();
                            Temizle();
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Hatalı İşlem Lütfen Tekrar Deneyiniz.");
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string updateQuery = "UPDATE HASTALAR SET AD=@param2, SOYAD=@param3, ADRES=@param4, TELEFON=@param5, G_TARIHI=@param8, RESIM=@param10 WHERE TC=@param1";

                    using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@param1", textBox6.Text);
                        command.Parameters.AddWithValue("@param2", textBox1.Text);
                        command.Parameters.AddWithValue("@param3", textBox8.Text);
                        command.Parameters.AddWithValue("@param4", textBox5.Text);
                        command.Parameters.AddWithValue("@param5", textBox4.Text);
                        command.Parameters.AddWithValue("@param8", dateTimePicker1.Value);
                        command.Parameters.AddWithValue("@param10", textBox9.Text);

                        if (textBox5.Text == "" || textBox1.Text == "" || textBox8.Text == "")
                        {
                            MessageBox.Show("Lütfen Doldurulması Zorunlu Alanları Doldurunuz");
                        }
                        else
                        {
                            command.ExecuteNonQuery();
                            MessageBox.Show("Mevcut Kayıt Başarıyla Güncellendi.");
                            LoadData();
                            Temizle();
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Hatalı İşlem Lütfen Tekrar Deneyiniz.");
            }
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Veri çekme sorgusu
                string sqlQuery = "SELECT * FROM HASTALAR WHERE TC LIKE @param1TC";

                // MySqlDataAdapter ve DataTable kullanarak verileri çekme
                dataAdapter = new MySqlDataAdapter(sqlQuery, connection);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@param1TC", textBox10.Text + "%");

                dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                // DataGridView'e verileri yükleme
                dataGridView1.DataSource = dataTable;
            }
        }

        private void btnİslemKayit_Click(object sender, EventArgs e)
        {
            islem_ekrani lgn2 = new islem_ekrani();

            lgn2.Show();
            this.Hide();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
