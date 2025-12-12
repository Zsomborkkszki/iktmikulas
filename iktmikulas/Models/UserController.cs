using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace iktmikulas.Models
{
    internal class UserController
    {
        static public List<Users> GetUserData()
        {
            //Establishing connection to MySQL database
            MySqlConnection connector = new MySqlConnection();
            Console.WriteLine("Csatlakozás a MySql adatbázishoz...");
            System.Threading.Thread.Sleep(2000);
            connector.ConnectionString = "server=localhost;user=root;password=;database=mikulas";
            connector.Open();
            Console.WriteLine("Sikeres csatlakozás a MySQL adatbázishoz");
            System.Threading.Thread.Sleep(1000);
            //File reading
            MySqlCommand command = new MySqlCommand("SELECT * FROM versenyzok ORDER BY Pont1 DESC, Ido1 ASC;", connector);
            MySqlDataReader reader = command.ExecuteReader();
            List<Users> users = new List<Users>();
            while (reader.Read())
            {
                string name = reader.GetString("Name");
                int point1 = reader.GetInt32("Pont1");
                double time1 = reader.GetDouble("Ido1");
                int point2 = reader.GetInt32("Pont2");
                double time2 = reader.GetDouble("Ido2");
                int point3 = reader.GetInt32("Pont3");
                double time3 = reader.GetDouble("Ido3");
                Users user = new Users(name, point1, time1, point2, time2, point3, time3);
                users.Add(user);
            }
            connector.Close();
            return users;
        }
    }
}
