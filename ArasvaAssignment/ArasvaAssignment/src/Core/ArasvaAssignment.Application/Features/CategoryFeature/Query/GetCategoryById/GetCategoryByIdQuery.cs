using ArasvaAssignment.Application.Dtos.CategoryDtos;
using ArasvaAssignment.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArasvaAssignment.Application.Features.CategoryFeature.Query.GetCategoryById
{
    public record GetCategoryByIdQuery(int CategoryId) : IRequest<ApiResponse<CategoryDto>>;
}
