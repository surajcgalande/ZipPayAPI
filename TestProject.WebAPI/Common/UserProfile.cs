using AutoMapper;
using TestProject.WebAPI.DTO;
using TestProject.WebAPI.Models;

namespace TestProject.WebAPI.Common
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<Account, AccountDTO>();
            CreateMap<Log, LogDTO>();
        }
    }
}
