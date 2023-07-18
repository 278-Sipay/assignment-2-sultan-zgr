using SipayApi.BaseLayer.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipayApi.DataLayer.Entities
{
    public class Customer: BaseModel
    {
        public int CustomerNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public bool? IsActive { get; set; } //false değeri döndüğü için nullable olabilir demem gerekti update-database hatası alıyorum
        public virtual List<Account> Accounts { get; set; }
    }
}
