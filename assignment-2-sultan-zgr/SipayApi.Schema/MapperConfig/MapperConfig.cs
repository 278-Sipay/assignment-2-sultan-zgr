using AutoMapper;
using SipayApi.DataLayer.Entities;

namespace SipayApi.Schema;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<CustomerRequest, Customer>();
        CreateMap<Customer, CustomerResponse>();

        CreateMap<AccountRequest, Account>();
        CreateMap<Account, AccountResponse>();

        CreateMap<TransactionRequest, Transaction>();
        CreateMap<Transaction, TransactionResponse>();
    }
}
