using ornico.core.contracts.Products;

namespace ornico.core.contracts.V1
{
    public interface IProductsControllerDependencyBlock
    {
        ICreateProductProcessor CreateProductProcessor { get; }
        IInquiryProductProcessor InquiryProductProcessor { get; }
        IUpdateProductProcessor UpdateProductProcessor { get; }
        IInquiryAllProductsProcessor InquiryAllProductsProcessor { get; }
        IDeleteProductProcessor DeleteProductProcessor { get; }
    }
}