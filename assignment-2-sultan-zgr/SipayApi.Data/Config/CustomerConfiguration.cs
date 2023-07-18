using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SipayApi.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipayApi.DataLayer.ValidationRules
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {  
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(x => x.CustomerNumber).IsRequired(true).ValueGeneratedNever();
            builder.HasIndex(x => x.CustomerNumber).IsUnique(true);
            builder.HasKey(x => x.CustomerNumber);

            builder.Property(x => x.InsertUser).IsRequired(true).HasMaxLength(50);

            builder.Property(x => x.FirstName).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.LastName).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);
            builder.Property(x => x.Address).IsRequired(true).HasMaxLength(350);

            builder.HasIndex(x => x.CustomerNumber).IsUnique(true);

            builder.HasMany(x => x.Accounts)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerNumber)
                .IsRequired(true);
        }
    }
}
