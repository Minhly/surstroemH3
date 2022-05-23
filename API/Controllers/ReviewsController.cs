using API.Dtos;
using API.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using surstroem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private IReviewRepository _reviewRepository;
        public ReviewsController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        // GET: api/Reviews/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReview(int id)
        {
            if (!await _reviewRepository.entityExists(id))
                return NotFound();
            var product = await _reviewRepository.GetById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(product);
        }

        //api/Review
        [HttpGet/*, Authorize(Roles = "Normal, Admin")*/]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetReviews()
        {
            var reviews = await _reviewRepository.GetAllAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviews);
        }

        //api/Review
        [HttpGet("ReviewByProduct/{id}")]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetReviewsByProduct(int id)
        {
            var reviews = await _reviewRepository.GetReviewsByProductId(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reviewDto = new List<ReviewForProductDto>();

            foreach (var review in reviews)
            {
                reviewDto.Add(new ReviewForProductDto
                {
                    Id = review.Id,
                    User = review.User.Firstname,
                    Star = review.Star,
                    Comment = review.Comment,
                });
            }
            return Ok(reviewDto);
        }

        [HttpGet("ReviewByUser/{id}")]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetReviewsByUser(int id)
        {
            var reviewDto = new List<ReviewByUserDto>();
            var products = new List<ProductDto>();
            
            HttpClient client = new HttpClient();

            string url = "http://10.130.54.110/api/products/";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<List<ProductDto>>(jsonString);
            }
            
            var reviews = await _reviewRepository.GetReviewsByUserId(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (var review in reviews)
            {
                ProductDto productName = products.First(q => q.Id == review.ProductId);
                reviewDto.Add(new ReviewByUserDto
                {
                    Id = review.Id,
                    Product = productName.Title,
                    Star = review.Star,
                    Comment = review.Comment
                });
            }
            return Ok(reviewDto);
        }

        //api/Reviews
        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] Review creditCardToCreate)
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
                await _reviewRepository.Insert(creditCardToCreate);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction("GetReview", new { id = creditCardToCreate.Id }, creditCardToCreate);
        }


        //api/Reviews/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(int id, [FromBody] Review updateReview)
        {
            if (updateReview == null)
            {
                return BadRequest(ModelState);
            }
            if (id != updateReview.Id)
            {
                return BadRequest(ModelState);
            }
            if (!await _reviewRepository.entityExists(id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _reviewRepository.Update(updateReview);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        // DELETE: api/Reviews/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            if (!await _reviewRepository.entityExists(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _reviewRepository.Delete(id);
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