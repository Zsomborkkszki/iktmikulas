using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iktmikulas.Models
{
    internal class Users
    {
        string Name;
        int Point1;
        double Time1;
        int Point2;
        double Time2;
        int Point3;
        double Time3;
        double besttime1;
        int bestscore1;

        public Users(string name, int point1, double time1, int point2, double time2, int point3, double time3, double besttime, int bestscore)
        {
            Name1 = name;
            Point11 = point1;
            Time11 = time1;
            Point21 = point2;
            Time21 = time2;
            Point31 = point3;
            Time31 = time3;
            Bestscore1 = bestscore;
            Besttime1 = besttime;
        }

        public string Name1 { get => Name; set => Name = value; }
        public int Point11 { get => Point1; set => Point1 = value; }
        public double Time11 { get => Time1; set => Time1 = value; }
        public int Point21 { get => Point2; set => Point2 = value; }
        public double Time21 { get => Time2; set => Time2 = value; }
        public int Point31 { get => Point3; set => Point3 = value; }
        public double Time31 { get => Time3; set => Time3 = value; }
        public double Besttime1 { get => besttime1; set => besttime1 = value; }
        public int Bestscore1 { get => bestscore1; set => bestscore1 = value; }

        public Users()
        {
        }
    }
}
