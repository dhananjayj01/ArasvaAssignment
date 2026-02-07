using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArasvaAssignment.Application.Dtos.BookCopyDtos
{
    public class BookCopyDto
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public string Barcode { get; set; }
        public string Status { get; set; }
    }
}
