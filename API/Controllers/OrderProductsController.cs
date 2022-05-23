using API.Dtos;
using API.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using surstroem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderProductsController : ControllerBase
    {
        private IOrderProductRepository _orderProductRepository;
        public OrderProductsController(IOrderProductRepository orderProductRepository)
        {
            _orderProductRepository = orderProductRepository;
        }

        // GET: api/OrderProducts/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderProduct(int id)
        {
            if (!await _orderProductRepository.entityExists(id))
                return NotFound();
            var orderProduct = await _orderProductRepository.GetById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(orderProduct);
        }

        //api/OrderProduct
        [HttpGet/*, Authorize(Roles = "Normal, Admin")*/]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetOrderProducts()
        {
            var orderProducts = await _orderProductRepository.GetAllAsync();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(orderProducts);
        }
        
        [HttpGet("GetListOfProductsByOrder/{orderId}")]
        public async Task<IActionResult> GetOrderProductsByOrderId(int orderId) 
        {
            var orderProductDtos = new List<OrderProductDto>();
            var products = new List<ProductDto>();
            HttpClient client = new HttpClient();

            var orderProducts = await _orderProductRepository.GetOrderProductsByOrderId(orderId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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

            foreach (var orderProduct in orderProducts) 
            {
                ProductDto productName = products.First(q => q.Id == orderProduct.ProductsId);
                orderProductDtos.Add(new OrderProductDto
                {
                    Id = orderProduct.Id,
                    Price = orderProduct.Price,
                    Quantity = orderProduct.Quantity,
                    ProductName = productName.Title
                    //Order = new OrderDto(orderProduct.Order)
                });
            }
            return Ok(orderProductDtos);
        }

        [HttpGet("GetOrdersByProductId/{productId}")]
        public async Task<IActionResult> GetOrderByProductId(int productId)
        {
            var orderProducts = await _orderProductRepository.GetOrdersByProductId(productId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderDto = new List<OrderDto>();

            foreach (var orderProduct in orderProducts)
            {
                orderDto.Add(new OrderDto(orderProduct.Order));
            }
            return Ok(orderDto);
        }

        //api/OrderProducts
        [HttpPost]
        public async Task<IActionResult> CreateOrderProduct([FromBody] OrderProduct creditCardToCreate)
        {
            if (creditCardToCreate == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _orderProductRepository.Insert(creditCardToCreate);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction("GetOrderProduct", new { id = creditCardToCreate.Id }, creditCardToCreate);
        }


        //api/OrderProducts/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderProduct(int id, [FromBody] OrderProduct updateOrderProduct)
        {
            if (updateOrderProduct == null)
            {
                return BadRequest(ModelState);
            }
            if (id != updateOrderProduct.Id)
            {
                return BadRequest(ModelState);
            }
            if (!await _orderProductRepository.entityExists(id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _orderProductRepository.Update(updateOrderProduct);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        // DELETE: api/OrderProducts/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderProduct(int id)
        {
            if (!await _orderProductRepository.entityExists(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _orderProductRepository.Delete(id);
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
