using API.Dtos;
using API.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using surstroem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private IStockRepository _stockRepository;
        public StocksController(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        // GET: api/Stocks/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStock(int id)
        {
            if (!await _stockRepository.entityExists(id))
                return NotFound();
            var stock = await _stockRepository.GetById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(stock);
        }

        // GET: api/Stocks/1
        [HttpGet("GetProductByWarehouseId/{id}")]
        public async Task<IActionResult> GetProductByWarehouseId(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _stockRepository.entityExists(id))
                return NotFound();
            var products = new List<ProductDto>();
            var stocks = await _stockRepository.GetProductByWarehouseId(id);
            HttpClient client = new HttpClient();

            string url = "http://10.130.54.110/api/products/";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<List<ProductDto>>(jsonString);
            }
            else
            {
                return BadRequest(ModelState);
            }

            var stockDto = new List<StockDto>();
            foreach (var stock in stocks)
            {
                ProductDto productName = products.First(q => q.Id == stock.ProductId);
                stockDto.Add(new StockDto
                {
                    Id = stock.Id,
                    ProductId = stock.ProductId,
                    Product = productName.Title,
                    Quantity = stock.Quantity,
                    Warehouse = new WarehouseDto(stock.Warehouse)
                });
            }
            return Ok(stockDto);
        }

        [HttpGet("GetStockQuantityByProductId/{id}")]
        public async Task<IActionResult> GetStockQuantityByProductId(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _stockRepository.entityExists(id))
                return NotFound();
            var products = new List<ProductDto>();
            var stocks = await _stockRepository.GetStockQuantityByProductId(id);
            HttpClient client = new HttpClient();

            string url = "http://10.130.54.110/api/products/";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<List<ProductDto>>(jsonString);
            }
            else
            {
                return BadRequest(ModelState);
            }

            var stockDto = new List<StockDto>();
            foreach (var stock in stocks)
            {
                ProductDto productName = products.First(q => q.Id == stock.ProductId);
                stockDto.Add(new StockDto
                {
                    Id = stock.Id,
                    ProductId = stock.ProductId,
                    Product = productName.Title,
                    Quantity = stock.Quantity,
                    Warehouse = new WarehouseDto(stock.Warehouse)
                });
            }
            return Ok(stockDto);
        }

        //api/Stocks
        [HttpGet]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetStocks()
        {
            var stocks = await _stockRepository.GetAllAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(stocks);
        }

        //api/Stocks
        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] Stock stockToCreate)
        {
            if (stockToCreate == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _stockRepository.Insert(stockToCreate);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction("GetStock", new { id = stockToCreate.Id }, stockToCreate);
        }


        //api/Stocks/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStock(int id, [FromBody] Stock updateStock)
        {
            if (updateStock == null)
            {
                return BadRequest(ModelState);
            }
            if (id != updateStock.Id)
            {
                return BadRequest(ModelState);
            }
            if (!await _stockRepository.entityExists(id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _stockRepository.Update(updateStock);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        // DELETE: api/Stocks/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock(int id)
        {
            if (!await _stockRepository.entityExists(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _stockRepository.Delete(id);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}