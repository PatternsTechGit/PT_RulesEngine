using Entities;
using Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IRulesService
    {
        Task<RuleResult> IsTransferAllowed(TransferRequest transferRequest);
    }
}
