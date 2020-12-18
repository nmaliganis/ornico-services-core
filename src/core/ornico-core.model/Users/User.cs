using System;
using System.Collections.Generic;
using ornico.common.infrastructure.Domain;
using ornico.common.infrastructure.Helpers.Security;
using ornico.core.model.Orders;

namespace ornico.core.model.Users
{
  public class User : EntityBase<Guid>, IAggregateRoot
  {
    public User()
    {
      OnCreate();
    }

    private void OnCreate()
    {
      this.CreatedDate = DateTime.Now;
      this.CurrentDate = DateTime.UtcNow;
      this.Orders = new HashSet<Order>();
    }

    public virtual string DisplayName { get; set; }
    public virtual string UserName { get; set; }
    public virtual string Password { get; set; }
    public virtual string Email { get; set; }
    public virtual DateTime CreatedDate { get; set; }
    public virtual DateTime CurrentDate { get; set; }
    public virtual string Address { get; set; }

    public virtual ISet<Order> Orders { get; set; }

    protected override void Validate()
    {
      if (DisplayName == string.Empty)
      {
        AddBrokenRule(UserBusinessRules.Displayname);
      }
      if (UserName == string.Empty)
      {
        AddBrokenRule(UserBusinessRules.Username);
      }
      if (Password == string.Empty)
      {
        AddBrokenRule(UserBusinessRules.Password);
      }
      if (Email == string.Empty)
      {
        AddBrokenRule(UserBusinessRules.Email);
      }
    }

    public virtual void InjectWithInitialAttributes(string displayName, string userName, string email, string password, string address)
    {
      this.DisplayName = displayName;
      this.UserName = userName;
      this.Email = email;
      this.Password = HashHelper.Sha512(password + userName);
      this.Address = address;
    }
  }
}


