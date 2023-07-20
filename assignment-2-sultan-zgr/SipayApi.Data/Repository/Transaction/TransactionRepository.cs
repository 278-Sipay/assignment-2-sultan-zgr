using SipayApi.Data.DBContext;
using SipayApi.DataLayer.Entities;
using System.Linq.Expressions;

namespace SipayApi.Data.Repository;

public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
{
    private readonly SimDbContext dbContext;
    public TransactionRepository(SimDbContext dbContext) : base(dbContext)
    {
        this.dbContext = dbContext;
    }
    public List<Transaction> GetByParameter(Expression<Func<Transaction, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public List<Transaction> GetByReference(string reference)
    {
        throw new NotImplementedException();
    }
}
