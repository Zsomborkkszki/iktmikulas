using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using iktmikulas.Models;
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
            System.Threading.Thread.Sleep(1000);
            MySqlConnection connector = new MySqlConnection();
            Console.WriteLine("Csatlakozás a MySql adatbázishoz...");
            System.Threading.Thread.Sleep(500);
            connector.ConnectionString = "server=localhost;user=root;password=;database=mikulas";
            connector.Open();
            Console.WriteLine("Sikeres csatlakozás a MySQL adatbázishoz");
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

                    break;
                case 3: 
                    Console.WriteLine($"'{menuItems[index]}' kiválasztva. Kilépés a programból.");
                    Environment.Exit(0);
                    break;
            }
        }

        static void UserDataRecording()
        {
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
            Console.WriteLine("Az adatok rögzítése sikeres...");
        }
    }
}

