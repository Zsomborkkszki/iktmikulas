using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace iktmikulas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Establishing connection to MySQL database
            MySqlConnection connector = new MySqlConnection();
            connector.ConnectionString = "server=localhost;user=root;password=;database=mikulas";
            connector.Open();
            Console.WriteLine("Sikeres csatlakozás a MySQL adatbázishoz.");
            //File reading
            MySqlCommand command = new MySqlCommand("SELECT * FROM versenyzok ORDER BY Pont1 DESC, Ido1 ASC;", connector);
            MySqlDataReader reader = command.ExecuteReader();
            connector.Close();
        }
    }
}

