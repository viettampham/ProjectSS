using System.Collections.Generic;
using ProjectSS.Models.RequestModels;
using ProjectSS.Models.ViewModels;

namespace ProjectSS.Services
{
    public interface IProductService
    {
        ProductResponse CreateProduct(CreateProductRequest request);
        ProductResponse EditProduct(EditProductRequest request);
        ProductResponse DeleteProduct(DeleteProductRequest request);
        List<GetListProductResponse> GetListProduct();
    }
}