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
    public class PostalCodesController : ControllerBase
    {
        private IPostalCodeRepository _postalCodeRepository;
        public PostalCodesController(IPostalCodeRepository postalCodeRepository)
        {
            _postalCodeRepository = postalCodeRepository;
        }

        // GET: api/PostalCodes/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostalCode(int id)
        {
            if (!await _postalCodeRepository.entityExists(id))
                return NotFound();
            var product = await _postalCodeRepository.GetById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(product);
        }

        //api/Address
        [HttpGet]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetPostalCodes()
        {
            var postalCodes = await _postalCodeRepository.GetAllAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(postalCodes);
        }

        //api/Address
        [HttpGet("WithAllInfo")]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetPostalCodesWithAllInfo()
        {
            var postalCodes = await _postalCodeRepository.GetPostalsWithAllInfo();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var postalCodeDto = new List<PostalCodeDto>();

            foreach (var postalCode in postalCodes)
            {
                postalCodeDto.Add(new PostalCodeDto
                {
                    Id = postalCode.Id,
                    Postal = postalCode.PostalCode1,
                    City = postalCode.CityName,
                    Country = new CountryDto(postalCode.Country)
                });
            }
            return Ok(postalCodeDto);
        }

        //api/PostalCodes
        [HttpPost]
        public async Task<IActionResult> CreatePostalCode([FromBody] PostalCode PostalCodeToCreate)
        {
            if (PostalCodeToCreate == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _postalCodeRepository.Insert(PostalCodeToCreate);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction("GetPostalCode", new { id = PostalCodeToCreate.Id }, PostalCodeToCreate);
        }


        //api/PostalCodes/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePostalCode(int id, [FromBody] PostalCode updatePostalCode)
        {
            if (updatePostalCode == null)
            {
                return BadRequest(ModelState);
            }
            if (id != updatePostalCode.Id)
            {
                return BadRequest(ModelState);
            }
            if (!await _postalCodeRepository.entityExists(id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _postalCodeRepository.Update(updatePostalCode);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        // DELETE: api/PostalCodes/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostalCode(int id)
        {
            if (!await _postalCodeRepository.entityExists(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _postalCodeRepository.Delete(id);
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