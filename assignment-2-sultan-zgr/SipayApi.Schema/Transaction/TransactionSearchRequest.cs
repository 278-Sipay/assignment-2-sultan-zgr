using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipayApi.Schema;

public class TransactionSearchRequest
{
    public int AccountNumber { get; set; }
    public int MinAccountCredit { get; set; }
    public int MaxAccountCredit { get; set; }
    public int MinAmountDebit { get; set; }
    public string? Description { get; set; }
    public DateTime BeginDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? ReferenceNumber { get; set; }
}
