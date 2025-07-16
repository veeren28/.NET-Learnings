
using AutoMapper;
using DTOpractice.Models;


namespace DTOpractice
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<UserModel, UserDTO>();
            CreateMap<CreateUserDTO, UserModel >();
        }
    }
}
