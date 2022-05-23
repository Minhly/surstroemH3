using API.Dtos;
using API.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using surstroem.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IOrderRepository _orderRepository;
        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        // GET: api/Orders/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            if (!await _orderRepository.entityExists(id))
                return NotFound();
            var order = await _orderRepository.GetById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(order);
        }

        //api/Orders
        [HttpGet]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderRepository.GetAllAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(orders);
        }

        [HttpGet("GetAllOrdersInfo")]
        public async Task<IActionResult> GetAllOrdersInfo()
        {
            var orders = await _orderRepository.GetOrdersWithAllInfo();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderDto = new List<OrderDto>();

            foreach (var order in orders)
            {
                orderDto.Add(new OrderDto
                {
                    Id = order.Id,
                    User = new UserDto(order.User),
                    ShippingAddress = new AddressDto(order.PayingAddress),
                    PayingAddress = new AddressDto(order.ShippingAddress),
                    DeliveryState = new DeliveryStateDto(order.DeliveryState),
                    DeliveryType = new DeliveryTypeDto(order.DeliveryType)
                });
            }
            return Ok(orderDto);
        }

        [HttpGet("GetAllByUserId/{userId}")]
        public async Task<IActionResult> GetAllOrdersByUserId(int userId)
        {
            var orders = await _orderRepository.GetOrdersFromUserId(userId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderDto = new List<OrderDto>();

            foreach (var order in orders)
            {
                orderDto.Add(new OrderDto
                {
                    Id = order.Id,
                    User = new UserDto(order.User),
                    ShippingAddress = new AddressDto(order.PayingAddress),
                    PayingAddress = new AddressDto(order.ShippingAddress),
                    DeliveryState = new DeliveryStateDto(order.DeliveryState),
                    DeliveryType = new DeliveryTypeDto(order.DeliveryType)
                });
            }
            return Ok(orderDto);
        }

        //api/Orders
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order OrderToCreate)
        {
            if (OrderToCreate == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _orderRepository.Insert(OrderToCreate);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction("GetOrder", new { id = OrderToCreate.Id }, OrderToCreate);
        }


        //api/Orders/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] Order updateOrder)
        {
            if (updateOrder == null)
            {
                return BadRequest(ModelState);
            }
            if (id != updateOrder.Id)
            {
                return BadRequest(ModelState);
            }
            if (!await _orderRepository.entityExists(id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _orderRepository.Update(updateOrder);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        // DELETE: api/Orders/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (!await _orderRepository.entityExists(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _orderRepository.Delete(id);
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