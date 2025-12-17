using iktmikulas.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
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
            GenerateHtmlAndOpen();
            DisplayInteractiveMenu();  
        }
        static void DataWrite()
        {
            Console.WriteLine("Kiírás...");
            MySqlConnection connector = new MySqlConnection();
            connector.ConnectionString = "server=localhost;user=root;password=;database=mikulas";
            Console.WriteLine("Csatlakozás a MySql adatbázishoz...");
            connector.Open();
            MySqlCommand command2 = new MySqlCommand("SELECT Name, Legjobbpont, Legjobbido FROM versenyzok ORDER BY Legjobbpont DESC, Legjobbido ASC;", connector);
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
                MySqlCommand reader = new MySqlCommand($"UPDATE versenyzok SET {valaszto} = '{ertek}' WHERE versenyzok.Name = '{valasztas}';", connector);
                reader.ExecuteNonQuery();
                
            }
            else
            {
                MySqlCommand reader = new MySqlCommand($"UPDATE versenyzok SET {valaszto} = '{idoertek}' WHERE versenyzok.Name = '{valasztas}';", connector);
                reader.ExecuteNonQuery();
                MySqlCommand command = new MySqlCommand($"SELECT * FROM versenyzok WHERE versenyzok.Name = '{valasztas}';", connector);
                MySqlDataReader reader2 = command.ExecuteReader();
                reader2.Read();
                    double Time1 = reader2.GetDouble("Ido1");
                    double Time2 = reader2.GetDouble("Ido2");
                    double Time3 = reader2.GetDouble("Ido3");
                    double[] tomb = { Time1, Time2, Time3 };
                reader2.Close();
                    MySqlCommand writer = new MySqlCommand($"UPDATE versenyzok SET Legjobbido = '{tomb.Min()}' WHERE versenyzok.Name = '{valasztas}';", connector);
                    writer.ExecuteNonQuery();
            }
            connector.Close();
            GenerateHtmlAndOpen();
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
            GenerateHtmlAndOpen();
        }

        static void GenerateHtmlAndOpen()
        {
            Console.WriteLine("HTML táblázat frissítése és megnyitása...");

            // A HTML fájlt a projekt főgyökerébe mentjük, ahol a .sln is van
            string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            string htmlFilePath = Path.Combine(projectRoot, "verseny_allas.html");

            StringBuilder htmlContent = new StringBuilder();
            htmlContent.AppendLine("<!DOCTYPE html><html lang='hu'><head><meta charset='UTF-8'>");
            htmlContent.AppendLine("<link href='https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css' rel='stylesheet'>");
            htmlContent.AppendLine("<style>");
            htmlContent.AppendLine("body{background:#f8f9fa; overflow:hidden; font-family:sans-serif;}");
            htmlContent.AppendLine(".village-logo{width:110px; height:110px; object-fit:contain; background:white; border-radius:10px; border:3px solid #d4af37; padding:5px;}");
            htmlContent.AppendLine(".table-container{height:70vh; overflow:hidden; position:relative; margin-top:10px;}");
            htmlContent.AppendLine(".scrolling-table{position:absolute; width:100%; transition:top 0.5s;}");
            htmlContent.AppendLine(".rank-1{background-color:#fff3cd!important; font-weight:bold; font-size:1.2rem;}");
            htmlContent.AppendLine("</style>");

            htmlContent.AppendLine("<script>");
            htmlContent.AppendLine("function scroll(){const b=document.getElementById('s');const t=document.getElementById('t');let top=0;");
            htmlContent.AppendLine("setInterval(()=>{if(t.offsetHeight>b.offsetHeight){top-=1; if(Math.abs(top)>(t.offsetHeight-b.offsetHeight+50)){top=20;} t.style.top=top+'px';}},40);}");
            htmlContent.AppendLine("setTimeout(()=>location.reload(),10000);"); // 10 mp frissítés
            htmlContent.AppendLine("</script></head><body onload='scroll()'>");

            htmlContent.AppendLine("<header class='bg-success text-white p-3 shadow-sm'><div class='container d-flex justify-content-between align-items-center'>");

            // KÉPEK ELÉRÉSE: Mivel a HTML a gyökérben van, közvetlenül a fájlneveket használjuk
            htmlContent.AppendLine("<div class='text-center'><img src='Képernyőkép 2025-12-17 172433.png' class='village-logo' alt='Címer'><br><small class='fw-bold'>SZÉLESBÁLÁS</small></div>");
            htmlContent.AppendLine("<div class='text-center'><h1 class='display-4 fw-bold'>KALAPLENGETŐ VERSENY</h1><p class='h5'>Faluház Bálterme - Panasonic OLED 4K</p></div>");
            htmlContent.AppendLine("<div class='text-center d-flex gap-2'>");
            htmlContent.AppendLine("<img src='Képernyőkép 2025-12-17 172615.png' class='village-logo' alt='Sör'>");
            htmlContent.AppendLine("<img src='Képernyőkép 2025-12-17 172728.png' class='village-logo' alt='Hurka'>");
            htmlContent.AppendLine("</div></div></header>");

            htmlContent.AppendLine("<main class='container-fluid px-4'>");
            htmlContent.AppendLine("<div class='table-container border bg-white shadow-lg rounded' id='s'>");
            htmlContent.AppendLine("<table class='table table-striped table-hover mb-0 scrolling-table' id='t'>");
            htmlContent.AppendLine("<thead class='table-dark sticky-top'><tr>");
            htmlContent.AppendLine("<th>Helyezés</th><th>Versenyző neve</th><th>1. kör</th><th>2. kör</th><th>3. kör</th><th class='table-warning text-dark'>LEGJOBB PONT</th>");
            htmlContent.AppendLine("</tr></thead><tbody>");

            try
            {
                using (MySqlConnection conn = new MySqlConnection("server=localhost;user=root;password=;database=mikulas"))
                {
                    conn.Open();
                    string query = "SELECT * FROM versenyzok ORDER BY Legjobbpont DESC, Legjobbido ASC, Name ASC";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    using (MySqlDataReader r = cmd.ExecuteReader())
                    {
                        int rank = 1;
                        while (r.Read())
                        {
                            string rowClass = (rank == 1) ? "class='rank-1'" : "";
                            htmlContent.AppendLine($"<tr {rowClass}>");
                            htmlContent.AppendLine($"<td class='fw-bold'>{rank}.</td>");
                            htmlContent.AppendLine($"<td>{r["Name"]}</td>");
                            htmlContent.AppendLine($"<td>{r["Pont1"]} pt ({r["Ido1"]}s)</td>");
                            htmlContent.AppendLine($"<td>{r["Pont2"]} pt ({r["Ido2"]}s)</td>");
                            htmlContent.AppendLine($"<td>{r["Pont3"]} pt ({r["Ido3"]}s)</td>");
                            htmlContent.AppendLine($"<td><span class='badge bg-danger fs-5'>{r["Legjobbpont"]} pt</span> <small class='text-muted'>({r["Legjobbido"]}s)</small></td>");
                            htmlContent.AppendLine("</tr>");
                            rank++;
                        }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine("Hiba az adatbázisnál: " + ex.Message); }

            htmlContent.AppendLine("</tbody></table></div></main>");
            htmlContent.AppendLine("<footer class='fixed-bottom bg-dark text-white p-2 d-flex justify-content-around'>");
            htmlContent.AppendLine("<span>Mérve: Géza gyerek (Temu) okosórája</span>");
            htmlContent.AppendLine("<span>Gép: Dell Latitude E5570 | Akku: 91% | OS: Szélesbálás v1.0</span>");
            htmlContent.AppendLine("</footer></body></html>");

            // Fájl mentése
            File.WriteAllText(htmlFilePath, htmlContent.ToString(), Encoding.UTF8);

            // BÖNGÉSZŐ MEGNYITÁSA (Csak ha még nincs megnyitva, vagy minden frissítéskor - a reload miatt elég egyszer)
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = htmlFilePath,
                UseShellExecute = true
            });
        }
    }
}

