using AutoMapper;
using FirstProject.Api.CustomModelVaildateAtrribute;
using FirstProject.Api.Data;
using FirstProject.Api.Model.Domain;
using FirstProject.Api.Model.DTO;
using FirstProject.Api.Repositires;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Options;

namespace FirstProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly WalkDbContext dbContext;
        private readonly IRepository repository;
        private readonly IMapper mapper;
        private readonly ILogger logger;

     
        public RegionsController(WalkDbContext dbContext, IRepository repository, IMapper mapper, ILogger<Region> logger)
        {
            this.dbContext = dbContext;
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }
        [HttpGet]
       // [Authorize(Roles= "writer,reader")]
        public async Task<IActionResult> GetAllRegions([FromQuery] string? filterOn, [FromQuery] string? filterQuery,[FromQuery]string? sortBy, [FromQuery] bool? isAscending)
        {
            try
            {
                var list = await repository.GetAllRegionsAsync(filterOn, filterQuery, sortBy, isAscending ?? true);
                //map/convert domain to dto
                var regionsDTO = mapper.Map<List<Region>>(list);
                return Ok(regionsDTO);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while retrieving regions");
                return StatusCode(500, "Internal server error");
            }
           
        }

        [HttpGet]
        [Route("{id}")]
       // [Authorize(Roles= "reader")]
        public async Task<IActionResult> GetId(Guid id)
        {
            var res = await repository.GetByIdAsync(id);
            if (res == null)
            {
                return NotFound();
            }
           

           var regionDTO =  mapper.Map<Region>(res);

            return Ok(regionDTO);
        }

        //create region
        //post http action methoe https//localhost:port/api/regions
        [HttpPost]
        //[VaildateModel]
        //[Authorize(Roles = "reader")]
        public async Task<IActionResult> Create([FromBody] CreateRegionDTO createRegionDtos)
        {
            
                var regionDomainModel = mapper.Map<Region>(createRegionDtos);

                regionDomainModel = await repository.CreateAsync(regionDomainModel);

                var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);


            return CreatedAtAction(nameof(GetId), new { id = regionDomainModel.Id }, regionDTO);
           

        }

        [HttpPut]
        [Route("{id:guid}")]
        [VaildateModel]
      //  [Authorize(Roles = "writer,reader")]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody] UpdateRegionDTO updateRegionDTO)
        {

                var regionDomainModel = mapper.Map<Region>(updateRegionDTO);
                regionDomainModel = await repository.UpdateAsync(id, regionDomainModel);
                //if it is null then return not found
                if (regionDomainModel == null)
                {
                    return NotFound();
                }
                var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);
                return Ok(regionDTO);
           
        }

        [HttpDelete]
        [Route("{id:guid}")]
      //  [Authorize(Roles = "writer,reader")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionModelDomain = await repository.DeleteAsync(id);
            if (regionModelDomain == null) 
            {
                return NotFound();
            }

            var regionDTO = mapper.Map<RegionDTO>(regionModelDomain);
            return Ok(regionDTO);
        }
    }
}
