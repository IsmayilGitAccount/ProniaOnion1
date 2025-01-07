using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Domain.Entities;
using ProniaOnion.Domain.Entities.Base;

namespace ProniaOnion.Persistence.Common
{
    internal static class GlobalQueryFilter
    {
        public static void ApplyFilter<T>(this ModelBuilder modelBuilder) where T : BaseEntity, new()
        {
            modelBuilder.Entity<T>().HasQueryFilter(c=>c.IsDeleted == false);
        }

        public static void ApplyQueryFilter(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyFilter<Color>();
            modelBuilder.ApplyFilter<Category>();
            modelBuilder.ApplyFilter<Product>();
            modelBuilder.ApplyFilter<Tag>();
            modelBuilder.ApplyFilter<Size>();
            modelBuilder.ApplyFilter<Blog>();
            modelBuilder.ApplyFilter<Genre>();
            modelBuilder.ApplyFilter<Author>();
        }
    }
}
