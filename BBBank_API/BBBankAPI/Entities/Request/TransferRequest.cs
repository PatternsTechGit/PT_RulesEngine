using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Request
{
    public class TransferRequest
    {
        public string AccountFromId { get; set; }
        public string AccountToId { get; set; }
        public decimal Amount { get; set; }
    }
}
