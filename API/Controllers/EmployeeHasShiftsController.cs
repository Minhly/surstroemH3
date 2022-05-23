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
    public class EmployeeHasShiftsController : ControllerBase
    {
        private IEmployeeHasShiftRepository _employeeHasShiftRepository;
        public EmployeeHasShiftsController(IEmployeeHasShiftRepository employeeHasShiftRepository)
        {
            _employeeHasShiftRepository = employeeHasShiftRepository;
        }

        // GET: api/EmployeeHasShifts/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeHasShift(int id)
        {
            if (!await _employeeHasShiftRepository.entityExists(id))
                return NotFound();
            var EmployeeHasShift = await _employeeHasShiftRepository.GetById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(EmployeeHasShift);
        }

        //api/EmployeeHasShifts
        [HttpGet]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetEmployeeHasShifts()
        {
            var EmployeeHasShifts = await _employeeHasShiftRepository.GetAllAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var EmployeeHasShiftDto = new List<EmployeeShiftsDto>();

            foreach (var employee in EmployeeHasShifts)
            {
                EmployeeHasShiftDto.Add(new EmployeeShiftsDto
                {
                    Id = employee.EmployeeId,
                    ShiftDate = employee.Date
                });
            }
            return Ok(EmployeeHasShiftDto);
        }

        //api/EmployeeHasShifts
        [HttpGet("GetAllShiftsByEmployeeId/{employeeId}")]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllShiftsByEmployeeId(int employeeId)
        {
            var employeeHasShifts = await _employeeHasShiftRepository.GetAllShiftsByEmployeeId(employeeId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employeeHasShiftDto = new List<EmployeeShiftsDto>();

            foreach(var employeeShift in employeeHasShifts) { 
            employeeHasShiftDto.Add(new EmployeeShiftsDto
            {
                Id = employeeShift.Id,
                ShiftDate = employeeShift.Date,
                Employee = new EmployeeDto(employeeShift.Employee),
                Shift = new ShiftInfoDto(employeeShift.Shifts)
            });
            }
            return Ok(employeeHasShiftDto);
        }

        //api/EmployeeHasShifts
        [HttpGet("GetAllShiftsByEmployees")]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllShiftsByEmployees()
        {
            var employeeHasShifts = await _employeeHasShiftRepository.GetAllShiftsByEmployees();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employeeHasShiftDto = new List<EmployeeShiftsDto>();

            foreach (var employeeShift in employeeHasShifts)
            {
                employeeHasShiftDto.Add(new EmployeeShiftsDto
                {
                    Id = employeeShift.Id,
                    ShiftDate = employeeShift.Date,
                    Employee = new EmployeeDto(employeeShift.Employee),
                    Shift = new ShiftInfoDto(employeeShift.Shifts)
                });
            }
            return Ok(employeeHasShiftDto);
        }

        //api/EmployeeHasShifts
        [HttpPost]
        public async Task<IActionResult> CreateEmployeeHasShift([FromBody] EmployeeHasShift EmployeeHasShiftToCreate)
        {
            if (EmployeeHasShiftToCreate == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _employeeHasShiftRepository.Insert(EmployeeHasShiftToCreate);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction("GetEmployeeHasShift", new { id = EmployeeHasShiftToCreate.Id }, EmployeeHasShiftToCreate);
        }


        //api/EmployeeHasShifts/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployeeHasShift(int id, [FromBody] EmployeeHasShift updateEmployeeHasShift)
        {
            if (updateEmployeeHasShift == null)
            {
                return BadRequest(ModelState);
            }
            if (id != updateEmployeeHasShift.Id)
            {
                return BadRequest(ModelState);
            }
            if (!await _employeeHasShiftRepository.entityExists(id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _employeeHasShiftRepository.Update(updateEmployeeHasShift);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        // DELETE: api/EmployeeHasShifts/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeHasShift(int id)
        {
            if (!await _employeeHasShiftRepository.entityExists(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _employeeHasShiftRepository.Delete(id);
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
