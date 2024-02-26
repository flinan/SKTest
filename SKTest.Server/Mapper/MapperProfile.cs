using AutoMapper;
using SKTest.Db.Entities;
using SKTest.Server.Dtos;

namespace SKTest.Server.Mapper
{
    public class MapperProfile: Profile
    {

        public MapperProfile() {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
