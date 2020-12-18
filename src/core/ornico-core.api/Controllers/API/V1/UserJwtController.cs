using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ornico.common.dtos.DTOs.Accounts;
using ornico.common.dtos.DTOs.Users;
using ornico.common.infrastructure.Helpers.Security;
using ornico.core.api.Validators;
using ornico.core.contracts.Users;
using ornico.core.contracts.V1;
using Serilog;

namespace ornico.core.api.Controllers.API.V1
{
  [ApiVersionNeutral]
  [ResponseCache(Duration = 0, NoStore = true, VaryByHeader = "*")]
  [Route("api/[controller]")]
  [ApiController]
  public class UsersController : ControllerBase
  {
    private readonly IConfiguration _configuration;
    private readonly IInquiryUserProcessor _inquiryUserProcessor;
    private readonly ICreateUserProcessor _createUserProcessor;

    public UsersController(IConfiguration configuration,
      IUsersControllerDependencyBlock userBlock)
    {
      _configuration = configuration;
      _inquiryUserProcessor = userBlock.InquiryUserProcessor;
      _createUserProcessor = userBlock.CreateUserProcessor;
    }

    [Route("signin", Name = "SetUserJwtRoute")]
    [HttpPost]
    [ValidateModel]
    [AllowAnonymous]
    public async Task<IActionResult> SetUserJwtAsync([FromBody] LoginUiModel loginVm)
    {
      var registeredUser = await _inquiryUserProcessor.GetUserAuthJwtTokenByLoginAndPasswordAsync(loginVm.Login,
        HashHelper.Sha512(loginVm.Password + loginVm.Login));

      if (registeredUser == null)
        return BadRequest("WRONG_USER_PASS");

      var tokenValue = GenerateJwtToken(registeredUser);

      return Ok(new AuthUiModel {Token = tokenValue, Message = "CORRECT_USER_PASS"});
    }

    /// <summary>
    /// POST : signup  a new user.
    /// </summary>
    /// <param name="managedUserVm">managedUserVM the managed user View Model</param>
    /// <remarks> return a ResponseEntity with status 201 (Created) if the new user is Signup or 400 (Bad Request) if the login or email is already in use or Validation Request Model Error </remarks>
    /// <response code="201">Created (if the user is registered)</response>
    /// <response code="400">email in use</response>
    /// <response code="400">username in use</response>
    [Route("signup", Name = "PostAccountSignupRoot")]
    [HttpPost]
    [ValidateModel]
    [AllowAnonymous]
    public async Task<IActionResult> PostAccountSignupAsync([FromBody] UserForRegistrationUiModel managedUserVm)
    {
      try
      {
        if (await _inquiryUserProcessor.SearchIfAnyPersonByEmailOrUsernameExistsAsync(managedUserVm.Email, managedUserVm.Username))
        {
          Log.Error(
            $"--Method:PostAccountSignupAsync -- Message:USER_SIGNUP_USERNAME_OR_EMAIL_ALREADY_EXIST-- Datetime:{DateTime.Now} " +
            $"-- UserInfo: Email : {managedUserVm.Email}, Username : {managedUserVm.Username}");
          return BadRequest(new {errorMessage = "USERNAME_OR_EMAIL_ALREADY_EXISTS"});
        }

        var registerResponse = await _createUserProcessor.CreateUserAsync(managedUserVm);

        switch (registerResponse.Message)
        {
          case ("SUCCESS_CREATION"):
          {
            Log.Information(
              $"--Method:PostAccountRegisterAsync -- Message:USER_REGISTERED_SUCCESSFULLY -- Datetime:{DateTime.UtcNow} -- UserInfo:{managedUserVm.Username}");
            return Created("/signup", new
            {
              id = registerResponse.Id,
              username = registerResponse.Username,
              email = registerResponse.Email,
              address = registerResponse.Address,
              displayname = registerResponse.DisplayName,
              status = "CORRECT_USER_SIGNUP"
            });
          }
          case ("ERROR_USER_ALREADY_EXISTS"):
          {
            Log.Error(
              $"--Method:PostAccountRegisterAsync -- Message:ERROR_USER_ALREADY_EXISTS -- Datetime:{DateTime.UtcNow} -- UserInfo:{managedUserVm.Username}");
            return BadRequest(new {errorMessage = "USERNAME_OR_EMAIL_ALREADY_EXISTS"});
          }
          case ("ERROR_USER_NOT_MADE_PERSISTENT"):
          {
            Log.Error(
              $"--Method:PostAccountRegisterAsync -- Message:ERROR_USER_NOT_MADE_PERSISTENT -- Datetime:{DateTime.UtcNow} -- UserInfo:{managedUserVm.Username}");
            return BadRequest(new {errorMessage = "ERROR_REGISTER_NEW_USER"});
          }
          case ("UNKNOWN_ERROR"):
          {
            Log.Error(
              $"--Method:PostAccountRegisterAsync -- Message:ERROR_REGISTER_NEW_USER -- Datetime:{DateTime.UtcNow} -- UserInfo:{managedUserVm.Username}");
            return BadRequest(new {errorMessage = "ERROR_REGISTER_NEW_USER"});
          }
        }
      }
      catch (Exception e)
      {
        return BadRequest(new {errorMessage = e.Message});
      }

      return Ok();
    }

    private string GenerateJwtToken(UserForRetrievalUiModel registeredUser)
    {
      List<Claim> claims = new List<Claim>
      {
        new Claim(ClaimTypes.Name, registeredUser.Login),
        new Claim(ClaimTypes.Email, registeredUser.Email),
        new Claim(ClaimTypes.UserData, registeredUser.Id.ToString()),
      };

      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(this._configuration.GetSection("TokenAuthentication:SecretKey").Value);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.Now.AddMinutes(int.Parse(this._configuration
          .GetSection("TokenAuthentication:ExpirationTimeInMinutes").Value)),
        Issuer = this._configuration.GetSection("TokenAuthentication:Issuer").Value,
        Audience = this._configuration.GetSection("TokenAuthentication:Audience").Value,
        SigningCredentials =
          new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      string tokenValue = tokenHandler.WriteToken(token);
      return tokenValue;
    }
  }
}
