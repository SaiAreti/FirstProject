using AutoMapper;
using FirstProject.Api.CustomModelVaildateAtrribute;
using FirstProject.Api.Model.Domain;
using FirstProject.Api.Model.DTO;
using FirstProject.Api.Repositires;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FirstProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }



        [HttpPost]
        [VaildateModel]
        //[Authorize(Roles = "reader")]
        public async Task<IActionResult> Create([FromBody] AddRequestWalkDTO addRequestwalkDto)
        {
            
                var walkDomianModel = mapper.Map<Walk>(addRequestwalkDto);

                await walkRepository.CreateAsync(walkDomianModel);

                return Ok(mapper.Map<WalkDTO>(walkDomianModel));
   
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalks([FromQuery] int PageNumber = 1, int PageSize =1000)
        {
            var walksDomainModel = await walkRepository.GetAllWalkAsync(PageNumber,PageSize);

            throw new Exception("This is a test exception");

            return Ok(mapper.Map<List<WalkDTO>>(walksDomainModel));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            var walksDomainModel = await walkRepository.GetByIdAsync(id);
            if (walksDomainModel == null) 
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDTO>(walksDomainModel));
        }

        [HttpPut("{id:guid}")]
        [VaildateModel]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody]UpdateWalkDTO updateWalkdto)
        {
           
            
                var walksDomainModel = mapper.Map<Walk>(updateWalkdto);

                walksDomainModel = await walkRepository.UpdateAsync(id, walksDomainModel);
                return Ok(mapper.Map<WalkDTO>(walksDomainModel));
            
           
           

        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walksDomainModel = await walkRepository.DeleteByIdAsync(id);
            if (walksDomainModel == null) 
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDTO>(walksDomainModel));
        } 

    }
}
