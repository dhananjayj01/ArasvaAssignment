using ArasvaAssignment.Application.Contracts.Persistence;
using ArasvaAssignment.Application.Dtos.CategoryDtos;
using ArasvaAssignment.Domain.Common;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArasvaAssignment.Application.Features.CategoryFeature.Query.GetCategoryById
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, ApiResponse<CategoryDto>>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public GetCategoryByIdQueryHandler(
            ICategoryRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<CategoryDto>> Handle(
            GetCategoryByIdQuery request,
            CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAsync(request.CategoryId);

            if (category == null)
            {
                return new ApiResponse<CategoryDto>
                {
                    Success = false,
                    Message = "Category not found",
                    Data = null
                };
            }

            return new ApiResponse<CategoryDto>
            {
                Success = true,
                Message = "Category fetched successfully",
                Data = _mapper.Map<CategoryDto>(category)
            };
        }
    }
}
