using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Przykladowy_kolos.DTOs.Requests
{
    public class WyrobRequest
    {
        public int Ilosc { get; set; }
        public string Wyrob { get; set; }
        public string Uwagi { get; set; }
    }
}
