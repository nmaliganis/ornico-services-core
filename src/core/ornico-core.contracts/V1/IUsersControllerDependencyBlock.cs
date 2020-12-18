using ornico.core.contracts.Users;

namespace ornico.core.contracts.V1
{
  public interface IUsersControllerDependencyBlock
  {
    ICreateUserProcessor CreateUserProcessor { get; }
    IInquiryUserProcessor InquiryUserProcessor { get; }
  }
}