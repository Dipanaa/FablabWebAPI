using AutoMapper;
using FablabWebAPI.DTOs;
using FablabWebAPI.DTOs.ProyectosDtos;
using FablabWebAPI.DTOs.UsuarioProyectoDtos;
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

            //Datos UsuarioProyecto a usuarios

            CreateMap<UsuarioProyecto, UsuarioDto>()
                .ForMember(dto => dto.Id, config => config.MapFrom(up => up.UsuarioId))
                .IncludeMembers(up => up.Usuario);



            //Mappers de proyectos


            //Datos create con proyectos y usuario en coleccion

            CreateMap<CreateProyectosDto, Proyectos>()
                .ForMember(pro => pro.Usuarios, config => config.MapFrom(dto => dto.Ids.Select(id => new UsuarioProyecto { UsuarioId = id })));

            CreateMap<Proyectos, ProyectosDto>();

            //ProyectosConUsuarios
            CreateMap<Proyectos, ProyectoConUsuariosDtos>()
                .ForMember(pcu => pcu.Usuarios, config => config.MapFrom(pro => pro.Usuarios.Select(up => up.Usuario)));




            //CreateMap<CreateProyectosDto, UsuarioProyecto>()
            //    .ForMember(up => up.Proyectos,
            //    config => config.MapFrom(dto =>
            //    new Proyectos { Titulo = dto.Titulo, Categoria = dto.Categoria, DescripcionProyecto = dto.DescripcionProyecto, AreaAplicacion = dto.AreaAplicacion, FechaInicio = dto.FechaInicio }));

            //Todos los proyectos de un usuario en especifico
            CreateMap<UsuarioProyecto, ProyectosDto>()
                .ForMember(dto => dto.Id, config => config.MapFrom(up => up.ProyectosId))
                .IncludeMembers(up => up.Proyectos);











        }




    }
}
