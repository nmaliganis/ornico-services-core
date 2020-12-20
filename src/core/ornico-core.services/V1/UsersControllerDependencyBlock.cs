using ornico.core.contracts.Users;
using ornico.core.contracts.V1;

namespace ornico.core.services.V1
{
    public class UsersControllerDependencyBlock : IUsersControllerDependencyBlock
    {
        public UsersControllerDependencyBlock(ICreateUserProcessor createUserProcessor, IInquiryUserProcessor inquiryVehicleProcessor)

        {
            InquiryUserProcessor = inquiryVehicleProcessor;
            CreateUserProcessor = createUserProcessor;
        }

        public ICreateUserProcessor CreateUserProcessor { get; }
        public IInquiryUserProcessor InquiryUserProcessor { get; }
    }
}