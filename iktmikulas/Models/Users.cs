using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iktmikulas.Models
{
    internal class Users
    {
        

        public Users(int id, string name, int point1, double time1, int point2, double time2, int point3, double time3)
        {
            Id = id;
            Name = name;
            Point1 = point1;
            Time1 = time1;
            Point2 = point2;
            Time2 = time2;
            Point3 = point3;
            Time3 = time3;
        }

        public int Id { get => Id; set => Id = value; }
        public string Name { get => Name; set => Name = value; }
        public int Point1 { get => Point1; set => Point1 = value; }
        public double Time1 { get => Time1; set => Time1 = value; }
        public int Point2 { get => Point2; set => Point2 = value; }
        public double Time2 { get => Time2; set => Time2 = value; }
        public int Point3 { get => Point3; set => Point3 = value; }
        public double Time3 { get => Time3; set => Time3 = value; }

        public Users()
        {
        }
    }
}
