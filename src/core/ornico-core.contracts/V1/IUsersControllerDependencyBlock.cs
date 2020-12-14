using ornico.core.contracts.Users;

namespace magic.button.cms.contracts.V1
{
    public interface IUsersControllerDependencyBlock
    {
        IInquiryUserProcessor InquiryUserProcessor { get; }
    }
}