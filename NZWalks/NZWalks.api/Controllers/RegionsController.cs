using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.api.Models.Domains;
using NZWalks.api.Repositories;

namespace NZWalks.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        public RegionsController(IRegionRepository regionRepository,IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]   
        public async Task<IActionResult> GetAllregions()
        {
            
            var regions = await regionRepository.GetAllAsync();

            // return DTO regions
            var regionsDTO =  mapper.Map<List<Models.DTO.Region>>(regions);
            return Ok(regionsDTO);
        }
    }
}
