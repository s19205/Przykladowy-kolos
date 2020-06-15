using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Przykladowy_kolos.DTOs.Requests
{
    public class OrderRequest
    {
        public DateTime DataPrzyjecia { get; set; }
        public string Uwagi { get; set; }
        public ICollection<WyrobRequest> Wyroby { get; set; }
    }
}
