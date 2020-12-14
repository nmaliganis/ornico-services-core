using System;
using System.Collections.Generic;
using ornico.common.infrastructure.Domain;
using ornico.core.model.Registrations;

namespace ornico.core.model.Product
{
    public class Product : EntityBase<Guid>, IAggregateRoot
    {
        public Product()
        {
            OnCreated();
        }

        private void OnCreated()
        {
            this.CreatedDate = DateTime.UtcNow;
            this.ModifiedDate = DateTime.UtcNow;
            this.IsActive = true;
            this.Registrations = new HashSet<Registration>();
        }

        public virtual string Firstname { get; set; }
        public virtual string Lastname { get; set; }
        public virtual int Age { get; set; }
        public virtual string Phone { get; set; }
        public virtual string ExtPhone { get; set; }
        public virtual string Mobile { get; set; }
        public virtual string ExtMobile { get; set; }
        public virtual string Notes { get; set; }
        public virtual string CarePersonName { get; set; }
        public virtual string CarePersonPhone { get; set; }
        public virtual string CarePersonExtPhone { get; set; }

        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime ModifiedDate { get; set; }
        public virtual Guid CreatedBy { get; set; }
        public virtual Guid ModifiedBy { get; set; }
        
        public virtual bool IsActive { get; set; }
        public virtual ISet<Registration> Registrations { get; set; }
        
        protected override void Validate()
        {

        }

        public virtual void InjectWithInitialAttributes(string personEmail)
        {
          
        }
    }
}

