using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.api.Models.DTO;
using NZWalks.api.Repositories;

namespace NZWalks.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllregionsAsync()
        {

            var regions = await regionRepository.GetAllAsync();

            // return DTO regions
            var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regions);
            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await regionRepository.GetAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population,
            };

            return Ok(regionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(AddRegionRequest regionrequest)
        {
            if (validateAddRegionRequestAsync(regionrequest) == false)
            {
                return BadRequest(ModelState);
            }

            var region = new Models.Domain.Region()
            {
                Code = regionrequest.Code,
                Name = regionrequest.Name,
                Area = regionrequest.Area,
                Lat = regionrequest.Lat,
                Long = regionrequest.Long,
                Population = regionrequest.Population,
            };

            region = await regionRepository.AddAsync(region);

            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population,
            };

            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.Id }, regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            // Get region from database
            var region = await regionRepository.DeleteAsync(id);

            // If null NotFound
            if (region == null)
            {
                return NotFound();
            }

            // Convert response back to DTO
            var regionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };


            // return Ok response
            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, Models.DTO.UpdateRegionRequest updateRegionRequest)
        {
            if(validateUpdateRegionRequest(id,updateRegionRequest) == false)
            {
                return BadRequest(ModelState);
            }
            var region = new Models.Domain.Region()
            {
                Code = updateRegionRequest.Code,
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Name = updateRegionRequest.Name,
                Population = updateRegionRequest.Population
            };


            // Update Region using repository
            region = await regionRepository.UpdateAsync(id, region);


            // If Null then NotFound
            if (region == null)
            {
                return NotFound();
            }

            // Convert Domain back to DTO
            var regionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };


            // Return Ok response
            return Ok(regionDTO);
        }
        #region Validator

        // Add validator
        private bool validateAddRegionRequestAsync(AddRegionRequest request)
        {
            if (request == null)
            {
                ModelState.AddModelError(nameof(request), $"Add Request can not be empty");
                return false;
            }
            if (string.IsNullOrWhiteSpace(request.Code))
            {
                ModelState.AddModelError(nameof(request.Code), $"{nameof(request.Code)} can not consists be null or empty or whitespace");
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                ModelState.AddModelError(nameof(request.Name), $"{nameof(request.Name)} can not consists be null or empty or whitespace");
            }

            if (request.Area <= 0.0)
            {
                ModelState.AddModelError(nameof(request.Area), $"{nameof(request.Area)} can not be zero or negative");
            }

            if (request.Population <= 0.0)
            {
                ModelState.AddModelError(nameof(request.Population), $"{nameof(request.Population)} can not be zero or negative");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }

        //Update Validator
        private bool validateUpdateRegionRequest(Guid id, UpdateRegionRequest request)
        {
            if (request == null)
            {
                ModelState.AddModelError(nameof(request), $"Update Request can not be empty");
                return false;
            }
            if (string.IsNullOrWhiteSpace(request.Code))
            {
                ModelState.AddModelError(nameof(request.Code), $"{nameof(request.Code)} can not consists be null or empty or whitespace");
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                ModelState.AddModelError(nameof(request.Name), $"{nameof(request.Name)} can not consists be null or empty or whitespace");
            }

            if (request.Area <= 0.0)
            {
                ModelState.AddModelError(nameof(request.Area), $"{nameof(request.Area)} can not be zero or negative");
            }

            if (request.Population <= 0.0)
            {
                ModelState.AddModelError(nameof(request.Population), $"{nameof(request.Population)} can not be zero or negative");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }
        #endregion


    }
}
