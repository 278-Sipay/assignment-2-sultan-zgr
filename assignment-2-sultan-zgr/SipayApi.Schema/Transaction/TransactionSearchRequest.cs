using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipayApi.Schema;

public class TransactionSearchRequest  //ÖDEV
{
    public int AccountNumber { get; set; }
    public decimal MinAccountCredit { get; set; }
    public decimal MaxAccountCredit { get; set; }
    public int MinAmountDebit { get; set; }
    public string? Description { get; set; }
    public DateTime BeginDate { get; set; }
    public DateTime EndDate { get; set; }
    public string ReferenceNumber { get; set; }
}
