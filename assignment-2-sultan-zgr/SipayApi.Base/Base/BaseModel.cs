using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipayApi.BaseLayer.Base
{
    public class BaseModel  //Entitylerimde ortak olan propertyler DRY olmaması için burada
    {
        public DateTime? InsertDate { get; set; }
        public string InsertUser { get; set; }
    }
}
