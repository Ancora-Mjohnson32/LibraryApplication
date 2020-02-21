using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LibraryApplication.Security
{
    public class CanEditOnlyOtherAdminRolesAndClaimsHandler : 
        AuthorizationHandler<ManageAdminRolesAndClaimsRequirement>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public CanEditOnlyOtherAdminRolesAndClaimsHandler(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));

        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        ManageAdminRolesAndClaimsRequirement requirement)
        {

            string loggedInAdminId = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value.ToString();

            string adminIdBeingEdited = httpContextAccessor.HttpContext.Request.Query["userId"].ToString();

            if (context.User.IsInRole("Admin") &&
            context.User.HasClaim(claim =>
            claim.Type == "Edit Role" && claim.Value == "true") && adminIdBeingEdited.ToLower() != loggedInAdminId.ToLower())
            {
                context.Succeed(requirement);
            }
            /*else {context.Fail();}
             This will cause the Super admin handler to fail even after is is a success*/

            return Task.CompletedTask;

        }
    }
}
