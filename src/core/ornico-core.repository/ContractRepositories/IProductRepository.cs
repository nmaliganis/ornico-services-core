using System;
using System.Collections.Generic;
using ornico.common.infrastructure.Domain;
using ornico.core.model.Products;

namespace ornico.core.repository.ContractRepositories
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
        //QueryResult<Product> FindAllProductsPagedOf(int? pageNum, int? pageSize);
        //Product FindByFirstNameAndLastName(string firstName, string lastName);
        IList<Product> FindActiveProducts(bool active);
        Product FindProductByEmail(string email);
        Product FindProductByUserId(Guid userId);
        IList<Product> FindProductByEmailOrLogin(string email, string login);
        IList<Product> FindProductsForRoutes();
        Product FindOneBy(Guid ProductId);
    }
}
