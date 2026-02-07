using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArasvaAssignment.Application.Dtos.CategoryDtos
{
    public class AddCategoryDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
