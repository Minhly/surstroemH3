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
    public class ReviewOpinionsController : ControllerBase
    {
        private IReviewOpinionRepository _reviewOpinionRepository;
        public ReviewOpinionsController(IReviewOpinionRepository reviewOpinionRepository)
        {
            _reviewOpinionRepository = reviewOpinionRepository;
        }

        // GET: api/ReviewOpinions/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewOpinion(int id)
        {
            if (!await _reviewOpinionRepository.entityExists(id))
                return NotFound();
            var reviewOpinion = await _reviewOpinionRepository.GetById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviewOpinion);
        }

        //api/ReviewOpinions
        [HttpGet]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetReviewOpinions()
        {
            var reviewOpinions = await _reviewOpinionRepository.GetAllAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviewOpinions);
        }

        //api/ReviewOpinions/GetAllByReviewId
        [HttpGet("GetAllByReviewId/{reviewId}")]
        public async Task<IActionResult> GetReviewOpinionsByReviewId(int reviewId)
        {
            var reviewOpinions = await _reviewOpinionRepository.GetReviewOpinionsByReviewId(reviewId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reviewOpinionsDto = new List<ReviewOpinionForLikeDto>();

            foreach (var reviewOpinion in reviewOpinions)
            {
                reviewOpinionsDto.Add(new ReviewOpinionForLikeDto
                {
                    Id = reviewOpinion.Id,
                    IsLiked = reviewOpinion.IsLiked,
                    IsDisliked = reviewOpinion.IsDisliked
                });
            }

            return Ok(reviewOpinionsDto);
        }

        //api/ReviewOpinions
        [HttpPost]
        public async Task<IActionResult> CreateReviewOpinion([FromBody] ReviewOpinion reviewOpinionToCreate)
        {
            if (reviewOpinionToCreate == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _reviewOpinionRepository.Insert(reviewOpinionToCreate);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction("GetReviewOpinions", new { id = reviewOpinionToCreate.Id }, reviewOpinionToCreate);
        }

        [HttpPatch]
        public async Task<IActionResult> PatchReviewOpinion([FromBody] ReviewOpinion reviewOpinionToPatch) 
        {
            if (reviewOpinionToPatch == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                reviewOpinionToPatch.Id = await _reviewOpinionRepository.GetROByUserIdAndReviewId(reviewOpinionToPatch.UserId, reviewOpinionToPatch.ReviewId);
                await _reviewOpinionRepository.Update(reviewOpinionToPatch);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }
            return Ok();
        }

        //api/ReviewOpinions/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReviewOpinion(int id, [FromBody] ReviewOpinion updateReviewOpinion)
        {
            if (updateReviewOpinion == null)
            {
                return BadRequest(ModelState);
            }
            if (id != updateReviewOpinion.Id)
            {
                return BadRequest(ModelState);
            }
            if (!await _reviewOpinionRepository.entityExists(id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _reviewOpinionRepository.Update(updateReviewOpinion);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.GetBaseException().Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        // DELETE: api/ReviewOpinions/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReviewOpinion(int id)
        {
            if (!await _reviewOpinionRepository.entityExists(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _reviewOpinionRepository.Delete(id);
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