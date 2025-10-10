using AutoMapper;
using FablabWebAPI.DTOs;
using FablabWebAPI.DTOs.UsuariosDtos;
using FablabWebAPI.Entities;

namespace FablabWebAPI.MapperUtilities
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles() {

            //Mappers de Noticias
            CreateMap<Noticias, NoticiaDto>().ReverseMap();

            //Mapper de Usuarios

            //Datos normales 
            CreateMap<Usuario,UsuarioDto>().ReverseMap();

            //Datos Put
            CreateMap<Usuario, UsuarioPutDto>().ReverseMap();



            CreateMap<Usuario, UsuarioDto>()
                .ForMember(userDto => userDto.Telefono, config => config.MapFrom(user => user.PhoneNumber));






        }




    }
}
