using System.Linq.Expressions;
using System.Transactions;

namespace SipayApi.Data.Repository;

public interface ITransactionRepository : IGenericRepository<Transaction>
{
    List<Transaction> GetByReference(string reference);

    List<Transaction> GetByParameter(Expression<Func<Transaction, bool>> expression);
}
