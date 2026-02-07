using ArasvaAssignment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArasvaAssignment.Application.Dtos.BookCopyDtos
{
    public class UpdateBookCopyDto
    {
        public string Barcode { get; set; }
        public BookCopyStatus Status { get; set; }
    }
}
