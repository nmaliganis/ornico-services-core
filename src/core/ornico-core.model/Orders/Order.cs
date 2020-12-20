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
      this.Items = new List<OrderItem>();
    }

    public virtual string Comments { get; set; }
    public virtual DateTime CreatedDate { get; set; }
    public virtual double Totals { get; set; }
    public virtual User User { get; set; }
    public virtual IList<OrderItem> Items { get; set; }


    protected override void Validate()
    {
    }

    public virtual void InjectWithInitialAttributes(string comments)
    {
      this.Comments = Comments;
    }

    public virtual void InjectedWithOrderItem(OrderItem newOrderItemToBeInjected)
    {
      this.Items.Add(newOrderItemToBeInjected);
      newOrderItemToBeInjected.Order = this;
    }

    public virtual void CalcNewTotals(int productProductQty, double price)
    {
      this.Totals += productProductQty * price;
    }

    public virtual void InjectWithUser(User userToBeInjected)
    {
      this.User = userToBeInjected;
      userToBeInjected.Orders.Add(this);
    }
  }
}

