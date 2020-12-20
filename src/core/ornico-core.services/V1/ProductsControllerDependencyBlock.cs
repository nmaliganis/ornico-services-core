using ornico.core.contracts.Products;
using ornico.core.contracts.V1;

namespace ornico.core.services.V1
{
    public class ProductsControllerDependencyBlock : IProductsControllerDependencyBlock
    {
        public ProductsControllerDependencyBlock(ICreateProductProcessor createProductProcessor,
                                                        IInquiryProductProcessor inquiryProductProcessor,
                                                        IUpdateProductProcessor updateProductProcessor,
                                                        IDeleteProductProcessor deleteProductProcessor)

        {
            CreateProductProcessor = createProductProcessor;
            InquiryProductProcessor = inquiryProductProcessor;
            UpdateProductProcessor = updateProductProcessor;
            DeleteProductProcessor = deleteProductProcessor;
        }

        public ICreateProductProcessor CreateProductProcessor { get; private set; }
        public IInquiryProductProcessor InquiryProductProcessor { get; private set; }
        public IUpdateProductProcessor UpdateProductProcessor { get; private set; }
        public IDeleteProductProcessor DeleteProductProcessor { get; private set; }
    }
}