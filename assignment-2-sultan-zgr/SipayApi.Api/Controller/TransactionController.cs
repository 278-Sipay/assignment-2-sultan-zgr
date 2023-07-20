using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SipayApi.Base;
using SipayApi.Data.Repository;
using SipayApi.DataLayer.Entities;
using SipayApi.Schema;
using System.Linq.Expressions;


namespace SipayApi.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository repository;
        private readonly IMapper mapper;
        public TransactionController(ITransactionRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }


        [HttpGet("GetByParameter")]
        public ApiResponse<List<TransactionResponse>> GetByParameter([FromQuery] TransactionSearchRequest request)   //ÖDEV
        {

           Expression<Func<Transaction, bool>> expression =
            a => a.AccountNumber == request.AccountNumber
            && a.ReferenceNumber == request.ReferenceNumber
        && a.CreditAmount == decimal.Parse(request.ReferenceNumber)
        && (a.CreditAmount >= request.MinAccountCredit && a.CreditAmount <= request.MaxAccountCredit)
            && (a.CreditAmount >= request.MinAmountDebit)
        && (request.BeginDate == null || a.TransactionDate >= request.BeginDate)
        && (request.EndDate == null || a.TransactionDate <= request.EndDate);

            var entityList = repository.GetByParameter(expression);
            var mapped = mapper.Map<List<Transaction>, List<TransactionResponse>>(entityList);
            return new ApiResponse<List<TransactionResponse>>(mapped);
        }
    }
}
