using API.Dtos;
using API.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
    public class AddressesController : ControllerBase
    {
        private IAddressRepository _addressRepository;
        public AddressesController(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        // GET: api/Addresss/1
        [HttpGet("{id}")/*, Authorize(Roles = "Admin")*/]
        public async Task<IActionResult> GetAddress(int id)
        {
            if (!await _addressRepository.entityExists(id))
                return NotFound();
            var address = await _addressRepository.GetById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(address);
        }

        //api/Address
        [HttpGet]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAddresses()
        {
            var addresses = await _addressRepository.GetAllAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(addresses);
        }

        [HttpGet("GetAllAddressInfo")]
        public async Task<IActionResult> GetAllInfoAddresses() 
        {
            var addresses = await _addressRepository.GetAddressWithAllInfo();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addressDto = new List<AddressDto>();

            foreach (var address in addresses)
            {
                addressDto.Add(new AddressDto
                {
                    Id = address.Id,
                    StreetName = address.StreetName,
                    HouseNumber = address.HouseNumber,
                    Floor = address.Floor,
                    Additional = address.Additional,
                    PostalCode = new PostalCodeDto(address.PostalCode)
                });
            }
            return Ok(addressDto);
        }

        //api/Addresss
        [HttpPost]
        public async Task<IActionResult> CreateAddress([FromBody] Address creditCardToCreate)
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
                await _addressRepository.Insert(creditCardToCreate);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction("GetAddress", new { id = creditCardToCreate.Id }, creditCardToCreate);
        }


        //api/Addresss/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress(int id, [FromBody] Address updateAddress)
        {
            if (updateAddress == null)
            {
                return BadRequest(ModelState);
            }
            if (id != updateAddress.Id)
            {
                return BadRequest(ModelState);
            }
            if (!await _addressRepository.entityExists(id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _addressRepository.Update(updateAddress);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        // DELETE: api/Addresss/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            if (!await _addressRepository.entityExists(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _addressRepository.Delete(id);
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
