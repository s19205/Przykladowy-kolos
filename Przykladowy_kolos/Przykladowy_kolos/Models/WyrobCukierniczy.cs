using System;
using System.Collections.Generic;

namespace Przykladowy_kolos.Models
{
    public partial class WyrobCukierniczy
    {
        public WyrobCukierniczy()
        {
            ZamowienieWyrobCukierniczy = new HashSet<ZamowienieWyrobCukierniczy>();
        }

        public int IdWyrobuCukierniczego { get; set; }
        public string Nazwa { get; set; }
        public double CenaZaSzt { get; set; }
        public string Typ { get; set; }

        public virtual ICollection<ZamowienieWyrobCukierniczy> ZamowienieWyrobCukierniczy { get; set; }
    }
}
