using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProniaOnion.Domain.Entities;
using ProniaOnion.Domain.Entities.Base;

namespace ProniaOnion.Application.Abstractions.Repositories
{
    public interface IProductRepository:IRepository<Product>
    {
        Task<IEnumerable<T>> GetManyToManyEntities<T>(ICollection<int> ids) where T : BaseEntity;
    }
}
