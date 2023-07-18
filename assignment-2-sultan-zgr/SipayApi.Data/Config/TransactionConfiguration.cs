using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SipayApi.DataLayer.Entities;

namespace SipayApi.DataLayer.Config
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {

            builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
            builder.Property(x => x.InsertUser).IsRequired(true).HasMaxLength(50);
            //builder.Property(x => x.InsertDate).IsRequired(false);

            builder.Property(x => x.TransactionDate).IsRequired(true);
            builder.Property(x => x.ReferenceNumber).IsRequired(true);
            builder.Property(x => x.AccountNumber).IsRequired(true);
            builder.Property(x => x.CreditAmount).IsRequired(true).HasPrecision(15, 4).HasDefaultValue(0);
            builder.Property(x => x.DebitAmount).IsRequired(true).HasPrecision(15, 4).HasMaxLength(0);

            builder.Property(x => x.Description).IsRequired(true).HasMaxLength(250);
            builder.Property(x => x.ReferenceNumber).IsRequired(true).HasMaxLength(50);

            builder.HasIndex(x => x.AccountNumber);
            builder.HasIndex(x => x.ReferenceNumber);
        }

    }
}
