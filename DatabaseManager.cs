using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace lab9_wizualne
{
    public class DatabaseManager
    {
        private string connectionString = @"Data Source=komis.db;Version=3;";

        public void CreateTable()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = @"
                CREATE TABLE IF NOT EXISTS Wnioski (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Data TEXT,
                NrAlbumu TEXT,
                Nazwisko TEXT,
                Imie TEXT,
                Semestr TEXT,
                Rok TEXT,
                Kierunek TEXT,
                Stopien TEXT,
                Przedmiot TEXT,
                Punkty TEXT,
                Prowadzacy TEXT,
                Uzasadnienie TEXT,
                SkladKomisji1 TEXT,
                SkladKomisji2 TEXT,
                SkladKomisji3 TEXT
                );";
                new SQLiteCommand(query, connection).ExecuteNonQuery();
            }
        }


        public void WriteData(string[] dane)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = @"
                INSERT INTO Wnioski 
                (Data, NrAlbumu, Nazwisko, Imie, Semestr, Rok, Kierunek, Stopien, Przedmiot, Punkty, Prowadzacy, Uzasadnienie, SkladKomisji1, SkladKomisji2, SkladKomisji3)
                VALUES 
                (@Data, @NrAlbumu, @Nazwisko, @Imie, @Semestr, @Rok, @Kierunek, @Stopien, @Przedmiot, @Punkty, @Prowadzacy, @Uzasadnienie, @SkladKomisji1, @SkladKomisji2, @SkladKomisji3);";

                var cmd = new SQLiteCommand(query, connection);
                string[] keys = { "Data", "NrAlbumu", "Nazwisko", "Imie", "Semestr", "Rok", "Kierunek", "Stopien", "Przedmiot", "Punkty", "Prowadzacy", "Uzasadnienie", "SkladKomisji1", "SkladKomisji2", "SkladKomisji3" };

                Console.WriteLine("Zapisuję dane:");
                for (int i = 0; i < keys.Length; i++)
                {
                    Console.WriteLine($"{keys[i]}: {dane[i]}");
                    cmd.Parameters.AddWithValue($"@{keys[i]}", dane[i]);
                }

                try
                {
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Dane zapisane!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Błąd przy zapisie do bazy: " + ex.Message);
                }
            }
        }


        public SQLiteDataReader ReadData()
        {
            var connection = new SQLiteConnection(connectionString);
            connection.Open();
            var command = new SQLiteCommand("SELECT * FROM Wnioski ORDER BY Id DESC", connection);

            // Logowanie zapytania
            Console.WriteLine("Wykonano zapytanie: SELECT * FROM Wnioski");

            return command.ExecuteReader();
        }

        public SQLiteDataReader ReadSingle(int id)
        {
            var connection = new SQLiteConnection(connectionString);
            connection.Open();
            var command = new SQLiteCommand("SELECT * FROM Wnioski WHERE Id = @id", connection);
            command.Parameters.AddWithValue("@id", id);
            return command.ExecuteReader(System.Data.CommandBehavior.CloseConnection); // pamiętaj: connection nie zamykamy od razu, bo reader działa na otwartym połączeniu
        }

        public void UpdateData(int id, string[] dane)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = @"
        UPDATE Wnioski SET 
        Data=@Data, NrAlbumu=@NrAlbumu, Nazwisko=@Nazwisko, Imie=@Imie, Semestr=@Semestr, Rok=@Rok, Kierunek=@Kierunek, 
        Stopien=@Stopien, Przedmiot=@Przedmiot, Punkty=@Punkty, Prowadzacy=@Prowadzacy, Uzasadnienie=@Uzasadnienie,
        SkladKomisji1=@SkladKomisji1, SkladKomisji2=@SkladKomisji2, SkladKomisji3=@SkladKomisji3
        WHERE Id=@Id;";

                var cmd = new SQLiteCommand(query, connection);
                string[] keys = { "Data", "NrAlbumu", "Nazwisko", "Imie", "Semestr", "Rok", "Kierunek", "Stopien", "Przedmiot", "Punkty", "Prowadzacy", "Uzasadnienie", "SkladKomisji1", "SkladKomisji2", "SkladKomisji3" };
                for (int i = 0; i < keys.Length; i++)
                    cmd.Parameters.AddWithValue($"@{keys[i]}", dane[i]);

                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteData(int id)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Wnioski WHERE Id = @Id";
                var cmd = new SQLiteCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }

    }
}
