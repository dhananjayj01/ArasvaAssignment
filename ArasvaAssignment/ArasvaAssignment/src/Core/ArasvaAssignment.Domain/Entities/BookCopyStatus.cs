using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArasvaAssignment.Domain.Entities
{
    public enum BookCopyStatus
    {
        Available = 1,
        Issued = 2,
        Reserved = 3,
        Lost = 4,
        Damaged = 5
    }
}
