using ArasvaAssignment.Application.Dtos.CategoryDtos;
using ArasvaAssignment.Domain.Common;
using MediatR;

namespace ArasvaAssignment.Application.Features.CategoryFeature.Query.GetAllCategories
{
    public record GetAllCategoryQuery(string? search = null, bool? isActive = null) : IRequest<ApiResponse<IEnumerable<CategoryDto>>>;
}
