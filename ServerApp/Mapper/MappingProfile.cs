using AutoMapper;
using ServerApp.DTO;
using ServerApp.Models;

namespace ServerApp.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Article, ArticleDTO>().ReverseMap();
            CreateMap<Comment, CommentDTO>().ReverseMap(); 
            CreateMap<Comment, CommentAddDTO>().ReverseMap(); 
            CreateMap<Tag, TagDTO>().ReverseMap();
        }
    }
}
