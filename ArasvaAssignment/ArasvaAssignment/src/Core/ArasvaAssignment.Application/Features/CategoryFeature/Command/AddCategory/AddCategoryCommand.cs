using ArasvaAssignment.Application.Dtos.BookDtos;
using ArasvaAssignment.Application.Dtos.CategoryDtos;
using ArasvaAssignment.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArasvaAssignment.Application.Features.CategoryFeature.Command.AddCategory
{
    public record AddCategoryCommand(AddCategoryDto AddCategoryDto) : IRequest<ApiResponse<CategoryDto>>;
}
