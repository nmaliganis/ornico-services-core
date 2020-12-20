using System;
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

        public Task<UserUiModel> GetUserByUserIdAsync(Guid idUser)
        {
          return Task.Run(() => _autoMapper.Map<UserUiModel>(_userRepository.FindBy(idUser)));
        }

        public Task<UserUiModel> GetUserByUsernameAsync(string login)
        {
            return Task.Run(() => _autoMapper.Map<UserUiModel>(_userRepository.FindUserByUsername(login)));
        }

        public Task<UserForRetrievalUiModel> GetUserAuthJwtTokenByLoginAndPasswordAsync(string login, string password)
        {
          return Task.Run(() => _autoMapper.Map<UserForRetrievalUiModel>(_userRepository.FindUserByUsernameAndPasswordAsync(login, password)));
        }

        public Task<bool> SearchIfAnyPersonByEmailOrUsernameExistsAsync(string email, string username)
        {
          return Task.Run(() =>  _userRepository.FindUsersByEmailOrUsername(email, username).Count > 0);
        }

        public Task<UserForRetrievalUiModel> GetAuthUserByLoginAsync(string login)
        {
            return Task.Run(() => _autoMapper.Map<UserForRetrievalUiModel>(_userRepository.FindUserByUsername(login)));
        }
    }
}