using System;
using System.Collections.Generic;
using ornico.common.infrastructure.Domain;
using ornico.core.model.Orders;

namespace ornico.core.model.Products
{
    public class Product : EntityBase<Guid>, IAggregateRoot
    {
        public Product()
        {
            OnCreated();
        }

        private void OnCreated()
        {
            this.CreatedDate = DateTime.Now;
            this.Price = 0;
            this.Items = new HashSet<OrderItem>();
        }

        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual double Price { get; set; }
        public virtual ISet<OrderItem> Items { get; set; }
        
        protected override void Validate()
        {

        }

        public virtual void InjectWithInitialAttributes(string name, string description, double price)
        {
          this.Name = name;
          this.Description = description;
          this.Price = price;
        }
    }
}

