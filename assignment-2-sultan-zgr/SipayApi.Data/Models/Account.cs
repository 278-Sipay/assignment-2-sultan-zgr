using SipayApi.BaseLayer.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipayApi.DataLayer.Entities
{
    public class Account : BaseModel
    {
        public int AccountNumber { get; set; }
        public int CustomerNumber { get; set; }
        public virtual Customer Customer { get; set; }
        public decimal Balance { get; set; }
        public string Name { get; set; }
        public DateTime OpenDate { get; set; }
        public string CurrencyCode { get; set; }
        public bool? IsActive { get; set; } //false değeri döndüğü için nullable olabilir demem gerekti update-database hatası alıyorum
        public List<Transaction> Transactions { get; set; }

    }
}
