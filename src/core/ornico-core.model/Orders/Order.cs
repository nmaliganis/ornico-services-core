using System;
using ornico.common.infrastructure.Domain;

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
            this.CreatedDate = DateTime.UtcNow;
            this.ModifiedDate = DateTime.UtcNow;
            this.IsActive = true;

        }

        public virtual string Firstname { get; set; }
        public virtual string Lastname { get; set; }
        public virtual string Email { get; set; }
        public virtual Guid UserId { get; set; }
        public virtual string Phone { get; set; }
        public virtual string ExtPhone { get; set; }
        public virtual string Mobile { get; set; }
        public virtual string ExtMobile { get; set; }
        public virtual string Notes { get; set; }


        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime ModifiedDate { get; set; }
        public virtual Guid CreatedBy { get; set; }
        public virtual Guid ModifiedBy { get; set; }
        
        public virtual bool IsActive { get; set; }
        
        protected override void Validate()
        {

        }

        public virtual void InjectWithInitialAttributes(string email)
        {
            this.Email = email;
            this.CreatedDate = DateTime.UtcNow;
        }
    }
}

