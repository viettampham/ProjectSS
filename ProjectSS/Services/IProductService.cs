using System;
using System.Collections.Generic;
using ProjectSS.Models;
using ProjectSS.Models.RequestModels;
using ProjectSS.Models.ViewModels;

namespace ProjectSS.Services
{
    public interface IProductService
    {
        ProductResponse CreateProduct(CreateProductRequest request);
        ProductResponse EditProduct(EditProductRequest request);
        ProductResponse DeleteProduct(Guid id);
        List<GetListProductResponse> GetListProduct();
        List<string> GetBrand();
        ProductResponse GetProductById(Guid id);
    }
}