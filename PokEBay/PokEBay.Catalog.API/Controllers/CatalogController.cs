using Microsoft.AspNetCore.Mvc;
using PokEBay.Catalog.API.Infrastructure;
using PokEBay.Catalog.API.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PokEBay.Catalog.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        // GET: /<CatalogController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CatalogItemDto>>> GetAllCatalogItemsAsync()
        {
            try
            {
                var items = await _catalogService.GetCatalogItemsAsync();

                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message} \n {ex.InnerException.Message}");
            }
        }

        // POST /<CatalogController>
        [HttpPost]
        public async Task<ActionResult> AddCatalogItemAsync([FromBody] CatalogItemDto catalogItem)
        {
            try
            {
                await _catalogService.AddITemToCatalogAsync(catalogItem);

                return Created("Item added to catalog.", catalogItem);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message} \n {ex.InnerException.Message}");
            }
        }

        // DELETE /<CatalogController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCatalogItemAsync(Guid id)
        {
            try
            {
                await _catalogService.DeleteItemFromCatalogAsync(id);

                return Ok($"Item with id {id} deleted from catalog.");
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message} \n {ex.InnerException.Message}");
            }
        }
    }
}
