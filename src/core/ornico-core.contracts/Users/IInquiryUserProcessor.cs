using System.Threading.Tasks;
using ornico.common.dtos.DTOs.Users;

namespace ornico.core.contracts.Users
{
    public interface IInquiryUserProcessor
    {
        Task<UserUiModel> GetUserByLoginAsync(string login);
        Task<UserForRetrievalUiModel> GetUserAuthJwtTokenByLoginAndPasswordAsync(string login, string password);
        Task<bool> SearchIfAnyPersonByEmailOrUsernameExistsAsync(string email, string username);
    }
}