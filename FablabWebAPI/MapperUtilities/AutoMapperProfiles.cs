using AutoMapper;
using FablabWebAPI.DTOs;
using FablabWebAPI.Entities;

namespace FablabWebAPI.MapperUtilities
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles() {


            CreateMap<Noticias, NoticiaDto>();
            CreateMap<NoticiaDto, Noticias>();



        }




    }
}
