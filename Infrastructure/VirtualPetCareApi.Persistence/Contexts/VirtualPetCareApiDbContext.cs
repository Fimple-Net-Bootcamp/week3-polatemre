using VirtualPetCareApi.Domain.Entities;
using VirtualPetCareApi.Domain.Entities.Common;
using VirtualPetCareApi.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualPetCareApi.Persistence.Contexts
{
    public class VirtualPetCareApiDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public VirtualPetCareApiDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Health> Healths { get; set; }
        public DbSet<Food> Foods { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<Donation>()
            //    .HasKey(b => b.Id);

            //builder.Entity<Donation>()
            //    .HasIndex(o => o.OrderCode)
            //    .IsUnique();

            //builder.Entity<Basket>()
            //    .HasOne(b => b.Donation)
            //    .WithOne(o => o.Basket)
            //    .HasForeignKey<Donation>(b => b.Id);

            base.OnModelCreating(builder);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //ChangeTracker: Entityler üzerinden yapılan değişikliklerin ya da yeni eklenen verinin yakalanmasını sağlayan propertydir.Update operasyonlarında track edilen verileri yakalayıp elde etmemizi sağlar.

            var datas = ChangeTracker
                .Entries<BaseEntity>();

            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow
                };
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
