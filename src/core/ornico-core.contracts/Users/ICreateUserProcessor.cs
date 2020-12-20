using System.Threading.Tasks;
using ornico.common.dtos.DTOs.Accounts;
using ornico.common.dtos.DTOs.Users;

namespace ornico.core.contracts.Users
{
    public interface ICreateUserProcessor
    {
        Task<UserUiModel> CreateUserAsync(UserForRegistrationUiModel newUserForRegistration);
    }
}