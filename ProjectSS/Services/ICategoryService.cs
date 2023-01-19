using System;
using System.Collections.Generic;
using ProjectSS.Models.RequestModels;
using ProjectSS.Models.ViewModels;

namespace ProjectSS.Services
{
    public interface ICategoryService
    {
        CategoryResponse CreateCategory(CreateCategoryRequest request);
        CategoryResponse EditCategory(EditCategoryRequest request);
        CategoryResponse DeleteCategory(Guid id);
        List<CategoryResponse> GetlistCategory();

        CategoryResponse GetCategoryById(Guid id);
    }
}