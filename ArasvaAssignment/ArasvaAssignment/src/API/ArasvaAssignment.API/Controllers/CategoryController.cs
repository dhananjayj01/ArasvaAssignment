using ArasvaAssignment.Application.Contracts.Persistence;
using ArasvaAssignment.Application.Dtos.BookDtos;
using ArasvaAssignment.Application.Dtos.CategoryDtos;
using ArasvaAssignment.Application.Features.BookFeature.Query.GetBookById;
using ArasvaAssignment.Application.Features.CategoryFeature.Command.AddCategory;
using ArasvaAssignment.Application.Features.CategoryFeature.Command.DeleteCategory;
using ArasvaAssignment.Application.Features.CategoryFeature.Command.UpdateCategory;
using ArasvaAssignment.Application.Features.CategoryFeature.Query.GetAllCategories;
using ArasvaAssignment.Application.Features.CategoryFeature.Query.GetCategoryById;
using ArasvaAssignment.Domain.Common;
using ArasvaAssignment.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ArasvaAssignment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAll([FromQuery] string? search = null, [FromQuery] bool? isActive = null)
        {
            var result = await _mediator.Send(new GetAllCategoryQuery(search, isActive));
            return Ok(result);
        }

        [HttpGet("GetCategoryById")]
        public async Task<IActionResult> GetById([FromQuery] string categoryId)
        {
            if (!int.TryParse(categoryId, out int validCategoryId))
            {
                return BadRequest(new ApiResponse<CategoryDto>
                {
                    Success = false,
                    Message = "Enter a valid Category Id",
                    Data = null
                });
            }

            var result = await _mediator.Send(new GetCategoryByIdQuery(validCategoryId));
            return Ok(result);
        }

        [HttpPost("CreateCategory")]
        public async Task<IActionResult> Create([FromBody] AddCategoryDto addCategoryDto)
        {
            var id = await _mediator.Send(new AddCategoryCommand(addCategoryDto));
            return Ok(id);
        }

        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> Update(int categoryId, [FromBody] UpdateCategoryDto updateCategoryDto)
        {
            var result = await _mediator.Send(new UpdateCategoryCommand(categoryId, updateCategoryDto));
            return Ok(result);
        }

        [HttpDelete("DeleteCategory")]
        public async Task<IActionResult> Delete(int categoryId)
        {
            var result = await _mediator.Send(new DeleteCategoryCommand { Id = categoryId });
            return Ok(result);
        }

    }
}
