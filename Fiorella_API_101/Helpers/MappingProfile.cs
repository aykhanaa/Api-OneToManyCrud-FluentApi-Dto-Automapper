using AutoMapper;
using Fiorella_API_101.DTOs.Blogs;
using Fiorella_API_101.Models;
using System.Diagnostics.Metrics;

namespace Fiorella_API_101.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BlogCreateDto, Blog>();

            CreateMap<Blog, BlogDto>();

            CreateMap<BlogEditDto, Blog>();




        }
    }
}
