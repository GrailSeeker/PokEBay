using Microsoft.AspNetCore.Mvc;
using PokEBay.Basket.API.DTO;
using PokEBay.Basket.API.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PokEBay.Basket.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        // GET: /<BasketController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BasketDto>>> GetItemsFromBasketAsync()
        {
            try
            {
                var items = await _basketService.GetItemsFromBasketAsync();

                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message} \n {ex.InnerException.Message}");
            }
        }

        // POST /<BasketController>
        [HttpPost]
        public async Task<ActionResult> AddToBasketAsync([FromBody] BasketDto basketDto)
        {
            try
            {
                await _basketService.AddToBasketAsync(basketDto);

                return Created("Item added to basket.", basketDto);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message} \n {ex.InnerException.Message}");
            }
        }

        // DELETE /<BasketController>
        [HttpDelete]
        public async Task<ActionResult> ClearBasketAsync()
        {
            try
            {
                await _basketService.ClearBasketAsync();

                return Ok($"Basket cleared!");
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message} \n {ex.InnerException.Message}");
            }
        }
    }
}
