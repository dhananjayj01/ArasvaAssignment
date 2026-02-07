using ArasvaAssignment.Application.Contracts.Persistence;
using ArasvaAssignment.Application.Dtos.CategoryDtos;
using ArasvaAssignment.Domain.Common;
using AutoMapper;
using MediatR;

namespace ArasvaAssignment.Application.Features.CategoryFeature.Command.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, ApiResponse<CategoryDto>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(
            ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<CategoryDto>> Handle(
            UpdateCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.UpdateCategoryDto;

            // Check if category exists
            var existingCategory = await _categoryRepository.GetByIdAsync(request.CategoryId);
            if (existingCategory == null)
            {
                return new ApiResponse<CategoryDto>
                {
                    Success = false,
                    Message = "Category not found",
                    Data = null
                };
            }

            // Optional: check uniqueness of category name (excluding current category)
            var isExists = await _categoryRepository.IsCategoryExistsAsync(
                dto.Name, request.CategoryId);

            if (isExists)
            {
                return new ApiResponse<CategoryDto>
                {
                    Success = false,
                    Message = "Category name already exists",
                    Data = null
                };
            }

            // Map DTO → Entity
            _mapper.Map(dto, existingCategory);

            // Save changes
            await _categoryRepository.UpdateAsync(existingCategory);

            return new ApiResponse<CategoryDto>
            {
                Success = true,
                Message = "Category updated successfully",
                Data = _mapper.Map<CategoryDto>(existingCategory)
            };
        }
    }
}
