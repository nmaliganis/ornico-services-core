using System;
using System.Linq;
using System.Threading.Tasks;
using ornico.common.dtos.DTOs.Accounts;
using ornico.common.dtos.DTOs.Users;
using ornico.common.infrastructure.Exceptions.Domain.Users;
using ornico.common.infrastructure.TypeMappings;
using ornico.common.infrastructure.UnitOfWorks;
using ornico.core.contracts.Users;
using ornico.core.model.Users;
using ornico.core.repository.ContractRepositories;
using Serilog;

namespace ornico.core.services.Users
{
  public class CreateUserProcessor : ICreateUserProcessor
  {
    private readonly IUnitOfWork _uOf;
    private readonly IUserRepository _userRepository;
    private readonly IAutoMapper _autoMapper;

    public CreateUserProcessor(IUnitOfWork uOf, IAutoMapper autoMapper,
      IUserRepository userRepository)
    {
      _uOf = uOf;
      _userRepository = userRepository;
      _autoMapper = autoMapper;
    }

    public Task<UserUiModel> CreateUserAsync(UserForRegistrationUiModel newUserForRegistration)
    {
      var response =
        new UserUiModel()
        {
          Message = "START_CREATION"
        };

      if (newUserForRegistration == null)
      {
        response.Message = "ERROR_INVALID_USER_MODEL";
        return Task.Run(() => response);
      }

      try
      {
        var userToBeCreated = new User();
        userToBeCreated.InjectWithInitialAttributes(newUserForRegistration.Displayname, 
          newUserForRegistration.Username, newUserForRegistration.Email, newUserForRegistration.Password, newUserForRegistration.Address);

        ThrowExcIfUserCannotBeCreated(userToBeCreated);
        ThrowExcIfThisUserAlreadyExist(userToBeCreated);

        Log.Debug(
          $"Create User: {newUserForRegistration.Username}" +
          "--CreateUser--  @NotComplete@ [CreateUserProcessor]. " +
          "Message: Just Before MakeItPersistence");

        MakeUserPersistent(userToBeCreated);

        Log.Debug(
          $"Create User: {newUserForRegistration.Username}" +
          "--CreateUser--  @NotComplete@ [CreateUserProcessor]. " +
          "Message: Just After MakeItPersistence");

        response = ThrowExcIfUserWasNotBeMadePersistent(userToBeCreated);
        response.Message = "SUCCESS_CREATION";
      }
      catch (InvalidUserException e)
      {
        response.Message = "ERROR_INVALID_USER_MODEL";
        Log.Error(
          $"Create User: {newUserForRegistration.Username}" +
          $"Error Message:{response.Message}" +
          "--CreateUser--  @NotComplete@ [CreateUserProcessor]. " +
          $"Broken rules: {e.BrokenRules}");
      }
      catch (UserEmailOrLoginAlreadyExistsException ex)
      {
        response.Message = "ERROR_USER_ALREADY_EXISTS";
        Log.Error(
          $"Create User: {newUserForRegistration.Username}" +
          $"Error Message:{response.Message}" +
          "--CreateUser--  @fail@ [CreateUserProcessor]. " +
          $"@inner-fault:{ex?.Message} and {ex?.InnerException}");
      }
      catch (UserDoesNotExistAfterMadePersistentException exx)
      {
        response.Message = "ERROR_USER_NOT_MADE_PERSISTENT";
        Log.Error(
          $"Create User: {newUserForRegistration.Username}" +
          $"Error Message:{response.Message}" +
          "--CreateUser--  @fail@ [CreateUserProcessor]." +
          $" @inner-fault:{exx?.Message} and {exx?.InnerException}");
      }
      catch (Exception exxx)
      {
        response.Message = "UNKNOWN_ERROR";
        Log.Error(
          $"Create User: {newUserForRegistration.Username}" +
          $"Error Message:{response.Message}" +
          $"--CreateUser--  @fail@ [CreateUserProcessor]. " +
          $"@inner-fault:{exxx.Message} and {exxx.InnerException}");
      }

      return Task.Run(() => response);
    }

    private void ThrowExcIfThisUserAlreadyExist(User userToBeCreated)
    {
      var userRetrieved = _userRepository.FindUserByUsernameAndEmail(userToBeCreated.UserName, userToBeCreated.Email);
      if (userRetrieved != null)
      {
        throw new UserEmailOrLoginAlreadyExistsException(userToBeCreated.UserName, userToBeCreated.Email,
          userToBeCreated.GetBrokenRulesAsString());
      }
    }

    private UserUiModel ThrowExcIfUserWasNotBeMadePersistent(User userToBeCreated)
    {
      var retrievedUser =
        _userRepository.FindUserByUsernameAndEmail(userToBeCreated.UserName, userToBeCreated.Email);
      if (retrievedUser != null)
        return _autoMapper.Map<UserUiModel>(retrievedUser);
      throw new UserDoesNotExistAfterMadePersistentException(userToBeCreated.UserName, userToBeCreated.Email);
    }

    private void ThrowExcIfUserCannotBeCreated(User userToBeCreated)
    {
      bool canBeCreated = !userToBeCreated.GetBrokenRules().Any();
      if (!canBeCreated)
        throw new InvalidUserException(userToBeCreated.GetBrokenRulesAsString());
    }

    private void MakeUserPersistent(User userToBeMadePersistence)
    {
      _userRepository.Save(userToBeMadePersistence);
      _uOf.Commit();
    }
  }
}
