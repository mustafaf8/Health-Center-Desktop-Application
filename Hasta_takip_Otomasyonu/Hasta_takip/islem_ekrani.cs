using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;  // Use MySQL namespace
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Hasta_takip
{
    public partial class islem_ekrani : Form
    {
        private string connectionString = "Server=localhost;Database=HastaTakipDB;User=root;Password=;";
        private MySqlDataAdapter dataAdapter;
        private DataTable dataTable;
        

        private void btnYazdir_Click(object sender, EventArgs e)
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);

            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            printPreviewDialog.Document = printDocument;

            printPreviewDialog.ShowDialog();
        }

        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Bitmap bmp = new Bitmap(dataGridView1.Width, dataGridView1.Height);
            dataGridView1.DrawToBitmap(bmp, new Rectangle(0, 0, dataGridView1.Width, dataGridView1.Height));
            e.Graphics.DrawImage(bmp, 50, 50);
        }

        public islem_ekrani()
        {
            InitializeComponent();
        }

        private void Diger_Islemler_Ekrani_Load(object sender, EventArgs e)
        {
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT * FROM YAPILAN_ISLEMLER";

                dataAdapter = new MySqlDataAdapter(sqlQuery, connection);
                dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Temizle()
        {
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            numericUpDown1.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            label15.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT * FROM YAPILAN_ISLEMLER WHERE ISLEM_ID LIKE @param1TC";

                dataAdapter = new MySqlDataAdapter(sqlQuery, connection);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@param1TC", textBox1.Text + "%");

                dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string sqlQuery = "SELECT * FROM HASTALAR WHERE AD LIKE @param1Ad";
                dataAdapter = new MySqlDataAdapter(sqlQuery, connection);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@param1Ad", textBox3.Text + "%");

                dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
                // Eğer sonuç varsa, ilk satırdaki TC'yi textBox6'ya ekle
                if (dataTable.Rows.Count > 0)
                {
                    
                    string tcValue = dataTable.Rows[0]["TC"].ToString();
                    textBox6.Text = tcValue;
                }
                else
                {
                    // Eğer sonuç yoksa textBox6'yı temizle
                    textBox6.Text = string.Empty;
                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT * FROM HASTALAR WHERE SOYAD LIKE @param1Soyad";
                dataAdapter = new MySqlDataAdapter(sqlQuery, connection);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@param1Soyad", textBox4.Text + "%");

                dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
        }

        

        private void btnYeni_Click(object sender, EventArgs e)
        {
            hasta_ekrani lgn3 = new hasta_ekrani();
            lgn3.Show();
            this.Hide();
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT POLIKLINIK_ID FROM POLIKLINIKLER";

                dataAdapter = new MySqlDataAdapter(sqlQuery, connection);
                dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                comboBox1.DataSource = dataTable;
                comboBox1.DisplayMember = "POLIKLINIK_ID";
            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                int randomIslemId = GenerateRandomIslemId();

                string insertQuery = "INSERT INTO YAPILAN_ISLEMLER (TC, POLIKLINIK_ID, YAPILAN_ISLEM_ID, DOKTOR_ID, MIKTAR, BIRIM_FIYAT, TOPLAM) " +
                                    "VALUES (@param1, @param2, @param3, @param4, @param5, @param6, @param7)";

                using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@param1", Convert.ToInt64(textBox6.Text));
                    command.Parameters.AddWithValue("@param2", Convert.ToInt64(((DataRowView)comboBox1.SelectedItem)["POLIKLINIK_ID"]));
                    command.Parameters.AddWithValue("@param3", randomIslemId);
                    DataRowView selectedRow = (DataRowView)comboBox3.SelectedItem;
                    int doktorID = Convert.ToInt32(selectedRow["DOKTOR_ID"]);
                    command.Parameters.AddWithValue("@param4", doktorID);
                    command.Parameters.AddWithValue("@param5", Convert.ToInt64(numericUpDown1.Value));
                    command.Parameters.AddWithValue("@param6", Convert.ToInt64(textBox7.Text));
                    command.Parameters.AddWithValue("@param7", Convert.ToInt64(numericUpDown1.Value) * Convert.ToInt64(textBox7.Text));
                    command.ExecuteNonQuery();

                    int toplamTutar = Convert.ToInt32(numericUpDown1.Value) * Convert.ToInt32(textBox7.Text);
                    label15.Text = toplamTutar.ToString();

                    MessageBox.Show("Yeni İşlem Başarıyla Eklendi.");
                }
            }
            //HesaplaVeToplamYaz();
        }

        private int GenerateRandomIslemId()
        {
            Random random = new Random();
            return random.Next(10000, 19999);
        }

        private void comboBox2_DropDown_1(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT ISLEM_ID FROM ISLEM_DETAY";
                dataAdapter = new MySqlDataAdapter(sqlQuery, connection);
                dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                comboBox2.DataSource = dataTable;
                comboBox2.DisplayMember = "ISLEM_ID";
            }
        }

        private void comboBox3_DropDown_1(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string sqlQuery = "SELECT DOKTOR_ID FROM DOKTORLAR";
                dataAdapter = new MySqlDataAdapter(sqlQuery, connection);
                dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                comboBox3.DataSource = dataTable;
                comboBox3.DisplayMember = "DOKTOR_ID";
            }
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string sqlQuery = "SELECT * FROM YAPILAN_ISLEMLER";

                dataAdapter = new MySqlDataAdapter(sqlQuery, connection);
                dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                Temizle();
            }
        }

        private void Sil(int islemID)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string deleteQuery = "DELETE FROM YAPILAN_ISLEMLER WHERE ISLEM_ID = @param11";

                    using (MySqlCommand command = new MySqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@param11", islemID);
                        int affectedRows = command.ExecuteNonQuery();

                        if (affectedRows > 0)
                        {
                            MessageBox.Show("Kayıt Başarıyla Silindi.");
                        }
                        else
                        {
                            MessageBox.Show("Belirtilen Dosya NO'ye sahip kayıt bulunamadı.");
                        }
                    }
                }

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlQuery = "SELECT * FROM YAPILAN_ISLEMLER";
                    dataAdapter = new MySqlDataAdapter(sqlQuery, connection);
                    dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }
        }

        private void btnSecSil_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int islemID))
            {
                Sil(islemID);
            }
            else
            {
                MessageBox.Show("Geçerli bir ISLEM_ID giriniz.");
            }
        }

        private void TaburcuEt()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string insertTaburcuQuery = "INSERT INTO TABURCU (TC, TABURCU_ID, TARIH) VALUES (@param1, @param2, @param3)";

                using (MySqlCommand insertTaburcuCommand = new MySqlCommand(insertTaburcuQuery, connection))
                {
                    insertTaburcuCommand.Parameters.AddWithValue("@param1", Convert.ToInt64(textBox6.Text));
                    insertTaburcuCommand.Parameters.AddWithValue("@param2", GenerateRandomTaburcuID());
                    insertTaburcuCommand.Parameters.AddWithValue("@param3", DateTime.Now);

                    insertTaburcuCommand.ExecuteNonQuery();
                }

                string deleteYapilanIslemQuery = "DELETE FROM YAPILAN_ISLEMLER WHERE TC = @param1";

                using (MySqlCommand deleteYapilanIslemCommand = new MySqlCommand(deleteYapilanIslemQuery, connection))
                {
                    deleteYapilanIslemCommand.Parameters.AddWithValue("@param1", Convert.ToInt64(textBox6.Text));

                    deleteYapilanIslemCommand.ExecuteNonQuery();
                }
            }
        }

        private int GenerateRandomTaburcuID()
        {
            Random random = new Random();
            return random.Next(1000, 9999);
        }

        private void btnTaburcuEt_Click(object sender, EventArgs e)
        {
            TaburcuEt();
            MessageBox.Show("Hasta Taburcu Edildi Lütfen Değişikleri Görmek İçin Formu Yenileyin.");
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedIndex)
            {
                case 0: // 1. seçenek
                    textBox7.Text = "150";
                    break;
                case 1: // 2. seçenek
                    textBox7.Text = "300";
                    break;
                case 2: // 3. seçenek
                    textBox7.Text = "1200";
                    break;
                case 3: // 4. seçenek
                    textBox7.Text = "2500";
                    break;
                case 4: // 5. seçenek
                    textBox7.Text = "100";
                    break;
                default:
                    textBox7.Text = "0"; // Geçersiz bir değer için varsayılan
                    break;
            }
            //HesaplaVeToplamYaz();
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            HesaplaVeToplamYaz();
        }


        private void HesaplaVeToplamYaz()
        {
            try
            {
                long miktar;
                if (long.TryParse(numericUpDown1.Text, out miktar))
                {
                }
                else
                {
                    MessageBox.Show("Miktar için geçerli bir sayı girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                long birimFiyat;
                if (long.TryParse(textBox7.Text, out birimFiyat))
                {
                }
                else
                {
                    MessageBox.Show("Birim fiyat için geçerli bir sayı girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                long toplamTutar = (miktar) * birimFiyat;
                label15.Text = toplamTutar.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hesaplama sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnYonetici_Click(object sender, EventArgs e)
        {
             yonetici_ekrani lgn4 = new yonetici_ekrani();

                lgn4.Show();
                this.Hide();
        }
    }
}
