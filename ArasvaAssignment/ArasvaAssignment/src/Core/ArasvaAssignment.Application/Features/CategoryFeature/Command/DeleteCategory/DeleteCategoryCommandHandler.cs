using ArasvaAssignment.Application.Contracts.Persistence;
using ArasvaAssignment.Domain.Common;
using MediatR;

namespace ArasvaAssignment.Application.Features.CategoryFeature.Command.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, ApiResponse<bool>>
    {
        private readonly ICategoryRepository _repository;

        public DeleteCategoryCommandHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<ApiResponse<bool>> Handle(
            DeleteCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAsync(request.Id);
            if (category == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Category not found",
                    Data = false
                };
            }

            await _repository.DeleteAsync(request.Id);

            return new ApiResponse<bool>
            {
                Success = true,
                Message = "Category deleted successfully",
                Data = true
            };
        }
    }
}
