using System;

namespace ornico.common.infrastructure.Exceptions.Domain.Products
{
    public class ProductDoesNotExistException : Exception
    {
        public Guid ProductId { get; }

        public ProductDoesNotExistException(Guid productId) => ProductId = productId;
        public override string Message => $"Product with Id: {ProductId}  doesn't exists!";
    }
}
