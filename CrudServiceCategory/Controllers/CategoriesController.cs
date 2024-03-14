using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebFormApp.BLL.DTOs;
using MyWebFormApp.BLL.Interfaces;
using MyWebFormApp.BLL;

namespace CrudServiceCategory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryBLL _categoryBLL;

        public CategoriesController(ICategoryBLL categoryBLL)
        {
            _categoryBLL = categoryBLL;
        }

        [HttpGet]
        public IEnumerable<CategoryDTO> Get() 
        {
            var categories = _categoryBLL.GetAll();
            return categories;
        }

        [HttpDelete]
        public void Delete(int id)
        {
            _categoryBLL.Delete(id);
        }

        [HttpGet("{id}")]
        public CategoryDTO GetById(int id) 
        { 
            var DTPCategory = _categoryBLL.GetById(id);
            return DTPCategory;
        }

        [HttpGet("GetByName/{name}")]
        public IEnumerable<CategoryDTO> GetByName(string name) 
        { 
            var DTPCategory = _categoryBLL.GetByName(name);
            return DTPCategory;
        }

        [HttpGet("GetCountCategories")]
        public int GetCountCategories(string name)
        {
            return _categoryBLL.GetCountCategories(name);
        }

        [HttpGet("{pageNumber}/{pageSize}/name")]
        public IEnumerable<CategoryDTO> GetWithPaging(int pageNumber, int pageSize, string name)
        {
            var DTPCategory = _categoryBLL.GetWithPaging(pageNumber, pageSize, name);
            return DTPCategory;
        }

        [HttpPost]
        public void Insert(CategoryCreateDTO entity)
        {
            _categoryBLL.Insert(entity);
        }
        [HttpPut]
        public void Update(CategoryUpdateDTO entity)
        {
            _categoryBLL.Update(entity);
        }
    }
}
