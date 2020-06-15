using Microsoft.AspNetCore.Mvc;
using Przykladowy_kolos.DTOs.Requests;
using Przykladowy_kolos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Przykladowy_kolos.Services
{
    public interface IOrderDbService
    {
        IActionResult getAllOrders();
        IActionResult GetOrders(String nazwisko);
        IActionResult AddOrder(OrderRequest request, int id);
    }
}
