using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArasvaAssignment.Application.Dtos.ReviewDtos
{
    public class ReviewDto
    {
        public Guid ReviewId { get; set; }
        public Guid BookId { get; set; }
        public Guid MemberId { get; set; }
        public int Rating {  get; set; }
        public string Comment { get; set; }
        public bool IsActive { get; set; }
    }
}
