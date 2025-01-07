using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProniaOnion.Domain.Entities.Base;

namespace ProniaOnion.Domain.Entities
{
    public class Category:BaseNameableEntity
    {
        //relational properties

        public ICollection<Product> Products { get; set; }
    }
}
