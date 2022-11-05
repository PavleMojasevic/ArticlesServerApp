using AutoMapper;
using ServerApp.DTO;
using ServerApp.Infrastucture;
using ServerApp.Interfaces;
using ServerApp.Models;

namespace ServerApp.Services
{
    public class ArticleService : IArticleService
    {
        private VoziNaStrujuDbContext _voziNaStrujuDbContext;
        private readonly IMapper _mapper;
        public bool Add(ArticleDTO articleDTO)
        {
            Article article = _mapper.Map<Article>(articleDTO);
            _voziNaStrujuDbContext.Articles.Add(article);
            return true;
        }

        public bool DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public List<ArticleDTO> Get()
        {
            List<ArticleDTO> articles=_mapper.Map<List<ArticleDTO>>(_voziNaStrujuDbContext.Articles).ToList();
            return articles;
        }

        public ArticleDTO GetById(int id)
        {
            ArticleDTO article = _mapper.Map<ArticleDTO>(_voziNaStrujuDbContext.Articles.Find(id));
            return article;
        }

        public bool Update(int id,ArticleDTO articleDTO)
        {
            throw new NotImplementedException();
        }
    }
}
