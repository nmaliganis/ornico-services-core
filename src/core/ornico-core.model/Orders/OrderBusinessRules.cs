using ornico.common.infrastructure.Domain;

namespace ornico.core.model.Orders
{
    public class OrderBusinessRules
    {
        public static BusinessRule FirstName => new BusinessRule("Order", "Order with Zero value in Totals is not valid!");
    }
}