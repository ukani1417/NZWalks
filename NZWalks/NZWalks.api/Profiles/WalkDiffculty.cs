using AutoMapper;

namespace NZWalks.api.Profiles
{
    public class WalkDiffculty : Profile
    {
        public WalkDiffculty()
        {
            CreateMap<Models.Domain.WalkDiffculty, Models.DTO.WalkDiffculty>().ReverseMap();
        }
    }
}
