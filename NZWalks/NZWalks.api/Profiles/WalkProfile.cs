using AutoMapper;

namespace NZWalks.api.Profiles
{
    public class WalkProfile : Profile
    {
        public WalkProfile()
        {
            CreateMap<Models.Domain.Walk, Models.DTO.Walks>().ReverseMap();
        }
    }
}
