using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.api.Models.DTO;
using NZWalks.api.Repositories;

namespace NZWalks.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository _iwalkRepository;
        private readonly IMapper _imapper;
        private readonly IRegionRepository regionRepository;
        private readonly IWalkDiffcultyRepository walkDiffcultyRepository;

        public WalksController(IWalkRepository iwalkRepository, IMapper imapper,IWalkDiffcultyRepository walkDiffcultyRepository,IRegionRepository regionRepository)
        {
            _iwalkRepository = iwalkRepository;
            _imapper = imapper;
            this.regionRepository = regionRepository;
            this.walkDiffcultyRepository = walkDiffcultyRepository;
        }

        [HttpGet]

        public async Task<IActionResult> GetAllAsync()
        {
            var request = await _iwalkRepository.GetAllAsync();

            var requestList = _imapper.Map<List<Models.DTO.Walks>>(request);

            return Ok(requestList);
        }

        [HttpGet("id:guid")]
        //[Route("{id:Guid}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {

            var request = await _iwalkRepository.GetAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            var requestDTO = _imapper.Map<Models.DTO.Walks>(request);

            return Ok(requestDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(Models.DTO.AddWalksRequest request)
        {
            if (!(await validateAddWalksRequest(request)))
            {
                return BadRequest(ModelState);
            }
            var requestToDomain = new Models.Domain.Walk
            {
                RegionId = request.RegionId,
                WalkDiffcultyId = request.WalkDiffcultyId,
                Length = request.Length,
                Name = request.Name,
            };

            var responce = await _iwalkRepository.AddAsync(requestToDomain);

            var requestDTO = DomainToDto(responce);

            return Ok(requestDTO);
        }

        [HttpPut("id:guid")]
        //[Route("id:guid")]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateWalksRequest walkupdate)
        {
            if (!(await validateUpdateWalksRequest(id, walkupdate)))
            {
                return BadRequest(ModelState);
            }

            var urDomain = new Models.Domain.Walk
            {
                RegionId = walkupdate.RegionId,
                WalkDiffcultyId = walkupdate.WalkDiffcultyId,
                Length = walkupdate.Length,
                Name = walkupdate.Name,
            };

            var request = await _iwalkRepository.UpdateAsync(id, urDomain);

            if (request == null)
            {
                return NotFound();
            }

            var requestDTO = DomainToDto(request);

            return Ok(requestDTO);
        }

        [HttpDelete("id:guid")]
        //[Route("id:guid")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var request = await _iwalkRepository.DeleteAsync(id);
            if (request == null)
            {
                return NotFound();
            }
            return Ok(request);
        }

        private Models.DTO.Walks DomainToDto(Models.Domain.Walk walk) => new Models.DTO.Walks
        {
            Id = walk.Id,
            RegionId = walk.RegionId,
            WalkDiffcultyId = walk.WalkDiffcultyId,
            Length = walk.Length,
            Name = walk.Name,
        };

        #region Validator
        // Add Validator
        private async Task<bool> validateAddWalksRequest(AddWalksRequest request)
        {
            if (request == null)
            {
                ModelState.AddModelError(nameof(request), $"Add Request can not be empty");
                return false;
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                ModelState.AddModelError(nameof(request.Name), $"{request.Name} can not be null or empty or whitespace");
            }
            if(request.Length < 0)
            {
                ModelState.AddModelError(nameof(request.Length), $"{nameof(request.Length)} Can not be less than zero");
            }

            if(request.WalkDiffcultyId == Guid.Empty)
            {
                ModelState.AddModelError(nameof(request.WalkDiffcultyId), $"{nameof(request.WalkDiffcultyId)} can not be empty");
            }

            if (request.RegionId == Guid.Empty)
            {
                ModelState.AddModelError(nameof(request.RegionId), $"{nameof(request.RegionId)} can not be empty");
            }

            var isRegionExists = await regionRepository.GetAsync(request.RegionId);

            if(isRegionExists == null)
            {
                ModelState.AddModelError(nameof(request.RegionId), $"{nameof(request.RegionId)} is not exists in data");
            }

            var isWDExists = await walkDiffcultyRepository.GetAsync(request.WalkDiffcultyId);

            if (isWDExists == null)
            {
                ModelState.AddModelError(nameof(request.WalkDiffcultyId), $"{nameof(request.WalkDiffcultyId)} is not exists in data");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;

            }

            return true;
        }

        // Update Validaotr
        private async Task<bool> validateUpdateWalksRequest(Guid id,UpdateWalksRequest request)
        {
            if (request == null)
            {
                ModelState.AddModelError(nameof(request), $"Update Request can not be empty");
                return false;
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                ModelState.AddModelError(nameof(request.Name), $"{request.Name} can not be null or empty or whitespace");
            }

            if (request.Length < 0)
            {
                ModelState.AddModelError(nameof(request.Length), $"{nameof(request.Length)} Can not be less than zero");
            }

            if (request.WalkDiffcultyId == Guid.Empty)
            {
                ModelState.AddModelError(nameof(request.WalkDiffcultyId), $"{nameof(request.WalkDiffcultyId)} can not be empty");
            }

            if (request.RegionId == Guid.Empty)
            {
                ModelState.AddModelError(nameof(request.RegionId), $"{nameof(request.RegionId)} can not be empty");
            }

            var isRegionExists = await regionRepository.GetAsync(request.RegionId);

            if (isRegionExists == null)
            {
                ModelState.AddModelError(nameof(request.RegionId), $"{nameof(request.RegionId)} is not exists in data");
            }

            var isWDExists = await walkDiffcultyRepository.GetAsync(request.WalkDiffcultyId);

            if (isWDExists == null)
            {
                ModelState.AddModelError(nameof(request.WalkDiffcultyId), $"{nameof(request.WalkDiffcultyId)} is not exists in data");
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
