using ornico.core.contracts.Orders;
using ornico.core.contracts.V1;

namespace ornico.core.services.V1
{
    public class OrdersControllerDependencyBlock : IOrdersControllerDependencyBlock
    {
        public OrdersControllerDependencyBlock(ICreateOrderProcessor createOrderProcessor,
                                                        IInquiryOrderProcessor inquiryOrderProcessor,
                                                        IUpdateOrderProcessor updateOrderProcessor,
                                                        IInquiryAllOrdersProcessor allOrderProcessor,
                                                        IDeleteOrderProcessor deleteOrderProcessor)

        {
            CreateOrderProcessor = createOrderProcessor;
            InquiryOrderProcessor = inquiryOrderProcessor;
            UpdateOrderProcessor = updateOrderProcessor;
            InquiryAllOrdersProcessor = allOrderProcessor;
            DeleteOrderProcessor = deleteOrderProcessor;
        }

        public ICreateOrderProcessor CreateOrderProcessor { get; private set; }
        public IInquiryOrderProcessor InquiryOrderProcessor { get; private set; }
        public IUpdateOrderProcessor UpdateOrderProcessor { get; private set; }
        public IInquiryAllOrdersProcessor InquiryAllOrdersProcessor { get; private set; }
        public IDeleteOrderProcessor DeleteOrderProcessor { get; private set; }
    }
}