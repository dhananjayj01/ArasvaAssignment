using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArasvaAssignment.Application.Dtos.BookCopyDtos
{
    public class AddBookCopyDto
    {
        public Guid BookId { get; set; }
        public string Barcode { get; set; }
    }
}
