using LibrariansInterface.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using LibrariansInterface.Models.ViewModels;

namespace LibrariansInterface.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            
            var client = _httpClientFactory.CreateClient("API Client");

            var result = await client.GetAsync("/api/borrowings/unreturneditems"); //kontrollera adressen noggrannt!
            if(result.IsSuccessStatusCode)
            {
                
                var content = await result.Content.ReadAsStringAsync();
                var import = JsonConvert.DeserializeObject<List<UnreturnedItemDTO>>(content); //Detta fungerar!
                
                LocalListUnreturnedItems.LocalListOfDTOs = import;
                
                return RedirectToAction(nameof(UnreturnedItems));
            }
            return View();
            
        }
        public IActionResult UnreturnedItems()
        {
            var viewmodel = new UnreturnedItemsViewModel();
            viewmodel.Dtos = LocalListUnreturnedItems.LocalListOfDTOs;
            return View(viewmodel);
        }
        [HttpPost]
        public async Task<IActionResult> UnreturnedItems(string borrowerID)
        {
            var viewmodel = new UnreturnedItemsViewModel();
            viewmodel.Dtos = LocalListUnreturnedItems.LocalListOfDTOs;
            if (borrowerID != null)
            {
                viewmodel.Dto = LocalListUnreturnedItems.LocalListOfDTOs.FirstOrDefault(x => x.BorrowerID == borrowerID);
            }

            return View(viewmodel);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
