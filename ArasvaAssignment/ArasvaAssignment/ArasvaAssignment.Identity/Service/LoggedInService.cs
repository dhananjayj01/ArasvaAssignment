using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ArasvaAssignment.Application.Contracts.Identity;
using Microsoft.AspNetCore.Http;

namespace ArasvaAssignment.Identity.Service
{
    public class LoggedInService:ILoggedInService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LoggedInService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor; 
        }

        public Guid? MemberId
        {
            get
            {
                var memberId = _httpContextAccessor.HttpContext?
                    .User?
                    .FindFirst("MemberId")?
                    .Value;

                return memberId != null ? Guid.Parse(memberId) : null;
            }
        }

        public string? Email =>
         _httpContextAccessor.HttpContext?
             .User?
             .FindFirst(ClaimTypes.Email)?
             .Value;
    }
}

