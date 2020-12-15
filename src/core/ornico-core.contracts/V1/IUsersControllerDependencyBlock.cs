using ornico.core.contracts.Users;

namespace ornico.core.contracts.V1
{
    public interface IUsersControllerDependencyBlock
    {
        IInquiryUserProcessor InquiryUserProcessor { get; }
    }
}