using System;
using System.Collections.Generic;
using ornico.common.infrastructure.Domain;
using ornico.core.model.Users;

namespace ornico.core.model.Orders
{
  public class Order : EntityBase<Guid>, IAggregateRoot
  {
    public Order()
    {
      OnCreated();
    }

    private void OnCreated()
    {
      this.Totals = 0;
      this.CreatedDate = DateTime.UtcNow;
      this.Items = new HashSet<OrderItem>();
    }

    public virtual string Comments { get; set; }
    public virtual DateTime CreatedDate { get; set; }
    public virtual double Totals { get; set; }
    public virtual User User { get; set; }
    public virtual ISet<OrderItem> Items { get; set; }


    protected override void Validate()
    {
    }

    public virtual void InjectWithInitialAttributes(string email)
    {
    }
  }
}

