using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PokEBayTestUI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        DaprClient _daprClient;

        public IndexModel(ILogger<IndexModel> logger, DaprClient daprClient)
        {
            _logger = logger;
            _daprClient = daprClient;
        }

        public async Task OnGet()
        {
            //var forecasts = await _daprClient.InvokeMethodAsync<IEnumerable<WeatherForecast>>(
            //    HttpMethod.Get,
            //    "superheroesapi",
            //    "weatherforecast");

            //ViewData["WeatherForecastData"] = forecasts;

            try
            {
                //await _daprClient.InvokeMethodAsync<object>(
                // "pokebaybasketapi",
                // "basket",
                // _demoSuperhero);

                //var getResponse = await _daprClient.InvokeMethodAsync<object>(
                // HttpMethod.Get,
                // "pokebaybasketapi",
                // "basket");

                //var request = _daprClient.CreateInvokeMethodRequest(HttpMethod.Get, "pokebaybasketapi", "basket");
                //var response = await _daprClient.InvokeMethodWithResponseAsync(request);

                var basketDto = new BasketDto
                {
                    Id = Guid.NewGuid(),
                    Description = "sdsdsds",
                    Name = "sdsds",
                    PictureUri = "adad",
                    Price = 10
                };
                var request2 = _daprClient.CreateInvokeMethodRequest("pokebaybasketapi", "basket", basketDto);
                var response2 = await _daprClient.InvokeMethodWithResponseAsync(request2);


                //ViewData["SuperHeroesData"] = getResponse;
            }
            catch (Exception ex)
            {
                ViewData["SuperHeroesData"] = ex.Message;
            }
        }
    }

    public class BasketDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string PictureUri { get; set; }
    }
}
