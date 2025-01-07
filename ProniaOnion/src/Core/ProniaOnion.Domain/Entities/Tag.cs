using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProniaOnion.Domain.Entities.Base;

namespace ProniaOnion.Domain.Entities
{
    public class Tag : BaseNameableEntity
    {
        //relational property

        public ICollection<ProductTag> ProductTags { get; set; }
        public ICollection<BlogTag> BlogTags { get; set; }

    }
}
