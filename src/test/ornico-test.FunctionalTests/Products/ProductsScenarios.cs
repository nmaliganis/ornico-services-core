using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ornico.test.functional.Base;
using Xunit;

namespace ornico.test.functional.Products
{
  public class ProductsScenarios : ProductsScenariosBase
  {
    private readonly string _jwtForUser;

    public ProductsScenarios()
    {
      _jwtForUser =  JwtTokenHelper.GenerateEmailVerificationLink("test");
    }

    [Fact]
    public async Task get_list_of_existing_customers_response_ok_status_code()
    {
      //using var server = CreateServer();
      //var customersResponse = await server.CreateIdempotentClient(_jwtForUser)
      //  .GetAsync(Get.GetCustomers());

      //Assert.True(customersResponse.StatusCode == HttpStatusCode.OK);
      //customersResponse.EnsureSuccessStatusCode();
    }

    [Theory]
    [InlineData("3E_Test1", "6974898552")]
    [InlineData("3E_Test2", "6974898553")]
    public async Task post_2_new_created_customers_and_delete_and_response_ok_status_code(
      string companyName, string phone)
    {
      //using var server = CreateServer();
      //CustomerForCreationDto customer = GetMockCustomerForCreationDto(
      //  companyName, phone,
      //  Guid.NewGuid().ToString(),
      //  Guid.NewGuid().ToString(),
      //  Guid.NewGuid().ToString(),
      //  Guid.NewGuid().ToString(),
      //  Guid.NewGuid().ToString(),
      //  Guid.NewGuid().ToString()
      //);
      //var newCustomer = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
      //var customerResponse = await server.CreateIdempotentClient(_jwtForUser)
      //  .PostAsync(Post.Customer, newCustomer);

      //customerResponse.EnsureSuccessStatusCode();
      //Assert.True(customerResponse.StatusCode == HttpStatusCode.Created);
      //var customerContentResponse = await customerResponse.Content.ReadAsStringAsync();
      //var customerCreated =
      //  JsonConvert.DeserializeObject<CustomerDto>(customerContentResponse);
      //var customerDeletedResponse = await server.CreateIdempotentClient(_jwtForUser)
      //  .DeleteAsync(Delete.Customer(customerCreated.Id));

      //Assert.True(customerDeletedResponse.StatusCode == HttpStatusCode.OK);
    }

    [Theory]
    [InlineData("3E_Test1", "6974898552", "6974898553")]
    public async Task post_1_new_customer_put_phone_and_delete_and_response_ok_status_code(
      string companyName, string phone1, string phone2)
    {
      //using var server = CreateServer();
      //CustomerForCreationDto customer = GetMockCustomerForCreationDto(
      //  companyName, phone1,
      //  Guid.NewGuid().ToString(),
      //  Guid.NewGuid().ToString(),
      //  Guid.NewGuid().ToString(),
      //  Guid.NewGuid().ToString(),
      //  Guid.NewGuid().ToString(),
      //  Guid.NewGuid().ToString()
      //);
      //var newCustomer = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
      //var customerResponse = await server.CreateIdempotentClient(_jwtForUser)
      //  .PostAsync(Post.Customer, newCustomer);

      //customerResponse.EnsureSuccessStatusCode();
      //Assert.True(customerResponse.StatusCode == HttpStatusCode.Created);
      //var customerContentResponse = await customerResponse.Content.ReadAsStringAsync();
      //var customerCreated =
      //  JsonConvert.DeserializeObject<CustomerDto>(customerContentResponse);

      //CustomerForModificationDto customerToBeModified = GetMockCustomerForModificationDto(customerCreated.Id,
      //  companyName, phone2,
      //  Guid.NewGuid().ToString(),
      //  Guid.NewGuid().ToString(),
      //  Guid.NewGuid().ToString(),
      //  Guid.NewGuid().ToString(),
      //  Guid.NewGuid().ToString(),
      //  Guid.NewGuid().ToString()
      //);

      //var modifiedCustomer = new StringContent(JsonConvert.SerializeObject(customerToBeModified), Encoding.UTF8, "application/json");
      //var customerModifiedResponse = await server.CreateIdempotentClient(_jwtForUser)
      //  .PutAsync(Put.Customer(customerCreated.Id), modifiedCustomer);

      //customerModifiedResponse.EnsureSuccessStatusCode();
      //Assert.True(customerResponse.StatusCode == HttpStatusCode.OK);
      //var customerModifiedContentResponse = await customerModifiedResponse.Content.ReadAsStringAsync();
      //var customerModified =
      //  JsonConvert.DeserializeObject<CustomerDto>(customerModifiedContentResponse);

      //var customerDeletedResponse = await server.CreateIdempotentClient(_jwtForUser)
      //  .DeleteAsync(Delete.Customer(customerModified.Id));

      //Assert.True(customerDeletedResponse.StatusCode == HttpStatusCode.OK);
    }

    //private static CustomerForCreationDto GetMockCustomerForCreationDto
    //(string companyName, string contactName, string phone, string address,
    //  string city, string region, string postal, string country)
    //{
    //  CustomerForCreationDto moqCustomer = new CustomerForCreationDto
    //  {
    //    CompanyName = companyName,
    //    Phone = phone,
    //    ContactName = contactName,
    //    Address = address,
    //    City = city,
    //    Region = region,
    //    PostalCode = postal,
    //    Country = country
    //  };
    //  return moqCustomer;
    //}

    //private static CustomerForModificationDto GetMockCustomerForModificationDto
    //(Guid id, string companyName, string contactName, string phone, string address,
    //  string city, string region, string postal, string country)
    //{
    //  CustomerForModificationDto moqCustomer = new CustomerForModificationDto
    //  {
    //    Id = id,
    //    CompanyName = companyName,
    //    Phone = phone,
    //    ContactName = contactName,
    //    Address = address,
    //    City = city,
    //    Region = region,
    //    PostalCode = postal,
    //    Country = country
    //  };
    //  return moqCustomer;
    //}

  }
}
