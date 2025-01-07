using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProniaOnion.Domain.Entities.Base;

namespace ProniaOnion.Domain.Entities
{
    public class Size: BaseNameableEntity
    {
        //relational property

        public ICollection<ProductSize> ProductSize { get; set; }
    }
}
