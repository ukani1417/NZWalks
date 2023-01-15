using Microsoft.AspNetCore.Mvc;
using NZWalks.api.Models.DTO;
using NZWalks.api.Repositories;

namespace NZWalks.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class WalkDiffcultyController : ControllerBase
    {
        private readonly IWalkDiffcultyRepository _iwalkDiffcultyRepository;

        public WalkDiffcultyController(IWalkDiffcultyRepository iwalkDiffcultyRepository)
        {
            this._iwalkDiffcultyRepository = iwalkDiffcultyRepository;
        }

        [HttpGet]

        public async Task<IActionResult> GetAllAsync()
        {
            var request = await _iwalkDiffcultyRepository.GetAllAsync();

            List<Models.DTO.WalkDiffculty> requestList = new List<Models.DTO.WalkDiffculty> { };

            foreach (var item in request)
            {
                requestList.Add(DomainToDto(item));
            }

            return Ok(requestList);
        }

        [HttpGet("id:guid")]
        //[Route("{id:Guid}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var request = await _iwalkDiffcultyRepository.GetAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            var requestDTO = DomainToDto(request);

            return Ok(requestDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(Models.DTO.AddWalkDiffcultyRequest request)
        {

            if(validateAddWDRequestAsync(request) == false)
            {
                return BadRequest(ModelState);
            }

            var requestToDomain = new Models.Domain.WalkDiffculty
            {
                Code = request.Code,
            };

            var responce = await _iwalkDiffcultyRepository.AddAsync(requestToDomain);

            var requestDTO = DomainToDto(responce);

            return Ok(requestDTO);
        }

        [HttpPut("id:guid")]
        //[Route("id:guid")]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateWalkDiffcultyRequest ur)
        {
            if(validateUpdateRequestAsync(id,ur) == false)
            {
                return BadRequest(ModelState);
            }

            var urDomain = new Models.Domain.WalkDiffculty
            {
                Id = id,
                Code = ur.Code,
            };
            var request = await _iwalkDiffcultyRepository.UpdateAsync(id, urDomain);

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
            var request = await _iwalkDiffcultyRepository.DeleteAsync(id);
            if (request == null)
            {
                return NotFound();
            }
            return Ok(request);
        }

        private Models.DTO.WalkDiffculty DomainToDto(Models.Domain.WalkDiffculty wd)
        {
            return new Models.DTO.WalkDiffculty
            {
                Id = wd.Id,
                Code = wd.Code,
            };
        }

        #region Validator
        // Add Validator
        private bool validateAddWDRequestAsync(AddWalkDiffcultyRequest request)
        {
            if (request == null)
            {
                ModelState.AddModelError(nameof(request), $"Request can not be empty");
                return false;
            }

            if (string.IsNullOrWhiteSpace(request.Code))
            {
                ModelState.AddModelError(nameof(request), $"{nameof(request.Code)} can not consists null or empty or whitespace");

            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }
        // Update Validator
        private bool validateUpdateRequestAsync(Guid id, UpdateWalkDiffcultyRequest request)
        {
            if (request == null)
            {
                ModelState.AddModelError(nameof(request), $"Request can not be empty");
                return false;
            }

            if (id == Guid.Empty)
            {
                ModelState.AddModelError(nameof(id), $"{nameof(id)} can not consists null or empty or whitespace");

            }

            if (string.IsNullOrWhiteSpace(request.Code))
            {
                ModelState.AddModelError(nameof(request.Code), $"{nameof(request.Code)} can not consists null or empty or whitespace");

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
