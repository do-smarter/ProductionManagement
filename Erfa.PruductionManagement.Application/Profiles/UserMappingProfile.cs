using AutoMapper;
using Erfa.PruductionManagement.Application.Features.User;
using System.IdentityModel.Tokens.Jwt;

namespace Erfa.PruductionManagement.Application.Profiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<JwtSecurityToken, JwtTokenVm>()
                .ForMember(vm => vm.Token, jwt => jwt.MapFrom(t => t.ToString()));
        }
    }
}
