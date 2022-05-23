using API.Dtos;
using API.Service.Interfaces;
using API.Service.Repositories;
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
    public class ShiftsController : ControllerBase
    {
        private IShiftRepository _shiftRepository;
        public ShiftsController(IShiftRepository shiftRepository)
        {
            _shiftRepository = shiftRepository;
        }

        // GET: api/Shifts/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShift(int id)
        {
            if (!await _shiftRepository.entityExists(id))
                return NotFound();
            var shift = await _shiftRepository.GetById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(shift);
        }

        //api/Shifts
        [HttpGet]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetShifts()
        {
            var shifts = await _shiftRepository.GetAllAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            return Ok(shifts);
        }

        //api/Shifts
        [HttpGet("GetAllShiftsByWarehouseId/{warehouseId}")]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllShiftsByWarehouseId(int warehouseId)
        {
            var shifts = await _shiftRepository.GetAllShiftsByWarehouseId(warehouseId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shiftDto = new List<ShiftDto>();

            foreach (var shift in shifts)
            {
                shiftDto.Add(new ShiftDto
                {
                    Id = shift.Id,
                    ShiftStart = shift.ShiftStart,
                    ShiftEnd = shift.ShiftEnd,
                    Warehouse = new WarehouseDto(shift.Warehouse)
                });
            }

            return Ok(shiftDto);
        }

        //api/Shifts
        [HttpPost]
        public async Task<IActionResult> CreateShift([FromBody] Shift ShiftToCreate)
        {
            if (ShiftToCreate == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _shiftRepository.Insert(ShiftToCreate);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction("GetShift", new { id = ShiftToCreate.Id }, ShiftToCreate);
        }


        //api/Shifts/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShift(int id, [FromBody] Shift updateShift)
        {
            if (updateShift == null)
            {
                return BadRequest(ModelState);
            }
            if (id != updateShift.Id)
            {
                return BadRequest(ModelState);
            }
            if (!await _shiftRepository.entityExists(id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _shiftRepository.Update(updateShift);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        // DELETE: api/Shifts/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShift(int id)
        {
            if (!await _shiftRepository.entityExists(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _shiftRepository.Delete(id);
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
