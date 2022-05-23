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
    public class DeliveryStatesController : ControllerBase
    {
        private IDeliveryStateRepository _deliveryStateRepository;
        public DeliveryStatesController(IDeliveryStateRepository deliveryStateRepository)
        {
            _deliveryStateRepository = deliveryStateRepository;
        }

        // GET: api/DeliveryStates/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeliveryState(int id)
        {
            if (!await _deliveryStateRepository.entityExists(id))
                return NotFound();
            var deliveryState = await _deliveryStateRepository.GetById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(deliveryState);
        }

        //api/DeliveryStates
        [HttpGet]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetDeliveryStates()
        {
            var DeliveryStates = await _deliveryStateRepository.GetAllAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(DeliveryStates);
        }

        //api/DeliveryStates
        [HttpPost]
        public async Task<IActionResult> CreateDeliveryState([FromBody] DeliveryState DeliveryStateToCreate)
        {
            if (DeliveryStateToCreate == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _deliveryStateRepository.Insert(DeliveryStateToCreate);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction("GetDeliveryState", new { id = DeliveryStateToCreate.Id }, DeliveryStateToCreate);
        }


        //api/DeliveryStates/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDeliveryState(int id, [FromBody] DeliveryState updateDeliveryState)
        {
            if (updateDeliveryState == null)
            {
                return BadRequest(ModelState);
            }
            if (id != updateDeliveryState.Id)
            {
                return BadRequest(ModelState);
            }
            if (!await _deliveryStateRepository.entityExists(id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _deliveryStateRepository.Update(updateDeliveryState);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        // DELETE: api/DeliveryStates/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeliveryState(int id)
        {
            if (!await _deliveryStateRepository.entityExists(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _deliveryStateRepository.Delete(id);
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
