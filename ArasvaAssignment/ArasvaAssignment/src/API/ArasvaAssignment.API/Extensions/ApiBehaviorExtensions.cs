using ArasvaAssignment.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace ArasvaAssignment.API.Extensions
{
    public static class ApiBehaviorExtensions
    {
        public static IMvcBuilder ConfigureCustomValidationResponse(this IMvcBuilder builder)
        {
            builder.ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .ToDictionary(
                            kvp => kvp.Key,
                            kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                        );

                    var response = new
                    {
                        Success = false,
                        Message = "Validation Failed",
                        Data = (object?)null,
                        Errors = errors
                    };

                    return new BadRequestObjectResult(response);
                };
            });

            return builder;
        }
    }
}
