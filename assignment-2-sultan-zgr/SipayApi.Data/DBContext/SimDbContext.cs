using Microsoft.EntityFrameworkCore;
using SipayApi.DataLayer.Config;
using SipayApi.DataLayer.Entities;
using SipayApi.DataLayer.ValidationRules;

namespace SipayApi.Data.DBContext
{
    public class SimDbContext : DbContext  //DbContext, uygulamanızdaki model sınıfları ve veritabanı tabloları arasında bağlantıyı sağlayarak veritabanı işlemlerini kolaylaştırır.
    {
        public SimDbContext(DbContextOptions<SimDbContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)  //Model oluştururken (model-building) kullanılan OnModelCreating override ediyoruz. 
        {
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
