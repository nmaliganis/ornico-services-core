using System;
using ornico.common.infrastructure.Domain;
using ornico.core.model.Products;

namespace ornico.core.model.Orders
{
  public class OrderItem : EntityBase<Guid>
  {
    public OrderItem()
    {
      OnCreated();
    }

    private void OnCreated()
    {
      this.Quantity = 0;
      this.InsertedDate = DateTime.UtcNow;
    }

    public virtual DateTime InsertedDate { get; set; }
    public virtual int Quantity { get; set; }
    public virtual Order Order { get; set; }
    public virtual Product Product { get; set; }


    protected override void Validate()
    {

    }

    public virtual void InjectWithInitialAttributes(string email)
    {
    }
  }
}

