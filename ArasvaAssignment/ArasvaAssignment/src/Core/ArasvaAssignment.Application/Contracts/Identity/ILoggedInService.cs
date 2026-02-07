using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArasvaAssignment.Application.Contracts.Identity
{
    public interface ILoggedInService
    {
        Guid? MemberId { get; }
        string? Email { get; }
    }
}
