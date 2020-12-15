using System;

namespace ornico.common.infrastructure.Exceptions.Controllers.Orders
{
    public class OrderControllerInitializeException : Exception
    {
        public OrderControllerInitializeException()
        {
        }

        public override string Message => $" Orders Controller initialization failed!";
    }
}
