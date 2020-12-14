using System;
using ornico.common.infrastructure.Domain;

namespace ornico.core.model.Registrations
{
  public class Registration : EntityBase<Guid>, IAggregateRoot
  {
    public Registration()
    {
      OnCreated();
    }

    private void OnCreated()
    {
      this.IsActive = true;
      this.IsCurrent = true;
    }

    public virtual DateTime RegisteredDate { get; set; }
    public virtual bool IsCurrent { get; set; }

    public virtual DateTime CreatedDate { get; set; }
    public virtual DateTime ModifiedDate { get; set; }
    public virtual Guid CreatedBy { get; set; }
    public virtual Guid ModifiedBy { get; set; }
        
    public virtual bool IsActive { get; set; }

    public virtual Product.Product Product { get; set; }

    protected override void Validate()
    {
    }
  }
}
