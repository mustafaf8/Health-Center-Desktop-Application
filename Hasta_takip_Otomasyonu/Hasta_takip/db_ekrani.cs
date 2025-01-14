using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Hasta_takip
{
    public partial class db_ekrani : Form
    {
        private string connectionString = "Server=localhost;Database=HastaTakipDB;Uid=root;Pwd=;"; // Bağlantı dizesi

        public db_ekrani()
        {
            InitializeComponent();
            LoadData();
        }

        // Form yüklendiğinde veritabanındaki bilgileri yükle
        private void db_ekrani_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Doktorlar tablosunu yükle
                    string doktorlarQuery = "SELECT * FROM doktorlar";
                    MySqlDataAdapter doktorlarAdapter = new MySqlDataAdapter(doktorlarQuery, connection);
                    DataTable doktorlarTable = new DataTable();
                    doktorlarAdapter.Fill(doktorlarTable);
                    dataGridView1.DataSource = doktorlarTable;

                    // İşlem detay tablosunu yükle
                    string islemDetayQuery = "SELECT * FROM islem_detay";
                    MySqlDataAdapter islemDetayAdapter = new MySqlDataAdapter(islemDetayQuery, connection);
                    DataTable islemDetayTable = new DataTable();
                    islemDetayAdapter.Fill(islemDetayTable);
                    dataGridView2.DataSource = islemDetayTable;

                    // Poliklinikler tablosunu yükle
                    string polikliniklerQuery = "SELECT * FROM poliklinikler";
                    MySqlDataAdapter polikliniklerAdapter = new MySqlDataAdapter(polikliniklerQuery, connection);
                    DataTable polikliniklerTable = new DataTable();
                    polikliniklerAdapter.Fill(polikliniklerTable);
                    dataGridView3.DataSource = polikliniklerTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Doktor ekleme ve doktorları listeleme
        private void btn1_Click(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string insertQuery = "INSERT INTO doktorlar (AD, SOYAD) VALUES (@ad, @soyad)";
                    MySqlCommand cmd = new MySqlCommand(insertQuery, connection);
                    cmd.Parameters.AddWithValue("@ad", textBox1.Text);
                    cmd.Parameters.AddWithValue("@soyad", textBox2.Text);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Doktor başarıyla eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // DataGridView'de doktorlar tablosunu güncelle
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // İşlem ekleme ve işlemleri listeleme
        private void btn2_Click(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string insertQuery = "INSERT INTO islem_detay (ISLEM_ADI, BIRIM_FIYAT) VALUES (@islemId, @birimFiyat)";
                    MySqlCommand cmd = new MySqlCommand(insertQuery, connection);
                    cmd.Parameters.AddWithValue("@islemId", textBox3.Text);
                    cmd.Parameters.AddWithValue("@birimFiyat", textBox4.Text);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("İşlem başarıyla eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // DataGridView'de islem_detay tablosunu güncelle
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Poliklinik ekleme ve poliklinikleri listeleme
        private void btn3_Click(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string insertQuery = "INSERT INTO poliklinikler (POLIKLINIK_ADI) VALUES (@poliklinikAdi)";
                    MySqlCommand cmd = new MySqlCommand(insertQuery, connection);
                    cmd.Parameters.AddWithValue("@poliklinikAdi", textBox5.Text);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Poliklinik başarıyla eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // DataGridView'de poliklinikler tablosunu güncelle
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // İşlem ekranına geçiş
        private void btnIslemKayit_Click(object sender, EventArgs e)
        {
            islem_ekrani islemEkrani = new islem_ekrani();
            islemEkrani.Show();
            this.Hide();
        }

        // Uygulamadan çıkış
        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
