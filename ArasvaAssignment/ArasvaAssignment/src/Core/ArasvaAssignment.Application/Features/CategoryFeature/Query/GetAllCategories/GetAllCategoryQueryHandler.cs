using ArasvaAssignment.Application.Contracts.Persistence;
using ArasvaAssignment.Application.Dtos.CategoryDtos;
using ArasvaAssignment.Domain.Common;
using AutoMapper;
using MediatR;

namespace ArasvaAssignment.Application.Features.CategoryFeature.Query.GetAllCategories
{
    public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, ApiResponse<IEnumerable<CategoryDto>>>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public GetAllCategoryQueryHandler(
            ICategoryRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<CategoryDto>>> Handle(
            GetAllCategoryQuery request,
            CancellationToken cancellationToken)
        {
            var categories = await _repository.GetAllAsync(request.search, request.isActive);
            var categoryDtos =  _mapper.Map<IEnumerable<CategoryDto>>(categories);
            return new ApiResponse<IEnumerable<CategoryDto>>
            {
                Success = true,
                Message = "Categories retrieved successfully",
                Data = categoryDtos
            };
        }
    }
}
