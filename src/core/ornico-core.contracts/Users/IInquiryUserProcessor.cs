using System.Threading.Tasks;
using ornico.common.dtos.DTOs.Users;

namespace ornico.core.contracts.Users
{
    public interface IInquiryUserProcessor
    {
        Task<UserUiModel> GetUserByLoginAsync(string login);
        Task<UserForRetrievalUiModel> GetAuthUserByLoginAsync(string login);
    }
}