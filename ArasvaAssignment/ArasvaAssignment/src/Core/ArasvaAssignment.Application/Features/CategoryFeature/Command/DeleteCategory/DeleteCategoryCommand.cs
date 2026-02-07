using ArasvaAssignment.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArasvaAssignment.Application.Features.CategoryFeature.Command.DeleteCategory
{
    public record DeleteCategoryCommand : IRequest<ApiResponse<bool>>
    {
        public int Id { get; set; }
    }
}
