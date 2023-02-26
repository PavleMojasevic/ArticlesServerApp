using AutoMapper;
using ServerApp.DTO;
using ServerApp.Models;

namespace ServerApp.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Article, ArticleDTO>().ReverseMap();
        CreateMap<Article, ArticleAddDTO>().ReverseMap();
        
        CreateMap<Comment, CommentDTO>().ReverseMap(); 
        CreateMap<Comment, CommentAddDTO>().ReverseMap(); 
        
        CreateMap<Category, CategoryDTO>().ReverseMap(); 
        CreateMap<Category, CategoryAddDTO>().ReverseMap();  

        CreateMap<Tag, TagDTO>().ReverseMap();
        CreateMap<ArticleTag, TagDTO>().ReverseMap();

        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<User, UserAddDTO>().ReverseMap();
    }
}
