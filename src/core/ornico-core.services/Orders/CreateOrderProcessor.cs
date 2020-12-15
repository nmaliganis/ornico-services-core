using System;
using System.Linq;
using System.Threading.Tasks;
using ornico.common.infrastructure.Exceptions.Domain.Persons;
using ornico.common.infrastructure.TypeMappings;
using ornico.common.infrastructure.UnitOfWorks;
using ornico.core.contracts.Orders;
using ornico.core.model.Orders;
using ornico.core.repository.ContractRepositories;
using Serilog;

namespace ornico.core.services.Orders
{
    public class CreateOrderProcessor : ICreateOrderProcessor
    {
        private readonly IUnitOfWork _uOf;
        private readonly IOrderRepository _orderRepository;
        private readonly IAutoMapper _autoMapper;

        public CreateOrderProcessor(IUnitOfWork uOf, IAutoMapper autoMapper, IOrderRepository orderRepository)
        {
            _uOf = uOf;
            _orderRepository = orderRepository;
            _autoMapper = autoMapper;
        }

        public Task<OrderUiModel> CreateOrderAsync(OrderForCreationUiModel newOrderUiModel)
        {
            var response =
                new OrderUiModel()
                {
                    Message = "START_CREATION"
                };

            if (newOrderUiModel == null)
            {
                response.Message = "ERROR_INVALID_PERSON_MODEL";
                return Task.Run(() => response);
            }

            var personToBeCreated = new Order();

            try
            {
                personToBeCreated.InjectWithInitialAttributes(newOrderUiModel.PersonEmail);

                ThrowExcIfPersonCannotBeCreated(personToBeCreated);
                ThrowExcIfThisPersonAlreadyExist(personToBeCreated);

                Log.Debug(
                    $"Create Person: {newOrderUiModel.PersonEmail}" +
                    "--CreatePerson--  @NotComplete@ [CreateOrderProcessor]. " +
                    "Message: Just Before MakeItPersistense");

                MakePersonPersistent(personToBeCreated);

                Log.Debug(
                    $"Create Person: {newOrderUiModel.PersonEmail}" +
                    "--CreatePerson--  @NotComplete@ [CreateOrderProcessor]. " +
                    "Message: Just After MakeItPersistense");
                response = ThrowExcIfPersonWasNotBeMadePersistent(personToBeCreated);
                response.Message = "SUCCESS_CREATION";
            }
            catch (InvalidPersonException e)
            {
                response.Message = "ERROR_INVALID_PERSON_MODEL";
                Log.Error(
                    $"Create Person: {newOrderUiModel.PersonEmail}" +
                    "--CreatePerson--  @NotComplete@ [CreateOrderProcessor]. " +
                    $"Broken rules: {e.BrokenRules}");
            }
            catch (PersonAlreadyExistsException ex)
            {
                response.Message = "ERRR_PERSON_ALREADY_EXISTS";
                Log.Error(
                    $"Create Person: {newOrderUiModel.PersonEmail}" +
                    "--CreatePerson--  @fail@ [CreateOrderProcessor]. " +
                    $"@innerfault:{ex?.Message} and {ex?.InnerException}");
            }
            catch (PersonDoesNotExistAfterMadePersistentException exx)
            {
                response.Message = "ERROR_PERSON_NOT_MADE_PERSISTENT";
                Log.Error(
                    $"Create Person: {newOrderUiModel.PersonEmail}" +
                    "--CreatePerson--  @fail@ [CreateOrderProcessor]." +
                    $" @innerfault:{exx?.Message} and {exx?.InnerException}");
            }
            catch (Exception exxx)
            {
                response.Message = "UNKNOWN_ERROR";
                Log.Error(
                    $"Create Person: {newOrderUiModel.PersonEmail}" +
                    $"--CreatePerson--  @fail@ [CreateOrderProcessor]. " +
                    $"@innerfault:{exxx.Message} and {exxx.InnerException}");
            }

            return Task.Run(() => response);
        }

        private void ThrowExcIfThisPersonAlreadyExist(Order personToBeCreated)
        {
            var customerRetrieved = _orderRepository.FindOrderByEmail(personToBeCreated.Email);
            if (customerRetrieved != null)
            {
                throw new PersonAlreadyExistsException(personToBeCreated.Email,
                    personToBeCreated.GetBrokenRulesAsString());
            }
        }

        private OrderUiModel ThrowExcIfPersonWasNotBeMadePersistent(Order personToBeCreated)
        {
            var retreievedPerson = _orderRepository.FindOrderByEmail(personToBeCreated.Email);
            if (retreievedPerson  != null)
                return _autoMapper.Map<OrderUiModel>(retreievedPerson);
            throw new PersonDoesNotExistAfterMadePersistentException(personToBeCreated.Email);
        }

        private void ThrowExcIfPersonCannotBeCreated(Order personToBeCreated)
        {
            bool canBeCreated = !personToBeCreated.GetBrokenRules().Any();
            if (!canBeCreated)
                throw new InvalidPersonException(personToBeCreated.GetBrokenRulesAsString());
        }

        private void MakePersonPersistent(Order personToBeMadePersistence)
        {
            _orderRepository.Save(personToBeMadePersistence);
            _uOf.Commit();
        }
    }
}
