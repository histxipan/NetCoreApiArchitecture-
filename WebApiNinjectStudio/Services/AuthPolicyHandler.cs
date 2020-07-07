using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WebApiNinjectStudio.Domain.Abstract;
using WebApiNinjectStudio.Domain.Concrete;
using WebApiNinjectStudio.Domain.Entities;

namespace WebApiNinjectStudio.Services
{
    public class AuthPolicyHandler : AuthorizationHandler<AuthPolicyRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthPolicyRequirement requirement)
        {
            //Valid token
            var isAuthenticated = context.User.Identity.IsAuthenticated;

            if (isAuthenticated && (requirement.RolePermissions != null))
            {
                //Get request url from context
                var requestUrl = "/" + (context.Resource as Microsoft.AspNetCore.Routing.RouteEndpoint).RoutePattern.RawText;
                //Get request medtode from context, for example: Get Post...
                var requestType = (context.Resource as Microsoft.AspNetCore.Routing.RouteEndpoint).RoutePattern.RequiredValues.Values.First().ToString();

                if (
                    context.User.HasClaim(c => c.Type == ClaimTypes.NameIdentifier) &&
                    context.User.HasClaim(c => c.Type == ClaimTypes.Role)
                    )
                {
                    //Get id of user and role permission of user from token
                    var userID = int.Parse(
                        context.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value
                        );
                    var userRole = context.User.Claims.First(c => c.Type == ClaimTypes.Role).Value;

                    //Valid permission
                    var apiUrlsOfRole = requirement.RolePermissions.First(r => r.Name == userRole).RolePermissionApiUrls;
                    if (apiUrlsOfRole == null)
                    {
                        context.Fail();
                    }

                    if (apiUrlsOfRole.FirstOrDefault(u => u.ApiUrl.ApiUrlString == requestUrl && u.ApiUrl.ApiRequestMethod == requestType) != null)
                    {
                        context.Succeed(requirement);
                    }

                }
            }
            return Task.CompletedTask;
        }
    }
}
