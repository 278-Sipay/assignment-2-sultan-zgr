using SipayApi.BaseLayer.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipayApi.DataLayer.Entities
{
    public class Transaction : IdBaseModel
    {
        public int AccountNumber { get; set; }
        public virtual Account Account { get; set; }
        public decimal CreditAmount { get; set; }   // -
        public decimal DebitAmount { get; set; }    // +
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public string ReferenceNumber { get; set; }
    }
}
