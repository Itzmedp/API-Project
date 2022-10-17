using AutoMapper;
using ProjectAPI.Data.EFModels;
using ProjectsAPI.Models;

namespace ProjectsAPI
{
    public class MapperService : Profile
    {
        public MapperService()
        {
            CreateMap<RegisterModel, Registration>();
        }
    }
}
