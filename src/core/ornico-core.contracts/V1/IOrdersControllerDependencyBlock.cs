using ornico.core.contracts.Orders;

namespace ornico.core.contracts.V1
{
    public interface IOrdersControllerDependencyBlock
    {
        ICreateOrderProcessor CreateOrderProcessor { get; }
        IInquiryOrderProcessor InquiryOrderProcessor { get; }
        IUpdateOrderProcessor UpdateOrderProcessor { get; }
        IInquiryAllOrdersProcessor InquiryAllOrdersProcessor { get; }
        IDeleteOrderProcessor DeleteOrderProcessor { get; }
    }
}