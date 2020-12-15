using System;
using System.Linq;
using System.Threading.Tasks;
using ornico.common.dtos.DTOs.Products;
using ornico.common.infrastructure.Exceptions.Domain.Persons;
using ornico.common.infrastructure.TypeMappings;
using ornico.common.infrastructure.UnitOfWorks;
using ornico.core.contracts.Products;
using ornico.core.model.Products;
using ornico.core.repository.ContractRepositories;
using Serilog;

namespace ornico.core.services.Products
{
    public class CreateProductProcessor : ICreateProductProcessor
    {
        private readonly IUnitOfWork _uOf;
        private readonly IProductRepository _productRepository;
        private readonly IAutoMapper _autoMapper;

        public CreateProductProcessor(IUnitOfWork uOf, IAutoMapper autoMapper, IProductRepository productRepository)
        {
            _uOf = uOf;
            _productRepository = productRepository;
            _autoMapper = autoMapper;
        }

        public Task<ProductUiModel> CreateProductAsync(Guid accountIdToCreateThisProduct, ProductForCreationUiModel newProductUiModel)
        {
            var response =
                new ProductUiModel()
                {
                    Message = "START_CREATION"
                };

            if (newProductUiModel == null)
            {
                response.Message = "ERROR_INVALID_PERSON_MODEL";
                return Task.Run(() => response);
            }

            var personToBeCreated = new Product();

            try
            {
                personToBeCreated.InjectWithInitialAttributes(newProductUiModel.PersonLastName);

                ThrowExcIfPersonCannotBeCreated(personToBeCreated);
                ThrowExcIfThisProductAlreadyExist(personToBeCreated);

                Log.Debug(
                    $"Create Person: {newProductUiModel.PersonLastName}" +
                    "--CreatePerson--  @NotComplete@ [CreateProductProcessor]. " +
                    "Message: Just Before MakeItPersistense");

                MakePersonPersistent(personToBeCreated);

                Log.Debug(
                    $"Create Person: {newProductUiModel.PersonLastName}" +
                    "--CreatePerson--  @NotComplete@ [CreateProductProcessor]. " +
                    "Message: Just After MakeItPersistense");
                response = ThrowExcIfProductWasNotBeMadePersistent(personToBeCreated);
                response.Message = "SUCCESS_CREATION";
            }
            catch (InvalidPersonException e)
            {
                response.Message = "ERROR_INVALID_PERSON_MODEL";
                Log.Error(
                    $"Create Person: {newProductUiModel.PersonLastName}" +
                    "--CreatePerson--  @NotComplete@ [CreateProductProcessor]. " +
                    $"Broken rules: {e.BrokenRules}");
            }
            catch (PersonAlreadyExistsException ex)
            {
                response.Message = "ERRR_PERSON_ALREADY_EXISTS";
                Log.Error(
                    $"Create Person: {newProductUiModel.PersonLastName}" +
                    "--CreatePerson--  @fail@ [CreateProductProcessor]. " +
                    $"@innerfault:{ex?.Message} and {ex?.InnerException}");
            }
            catch (PersonDoesNotExistAfterMadePersistentException exx)
            {
                response.Message = "ERROR_PERSON_NOT_MADE_PERSISTENT";
                Log.Error(
                    $"Create Person: {newProductUiModel.PersonLastName}" +
                    "--CreatePerson--  @fail@ [CreateProductProcessor]." +
                    $" @innerfault:{exx?.Message} and {exx?.InnerException}");
            }
            catch (Exception exxx)
            {
                response.Message = "UNKNOWN_ERROR";
                Log.Error(
                    $"Create Person: {newProductUiModel.PersonLastName}" +
                    $"--CreatePerson--  @fail@ [CreateProductProcessor]. " +
                    $"@innerfault:{exxx.Message} and {exxx.InnerException}");
            }

            return Task.Run(() => response);
        }

        private void ThrowExcIfThisProductAlreadyExist(Product ProductToBeCreated)
        {
            var customerRetrieved = _productRepository.FindOneProductByMobile(ProductToBeCreated.Mobile);
            if (customerRetrieved != null)
            {
                throw new ProductAlreadyExistsException(ProductToBeCreated.Mobile,
                    ProductToBeCreated.GetBrokenRulesAsString());
            }
        }

        private ProductUiModel ThrowExcIfProductWasNotBeMadePersistent(Product personToBeCreated)
        {
            var retrievedPerson = _productRepository.FindOneProductByMobile(personToBeCreated.Mobile);
            if (retrievedPerson  != null)
                return _autoMapper.Map<ProductUiModel>(retrievedPerson);
            throw new ProductDoesNotExistAfterMadePersistentException(personToBeCreated.Mobile);
        }

        private void ThrowExcIfPersonCannotBeCreated(Product personToBeCreated)
        {
            bool canBeCreated = !personToBeCreated.GetBrokenRules().Any();
            if (!canBeCreated)
                throw new InvalidPersonException(personToBeCreated.GetBrokenRulesAsString());
        }

        private void MakePersonPersistent(Product personToBeMadePersistence)
        {
            _productRepository.Save(personToBeMadePersistence);
            _uOf.Commit();
        }
    }
}
