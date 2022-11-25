using System.Collections.Generic;
using ProjectSS.Models.RequestModels;
using ProjectSS.Models.ViewModels;

namespace ProjectSS.Services
{
    public interface ICategoryService
    {
        CategoryResponse CreateCategory(CreateCategoryRequest request);
        CategoryResponse EditCategory(EditCategoryRequest request);
        CategoryResponse DeleteCategory(DeleteCategoryRequest request);
        List<CategoryResponse> GetlistCategory();
        
        
    }
}