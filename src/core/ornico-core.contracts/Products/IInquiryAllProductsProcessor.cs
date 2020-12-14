using System.Threading.Tasks;
using ornico.common.infrastructure.Helpers.ResourceParameters;
using ornico.common.infrastructure.Paging;
using ornico.core.model.Product;

namespace ornico.core.contracts.Products
{
    public interface IInquiryAllProductsProcessor
    {
      Task<PagedList<Product>> GetProductsAsync(ProductsResourceParameters productsResourceParameters);
    }
}