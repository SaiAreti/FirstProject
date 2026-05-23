using AutoMapper;
using FirstProject.Api.Model.Domain;
using FirstProject.Api.Model.DTO;
using System.Runtime;

namespace FirstProject.Api.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //Region
            CreateMap<Region,RegionDTO>().ReverseMap();
            CreateMap<CreateRegionDTO,Region>().ReverseMap();
            CreateMap<UpdateRegionDTO,Region>().ReverseMap();

            //Walk
            CreateMap<AddRequestWalkDTO, Walk>().ReverseMap();
            CreateMap<Walk, WalkDTO>().ReverseMap();
            CreateMap<UpdateWalkDTO, Walk>().ReverseMap();

            CreateMap<Difficulty, DifficultyDTO>().ReverseMap();

        }
    }
}
