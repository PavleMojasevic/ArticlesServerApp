using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerApp.DTO;
using ServerApp.Interfaces;
using ServerApp.Models;

namespace ServerApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    ICategoryService _CategoryService;
    IMapper _Mapper;

    public CategoryController(ICategoryService categoryService, IMapper mapper)
    {
        _Mapper = mapper;
        _CategoryService = categoryService;
    }

    [HttpPost]
    public async Task<IActionResult> AddCategoryAsync([FromBody] CategoryAddDTO categoryDTO)
    {
        Category category = _Mapper.Map<Category>(categoryDTO);
        if (await _CategoryService.AddAsync(category))
            return Ok();
        return BadRequest();
    }
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        List<CategoryDTO> category = _Mapper.Map<List<CategoryDTO>>(await _CategoryService.GetAsync());
        return Ok(category);
    }
    [HttpPut("{categoryId}")]
    public async Task<IActionResult> EditCategoryAsync(long categoryId, [FromBody] CategoryDTO categoryDTO)
    {
        if (await _CategoryService.EditAsync(categoryId, categoryDTO))
            return Ok();
        return BadRequest();
    }
}
