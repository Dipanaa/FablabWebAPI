using AutoMapper;
using FablabWebAPI.DTOs;
using FablabWebAPI.Entities;

namespace FablabWebAPI.MapperUtilities
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles() {

            //Mappers de Noticias
            CreateMap<Noticias, NoticiaDto>().ReverseMap();

            //Mapper de Usuarios
            CreateMap<Usuario,UsuarioDto>().ReverseMap();
            CreateMap<Usuario, UsuarioDto>()
                .ForMember(userDto => userDto.Telefono, config => config.MapFrom(user => user.PhoneNumber));






        }




    }
}
