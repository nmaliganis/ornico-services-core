using System;
using ornico.common.infrastructure.Domain;
using ornico.core.model.Products;

namespace ornico.core.repository.ContractRepositories
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
        Product FindOneProductByMobile(string name);
    }
}
