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
    public class CountriesController : ControllerBase
    {
        private ICountryRepository _countryRepository;
        public CountriesController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        // GET: api/Countrys/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCountry(int id)
        {
            if (!await _countryRepository.entityExists(id))
                return NotFound();
            var product = await _countryRepository.GetById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(product);
        }

        //api/Countries
        [HttpGet]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCountries()
        {
            var Countrys = await _countryRepository.GetAllAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var CountryDto = new List<CountryDto>();

            foreach (var country in Countrys)
            {
                CountryDto.Add(new CountryDto
                {
                    Id = country.Id,
                    Country = country.Country1
                });
            }
            return Ok(CountryDto);
        }

        //api/Countrys
        [HttpPost]
        public async Task<IActionResult> CreateCountry([FromBody] Country CountryToCreate)
        {
            if (CountryToCreate == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _countryRepository.Insert(CountryToCreate);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction("GetCountry", new { id = CountryToCreate.Id }, CountryToCreate);
        }


        //api/Countrys/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCountry(int id, [FromBody] Country updateCountry)
        {
            if (updateCountry == null)
            {
                return BadRequest(ModelState);
            }
            if (id != updateCountry.Id)
            {
                return BadRequest(ModelState);
            }
            if (!await _countryRepository.entityExists(id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _countryRepository.Update(updateCountry);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        // DELETE: api/Countrys/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            if (!await _countryRepository.entityExists(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _countryRepository.Delete(id);
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