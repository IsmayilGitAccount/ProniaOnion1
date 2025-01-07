using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProniaOnion.Domain.Entities.Base;

namespace ProniaOnion.Domain.Entities
{
    public class Product:BaseNameableEntity
    {
        public decimal Price { get; set; }

        public string SKU { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }


        //relational properties
        public Category Category { get; set; }

        public ICollection<ProductColor> ProductColors { get; set; }
        public ICollection<ProductTag> ProductTags { get; set; }
        public ICollection<ProductSize> ProductSize { get; set; }
    }
}
