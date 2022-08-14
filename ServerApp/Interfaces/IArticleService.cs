using ServerApp.DTO;

namespace ServerApp.Interfaces
{
    public interface IArticleService
    {
        List<ArticleDTO> Get();
        ArticleDTO GetById(int id);
        bool Add(ArticleDTO articleDTO);
        bool Update(int id,ArticleDTO articleDTO);
        bool DeleteById(int id);
    }
}
