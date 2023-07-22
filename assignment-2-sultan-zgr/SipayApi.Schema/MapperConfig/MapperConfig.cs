using AutoMapper;
using SipayApi.DataLayer.Entities;

namespace SipayApi.Schema;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<TransactionRequest,Transaction>();  //ÖDEV
        CreateMap<Transaction, TransactionResponse>();
        CreateMap<TransactionSearchRequest, Transaction>().ReverseMap();
    }
}
