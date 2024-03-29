﻿using System;
using Microsoft.AspNetCore.Mvc;
using ProjectSS.Models.RequestModels;
using ProjectSS.Services;

namespace ProjectSS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController:ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        
        
        [HttpPost("Create-category")]
        public IActionResult CreateCategory(CreateCategoryRequest request)
        {
            var newCategory = _categoryService.CreateCategory(request);
            return Ok(newCategory);
        }


        [HttpPost("Edit-category")]
        public IActionResult EditProduct(EditCategoryRequest request)
        {
            var targetCategory = _categoryService.EditCategory(request);
            return Ok(targetCategory);
        }

        [HttpDelete("Delete-category/{id}")]
        public IActionResult DeleteCategory(Guid id)
        {
            var targetCategory = _categoryService.DeleteCategory(id);
            return Ok(targetCategory);
        }
        
        [HttpGet("Get-list")]
        public IActionResult GetList()
        {
            var listCategory = _categoryService.GetlistCategory();
            return Ok(listCategory);
        }

        [HttpGet("get-category-by-id/{id}")]
        public IActionResult GetCategoryById(Guid id)
        {
            var targetCategory = _categoryService.GetCategoryById(id);
            return Ok(targetCategory);
        }
    }
}