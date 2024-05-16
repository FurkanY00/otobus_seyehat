using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace otobüs_seyehat_proje
{
   
    public partial class Form1 : Form
    {
        public int value = 0;
        public int money = 0;
        public string koltuk;
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con=new SqlConnection(@"Data Source=DESKTOP-KPC6PV7\SQLEXPRESS;Initial Catalog=Otobus-seyehat;Integrated Security=True");

        public void koltukgetir()
        {
            con.Open();

            SqlCommand com = new SqlCommand("select Koltuk from seyehat_bilgileri", con);

            SqlDataReader reader = com.ExecuteReader();
            ///////////////////////////////
            List<string> buttonNumbers = new List<string>();

            while (reader.Read())
            {
                string buttonNumber = reader.GetString(0);
                buttonNumbers.Add(buttonNumber);
            }
            con.Close();
            ////////////////////////////////



            foreach (string koltuk in buttonNumbers)
            {
              

                foreach (Control control in this.Controls)
                {
                    if (control is Button)
                    {
                        if (control.Name == koltuk)
                        {
                            control.BackColor = Color.Red;
                        }
                    }
                }
            }
        }
        void listele()
        {
           
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("select Firma, Nerden, Nereye, Tarih, Kalkissaati, isim, soyisim, tc, numara, cinsiyet,Fiyat from seyehat_bilgileri", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();


           
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try {
            listele();
            }
            catch
            {
                MessageBox.Show("Serferler getirilemedi! PROGRAM KAPATILIYOR","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();

            }
            koltukgetir();
        }

        private void btnsatinal_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == null || textBox3.Text.Trim() == "")
            {
                MessageBox.Show("Lütfen Koltuk Seçin", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {

                koltuk = "button" + (Convert.ToInt32( textBox3.Text) +1);



                if (value >= 1)
                {
                    value = 0;

                    if (comboBox5.SelectedItem == null ||
                  comboBox1.SelectedItem == null ||
                  comboBox2.SelectedItem == null ||
                  dateTimePicker1.Value == null ||
                  comboBox4.SelectedItem == null ||
                  textBox1.Text == null ||
                  textBox2.Text == null ||
                  maskedTextBox1.Text == null ||
                  maskedTextBox2.Text == null ||
                  comboBox3.SelectedItem == null)
                    {
                        MessageBox.Show("Lütfen tüm bilgileri eksiksiz doldurun!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("INSERT INTO seyehat_bilgileri (Firma, Nerden, Nereye, Tarih, Kalkissaati,isim, soyisim, tc, numara, cinsiyet,Fiyat,Koltuk) VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10,@p11,@p12)", con);

                        cmd.Parameters.AddWithValue("@p1", comboBox5.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@p2", comboBox1.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@p3", comboBox2.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@p4", dateTimePicker1.Value.Date);
                        cmd.Parameters.AddWithValue("@p5", comboBox4.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@p6", textBox1.Text);
                        cmd.Parameters.AddWithValue("@p7", textBox2.Text);
                        cmd.Parameters.AddWithValue("@p8", maskedTextBox1.Text);
                        cmd.Parameters.AddWithValue("@p9", maskedTextBox2.Text);
                        cmd.Parameters.AddWithValue("@p10", comboBox3.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@p11", money);
                        cmd.Parameters.AddWithValue("@p12", koltuk);


                        cmd.ExecuteNonQuery();
                        con.Close();

                        MessageBox.Show("Başarıyla Satın Alındı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        MessageBox.Show("Hata oluştu ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (con.State == ConnectionState.Open)
                            con.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen önce fiyat hesaplaması yapın!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                try
                {
                    listele();

                }
                catch
                {
                    MessageBox.Show("Güncel liste getirilemedi!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                label15.Text = "00TL";
            }
            textBox3.Text = "";

            koltukgetir();
        }
        
        
        private void btnhesapla_Click(object sender, EventArgs e)
        {
            value += 1;


            DateTime dateTime = DateTime.Now;
            int day = dateTime.Day;
            int month = dateTime.Month; 
            string selectionDay = dateTimePicker1.Value.ToString("dd");
            string selectionMonth = dateTimePicker1.Value.ToString("MMM");
            int selectiondy=Convert.ToInt32(selectionDay);

            int monthNumber = 0;
            int calculatedDay = 0;
            int calculationNowday = 0;
           
            if (selectionMonth == "Oca")
            {
                monthNumber = 1;
            }
            else if (selectionMonth == "Şub")
            {
                monthNumber = 2;
            }
            else if (selectionMonth == "Mar")
            {
                monthNumber = 3;
            }
            else if (selectionMonth == "Nis")
            {
                monthNumber = 4;
            }
            else if (selectionMonth == "May")
            {
                monthNumber = 5;
            }
            else if (selectionMonth == "Haz")
            {
                monthNumber = 6;
            }
            else if (selectionMonth == "Tem")
            {
                monthNumber = 7;
            }
            else if (selectionMonth == "Ağu")
            {
                monthNumber = 8;
            }
            else if (selectionMonth == "Eyl")
            {
                monthNumber = 9;
            }
            else if (selectionMonth == "Eki")
            {
                monthNumber = 10;
            }
            else if (selectionMonth == "Kas")
            {
                monthNumber = 11;
            }
            else if (selectionMonth == "Ara")
            {
                monthNumber = 12;
            }

            calculationNowday = (month * 30) - (30-day);

            if((monthNumber-1)%2==0)
            {
                calculatedDay = (monthNumber * 30) - (30 - selectiondy);
            }
            else
            {
                calculatedDay = (monthNumber * 30) - (30 - selectiondy)+1;
            }
            int select=calculatedDay - calculationNowday;

            if (select==0 && select<=2)
            {
                label15.Text = "1500TL";
                money = 1500;
            }

            if (select >2 && select <= 4)
            {
                label15.Text = "1400TL";
                money = 1400;

            }
            if (select > 4 && select <= 8)
            {
                label15.Text = "1300TL";
                money = 1300;

            }
            if (select > 8 && select <= 14)
            {
                label15.Text = "1100TL";
                money = 1100;

            }
            if (select > 14 && select <= 20)
            {
                label15.Text = "900TL";
                money = 900;

            }
            if (select > 20 && select <= 25)
            {
                label15.Text = "850TL";
                money = 850;

            }
            if (select > 25 && select <= 30)
            {
                label15.Text = "700TL";
                money = 700;

            }
            if (select > 30 && select <= 40)
            {
                label15.Text = "550TL";
                money = 550;

            }
            if (select > 40 )
            {
                label15.Text = "400TL";
                money = 400;

            }
        }

       
    }
}
