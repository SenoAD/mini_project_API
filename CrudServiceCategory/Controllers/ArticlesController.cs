using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebFormApp.BLL.DTOs;
using MyWebFormApp.BLL.Interfaces;

namespace CrudServiceCategory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleBLL _articleBLL;

        public ArticlesController(IArticleBLL articleBLL)
        {
            _articleBLL = articleBLL;
        }
        [HttpDelete]
        public void Delete(int id) 
        { 
            _articleBLL.Delete(id);
        }
        [HttpGet("GetArticleByCategory")]
        public IEnumerable<ArticleDTO> GetArticleByCategory(int categoryId) 
        { 
            var DTOarticle = _articleBLL.GetArticleByCategory(categoryId);
            return DTOarticle;
        }

        [HttpGet("GetArticleById")]
        public ArticleDTO GetArticleById(int id)
        {
            var DTOarticle = _articleBLL.GetArticleById(id);
            return DTOarticle;
        }

        [HttpGet("GetArticleWithCategory")]
        public IEnumerable<ArticleDTO> GetArticleWithCategory()
        {
            var DTOarticle = _articleBLL.GetArticleWithCategory();
            return DTOarticle;
        }

        [HttpGet("GetCountArticles")]
        public int GetCountArticles()
        {
            return _articleBLL.GetCountArticles();
        }

        [HttpGet("GetWithPaging")]
        public IEnumerable<ArticleDTO> GetWithPaging(int categoryId, int pageNumber, int pageSize)
        {
            return _articleBLL.GetWithPaging(categoryId, pageNumber, pageSize);
        }

        [HttpPost]
        public void Insert(ArticleCreateDTO articleDto)
        {
            _articleBLL.Insert(articleDto);
        }

        [HttpPost("InsertWithIdentity")]
        public int InsertWithIdentity(ArticleCreateDTO articleDto)
        {
            return _articleBLL.InsertWithIdentity(articleDto);
        }

        [HttpPut]
        public void Update(ArticleUpdateDTO article)
        {
            _articleBLL.Update(article);
        }


    }
}
