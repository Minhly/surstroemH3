using API.Dtos;
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
    public class WarehouseTypesController : ControllerBase
    {
        private IWarehouseTypeRepository _warehouseTypeRepository;
        public WarehouseTypesController(IWarehouseTypeRepository warehouseTypeRepository)
        {
            _warehouseTypeRepository = warehouseTypeRepository;
        }

        // GET: api/WarehouseTypes/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWarehouse(int id)
        {
            if (!await _warehouseTypeRepository.entityExists(id))
                return NotFound();
            var warehouse = await _warehouseTypeRepository.GetById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(warehouse);
        }

        //api/WarehouseTypes
        [HttpGet]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetWarehouseTypes()
        {
            var WarehouseTypes = await _warehouseTypeRepository.GetAllAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var warehouseTypeDto = new List<WarehouseTypeDto>();

            foreach (var type in WarehouseTypes)
            {
                warehouseTypeDto.Add(new WarehouseTypeDto
                {
                    Id = type.Id,
                    Type = type.Type
                });
            }
            return Ok(warehouseTypeDto);
        }

        //api/WarehouseTypes
        [HttpPost]
        public async Task<IActionResult> CreateWarehouse([FromBody] WarehouseType warehouseToCreate)
        {
            if (warehouseToCreate == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _warehouseTypeRepository.Insert(warehouseToCreate);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction("GetWarehouse", new { id = warehouseToCreate.Id }, warehouseToCreate);
        }


        //api/WarehouseTypes/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWarehouse(int id, [FromBody] WarehouseType updateWarehouseType)
        {
            if (updateWarehouseType == null)
            {
                return BadRequest(ModelState);
            }
            if (id != updateWarehouseType.Id)
            {
                return BadRequest(ModelState);
            }
            if (!await _warehouseTypeRepository.entityExists(id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _warehouseTypeRepository.Update(updateWarehouseType);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        // DELETE: api/WarehouseTypes/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWarehouse(int id)
        {
            if (!await _warehouseTypeRepository.entityExists(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _warehouseTypeRepository.Delete(id);
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

