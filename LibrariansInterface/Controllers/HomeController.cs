using LibrariansInterface.Models;
using LibrariansInterface.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

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

        public IActionResult Index()
        {

            return View();

        }
        public IActionResult UnreturnedItems()
        {
            var viewmodel = new UnreturnedItemsViewModel();
            viewmodel.Dtos = LocalListUnreturnedItems.LocalListOfDTOs;
            if (LocalListUnreturnedItems.LocalListOfDTOs == null)
            {
                return RedirectToAction(nameof(LoadUnreturnedItemsFromAPI));
            }
            return View(viewmodel);
        }
        [HttpPost]
        public IActionResult UnreturnedItems(string borrowerID)
        {

            var viewmodel = new UnreturnedItemsViewModel();
            viewmodel.Dtos = LocalListUnreturnedItems.LocalListOfDTOs;
            if (borrowerID != null)
            {
                viewmodel.Dto = LocalListUnreturnedItems.LocalListOfDTOs.FirstOrDefault(x => x.BorrowerID == borrowerID);
            }

            return View(viewmodel);

        }
        public async Task<IActionResult> LoadUnreturnedItemsFromAPI()
        {

            var client = _httpClientFactory.CreateClient("API Client");
            HttpResponseMessage result = null;
            try
            {
                result = await client.GetAsync("/api/borrowings/unreturneditems"); //kontrollera adressen noggrannt!

            }
            catch (Exception epicFail)
            {
                _logger.LogInformation(epicFail.Message); //Så att man i outputfönstret ser felmeddelandet. 
            }

            if (result != null && result.IsSuccessStatusCode)
            {

                var content = await result.Content.ReadAsStringAsync();
                var import = JsonConvert.DeserializeObject<List<UnreturnedItemDTO>>(content);

                LocalListUnreturnedItems.LocalListOfDTOs = import;

                return RedirectToAction(nameof(UnreturnedItems));
            }

            return RedirectToAction(nameof(ConnectionFailed));
        }
        public IActionResult ConnectionFailed()
        {
            return View();
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
