using iktmikulas.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iktmikulas.Models.UserController;

namespace iktmikulas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Users> users = UserController.GetUserData();
            DisplayInteractiveMenu();
            
        }
        static void DataWrite()
        {
            Console.WriteLine("Kiírás...");
            MySqlConnection connector = new MySqlConnection();
            connector.ConnectionString = "server=localhost;user=root;password=;database=mikulas";
            Console.WriteLine("Csatlakozás a MySql adatbázishoz...");
            connector.Open();
            MySqlCommand command2 = new MySqlCommand("SELECT Name, (Pont1 + Pont2 + Pont3) AS OsszesPont, Legjobbido FROM versenyzok ORDER BY OsszesPont DESC, Legjobbido ASC;", connector);
            MySqlDataReader reader2 = command2.ExecuteReader();
            while (reader2.Read())
            {
                string name = reader2.GetString("Name");
                int osszespont = reader2.GetInt32("OsszesPont");
                double legjobbido = reader2.GetDouble("Legjobbido");
                Console.WriteLine($"Név: {name}, Összpont: {osszespont}, Legjobb idő: {legjobbido}");
            }
            connector.Close();
        }
        static void DisplayInteractiveMenu()
        {

            List<string> menuItems = new List<string>
        {
            "Adatok kiírása",
            "Adatok rögzítése",
            "Adatok változtatása",
            "Kilépés"
        };
            int selectedIndex = 0;

            Console.CursorVisible = false;
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("--- Főmenü ---");

                for (int i = 0; i < menuItems.Count; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"> {menuItems[i]} <"); 
                        Console.ResetColor(); 
                    }
                    else
                    {
                        Console.WriteLine($"  {menuItems[i]}");
                    }
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = (selectedIndex == 0) ? menuItems.Count - 1 : selectedIndex - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = (selectedIndex == menuItems.Count - 1) ? 0 : selectedIndex + 1;
                        break;
                    case ConsoleKey.Enter:
                        HandleSelection(selectedIndex, menuItems);
                        if (selectedIndex == menuItems.Count - 1)
                        {
                            exit = true;
                        }
                        if (!exit)
                        {
                            Console.WriteLine("\nNyomjon meg egy gombot a folytatáshoz...");
                            Console.ReadKey(true);
                        }
                        break;
                }
            }
            Console.CursorVisible = true;
            Console.WriteLine("\nKilépés a programból. Viszontlátásra!");
        }

        private static void HandleSelection(int index, List<string> menuItems)
        {
            Console.Clear();
            switch (index)
            {
                case 0:
                    Console.WriteLine($"'{menuItems[index]}' került kiválasztásra.");
                    DataWrite();
                    break;
                case 1:
                    Console.WriteLine($"'{menuItems[index]}' került kiválasztásra.");
                    UserDataRecording();
                    break;
                case 2:
                    Console.WriteLine($"'{menuItems[index]}' került kiválasztásra.");
                    UserChanger();
                    break;
                case 3: 
                    Console.WriteLine($"'{menuItems[index]}' kiválasztva. Kilépés a programból.");
                    Environment.Exit(0);
                    break;
            }
        }

        static void UserDataRecording()
        {
            Console.CursorVisible= true;
            Console.WriteLine("---Adatok rögzítése---");
            Console.Write("Adja meg a verszenyző nevét: ");
            string name = Console.ReadLine();
            Console.Write("Adja meg a versenyző első pontszámát: ");
            int point1 = int.Parse(Console.ReadLine());
            Console.Write("Adja meg a versenyző első idejét: ");
            double time1 = double.Parse(Console.ReadLine());
            Console.Write("Adja meg a versenyző második pontszámát: ");
            int point2 = int.Parse(Console.ReadLine());
            Console.Write("Adja meg a versenyző második idejét: ");
            double time2 = double.Parse(Console.ReadLine());
            Console.Write("Adja meg a versenyző harmadik pontszámát: ");
            int point3 = int.Parse(Console.ReadLine());
            Console.Write("Adja meg a versenyző harmadik idejét: ");
            double time3 = double.Parse(Console.ReadLine());
            Console.WriteLine("Az adatok rögzítése sikeres.");
            UserDataWrite(name, point1, time1, point2, time2, point3, time3);
            DisplayInteractiveMenu();
        }
        static void UserDataWrite(string name, int point1, double time1, int point2, double time2, int point3, double time3)
        {
            MySqlConnection connector = new MySqlConnection();
            //Adatok írása az adatbázisba
            connector.ConnectionString = "server=localhost;user=root;password=;database=mikulas";
            connector.Open();
            string command = "INSERT INTO versenyzok (Name, Pont1, Ido1, Pont2, Ido2, Pont3, Ido3, Legjobbpont, Legjobbido) VALUES (@name, @point1, @time1, @point2, @time2, @point3, @time3, @legjobbpont, @legjobbido);";
            MySqlCommand writer = new MySqlCommand(command, connector);
            int[] tomb = new int[3] {point1, point2, point3};
            double[] tomb2 = new double[3] {time1, time2, time3};
            writer.Parameters.AddWithValue("@name", name);
            writer.Parameters.AddWithValue("@point1", point1);
            writer.Parameters.AddWithValue("@time1", time1);
            writer.Parameters.AddWithValue("@point2", point2);
            writer.Parameters.AddWithValue("@time2", time2);
            writer.Parameters.AddWithValue("@point3", point3);
            writer.Parameters.AddWithValue("@time3", time3);
            writer.Parameters.AddWithValue("@legjobbpont", tomb.Max());
            writer.Parameters.AddWithValue("@legjobbido", tomb2.Min());
            writer.ExecuteNonQuery();
            connector.Close();
        }

        static void UserChanger()
        {
            Console.WriteLine("---Változtató menü---");
            Console.WriteLine("1. Adatok szerkesztése");
            Console.WriteLine("2. Versenyző törlése");
            Console.WriteLine("Válasszon funkciót (1 or 2): ");
            int menu = int.Parse(Console.ReadLine());
            if (menu == 1)
            {
                Console.WriteLine("Mi a versenyző neve: "); //Feltételezem hogy a versenybíró változtathat csak
                string valasztas = Console.ReadLine();
                Console.WriteLine("Mit szeretne változatni (Pont1 or Pont2 or Pont3 or Ido1 or Ido2 or Ido3): ");
                string valaszto = Console.ReadLine();
                int ertek = 0;
                double idoertek = 0;
                bool idovagypont = false;
                if (valaszto == "Pont1")
                {
                    Console.WriteLine($"Adja meg az új {valaszto}-et: ");
                    ertek = int.Parse(Console.ReadLine());
                    idovagypont = true;

                }
                if (valaszto == "Pont2")
                {
                    Console.WriteLine($"Adja meg az új {valaszto}-őt: ");
                    ertek = int.Parse(Console.ReadLine());
                    idovagypont = true;
                }
                if (valaszto == "Pont3")
                {
                    Console.WriteLine($"Adja meg az új {valaszto}-at: ");
                    ertek = int.Parse(Console.ReadLine());
                    idovagypont = true;
                }
                if (valaszto == "Ido1")
                {
                    Console.WriteLine($"Adja meg az új {valaszto}-et: ");
                    idoertek = double.Parse(Console.ReadLine());
                }
                if (valaszto == "Ido2")
                {
                    Console.WriteLine($"Adja meg az új {valaszto}-őt: ");
                    idoertek = double.Parse(Console.ReadLine());
                }
                if (valaszto == "Ido3")
                {
                    Console.WriteLine($"Adja meg az új {valaszto}-at: ");
                    idoertek = double.Parse(Console.ReadLine());
                }
                UserChangerCommand(valasztas, valaszto, idovagypont, ertek, idoertek);
            }
            if (menu == 2)
            {
                Console.WriteLine("Melyik versenyzőt szeretné törölni: ");
                string name = Console.ReadLine();
                Console.WriteLine("Biztos hogy törli? (i/n)");
                string choose = Console.ReadLine();
                if (choose.ToLower() == "i")
                {
                    UserDelete(name);
                }
                else
                {
                    Console.Clear();
                    UserChanger();
                }
                
            }
            else
            {
                Console.WriteLine("Rossz menupontot adott meg");
                Console.Clear();
                UserChanger();
            }

        }
        static void UserChangerCommand(string valasztas, string valaszto, bool idovagypont, int ertek, double idoertek)
        {
            MySqlConnection connector = new MySqlConnection();
            connector.ConnectionString = "server=localhost;user=root;password=;database=mikulas";
            connector.Open();
            if (idovagypont == true)
            {
                MySqlCommand beolvas = new MySqlCommand("SELECT * FROM versenyzok;", connector);
                MySqlCommand reader = new MySqlCommand($"UPDATE versenyzok SET {valaszto} = '{ertek}' WHERE versenyzok.Name = '{valasztas}';", connector);
                reader.ExecuteNonQuery();
                
            }
            else
            {
                MySqlCommand beolvas = new MySqlCommand("SELECT * FROM versenyzok", connector);
                MySqlCommand reader = new MySqlCommand($"UPDATE versenyzok SET {valaszto} = {idoertek} WHERE versenyzok.Name = '{valasztas}';", connector);
                reader.ExecuteNonQuery();
            }
            connector.Close();
        }

        static void UserDelete(string name)
        {
            MySqlConnection connector = new MySqlConnection();
            connector.ConnectionString = "server=localhost;user=root;password=;database=mikulas";
            connector.Open();
            MySqlCommand beolvas = new MySqlCommand("SELECT * FROM versenyzok", connector);
            MySqlCommand delete = new MySqlCommand($"DELETE FROM versenyzok WHERE versenyzok.Name = '{name}';", connector);
            delete.ExecuteNonQuery();
            connector.Close();
        }
    }
}

