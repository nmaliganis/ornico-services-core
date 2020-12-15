using System.Threading.Tasks;
using ornico.common.dtos.DTOs.Users;
using ornico.common.infrastructure.TypeMappings;
using ornico.core.contracts.Users;
using ornico.core.repository.ContractRepositories;

namespace ornico.core.services.Users
{
    public class InquiryUserProcessor : IInquiryUserProcessor
    {
        private readonly IAutoMapper _autoMapper;
        private readonly IUserRepository _userRepository;
        public InquiryUserProcessor(IUserRepository userRepository, IAutoMapper autoMapper)
        {
            _userRepository = userRepository;
            _autoMapper = autoMapper;
        }

        public Task<UserUiModel> GetUserByLoginAsync(string login)
        {
            return Task.Run(() => _autoMapper.Map<UserUiModel>(_userRepository.FindUserByLogin(login)));
        }

        public Task<UserForRetrievalUiModel> GetAuthUserByLoginAsync(string login)
        {
            return Task.Run(() => _autoMapper.Map<UserForRetrievalUiModel>(_userRepository.FindUserByLogin(login)));
        }
    }
}