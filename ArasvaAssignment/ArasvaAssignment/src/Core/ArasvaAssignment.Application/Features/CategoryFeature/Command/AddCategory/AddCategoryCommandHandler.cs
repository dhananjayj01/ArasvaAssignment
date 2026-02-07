using ArasvaAssignment.Application.Contracts.Persistence;
using ArasvaAssignment.Application.Dtos.CategoryDtos;
using ArasvaAssignment.Domain.Common;
using ArasvaAssignment.Domain.Entities;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArasvaAssignment.Application.Features.CategoryFeature.Command.AddCategory
{
    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, ApiResponse<CategoryDto>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public AddCategoryCommandHandler(
            ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<CategoryDto>> Handle(
            AddCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.AddCategoryDto;

            // Uniqueness check (Category Name)
            var exists = await _categoryRepository.IsCategoryExistsAsync(dto.Name, null);
            if (exists)
            {
                return new ApiResponse<CategoryDto>
                {
                    Success = false,
                    Message = "Category name already exists"
                };
            }

            // Map DTO → Entity
            var category = _mapper.Map<Category>(dto);

            await _categoryRepository.AddAsync(category);

            return new ApiResponse<CategoryDto>
            {
                Success = true,
                Message = "Category created successfully",
                Data = _mapper.Map<CategoryDto>(category)
            };
        }
    }
}
