using ArasvaAssignment.Application.Dtos.CategoryDtos;
using ArasvaAssignment.Application.Dtos.MemberDtos;
using ArasvaAssignment.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArasvaAssignment.Application.Features.CategoryFeature.Command.UpdateCategory
{
    public record UpdateCategoryCommand(int CategoryId, UpdateCategoryDto UpdateCategoryDto)
        : IRequest<ApiResponse<CategoryDto>>;
}
