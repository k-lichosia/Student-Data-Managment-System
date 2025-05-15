using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using Xceed.Words.NET;
using System.Diagnostics;

namespace lab9_wizualne
{
    public partial class Form1 : Form
    {
        DatabaseManager db = new DatabaseManager();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            db.CreateTable();
            btnPokaz_Click(null, null);
        }

        private void btnZapisz_Click(object sender, EventArgs e) //zapisuje dane z textboxów do bazy 
        {
            string[] dane = new string[]
            {
                textData.Text,
                textAlbum.Text,
                textNazwisko.Text,
                textImie.Text,
                textSemestr.Text,
                textRok.Text,
                textKierunek.Text,
                textStopien.Text,
                textPrzedmiot.Text,
                textPunkty.Text,
                textProwadzacy.Text,
                textUzasadnienie.Text,
                textSklad1.Text,
                textSklad2.Text,
                textSklad3.Text
            };

            db.WriteData(dane);
            MessageBox.Show("Dane zapisane!");
        }

        private void btnPokaz_Click(object sender, EventArgs e) //wyświetla aktualne dane z bazy w listBoxie
        {
            try
            {
                var reader = db.ReadData();
                listBox1.Items.Clear();

                Console.WriteLine("Pobieram dane z bazy:");

                while (reader.Read())
                {
                    string wpis = $"{reader["Id"]}: {reader["Imie"]} {reader["Nazwisko"]} - {reader["Przedmiot"]} - {reader["Data"]}";
                    listBox1.Items.Add(wpis);

                    Console.WriteLine($"Dodaję do listBox: {wpis}");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd podczas wyświetlania danych: " + ex.Message);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null) return;

            string selected = listBox1.SelectedItem.ToString();
            int id = int.Parse(selected.Split(':')[0]); // pobieramy ID z przodu

            var reader = db.ReadSingle(id);
            if (reader.Read())
            {
                textData.Text = reader["Data"].ToString();
                textAlbum.Text = reader["NrAlbumu"].ToString();
                textNazwisko.Text = reader["Nazwisko"].ToString();
                textImie.Text = reader["Imie"].ToString();
                textSemestr.Text = reader["Semestr"].ToString();
                textRok.Text = reader["Rok"].ToString();
                textKierunek.Text = reader["Kierunek"].ToString();
                textStopien.Text = reader["Stopien"].ToString();
                textPrzedmiot.Text = reader["Przedmiot"].ToString();
                textPunkty.Text = reader["Punkty"].ToString();
                textProwadzacy.Text = reader["Prowadzacy"].ToString();
                textUzasadnienie.Text = reader["Uzasadnienie"].ToString();
                textSklad1.Text = reader["SkladKomisji1"].ToString();
                textSklad2.Text = reader["SkladKomisji2"].ToString();
                textSklad3.Text = reader["SkladKomisji3"].ToString();
            }
            reader.Close();
        }

        private void btnAktualizuj_Click(object sender, EventArgs e) //Aktualizuje zaznaczony wpis
        {
            if (listBox1.SelectedItem == null) return;

            int id = int.Parse(listBox1.SelectedItem.ToString().Split(':')[0]);
            string[] dane = new string[]
            {
        textData.Text,
        textAlbum.Text,
        textNazwisko.Text,
        textImie.Text,
        textSemestr.Text,
        textRok.Text,
        textKierunek.Text,
        textStopien.Text,
        textPrzedmiot.Text,
        textPunkty.Text,
        textProwadzacy.Text,
        textUzasadnienie.Text,
        textSklad1.Text,
        textSklad2.Text,
        textSklad3.Text
            };

            db.UpdateData(id, dane);
            MessageBox.Show("Zaktualizowano wpis!");
            btnPokaz_Click(null, null); // odśwież listę
        }

        private void btnCzysc_Click(object sender, EventArgs e) //Czyści tekst z textBoxów
        {
            textData.Clear();
            textAlbum.Clear();
            textNazwisko.Clear();
            textImie.Clear();
            textSemestr.Clear();
            textRok.Clear();
            textKierunek.Clear();
            textStopien.Clear();
            textPrzedmiot.Clear();
            textPunkty.Clear();
            textProwadzacy.Clear();
            textUzasadnienie.Clear();
            textSklad1.Clear();
            textSklad2.Clear();
            textSklad3.Clear();
        }

        private void btnUsun_Click(object sender, EventArgs e) //Usuwa zaznaczony wpis z bazy
        {
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("Wybierz wpis do usunięcia.");
                return;
            }

            string selected = listBox1.SelectedItem.ToString();
            int id = int.Parse(selected.Split(':')[0]);

            var confirmResult = MessageBox.Show("Czy na pewno chcesz usunąć ten wpis?", "Potwierdzenie", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                db.DeleteData(id);
                MessageBox.Show("Wpis usunięty.");
                btnPokaz_Click(null, null); 
                btnCzysc_Click(null, null); 
            }
        }
    }
}
