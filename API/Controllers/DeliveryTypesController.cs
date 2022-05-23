using API.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using surstroem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryTypesController : ControllerBase
    {
        private IDeliveryTypeRepository _deliveryTypeRepository;
        public DeliveryTypesController(IDeliveryTypeRepository deliveryTypeRepository)
        {
            _deliveryTypeRepository = deliveryTypeRepository;
        }

        // GET: api/DeliveryTypes/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeliveryType(int id)
        {
            if (!await _deliveryTypeRepository.entityExists(id))
                return NotFound();
            var deliveryType = await _deliveryTypeRepository.GetById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(deliveryType);
        }

        //api/DeliveryTypes
        [HttpGet]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetDeliveryTypes()
        {
            var deliveryTypes = await _deliveryTypeRepository.GetAllAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(deliveryTypes);
        }

        //api/DeliveryTypes
        [HttpPost]
        public async Task<IActionResult> CreateDeliveryType([FromBody] DeliveryType DeliveryTypeToCreate)
        {
            if (DeliveryTypeToCreate == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _deliveryTypeRepository.Insert(DeliveryTypeToCreate);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction("GetDeliveryType", new { id = DeliveryTypeToCreate.Id }, DeliveryTypeToCreate);
        }


        //api/DeliveryTypes/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDeliveryType(int id, [FromBody] DeliveryType updateDeliveryType)
        {
            if (updateDeliveryType == null)
            {
                return BadRequest(ModelState);
            }
            if (id != updateDeliveryType.Id)
            {
                return BadRequest(ModelState);
            }
            if (!await _deliveryTypeRepository.entityExists(id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _deliveryTypeRepository.Update(updateDeliveryType);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        // DELETE: api/DeliveryTypes/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeliveryType(int id)
        {
            if (!await _deliveryTypeRepository.entityExists(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _deliveryTypeRepository.Delete(id);
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