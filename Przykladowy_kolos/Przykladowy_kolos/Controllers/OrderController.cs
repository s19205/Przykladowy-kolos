using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Przykladowy_kolos.Services;

namespace Przykladowy_kolos.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderDbService _service;

        public OrderController(IOrderDbService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult showAllOrders()
        {
            try
            {
                return Ok(_service.GetZamowienie());
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("{nazwisko}")]
        public IActionResult showOrders(string nazwisko)
        {
            try
            {
                return Ok(_service.GetZamowienie(nazwisko));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost("{id}")]
        public IActionResult addOrder(DTOs.Requests.OrderRequest request, int id)
        {
            return _service.AddOrder(request, id);
        }



    }
}