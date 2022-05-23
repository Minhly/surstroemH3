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
    public class WarehousesController : ControllerBase
    {
        private IWarehouseRepository _warehouseRepository;
        public WarehousesController(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        // GET: api/Warehouses/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWarehouse(int id)
        {
            if (!await _warehouseRepository.entityExists(id))
                return NotFound();
            var warehouse = await _warehouseRepository.GetById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(warehouse);
        }

        //api/Warehouses
        [HttpGet]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetWarehouses()
        {
            var Warehouses = await _warehouseRepository.GetAllAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var warehouseDto = new List<WarehouseDto>();

            foreach (var warehouse in Warehouses)
            {
                warehouseDto.Add(new WarehouseDto
                {
                    Id = warehouse.Id,
                    WarehouseType = new WarehouseTypeDto(warehouse.WarehouseType)
                });
            }
            return Ok(warehouseDto);
        }

        //api/Warehouses
        [HttpGet("GetWarehousesFullInfById/{id}")]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetWarehouseFullInfoById(int id)
        {
            if (!await _warehouseRepository.entityExists(id))
                return NotFound();

            var warehouse = await _warehouseRepository.GetWarehouseFullInfoById(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var warehouseDto = new WarehouseDto();

            warehouseDto.Id = warehouse.Id;
            warehouseDto.WarehouseType = new WarehouseTypeDto(warehouse.WarehouseType);
            warehouseDto.Address = new AddressDto(warehouse.Address);
            
            return Ok(warehouseDto);
        }

        //api/Warehouses
        [HttpGet("GetWarehousesFullInfo")]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetWarehouseFullInfo()
        {
            var warehouses = await _warehouseRepository.GetWarehouseFullInfo();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var warehouseDto = new List<WarehouseDto>();

            foreach (var warehouse in warehouses)
            {
                warehouseDto.Add(new WarehouseDto
                {
                    Id = warehouse.Id,
                    WarehouseType = new WarehouseTypeDto(warehouse.WarehouseType),
                    Address = new AddressDto(warehouse.Address)
                });
            }
            return Ok(warehouseDto);
        }

        //api/Warehouses
        [HttpPost]
        public async Task<IActionResult> CreateWarehouse([FromBody] Warehouse warehouseToCreate)
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
                await _warehouseRepository.Insert(warehouseToCreate);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction("GetWarehouse", new { id = warehouseToCreate.Id }, warehouseToCreate);
        }


        //api/Warehouses/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWarehouse(int id, [FromBody] Warehouse updateWarehouse)
        {
            if (updateWarehouse == null)
            {
                return BadRequest(ModelState);
            }
            if (id != updateWarehouse.Id)
            {
                return BadRequest(ModelState);
            }
            if (!await _warehouseRepository.entityExists(id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _warehouseRepository.Update(updateWarehouse);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        // DELETE: api/Warehouses/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWarehouse(int id)
        {
            if (!await _warehouseRepository.entityExists(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _warehouseRepository.Delete(id);
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

