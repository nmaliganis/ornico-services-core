using ornico.core.contracts.Users;
using ornico.core.contracts.V1;

namespace ornico.core.services.V1
{
    public class UsersControllerDependencyBlock : IUsersControllerDependencyBlock
    {
        public UsersControllerDependencyBlock(IInquiryUserProcessor inquiryVehicleProcessor)

        {
            InquiryUserProcessor = inquiryVehicleProcessor;
        }

        public IInquiryUserProcessor InquiryUserProcessor { get; }
    }
}