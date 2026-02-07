using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArasvaAssignment.Domain.Entities
{
    public class Review
    {
        public Guid ReviewId { get; set; }
        public Guid BookId { get; set; }
        public Guid MemberId { get; set; }

        public int Rating { get; set; } // 1–5
        public string Comment { get; set; }

        public bool IsActive { get; set; } = true;

        public Book Book { get; set; }
        public Member Member { get; set; }
    }
}
