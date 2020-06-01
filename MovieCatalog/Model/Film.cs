using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MovieCatalog.Model
{
    class Film
    {
        public int ID { get; set; }
        public String Title { get; set; }
        public String FilmGenre { get; set; }
        public int Age { get; set; }
        public DateTime Release { get; set; }
        public DateTime Completion { get; set; }
        public String Director { get; set; }
        public int Timing { get; set; }
        public String Description { get; set; }
        public String Actors { get; set; }
        public int Rating { get; set; }
        public BitmapImage Picture { get; set; }
    }
}
