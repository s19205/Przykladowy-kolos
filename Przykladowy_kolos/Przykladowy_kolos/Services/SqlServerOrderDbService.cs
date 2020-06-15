using Microsoft.AspNetCore.Mvc;
using Przykladowy_kolos.DTOs.Requests;
using Przykladowy_kolos.DTOs.Responses;
using Przykladowy_kolos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Przykladowy_kolos.Services
{
    public class SqlServerOrderDbService : IOrderDbService
    {
        s19205Context dbContext;

        public SqlServerOrderDbService(s19205Context dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult AddOrder(OrderRequest request, int id)
        {
            if(request.Wyroby.Count() == 0)
            {
                return new BadRequestObjectResult("Nie podano żadnego wyrobu");
            }

            var zamowienie = new Zamowienie
            {
                DataPrzyjecia = request.DataPrzyjecia,
                Uwagi = request.Uwagi,
                IdKlient = id,
                IdPracownik = 1
            };
            dbContext.Zamowienie.Add(zamowienie);

            foreach (WyrobRequest wyrob in request.Wyroby)
            {
                var productId = dbContext.WyrobCukierniczy.Where(k => k.Nazwa == wyrob.Wyrob).Select(k => k.IdWyrobuCukierniczego).FirstOrDefault();
                if(productId == 0)
                {
                    return new BadRequestObjectResult("Zaden z podanych wyrobow nie jest w bazie");
                }
                var zamowienieWyrob = new ZamowienieWyrobCukierniczy
                {
                    IdWyrobuCukierniczego = productId,
                    IdZamowienia = zamowienie.IdZamowienia,
                    Ilosc = wyrob.Ilosc,
                    Uwagi = wyrob.Uwagi
                };
                dbContext.ZamowienieWyrobCukierniczy.Add(zamowienieWyrob);
            }
            dbContext.SaveChanges();
            return new OkResult();
        }

        public IActionResult getAllOrders()
        {
            List<ZamowieniaResponse> wyroby = new List<ZamowieniaResponse>();
            var orderIds = dbContext.Zamowienie.Select(o => o.IdZamowienia).ToList();

            foreach (int orderId in orderIds)
            {
                var productIds = dbContext.ZamowienieWyrobCukierniczy.Where(o => o.IdZamowienia == orderId).Select(o => o.IdWyrobuCukierniczego).ToList();
                foreach (int productId in productIds)
                {
                    var wyrob = new ZamowieniaResponse()
                    {
                        Nazwa = dbContext.WyrobCukierniczy.Where(p => p.IdWyrobuCukierniczego == productId).Select(p => p.Nazwa).FirstOrDefault(),
                        Rodzaj = dbContext.WyrobCukierniczy.Where(p => p.IdWyrobuCukierniczego == productId).Select(p => p.Typ).FirstOrDefault(),
                        Ilosc = dbContext.ZamowienieWyrobCukierniczy.Where(p => p.IdWyrobuCukierniczego == productId && p.IdWyrobuCukierniczego == orderId).Select(p => p.Ilosc).FirstOrDefault()
                    };
                    wyroby.Add(wyrob);
                }
            }
            return new OkObjectResult(wyroby);
        }

        public IActionResult GetOrders(string nazwisko)
        {
    
                var clientId = dbContext.Klient.Where(cl => cl.Nazwisko == nazwisko).Select(a => a.IdKlient).FirstOrDefault();
                if (clientId == 0)
                {
                    return new BadRequestObjectResult("Nie ma klienta o podanym nazwisku: " + nazwisko);
                }
                List<ZamowieniaResponse> wyroby = new List<ZamowieniaResponse>();
                var orderIds = dbContext.Zamowienie.Where(id => id.IdKlient == clientId).Select(o => o.IdZamowienia).ToList();
                
                foreach (int orderId in orderIds)
                {
                    var productIds = dbContext.ZamowienieWyrobCukierniczy
                        .Where(o => o.IdZamowienia == orderId)
                        .Select(o => o.IdWyrobuCukierniczego)
                        .ToList();

                    foreach (int productId in productIds)
                    {
                        var wyrob = new ZamowieniaResponse()
                        {
                            Nazwa = dbContext.WyrobCukierniczy.Where(p => p.IdWyrobuCukierniczego == productId).Select(p => p.Nazwa).FirstOrDefault(),
                            Rodzaj = dbContext.WyrobCukierniczy.Where(p => p.IdWyrobuCukierniczego == productId).Select(p => p.Typ).FirstOrDefault(),
                            Ilosc = dbContext.ZamowienieWyrobCukierniczy.Where(p => p.IdWyrobuCukierniczego == productId && p.IdWyrobuCukierniczego == orderId).Select(p => p.Ilosc).FirstOrDefault()
                        };
                        wyroby.Add(wyrob);
                    }
                }

                return new OkObjectResult(wyroby);

        }

        IActionResult IOrderDbService.getAllOrders()
        {
            throw new NotImplementedException();
        }
    }
}
