using System;
using System.Collections.Generic;
using ornico.common.infrastructure.Domain;
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

    public virtual ISet<Order> Orders { get; set; }

    protected override void Validate()
    {
    }
  }
}


