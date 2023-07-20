using SipayApi.DataLayer.Entities;
using System.Linq.Expressions;


namespace SipayApi.Data.Repository;

public interface ITransactionRepository : IGenericRepository<Transaction>
{
    List<Transaction> GetByReference(string reference);

    List<Transaction> GetByParameter(Expression<Func<Transaction, bool>> expression); //ÖDEV
    //object GetByParameter(Expression<Func<DataLayer.Entities.Transaction, bool>> expression);
}
